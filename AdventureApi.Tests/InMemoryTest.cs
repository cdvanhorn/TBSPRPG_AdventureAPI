using AdventureApi.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AdventureApi.Tests
{
    public class InMemoryTest
    {
        protected readonly DbContextOptions<AdventureContext> _dbContextOptions;

        protected InMemoryTest(string dbName)
        {
            _dbContextOptions = new DbContextOptionsBuilder<AdventureContext>()
                    .UseInMemoryDatabase(dbName)
                    .Options;
        }
    }
}