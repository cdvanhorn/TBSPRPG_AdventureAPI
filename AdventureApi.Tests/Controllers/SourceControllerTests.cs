using System;
using AdventureApi.Controllers;
using AdventureApi.Entities.LanguageSources;
using AdventureApi.Repositories;
using AdventureApi.Services;
using AdventureApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using TbspRpgLib.InterServiceCommunication.RequestModels;
using Xunit;

namespace AdventureApi.Tests.Controllers
{
    public class SourceControllerTests : InMemoryTest
    {
         #region Setup

        private readonly Guid _testContentKey = Guid.NewGuid();
        private const string _testEnglishText = "text in english";
        public SourceControllerTests() : base("ContentControllerTests")
        {
            Seed();
        }
        
        private void Seed()
        {
            using var context = new AdventureContext(_dbContextOptions);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var enSource = new En()
            {
                Id = Guid.NewGuid(),
                Key = _testContentKey,
                Text = _testEnglishText
            };
            
            var enSource2 = new En()
            {
                Id = Guid.NewGuid(),
                Key = Guid.NewGuid(),
                Text = "other english text"
            };

            context.SourcesEn.AddRange(enSource, enSource2);
            context.SaveChanges();
        }

        private static SourcesController CreateController(AdventureContext context)
        {
            var sourceRepository = new SourceRepository(context);
            var sourceService = new SourceService(sourceRepository);
            return new SourcesController(sourceService);
        }

        #endregion
        
        #region GetSourceContent

        [Fact]
        public async void GetSourceContent_Valid_ReturnSource()
        {
            //arrange
            await using var context = new AdventureContext(_dbContextOptions);
            var controller = CreateController(context);
            
            //act
            var result = await controller.GetSourceContent("en", _testContentKey);

            //assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            var sourceViewModel = okObjectResult.Value as SourceViewModel;
            Assert.NotNull(sourceViewModel);
            Assert.Equal(_testContentKey, sourceViewModel.Key);
            Assert.Equal("en", sourceViewModel.Language);
            Assert.Equal(_testEnglishText, sourceViewModel.Source);
        }

        [Fact]
        public async void GetSourceContent_InValidLanguage_ReturnError()
        {
            //arrange
            await using var context = new AdventureContext(_dbContextOptions);
            var controller = CreateController(context);
            
            //act
            var result = await controller.GetSourceContent("eng", _testContentKey);

            //assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.NotNull(badRequestResult);
            Assert.Equal(400, badRequestResult.StatusCode);
        }

        [Fact]
        public async void GetSourceContent_InValidKey_ReturnSourceError()
        {
            //arrange
            await using var context = new AdventureContext(_dbContextOptions);
            var controller = CreateController(context);
            var invalidKey = Guid.NewGuid();
            
            //act
            var result = await controller.GetSourceContent("en", invalidKey);

            //assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            var sourceViewModel = okObjectResult.Value as SourceViewModel;
            Assert.NotNull(sourceViewModel);
            Assert.Equal(invalidKey, sourceViewModel.Key);
            Assert.Equal("en", sourceViewModel.Language);
            Assert.Equal($"invalid source key {invalidKey}", sourceViewModel.Source);
        }
        
        #endregion

    }
}