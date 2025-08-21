using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentClassworkPortal.Data;
using StudentClassworkPortal.Models;
using System.IO;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using StudentClassworkPortal.Areas.Identity.Data;
using System.Linq;
using System.Text;

namespace StudentClassworkPortal.Pages.FileViewer
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            FileToDisplay = new FileViewModel { FileName = string.Empty, Content = string.Empty };
            FileExtension = string.Empty;
        }

        public FileViewModel FileToDisplay { get; set; }
        public string FileExtension { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Forbid();
            }

            var userFile = await _context.UserFiles.FindAsync(id);

            if (userFile == null)
            {
                return NotFound();
            }

            // Security Check: Ensure the user has access to this file.
            // A teacher can see any file. A student can only see their own files.
            var isTeacher = await _userManager.IsInRoleAsync(user, "Teacher");
            if (!isTeacher && userFile.UserId != user.Id)
            {
                return Forbid();
            }
            
            FileToDisplay = new FileViewModel
            {
                FileName = userFile.FileName,
                Content = Encoding.UTF8.GetString(userFile.Content)
            };

            FileExtension = Path.GetExtension(userFile.FileName).TrimStart('.');

            return Page();
        }
    }

    public class FileViewModel
    {
        public string FileName { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
    }
}