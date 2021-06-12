using System;
using System.Linq;
using AdventureApi.Entities;
using AdventureApi.Repositories;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Xunit;

namespace AdventureApi.Tests.Repositories
{
    public class RouteRepositoryTests : InMemoryTest
    {
        #region Setup

        private readonly Guid _testLocationId = Guid.NewGuid();
        private const string _testRouteName = "testroutename";

        public RouteRepositoryTests() : base("RouteRepositoryTests")
        {
            Seed();
        }

        private void Seed()
        {
            using var context = new AdventureContext(_dbContextOptions);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var route = new Route()
            {
                Id = Guid.NewGuid(),
                LocationId = _testLocationId,
                Name = _testRouteName
            };

            var route2 = new Route()
            {
                Id = Guid.NewGuid(),
                LocationId = _testLocationId,
                Name = "foo"
            };

            var route3 = new Route()
            {
                Id = Guid.NewGuid(),
                LocationId = Guid.NewGuid(),
                Name = "bar"
            };
            
            context.Routes.AddRange(route, route2, route3);
            context.SaveChanges();
        }

        #endregion

        #region GetRoutesForLocation

        [Fact]
        public async void GetRoutesForLocation_ValidLocation_RoutesReturned()
        {
            //arrange
            await using var context = new AdventureContext(_dbContextOptions);
            var repo = new RouteRepository(context);
            
            //act
            var routes = await repo.GetRoutesForLocation(_testLocationId);
            
            //assert
            Assert.Equal(2, routes.Count);
            Assert.Equal(_testRouteName, routes.First().Name);
        }

        [Fact]
        public async void GetRoutesForLocation_InvalidLocation_NoRoutes()
        {
            //arrange
            await using var context = new AdventureContext(_dbContextOptions);
            var repo = new RouteRepository(context);
            
            //act
            var routes = await repo.GetRoutesForLocation(Guid.NewGuid());
            
            //assert
            Assert.Empty(routes);
        }

        #endregion
    }
}