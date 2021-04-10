using System;
using System.Threading.Tasks;
using AdventureApi.Entities;
using AdventureApi.Repositories;
using AdventureApi.Services;
using Xunit;

namespace AdventureApi.Tests.Services
{
    public class AdventureServiceTests : InMemoryTest
    {
        #region Setup
        private Guid _testAdventureId;
        
        public AdventureServiceTests() : base("AdventureServiceTests")
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

        private static AdventuresService CreateService(AdventureContext context)
        {
            var adventureRepository = new AdventuresRepository(context);
            return new AdventuresService(adventureRepository);
        }
        #endregion
        
        #region GetAllAdventures
        [Fact]
        public async Task GetAllAdventures_ReturnAll()
        {
            //arrange
            await using var context = new AdventureContext(_dbContextOptions);
            var service = CreateService(context);
            
            //act
            var adventures = await service.GetAll();
            
            //assert
            Assert.Equal(2, adventures.Count);
            Assert.Equal(_testAdventureId.ToString(), adventures[0].Id);
            Assert.Equal("TestOne", adventures[0].Name);
            Assert.Equal("TestTwo", adventures[1].Name);
        }
        #endregion
        
        #region GetAdventureById
        [Fact]
        public async Task GetAdventureById_Valid_ReturnOne()
        {
            //arrange
            await using var context = new AdventureContext(_dbContextOptions);
            var service = CreateService(context);
            
            //act
            var adventure = await service.GetById(_testAdventureId.ToString());
            
            //assert
            Assert.NotNull(adventure);
            Assert.Equal(_testAdventureId.ToString(), adventure.Id);
            Assert.Equal("TestOne", adventure.Name);
        }

        [Fact]
        public async Task GetAdventureById_Invalid_ReturnNothing()
        {
            //arrange
            await using var context = new AdventureContext(_dbContextOptions);
            var service = CreateService(context);
            
            //act
            var adventure = await service.GetById(Guid.NewGuid().ToString());
            
            //assert
            Assert.Null(adventure);
        }
        
        [Fact]
        public async Task GetAdventureById_InvalidNotGuid_ReturnNothing()
        {
            //arrange
            await using var context = new AdventureContext(_dbContextOptions);
            var service = CreateService(context);
            
            //act
            var adventure = await service.GetById("banana tom");
            
            //assert
            Assert.Null(adventure);
        }

        [Fact]
        public async Task GetAdventureById_Empty_ReturnNothing()
        {
            //arrange
            await using var context = new AdventureContext(_dbContextOptions);
            var service = CreateService(context);
            
            //act
            var adventure = await service.GetById("");
            
            //assert
            Assert.Null(adventure);
        }
        #endregion
        
        #region GetAdventureByName
        [Fact]
        public async Task GetAdventureByName_Valid_ReturnOne()
        {
            //arrange
            await using var context = new AdventureContext(_dbContextOptions);
            var service = CreateService(context);
            
            //act
            var adventure = await service.GetByName("TestOne");
            
            //assert
            Assert.NotNull(adventure);
            Assert.Equal(_testAdventureId.ToString(), adventure.Id);
            Assert.Equal("TestOne", adventure.Name);
        }

        [Fact]
        public async Task GetAdventureByName_CaseInsensitive_ReturnOne()
        {
            //arrange
            await using var context = new AdventureContext(_dbContextOptions);
            var service = CreateService(context);
            
            //act
            var adventure = await service.GetByName("TestONE");
            
            //assert
            Assert.NotNull(adventure);
            Assert.Equal(_testAdventureId.ToString(), adventure.Id);
            Assert.Equal("TestOne", adventure.Name);
        }
        
        [Fact]
        public async Task GetAdventureByName_Invalid_ReturnNone()
        {
            //arrange
            await using var context = new AdventureContext(_dbContextOptions);
            var service = CreateService(context);
            
            //act
            var adventure = await service.GetByName("TestO");
            
            //assert
            Assert.Null(adventure);
        }
        
        [Fact]
        public async Task GetAdventureByName_Empty_ReturnNone()
        {
            //arrange
            await using var context = new AdventureContext(_dbContextOptions);
            var service = CreateService(context);
            
            //act
            var adventure = await service.GetByName("");
            
            //assert
            Assert.Null(adventure);
        }
        #endregion
    }
}