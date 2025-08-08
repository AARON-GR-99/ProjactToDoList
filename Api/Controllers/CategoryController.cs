using Api.Models.DTO;
using Api.Services.Catalogs;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace TodoList.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController(
    IMapper mapper,
    ICategoryService categoryService,
    ILogger<CategoryController> logger)
    : ControllerBase
{
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CategoryDto categoryDto)
    {
        logger.LogInformation("Attempt to create a new category");

        try
        {
            var createCategory = await categoryService.CreateAsync(categoryDto);
            logger.LogInformation("Category created successfully with ID {CategoryId}", createCategory.CategoryId); 
            return Ok(createCategory);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while creating a category."); 
            return StatusCode(500);
        }
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CategoryDto categoryDto)
    {
        logger.LogInformation("Attempt to update category with ID {CategoryId}", id);

        try
        {
            if (id != categoryDto.CategoryId)
            {
                return BadRequest("Category ID mismatch.");
            }

            var updatedCategory = await categoryService.UpdateAsync(categoryDto);
            logger.LogInformation("Category updated successfully with ID {CategoryId}", updatedCategory.CategoryId); 
            return Ok(updatedCategory);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while updating the category with ID {CategoryId}", id); 
            return StatusCode(500);
        }
    }

    [HttpGet("getall")]
    public async Task<IActionResult> GetAll()
    {
        logger.LogInformation("Attempt to retrieve all category");

        try
        {
            var categories = await categoryService.GetAllAsync();
            if (!categories.Any())
            {
                logger.LogWarning("No categories found"); 
                return NotFound();
            }

            var categoryDtos = mapper.Map<List<CategoryDto>>(categories);
            logger.LogInformation("Categories retrieved successfully"); 
            return Ok(categoryDtos);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while retrieving all categories."); 
            return StatusCode(500);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        logger.LogInformation("Attempt to retrieve category with ID {CategoryId}", id);

        try
        {
            var category = await categoryService.GetByIdAsync(id);
            if (category.CategoryId == 0)
            {
                logger.LogWarning("Category with ID {CategoryId} not found", id); 
                return NotFound();
            }

            logger.LogInformation("Category with ID {CategoryId} retrieved successfully", id); 
            return Ok(category);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while retrieving the category with ID {CategoryId}", id); 
            return StatusCode(500);
        }
    }
}
