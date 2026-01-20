using Microsoft.AspNetCore.Mvc;
using NoteMVCTestNeuro.Services;
using NoteMVCTestNeuro.ViewModels;

namespace NoteMVCTestNeuro.Controllers
{
    public class NotesController : Controller
    {
        private readonly INotesService _notes;

        public NotesController(INotesService notes)
        {
            _notes = notes;
        }

        public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        {
            var model = await _notes.GetPagedAsync(page, pageSize);
            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var note = await _notes.GetByIdAsync(id);
            if (note == null) return NotFound();

            return View(note);
        }

        public IActionResult Create() => View(new NoteEditVm());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NoteEditVm vm)
        {
            if (!ModelState.IsValid) return View(vm);

            await _notes.CreateAsync(vm.Title, vm.Content);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var note = await _notes.GetByIdAsync(id);
            if (note == null) return NotFound();

            return View(new NoteEditVm
            {
                Id = note.Id,
                Title = note.Title,
                Content = note.Content
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, NoteEditVm vm)
        {
            if (id != vm.Id) return BadRequest();
            if (!ModelState.IsValid) return View(vm);

            var ok = await _notes.UpdateAsync(vm.Id, vm.Title, vm.Content);
            if (!ok) return NotFound();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var note = await _notes.GetByIdAsync(id);
            if (note == null) return NotFound();

            return View(note);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ok = await _notes.DeleteAsync(id);
            if (!ok) return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}