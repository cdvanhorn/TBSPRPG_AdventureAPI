using System;
using System.Collections.Generic;
using System.Linq;
using AdventureApi.Controllers;
using AdventureApi.Entities;
using AdventureApi.Repositories;
using AdventureApi.Services;
using AdventureApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace AdventureApi.Tests.Controllers
{
    public class LocationsControllerTests : InMemoryTest
    {
        #region Setup

        private readonly Guid _testLocationId = Guid.NewGuid();
        private const string _testRouteName = "testroutename";

        public LocationsControllerTests() : base("LocationsControllerTests")
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

        private LocationsController CreateController(AdventureContext context)
        {
            return new LocationsController(
                new RouteService(new RouteRepository(context)));
        }

        #endregion

        #region GetRoutesForLocation

        [Fact]
        public async void GetRoutesForLocation_ValidLocation_ReturnsRoutes()
        {
            //arrange
            await using var context = new AdventureContext(_dbContextOptions);
            var controller = CreateController(context);
            
            //act
            var result = await controller.GetRoutesForLocation(_testLocationId);
            
            //assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            var routes = okObjectResult.Value as IEnumerable<RouteViewModel>;
            Assert.NotNull(routes);
            Assert.Equal(2, routes.Count());
        }

        [Fact]
        public async void GetRoutesForLocation_InvalidLocation_NoRoutes()
        {
            //arrange
            await using var context = new AdventureContext(_dbContextOptions);
            var controller = CreateController(context);
            
            //act
            var result = await controller.GetRoutesForLocation(Guid.NewGuid());
            
            //assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            var routes = okObjectResult.Value as IEnumerable<RouteViewModel>;
            Assert.NotNull(routes);
            Assert.Empty(routes);
        }

        #endregion
    }
}