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
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _courseService.GetAllAsync();
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _courseService.GetByIdAsync(id);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpGet("{courseName}")]
        public async Task<IActionResult> GetByName(string courseName)
        {
            var response = await _courseService.GetByNameAsync(courseName);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpGet("{categoryName}")]
        public async Task<IActionResult> GetByCategoryName(string categoryName)
        {
            var response = await _courseService.GetByCategoryNameAsync(categoryName);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(Entities.Concrete.Course course)
        {

            var response = await _courseService.AddCourse(course);
            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(Entities.Concrete.Course course)
        {

            var response = await _courseService.UpdateCourse(course);
            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }


        [HttpPost("delete")]
        public async Task<IActionResult> Delete(string id)
        {

            var response = await _courseService.DeleteCourse(id);
            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }


    }
}
