using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StudentClassworkPortal.Areas.Identity.Data;
using StudentClassworkPortal.Data;
using StudentClassworkPortal.Models;

namespace StudentClassworkPortal.Pages.Teacher;

[Authorize(Roles = "Teacher")]
public class PublicResourcesModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public PublicResourcesModel(ApplicationDbContext context)
    {
        _context = context;
        Input = new InputModel();
        PublicFolders = new List<VirtualFolder>();
    }

    [BindProperty] public InputModel Input { get; set; }

    public IList<VirtualFolder> PublicFolders { get; set; }

    public async Task OnGetAsync()
    {
        PublicFolders = await _context.VirtualFolders
            .Where(vf => vf.IsPublicResource)
            .ToListAsync();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            await OnGetAsync();
            return Page();
        }

        var newFolder = new VirtualFolder
        {
            AssignmentName = Input.Subject,
            Chapter = Input.Chapter,
            Topic = "N/A", // Topic is not relevant for public resources
            Class = Input.Class,
            Section = StudentSection.A, // Section is not relevant, but required. Using a default.
            IsPublicResource = true
        };

        _context.VirtualFolders.Add(newFolder);
        await _context.SaveChangesAsync();

        return RedirectToPage();
    }

    public class InputModel
    {
        [Required] [Display(Name = "Subject")] public string Subject { get; set; } = string.Empty;

        [Required] public string Chapter { get; set; } = string.Empty;

        [Required] public StudentClass Class { get; set; }
    }
}