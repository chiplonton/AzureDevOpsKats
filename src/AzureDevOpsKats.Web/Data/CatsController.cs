﻿using System;
using System.Collections.Generic;
using System.IO;
using AzureDevOpsKats.Service.Configuration;
using AzureDevOpsKats.Service.Interface;
using AzureDevOpsKats.Service.Models;
using AzureDevOpsKats.Web.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AzureDevOpsKats.Web.Controllers
{
    /// <summary>
    ///
    /// </summary>
    [Route("api/[controller]")]
    [EnableCors("AllowAll")]
    [ApiController]
    public class CatsController : ControllerBase
    {
        private readonly ICatService _catService;

        private readonly IFileService _fileService;

        private readonly ILogger<CatsController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CatsController"/> class.
        /// </summary>
        /// <param name="catService"></param>
        /// <param name="fileService"></param>
        /// <param name="logger"></param>
        /// <param name="settings"></param>
        public CatsController(ICatService catService, IFileService fileService, ILogger<CatsController> logger, IOptions<ApplicationOptions> settings)
        {
            _catService = catService ?? throw new ArgumentNullException(nameof(catService));
            _fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            ApplicationSettings = settings.Value;
        }

        private ApplicationOptions ApplicationSettings { get; set; }

        /// <summary>
        /// Get List of Cats
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json", Type = typeof(IEnumerable<CatModel>))]
        public IActionResult Get()
        {
            _logger.LogWarning("Get All Cats");

            var results = _catService.GetCats();
            return Ok(results);
        }

        /// <summary>
        /// Get Cat
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Produces("application/json", Type = typeof(CatModel))]
        public IActionResult Get(int id)
        {
            var result = _catService.GetCat(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Delete Cat
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _catService.GetCat(id);
            if (result == null)
                return NotFound();

            _catService.DeleteCat(id);
            _fileService.DeleteFile(result.Photo);

            return NoContent();
        }

        /// <summary>
        ///  Create Cat
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        [Consumes("application/json")]
        public IActionResult Post([FromBody] CatCreateModel value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            $"Bytes Exist:{value.Bytes != null}".ConsoleRed();

            string fileName = $"{Guid.NewGuid()}.jpg";
            var filePath = Path.Combine($"{Path.GetFullPath(ApplicationSettings.FileStorage.RequestPath)}/{fileName}");

            var catModel = new CatModel
            {
                Name = value.Name,
                Description = value.Description,
                Photo = fileName,
            };

            _fileService.SaveFile(filePath, value.Bytes);
            _catService.CreateCat(catModel);

            return Ok();
        }

        /// <summary>
        /// Update Cat Properties
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CatUpdateModel value)
        {
            if (value == null)
            {
                return BadRequest();
            }

            _catService.EditCat(id, value);

            return NoContent();
        }
    }
}
