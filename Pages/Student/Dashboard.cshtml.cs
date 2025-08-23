using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StudentClassworkPortal.Areas.Identity.Data;
using StudentClassworkPortal.Data;
using StudentClassworkPortal.Models;

namespace StudentClassworkPortal.Pages.Student;

[Authorize(Roles = "Student")]
public class DashboardModel : PageModel
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public DashboardModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
        UserFiles = new List<UserFile>();
        AvailableAssignments = new List<VirtualFolder>();
        ErrorMessage = string.Empty;
    }

    public IList<UserFile> UserFiles { get; set; }
    public IList<VirtualFolder> AvailableAssignments { get; set; }

    [TempData] public string ErrorMessage { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return NotFound("User not found.");

        UserFiles = await _context.UserFiles
            .Where(f => f.UserId == user.Id)
            .Include(f => f.VirtualFolder)
            .ToListAsync();

        AvailableAssignments = await _context.VirtualFolders
            .Where(vf => !vf.IsPublicResource && vf.Class == user.Class && vf.Section == user.Section)
            .ToListAsync();

        return Page();
    }

    public async Task<IActionResult> OnPostUploadFileAsync(IFormFile fileUpload, int virtualFolderId)
    {
        if (fileUpload == null || fileUpload.Length == 0)
        {
            ErrorMessage = "Please select a file to upload.";
            return RedirectToPage();
        }

        var user = await _userManager.GetUserAsync(User);
        if (user == null) return NotFound("User not found.");

        // File Type Validation
        var allowedExtensions = new[] {".java", ".sql", ".odt", ".ods", ".odp", ".pdf", ".txt", ".md", ".zip"};
        var fileExtension = Path.GetExtension(fileUpload.FileName).ToLowerInvariant();

        if (string.IsNullOrEmpty(fileExtension) || !allowedExtensions.Contains(fileExtension))
        {
            ErrorMessage = "Invalid file type. Allowed types are: .java, .sql, .odt, .ods, .odp, .pdf, .txt, .md, .zip";
            return RedirectToPage();
        }

        var assignment = await _context.VirtualFolders.FindAsync(virtualFolderId);
        if (assignment == null) return NotFound("Assignment not found.");

        // Check for and delete existing submission
        var existingFile = await _context.UserFiles
            .FirstOrDefaultAsync(f => f.UserId == user.Id && f.VirtualFolderId == virtualFolderId);

        if (existingFile != null) _context.UserFiles.Remove(existingFile);

        using var memoryStream = new MemoryStream();
        await fileUpload.CopyToAsync(memoryStream);

        var newFile = new UserFile
        {
            FileName = Path.GetFileName(fileUpload.FileName),
            Content = memoryStream.ToArray(),
            UserId = user.Id,
            VirtualFolderId = virtualFolderId
        };

        _context.UserFiles.Add(newFile);
        await _context.SaveChangesAsync();

        return RedirectToPage();
    }

    public async Task<IActionResult> OnGetDownloadFileAsync(int fileId)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return Forbid();

        var file = await _context.UserFiles.FirstOrDefaultAsync(f => f.Id == fileId && f.UserId == user.Id);

        if (file == null) return NotFound();

        return File(file.Content, "application/octet-stream", file.FileName);
    }

    public async Task<IActionResult> OnPostCreateFileAsync(string fileName, string fileContent, int virtualFolderId)
    {
        if (string.IsNullOrWhiteSpace(fileName) || string.IsNullOrWhiteSpace(fileContent))
        {
            ErrorMessage = "File name and content cannot be empty.";
            return RedirectToPage();
        }

        var user = await _userManager.GetUserAsync(User);
        if (user == null) return NotFound("User not found.");

        var assignment = await _context.VirtualFolders.FindAsync(virtualFolderId);
        if (assignment == null) return NotFound("Assignment not found.");

        // Check for and delete existing submission
        var existingFile = await _context.UserFiles
            .FirstOrDefaultAsync(f => f.UserId == user.Id && f.VirtualFolderId == virtualFolderId);

        if (existingFile != null) _context.UserFiles.Remove(existingFile);

        // Handle file extension
        var finalFileName = fileName;
        if (!Path.HasExtension(finalFileName) || Path.GetExtension(finalFileName).Length < 2) finalFileName += ".txt";

        var newFile = new UserFile
        {
            FileName = finalFileName,
            Content = Encoding.UTF8.GetBytes(fileContent),
            UserId = user.Id,
            VirtualFolderId = virtualFolderId
        };

        _context.UserFiles.Add(newFile);
        await _context.SaveChangesAsync();

        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostDeleteFileAsync(int fileId)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return Forbid();

        var fileToDelete = await _context.UserFiles.FirstOrDefaultAsync(f => f.Id == fileId && f.UserId == user.Id);

        if (fileToDelete == null) return NotFound();

        _context.UserFiles.Remove(fileToDelete);
        await _context.SaveChangesAsync();

        return RedirectToPage();
    }
}