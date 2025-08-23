using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StudentClassworkPortal.Areas.Identity.Data;
using StudentClassworkPortal.Data;
using StudentClassworkPortal.Models;

namespace StudentClassworkPortal.Pages;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public IndexModel(SignInManager<ApplicationUser> signInManager, ApplicationDbContext context)
    {
        _signInManager = signInManager;
        _context = context;
        PublicResources = new List<VirtualFolder>();
    }

    public IList<VirtualFolder> PublicResources { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        if (_signInManager.IsSignedIn(User))
        {
            if (User.IsInRole("Teacher")) return RedirectToPage("/Teacher/Dashboard");

            if (User.IsInRole("Student")) return RedirectToPage("/Student/Dashboard");
        }

        PublicResources = await _context.VirtualFolders
            .Where(vf => vf.IsPublicResource)
            .Include(vf => vf.UserFiles)
            .ToListAsync();

        return Page();
    }
}