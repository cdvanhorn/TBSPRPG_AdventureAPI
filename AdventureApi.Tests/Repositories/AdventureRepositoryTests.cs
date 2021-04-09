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

        private Guid _testAdventureId;
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
            _testAdventureId = adv.Id;

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
            Assert.Equal("TestOne", adventures[0].Name);
            Assert.Equal("TestTwo", adventures[1].Name);
        }
        #endregion
        
        #region GetAdventureByName
        [Fact]
        public async Task GetAdventureByName_ReturnAdventure()
        {
            //arrange
            await using var context = new AdventureContext(_dbContextOptions);
            var adventureRepository = new AdventuresRepository(context);
            
            //act
            var adventure = await adventureRepository.GetAdventureByName("TestOne");
            
            //assert
            Assert.Equal("TestOne", adventure.Name);
        }
        
        [Fact]
        public async Task GetAdventureByName_CaseInsensitive_ReturnAdventure()
        {
            //arrange
            await using var context = new AdventureContext(_dbContextOptions);
            var adventureRepository = new AdventuresRepository(context);
            
            //act
            var adventure = await adventureRepository.GetAdventureByName("testTWO");
            
            //assert
            Assert.Equal("TestTwo", adventure.Name);
        }
        
        [Fact]
        public async Task GetAdventureByName_InvalidName_ReturnNothing()
        {
            //arrange
            await using var context = new AdventureContext(_dbContextOptions);
            var adventureRepository = new AdventuresRepository(context);
            
            //act
            var adventure = await adventureRepository.GetAdventureByName("invalid");
            
            //assert
            Assert.Null(adventure);
        }
        #endregion
        
        #region GetAdventureById
        [Fact]
        public async Task GetAdventureById_ReturnAdventure()
        {
            //arrange
            await using var context = new AdventureContext(_dbContextOptions);
            var adventureRepository = new AdventuresRepository(context);
            
            //act
            var adventure = await adventureRepository.GetAdventureById(_testAdventureId);
            
            //assert
            Assert.Equal("TestOne", adventure.Name);
        }
        
        [Fact]
        public async Task GetAdventureById_Invalid_ReturnNothing()
        {
            //arrange
            await using var context = new AdventureContext(_dbContextOptions);
            var adventureRepository = new AdventuresRepository(context);
            
            //act
            var adventure = await adventureRepository.GetAdventureById(Guid.NewGuid());
            
            //assert
            Assert.Null(adventure);
        }
        #endregion
    }
}