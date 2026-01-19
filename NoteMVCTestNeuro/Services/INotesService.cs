using NoteMVCTestNeuro.Models;

namespace NoteMVCTestNeuro.Services
{
    public interface INotesService
    {
        Task<List<Note>> GetAllAsync();
        Task<Note?> GetByIdAsync(int id);

        Task<int> CreateAsync(string title, string? content);
        Task<bool> UpdateAsync(int id, string title, string? content);
    }
}
