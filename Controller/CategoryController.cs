using Api.Models.DTO;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TodoList.Controllers;

[Authorize]
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
            return Ok(ApiResponseHelper.Success(createCategory, "Category created successfully."));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while creating a category.");
            return StatusCode(500, ApiResponseHelper.Error<CategoryDto>("An error occurred while creating the category", new ValidationError { Field = "InternalError", Message = ex.Message }));
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
            return Ok(ApiResponseHelper.Success(updatedCategory, "Category updated successfully."));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while updating the category with ID {CategoryId}", id);
            return StatusCode(500, ApiResponseHelper.Error<CategoryDto>("An error occurred while updating the category", new ValidationError { Field = "InternalError", Message = ex.Message }));
        }
    }

    [HttpGet("getall")]
    public async Task<IActionResult> GetAll()
    {
        logger.LogInformation("Attempt to retrieve all category");

        try
        {
            var categories = await categoryService.GetAllAsync();
            if (categories == null || !categories.Any())
            {
                logger.LogWarning("No categories found");
                return NotFound(ApiResponseHelper.Error<List<CategoryDto>>("No categories found."));
            }

            var categoryDtos = mapper.Map<List<CategoryDto>>(categories);
            logger.LogInformation("Categories retrieved successfully");
            return Ok(ApiResponseHelper.Success(categoryDtos, "Categories retrieved successfully."));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while retrieving all categories.");
            return StatusCode(500, ApiResponseHelper.Error<List<CategoryDto>>("An error occurred while retrieving the categories. Please contact support.", new ValidationError { Field = "InternalError", Message = ex.Message }));
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        logger.LogInformation("Attempt to retrieve category with ID {CategoryId}", id);

        try
        {
            var category = await categoryService.GetByIdAsync(id);
            if (category == null || category.CategoryId == 0)
            {
                logger.LogWarning("Category with ID {CategoryId} not found", id);
                return NotFound(ApiResponseHelper.Error<CategoryDto>("Category not found."));
            }

            logger.LogInformation("Category with ID {CategoryId} retrieved successfully", id);
            return Ok(ApiResponseHelper.Success(category, "Category retrieved successfully."));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while retrieving the category with ID {CategoryId}", id);
            return StatusCode(500, ApiResponseHelper.Error<CategoryDto>("An error occurred while retrieving the category", new ValidationError { Field = "InternalError", Message = ex.Message }));
        }
    }

    [HttpGet("getactive")]
    public async Task<IActionResult> GetActiveCategories()
    {
        logger.LogInformation("Attempt to retrieve active categories");

        try
        {
            var activeCategories = await categoryService.GetActiveCategoriesAsync();
            if (activeCategories == null || !activeCategories.Any())
            {
                logger.LogWarning("No active categories found");
                return NotFound(ApiResponseHelper.Error<IEnumerable<CategoryDto>>("No active categories found."));
            }

            logger.LogInformation("Active categories retrieved successfully");
            return Ok(ApiResponseHelper.Success(activeCategories, "Active categories retrieved successfully."));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while retrieving active categories.");
            return StatusCode(500, ApiResponseHelper.Error<string>("An error occurred while retrieving active categories. Please contact support.", new ValidationError { Field = "InternalError", Message = ex.Message }));
        }
    }
}
