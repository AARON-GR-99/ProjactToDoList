using Api.Models.DTO;
using AutoMapper;
using Data.Entities;
using Data.Repisitories.Catalogs;
using Microsoft.Extensions.Logging;

namespace Api.Services.Catalogs;

public class CategoryService(ICategoryRepository categoryRepository, IMapper mapper, ILogger<CategoryService> logger) : ICategoryService
{
    public async Task<CategoryDto> CreateAsync(CategoryDto categoryDto)
    {
        logger.LogInformation("Attempting to create a new category");
        
        var category = mapper.Map<Category>(categoryDto);
        await categoryRepository.AddAsync(category);
        await categoryRepository.SaveChangesAsync();

        logger.LogInformation("Category created successfully with ID {CategoryId}", category.CategoryId);

        return mapper.Map<CategoryDto>(category);
    }

    public async Task<CategoryDto> UpdateAsync(CategoryDto categoryDto)
    {
        logger.LogInformation("Attempting to update category with ID {CategoryId}", categoryDto.CategoryId);

        var category = mapper.Map<Category>(categoryDto);
        await categoryRepository.UpdateAsync(category);
        await categoryRepository.SaveChangesAsync();

        logger.LogInformation("Category updated successfully with ID {CategoryId}", category.CategoryId);

        return mapper.Map<CategoryDto>(category);
    }

    public async Task<IEnumerable<CategoryDto>> GetAllAsync()
    {
        logger.LogInformation("Fetching all categories");

        var categories = await categoryRepository.GetAllAsync();
        return mapper.Map<IEnumerable<CategoryDto>>(categories);
    }

    public async Task<CategoryDto> GetByIdAsync(int categoryId)
    {
        logger.LogInformation("Fetching category with ID {CategoryId}", categoryId);

        var category = await categoryRepository.GetByIdAsync(categoryId);
        if (category == null)
        {
            logger.LogWarning("Category with ID {CategoryId} not found", categoryId);
            throw new KeyNotFoundException($"Category with ID {categoryId} not found");
        }

        return mapper.Map<CategoryDto>(category);
    }
}