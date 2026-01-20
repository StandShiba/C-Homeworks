using NoteMVCTestNeuro.Models;
using NoteMVCTestNeuro.ViewModels;

namespace NoteMVCTestNeuro.Services
{
    public interface INotesService
    {
        Task<PagedResult<Note>> GetPagedAsync(int page, int pageSize);

        Task<Note?> GetByIdAsync(int id);

        Task<int> CreateAsync(string title, string? content);
        Task<bool> UpdateAsync(int id, string title, string? content);

        Task<bool> DeleteAsync(int id);
    }
}
