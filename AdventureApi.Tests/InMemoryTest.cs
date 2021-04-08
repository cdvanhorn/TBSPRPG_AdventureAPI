using AdventureApi.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AdventureApi.Tests
{
    public class InMemoryTest
    {
        protected readonly DbContextOptions<AdventureContext> _dbContextOptions;

        protected InMemoryTest()
        {
            _dbContextOptions = new DbContextOptionsBuilder<AdventureContext>()
                    .UseInMemoryDatabase("AdventureDatabase")
                    .Options;
        }
    }
}