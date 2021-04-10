using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdventureApi.Controllers;
using AdventureApi.Entities;
using AdventureApi.Repositories;
using AdventureApi.Services;
using AdventureApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace AdventureApi.Tests.Controllers
{
    public class AdventuresControllerTests : InMemoryTest
    {
        #region Setup
        private Guid _testAdventureId;
        private Guid _testAdventure2Id;
        private Guid _testAdventure3Id;
        
        public AdventuresControllerTests() : base("AdventuresControllerTests")
        {
            Seed();
        }
        
        private void Seed()
        {
            using var context = new AdventureContext(_dbContextOptions);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            _testAdventureId = Guid.NewGuid();
            var adv = new Adventure
            {
                Id = _testAdventureId,
                Locations = new List<Location>()
                {
                    new Location
                    {
                        Id = Guid.NewGuid(),
                        Initial = true,
                        AdventureId = _testAdventureId
                    },
                    new Location
                    {
                        Id = Guid.NewGuid(),
                        Initial = false,
                        AdventureId = _testAdventureId
                    }
                },
                Name = "TestOne"
            };
            

            _testAdventure2Id = Guid.NewGuid();
            var adv2 = new Adventure
            {
                Id = _testAdventure2Id,
                Locations = new List<Location>()
                {
                    new Location
                    {
                        Id = Guid.NewGuid(),
                        Initial = false,
                        AdventureId = _testAdventure2Id
                    },
                    new Location
                    {
                        Id = Guid.NewGuid(),
                        Initial = false,
                        AdventureId = _testAdventure2Id
                    }
                },
                Name = "TestTwo"
            };
            
            _testAdventure3Id = Guid.NewGuid();
            var adv3 = new Adventure
            {
                Id = _testAdventure3Id,
                Locations = new List<Location>()
                {
                },
                Name = "TestThree"
            };
            
            context.AddRange(adv, adv2, adv3);
            context.SaveChanges();
        }
        private static AdventuresController CreateController(AdventureContext context)
        {
            var locationRepository = new LocationRepository(context);
            var adventureRepository = new AdventuresRepository(context);
            var locationService = new LocationService(locationRepository);
            var adventureService = new AdventuresService(adventureRepository);
            return new AdventuresController(adventureService, locationService);
        }
        #endregion

        #region GetAllAdventures
        [Fact]
        public async Task GetAllAdventures_ReturnAll()
        {
            await using var context = new AdventureContext(_dbContextOptions);
            var controller = CreateController(context);
            
            //act
            var result = await controller.GetAll();
            
            //assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            var adventures = okObjectResult.Value as IEnumerable<AdventureViewModel>;
            Assert.NotNull(adventures);
            Assert.Equal(3, adventures.Count());
        }
        #endregion

        #region GetByName
        [Fact]
        public async Task GetAdventureByName_ReturnOne()
        {
            await using var context = new AdventureContext(_dbContextOptions);
            var controller = CreateController(context);
            
            //act
            var result = await controller.GetByName("testTHRee");
            
            //assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            var adventure = okObjectResult.Value as AdventureViewModel;
            Assert.NotNull(adventure);
            Assert.Equal("TestThree", adventure.Name);
        }
        
        [Fact]
        public async Task GetAdventureByName_Invalid_ReturnBadRequest()
        {
            await using var context = new AdventureContext(_dbContextOptions);
            var controller = CreateController(context);
            
            //act
            var result = await controller.GetByName("testTHRe");
            
            //assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.NotNull(badRequestResult);
            Assert.Equal(400, badRequestResult.StatusCode);
        }
        #endregion

        #region GetInitialLocation
        [Fact]
        public async Task GetInitialLocation_Valid_ReturnOne()
        {
            await using var context = new AdventureContext(_dbContextOptions);
            var controller = CreateController(context);
            
            //act
            var result = await controller.GetInitialLocation(_testAdventureId.ToString());
            
            //assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            var location = okObjectResult.Value as LocationViewModel;
            Assert.NotNull(location);
            Assert.Equal(_testAdventureId.ToString(), location.AdventureId);
        }
        
        [Fact]
        public async Task GetInitialLocation_Invalid_ReturnBadRequest()
        {
            await using var context = new AdventureContext(_dbContextOptions);
            var controller = CreateController(context);
            
            //act
            var result = await controller.GetInitialLocation(_testAdventure2Id.ToString());
            
            //assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.NotNull(badRequestResult);
            Assert.Equal(400, badRequestResult.StatusCode);
        }
        #endregion
    }
    
    
}