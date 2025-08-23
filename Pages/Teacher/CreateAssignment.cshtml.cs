using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentClassworkPortal.Areas.Identity.Data;
using StudentClassworkPortal.Data;
using StudentClassworkPortal.Models;

namespace StudentClassworkPortal.Pages.Teacher;

[Authorize(Roles = "Teacher")]
public class CreateAssignmentModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public CreateAssignmentModel(ApplicationDbContext context)
    {
        _context = context;
        Input = new InputModel();
    }

    [BindProperty] public InputModel Input { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        var newAssignment = new VirtualFolder
        {
            AssignmentName = Input.AssignmentName,
            Chapter = Input.Chapter,
            Topic = Input.Topic,
            Class = Input.Class,
            Section = Input.Section
        };

        _context.VirtualFolders.Add(newAssignment);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Dashboard");
    }

    public class InputModel
    {
        [Required]
        [Display(Name = "Assignment Name")]
        public string AssignmentName { get; set; } = string.Empty;

        [Required] public string Chapter { get; set; } = string.Empty;

        [Required] public string Topic { get; set; } = string.Empty;

        [Required] public StudentClass Class { get; set; }

        [Required] public StudentSection Section { get; set; }
    }
}