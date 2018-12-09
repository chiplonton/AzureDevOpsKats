﻿using System;
using System.Collections.Generic;
using System.Linq;
using AzureDevOpsKats.Data.Entities;
using AzureDevOpsKats.Data.Repository;
using AzureDevOpsKats.Test.Fixture;
using Xunit;
using Xunit.Abstractions;

namespace AzureDevOpsKats.Test.Repository
{
    public class CatRepositoryTest : IClassFixture<CatRepositoryFixture>
    {
        private readonly ICatRepository _catRepository;

        private readonly ITestOutputHelper _output;

        public CatRepositoryTest(CatRepositoryFixture fixture, ITestOutputHelper output)
        {
            this._catRepository = fixture.CatRepository;
            this._output = output;
        }

        [Fact]
        [Trait("Category", "Intergration")]
        public void Get_Cats()
        {
            var results = _catRepository.GetCats();
            Assert.NotNull(results);
            Assert.IsType<List<Cat>>(results);
        }

        [Fact]
        [Trait("Category", "Intergration")]
        public void Create_Cat()
        {
            var cat = new Cat
            {
                Name = "Test 2",
                Description = "Test",
                Photo = "Test-Cat.jpg"
            };

            _catRepository.CreateCat(cat);
        }

        [Fact]
        [Trait("Category", "Intergration")]
        public void Edit_Cat()
        {
            var id = 2;
            var description = $"Updated_Cat_{DateTime.Now.Second}";
            var cat = new Cat
            {
                Id = id,
                Name = "Test 2",
                Description = description,
                Photo = "Test-Cat.jpg"
            };

            _catRepository.EditCat(cat);
            var newCat = _catRepository.GetCat(id);

            Assert.Equal(id, newCat.Id);
            Assert.Equal(description, newCat.Description);
        }

        [Theory]
        [InlineData(1)]
        [Trait("Category", "Intergration")]
        public void Get_Cat(int id)
        {
            var result = _catRepository.GetCat(id);
            Assert.IsType<Cat>(result);

            Assert.NotNull(result);
            Assert.IsType<Cat>(result);
        }

        [Fact]
        [Trait("Category", "Intergration")]
        public void Delete_Cat()
        {
            var cat = new Cat
            {
                Name = "Test Cat",
                Description = "Test Cat",
                Photo = "Test-Cat.jpg"
            };

            _catRepository.CreateCat(cat);
            var resultId = _catRepository.GetCats().OrderByDescending(c => c.Id).Select(c => c.Id).FirstOrDefault();

            _catRepository.DeleteCat(resultId);
        }
    }
}
