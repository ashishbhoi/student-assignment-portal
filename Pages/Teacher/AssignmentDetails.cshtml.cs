using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StudentClassworkPortal.Areas.Identity.Data;
using StudentClassworkPortal.Data;
using StudentClassworkPortal.Models;

namespace StudentClassworkPortal.Pages.Teacher;

[Authorize(Roles = "Teacher")]
public class AssignmentDetailsModel : PageModel
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public AssignmentDetailsModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
        Assignment = new VirtualFolder();
        Submissions = new List<SubmissionViewModel>();
    }

    public VirtualFolder Assignment { get; set; }
    public List<SubmissionViewModel> Submissions { get; set; }
    public int SubmissionCount { get; set; }
    public int TotalStudents { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var assignment = await _context.VirtualFolders
            .Include(vf => vf.UserFiles)
            .FirstOrDefaultAsync(vf => vf.Id == id && !vf.IsPublicResource);

        if (assignment == null) return NotFound();

        Assignment = assignment;

        var studentsInClass = await _userManager.Users
            .Where(u => u.Class == assignment.Class && u.Section == assignment.Section)
            .ToListAsync();

        TotalStudents = studentsInClass.Count;
        SubmissionCount = Assignment.UserFiles.Count;

        Submissions = studentsInClass.Select(student => new SubmissionViewModel
        {
            StudentName = student.Name,
            StudentEmail = student.Email ?? "N/A",
            Submission = Assignment.UserFiles.FirstOrDefault(f => f.UserId == student.Id)
        }).ToList();

        return Page();
    }
}

public class SubmissionViewModel
{
    public string? StudentName { get; set; }
    public string? StudentEmail { get; set; }
    public UserFile? Submission { get; set; }
}