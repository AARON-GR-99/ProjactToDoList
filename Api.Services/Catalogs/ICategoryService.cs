using Api.Models.DTO;

namespace Api.Services.Catalogs;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDto>> GetAllAsync();
    Task<CategoryDto> GetByIdAsync(int categoryId);
    Task<CategoryDto> CreateAsync(CategoryDto categoryDto);
    Task<CategoryDto> UpdateAsync(CategoryDto categoryDto);
}