using System;
using System.Linq;
using Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using Tests.ToolsTests;
using web.api.App.Common;
using web.api.App.Ingredients.ReferenceIngredients;
using web.api.App.Ingredients.ReferenceIngredients.Queries;
using Xunit;

namespace Tests.Ingredients
{
    public class ReferenceIngredientServiceTests : BaseUnitTest
    {
        private readonly ReferenceIngredientService _referenceIngredientService;

        public ReferenceIngredientServiceTests()
        {
            var diServiceBuilder = new DIServiceBuilder();
            _referenceIngredientService = diServiceBuilder.GetService<ReferenceIngredientService>();
        }

        [Fact]
        public async void Get_ReferenceIngredient_Ok_Test()
        {
            var ingredient = await _creator.ReferenceIngredientCreator.CreateOne();

            var result = await _referenceIngredientService.Get(ingredient.Id);
            Assert.Equal(ingredient.Id, result.Id);
        }

        [Fact]
        public async void Add_ReferenceIngredient_Ok_Test()
        {
            var ingredient = new ReferenceIngredient
            {
                Name = $"Ingredient_{Guid.NewGuid().ToString()[..5]}",
                Price = 11,
                Unit = Unit.Items,
                PackAmount = 1,
                PackPrice = 135,
                TeamId = (await _creator.TeamsCreator.CreateOne()).Id
            };

            var result = await _referenceIngredientService.Create(ingredient);
            Assert.True(result.Id != 0);
            Assert.Equal(11, result.Price);
        }

        [Fact]
        public async void Add_ReferenceIngredient_WrongTeam_Test()
        {
            var ingredient = new ReferenceIngredient
            {
                Name = Guid.NewGuid().ToString(),
                Price = 123,
                Unit = Unit.Items,
                PackAmount = 123,
                PackPrice = 123,
                TeamId = 9999999 // <-- This is wrong!
            };
            await Assert.ThrowsAsync<DbUpdateException>(async ()
                =>
            {
                await _referenceIngredientService.Create(ingredient);
            });
        }

        [Fact]
        public async void Search_ReferenceIngredient_OK_Test()
        {
            var ingredients = await _creator.ReferenceIngredientCreator.CreateMany(6);
            var result = await _referenceIngredientService.Search(new ReferenceIngredientSearchQuery()
            {
                Word = ingredients.First().Name[..4].ToUpper(),
            });
            Assert.True(result.ItemsCount > 0);
        }

        [Fact]
        public async void Update_ReferenceIngredient_OK_Test()
        {
            var ingredient = await _creator.ReferenceIngredientCreator.CreateOne();
            var modified = new ReferenceIngredient
            {
                Name = "ingredient_" + Guid.NewGuid().ToString()[..5],
                Price = 123,
                Unit = Unit.Kg,
                PackAmount = 123,
                PackPrice = 123,
                TeamId = ingredient.TeamId,
                Id = ingredient.Id
            };

            await _referenceIngredientService.Update(modified);

            var updated = await _referenceIngredientService.Get(modified.Id);
            Assert.Equal(modified.Id, updated.Id);
            Assert.Equal(modified.Name, updated.Name);
            Assert.Equal(modified.Price, updated.Price);
            Assert.Equal(modified.TeamId, updated.TeamId);
        }

        [Fact]
        public async void Delete_ReferenceIngredient_OK_Test()
        {
            var ingredient = _creator.ReferenceIngredientCreator.CreateOne();
            await _referenceIngredientService.Delete(ingredient.Id);
            await Assert.ThrowsAsync<EntityNotFoundException<ReferenceIngredient>>(async () =>
            {
                await _referenceIngredientService.Get(ingredient.Id);
            });
        }
    }
}