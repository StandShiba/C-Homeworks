using Microsoft.EntityFrameworkCore;
using NoteMVCTestNeuro.Data;
using NoteMVCTestNeuro.Models;
using NoteMVCTestNeuro.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoteMVCTestNeuro.Services
{
    public class NotesService : INotesService
    {
        private readonly AppDbContext _db;

        public NotesService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<PagedResult<Note>> GetPagedAsync(int page, int pageSize)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;
            if (pageSize > 100) pageSize = 100;

            var query = _db.Notes
                .OrderByDescending(n => n.UpdatedUtc)
                .AsNoTracking();

            var total = await query.CountAsync();

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Note>
            {
                Items = items,
                Page = page,
                PageSize = pageSize,
                TotalCount = total
            };
        }

        public Task<Note?> GetByIdAsync(int id)
        {
            return _db.Notes.AsNoTracking().FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task<int> CreateAsync(string title, string? content)
        {
            var note = new Note
            {
                Title = title,
                Content = content,
                CreatedUtc = DateTime.UtcNow,
                UpdatedUtc = DateTime.UtcNow
            };

            _db.Notes.Add(note);
            await _db.SaveChangesAsync();
            return note.Id;
        }

        public async Task<bool> UpdateAsync(int id, string title, string? content)
        {
            var note = await _db.Notes.FirstOrDefaultAsync(n => n.Id == id);
            if (note == null) return false;

            note.Title = title;
            note.Content = content;
            note.UpdatedUtc = DateTime.UtcNow;

            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var note = await _db.Notes.FirstOrDefaultAsync(n => n.Id == id);
            if (note == null) return false;

            _db.Notes.Remove(note);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
