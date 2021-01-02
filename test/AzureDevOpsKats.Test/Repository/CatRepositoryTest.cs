﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task Get_Cats()
        {
            var results = await _catRepository.GetCats();
            Assert.NotNull(results);
            Assert.IsType<List<Cat>>(results);
        }

        [Fact]
        [Trait("Category", "Intergration")]
        public async Task Get_Cats_Paging()
        {
            var results = await _catRepository.GetCats(3, 0);
            Assert.NotNull(results);
            Assert.IsType<List<Cat>>(results);
            Assert.True(results.Count() == 3);
        }

        [Fact]
        [Trait("Category", "Intergration")]
        public async Task Get_Count()
        {
            var result = await _catRepository.GetCount();

            Assert.IsType<long>(result);
            Assert.True(result > 0);
        }

        [Fact]
        [Trait("Category", "Intergration")]
        public async Task Create_Cat()
        {
            var name = "Test 2";
            var description = "Test Description";

            var cat = new Cat
            {
                Name = name,
                Description = description,
                Photo = "Test-Cat.jpg"
            };

            var sut = await _catRepository.CreateCat(cat);
            Assert.True(sut > 0);
        }

        [Fact]
        [Trait("Category", "Intergration")]
        public async Task Edit_Cat()
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

            await _catRepository.EditCat(cat);
            var newCat = await _catRepository.GetCat(id);

            Assert.Equal(id, newCat.Id);
            Assert.Equal(description, newCat.Description);
        }

        [Theory]
        [InlineData(1)]
        [Trait("Category", "Intergration")]
        public async Task Get_Cat(int id)
        {
            var result = await _catRepository.GetCat(id);
            Assert.IsType<Cat>(result);

            Assert.NotNull(result);
            Assert.IsType<Cat>(result);
        }

        [Theory]
        [InlineData(int.MaxValue)]
        [Trait("Category", "Intergration")]
        public async Task Get_Cat_Returns_Null(int id)
        {
            var result = await _catRepository.GetCat(id);
            Assert.Null(result);
        }

        [Fact]
        [Trait("Category", "Intergration")]
        public async Task Delete_Cat()
        {
            var cat = new Cat
            {
                Name = "Test Cat",
                Description = "Test Cat",
                Photo = "Test-Cat.jpg"
            };

            await _catRepository.CreateCat(cat);
            var resultId = await _catRepository.GetCats();//.Select(c => c.Id); //  .OrderByDescending(c => c.Id).Select(c => c.Id).FirstOrDefaultAsync();

            //await _catRepository.DeleteCat(resultId);
        }
    }
}
