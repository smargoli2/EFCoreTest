using Microsoft.EntityFrameworkCore;

namespace TestEFCore;
public class DbTestEF
{
    private DbContextOptions<PersonDbContext> _options;

    public DbTestEF()
    {
        _options = new DbContextOptionsBuilder<PersonDbContext>()
            .UseSqlite("DataSource=testdb.db")
            .Options;
    }

    [Fact]
    public async void AddPerson_ModifyName_Fails()
    {
        var person = new Person { Id = 1, Name = "John Doe" };
        var changeEntity = new ChangeEntityTest<Person, int, string>()
        {
            Date = DateTime.Now,
            ChangeEntityInfoTest = new ChangeEntityInfoTest<Person, int, string>()
            {
                ChangedEntity = person
            },
            ObjectId = person.Id,
            UserId = "CurrentUser"
        };

        using (var context = new PersonDbContext(_options))
        {
            context.Database.EnsureCreated();
        }

        using (var context = new PersonDbContext(_options))
        {
            context.Set<Person>().Add(person);
            context.Add(changeEntity);
            context.SaveChanges();
        }

        using (var context = new PersonDbContext(_options))
        {
            var change = await context.Set<ChangeEntityTest<Person, int, string>>().SingleAsync(x => Equals(changeEntity.Id, x.Id));
            change.ChangeEntityInfoTest = await context.Set<ChangeEntityInfoTest<Person, int, string>>().SingleAsync(x => Equals(x.Id, changeEntity.Id));
            var originalSaved = change.GetChangedEntity();
            originalSaved.Name = "Jane Smith";
            context.SaveChanges();
        }

        using (var context = new PersonDbContext(_options))
        {
            var change = await context.Set<ChangeEntityTest<Person, int, string>>().SingleAsync(x => Equals(changeEntity.Id, x.Id));
            change.ChangeEntityInfoTest = await context.Set<ChangeEntityInfoTest<Person, int, string>>().SingleAsync(x => Equals(x.Id, changeEntity.Id));
            var originalSaved = change.GetChangedEntity();
            Assert.Equal("John Doe", originalSaved.Name);
        }
    }
}
