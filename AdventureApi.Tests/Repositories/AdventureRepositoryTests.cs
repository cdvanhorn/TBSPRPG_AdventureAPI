using System;
using System.Linq;
using System.Threading.Tasks;
using AdventureApi.Entities;
using AdventureApi.Repositories;
using Xunit;

namespace AdventureApi.Tests.Repositories
{
    public class AdventureRepositoryTests : InMemoryTest
    {
        #region Seeding
        public AdventureRepositoryTests()
        {
            Seed();
        }

        private void Seed()
        {
            using var context = new AdventureContext(_dbContextOptions);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var adv = new Adventure
            {
                Id = Guid.NewGuid(),
                Name = "TestOne"
            };

            var advTwo = new Adventure
            {
                Id = Guid.NewGuid(),
                Name = "TestTwo"
            };
            
            context.AddRange(adv, advTwo);
            context.SaveChanges();
        }
        #endregion

        #region GetAllAdventures
        [Fact]
        public async Task GetAllAdventures_ReturnAll()
        {
            //arrange
            await using var context = new AdventureContext(_dbContextOptions);
            var adventureRepository = new AdventuresRepository(context);
            
            //act
            var adventures = await adventureRepository.GetAllAdventures();
            
            //assert
            Assert.Equal(2, adventures.Count);
        }
        #endregion
    }
}