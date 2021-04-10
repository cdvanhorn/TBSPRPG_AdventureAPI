using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AdventureApi.Entities;
using AdventureApi.Repositories;
using AdventureApi.Services;
using Xunit;

namespace AdventureApi.Tests.Services
{
    public class LocationServiceTests : InMemoryTest
    {
        #region Setup
        private Guid _testAdventureId;
        private Guid _testAdventure2Id;
        private Guid _testAdventure3Id;
        
        public LocationServiceTests() : base("LocationServiceTests")
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
        
        private static LocationService CreateService(AdventureContext context)
        {
            var locationRepository = new LocationRepository(context);
            return new LocationService(locationRepository);
        }
        #endregion

        #region GetInitialForAdventure
        [Fact]
        public async Task GetInitialLocation_Valid_ReturnLocation()
        {
            //arrange
            await using var context = new AdventureContext(_dbContextOptions);
            var service = CreateService(context);
            
            //act
            var location = await service.GetInitialForLocation(_testAdventureId.ToString());

            //assert
            Assert.Equal(_testAdventureId.ToString(), location.AdventureId);
        }
        
        [Fact]
        public async Task GetInitialLocation_Invalid_ReturnNothing()
        {
            //arrange
            await using var context = new AdventureContext(_dbContextOptions);
            var service = CreateService(context);
            
            //act
            var location = await service.GetInitialForLocation(Guid.NewGuid().ToString());

            //assert
            Assert.Null(location);
        }
        
        [Fact]
        public async Task GetInitialLocation_NoInitial_ReturnNothing()
        {
            //arrange
            await using var context = new AdventureContext(_dbContextOptions);
            var service = CreateService(context);
            
            //act
            var location = await service.GetInitialForLocation(_testAdventure2Id.ToString());

            //assert
            Assert.Null(location);
        }
        
        [Fact]
        public async Task GetInitialLocation_NoLocations_ReturnNothing()
        {
            //arrange
            await using var context = new AdventureContext(_dbContextOptions);
            var service = CreateService(context);
            
            //act
            var location = await service.GetInitialForLocation(_testAdventure3Id.ToString());

            //assert
            Assert.Null(location);
        }
        #endregion
    }
}