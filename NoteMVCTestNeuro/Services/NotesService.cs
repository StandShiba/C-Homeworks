using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NoteMVCTestNeuro.Data;
using NoteMVCTestNeuro.Models;

namespace NoteMVCTestNeuro.Services
{
    public class NotesService : INotesService
    {
        private readonly AppDbContext _db;

        public NotesService(AppDbContext db)
        {
            _db = db;
        }

        public Task<List<Note>> GetAllAsync()
        {
            return _db.Notes
                .OrderByDescending(n => n.UpdatedUtc)
                .ToListAsync();
        }

        public Task<Note?> GetByIdAsync(int id)
        {
            return _db.Notes.FirstOrDefaultAsync(n => n.Id == id);
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
    }
}
