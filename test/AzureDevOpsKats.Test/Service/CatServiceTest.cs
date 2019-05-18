﻿using System.Linq;
using AzureDevOpsKats.Service.Interface;
using AzureDevOpsKats.Service.Models;
using AzureDevOpsKats.Test.Fixture;
using Xunit;
using Xunit.Abstractions;

namespace AzureDevOpsKats.Test.Service
{
    public class CatServiceTest : IClassFixture<CatServiceFixture>
    {
        private readonly ICatService _catService;

        private readonly ITestOutputHelper _output;

        public CatServiceTest(CatServiceFixture fixture, ITestOutputHelper output)
        {
            this._catService = fixture.CatService;
            this._output = output;
        }

        [Fact]
        [Trait("Category", "Intergration")]
        public void Can_Get_Cat_List()
        {
            var results = _catService.GetCats();
            var count = results.Count();

            Assert.NotNull(results);
            Assert.NotEqual(0, count);
        }

        [Fact]
        [Trait("Category", "Intergration")]
        public void Can_Get_Cat_Paging_List()
        {
            var results = _catService.GetCats(10,1);
            var count = results.Count();

            Assert.NotNull(results);
            Assert.NotEqual(0, count);
        }

        [Theory]
        [InlineData(1)]
        [Trait("Category", "Intergration")]
        public void Get_Cat(int id)
        {
            var result = _catService.GetCat(id);
            Assert.IsType<CatModel>(result);
            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
        }
    }
}
