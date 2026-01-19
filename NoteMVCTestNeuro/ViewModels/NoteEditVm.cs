using System.ComponentModel.DataAnnotations;

namespace NoteMVCTestNeuro.ViewModels
{
    public class NoteEditVm
    {
        public int Id { get; set; }

        [Required, StringLength(200)]
        public string Title { get; set; } = "";

        [StringLength(4000)]
        public string? Content { get; set; }
    }
}
