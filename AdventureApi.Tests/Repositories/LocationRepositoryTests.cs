using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AdventureApi.Entities;
using AdventureApi.Repositories;
using Xunit;

namespace AdventureApi.Tests.Repositories
{
    public class LocationRepositoryTests : InMemoryTest
    {
        #region Seeding
        private Guid _testAdventureId;
        private Guid _testAdventure2Id;
        private Guid _testAdventure3Id;
        public LocationRepositoryTests()
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
        #endregion
        
        [Fact]
        public async Task GetInitialLocation_Valid_ReturnLocation()
        {
            //arrange
            await using var context = new AdventureContext(_dbContextOptions);
            var locationRepository = new LocationRepository(context);
            
            //act
            var location = await locationRepository.GetInitialForAdventure(_testAdventureId);

            //assert
            Assert.Equal(_testAdventureId, location.AdventureId);
            Assert.True(location.Initial);
        }
        
        [Fact]
        public async Task GetInitialLocation_Invalid_ReturnNothing()
        {
            //arrange
            await using var context = new AdventureContext(_dbContextOptions);
            var locationRepository = new LocationRepository(context);
            
            //act
            var location = await locationRepository.GetInitialForAdventure(Guid.NewGuid());

            //assert
            Assert.Null(location);
        }
        
        [Fact]
        public async Task GetInitialLocation_NoInitial_ReturnNothing()
        {
            //arrange
            await using var context = new AdventureContext(_dbContextOptions);
            var locationRepository = new LocationRepository(context);
            
            //act
            var location = await locationRepository.GetInitialForAdventure(_testAdventure2Id);

            //assert
            Assert.Null(location);
        }
        
        [Fact]
        public async Task GetInitialLocation_NoLocations_ReturnNothing()
        {
            //arrange
            await using var context = new AdventureContext(_dbContextOptions);
            var locationRepository = new LocationRepository(context);
            
            //act
            var location = await locationRepository.GetInitialForAdventure(_testAdventure3Id);

            //assert
            Assert.Null(location);
        }
    }
}