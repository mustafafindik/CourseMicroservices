using Course.Services.Catalog.Entities.Concrete;
using Course.Services.Catalog.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Services.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _categoryService.GetAllAsync();
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _categoryService.GetByIdAsync(id);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return NotFound(response);
        }


        [HttpGet("GetByCategoryName/{categoryName}")]
        public async Task<IActionResult> GetByCategoryName(string categoryName)
        {
            var response = await _categoryService.GetByNameAsync(categoryName);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(Category category)
        {

            var response = await _categoryService.AddCategory(category);
            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(Category category)
        {

            var response = await _categoryService.UpdateCategory(category);
            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }


        [HttpPost("delete")]
        public async Task<IActionResult> Delete(string id)
        {

            var response = await _categoryService.DeleteCategory(id);
            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }
    }
}
