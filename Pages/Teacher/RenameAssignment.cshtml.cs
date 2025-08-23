using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentClassworkPortal.Data;

namespace StudentClassworkPortal.Pages.Teacher;

[Authorize(Roles = "Teacher")]
public class RenameAssignmentModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public RenameAssignmentModel(ApplicationDbContext context)
    {
        _context = context;
        Input = new InputModel();
    }

    [BindProperty] public InputModel Input { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var assignment = await _context.VirtualFolders.FindAsync(id);

        if (assignment == null || assignment.IsPublicResource) return NotFound();

        Input = new InputModel
        {
            Id = assignment.Id,
            AssignmentName = assignment.AssignmentName
        };

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        var assignmentToUpdate = await _context.VirtualFolders.FindAsync(Input.Id);

        if (assignmentToUpdate == null) return NotFound();

        assignmentToUpdate.AssignmentName = Input.AssignmentName;
        await _context.SaveChangesAsync();

        return RedirectToPage("./Dashboard");
    }

    public class InputModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "New Assignment Name")]
        public string AssignmentName { get; set; } = string.Empty;
    }
}