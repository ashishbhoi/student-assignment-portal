
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StudentClassworkPortal.Areas.Identity.Data;
using StudentClassworkPortal.Data;
using StudentClassworkPortal.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentClassworkPortal.Pages.Teacher
{
    [Authorize(Roles = "Teacher")]
    public class DashboardModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DashboardModel(ApplicationDbContext context)
        {
            _context = context;
            RecentAssignments = new List<VirtualFolder>();
            AssignmentsByClass = new Dictionary<StudentClass, Dictionary<StudentSection, List<VirtualFolder>>>();
        }

        public IList<VirtualFolder> RecentAssignments { get; set; }
        public Dictionary<StudentClass, Dictionary<StudentSection, List<VirtualFolder>>> AssignmentsByClass { get; set; }


        public async Task OnGetAsync()
        {
            var allAssignments = await _context.VirtualFolders
                .Where(vf => !vf.IsPublicResource)
                .OrderByDescending(vf => vf.Id)
                .ToListAsync();

            RecentAssignments = allAssignments.Take(4).ToList();

            var groupedAssignments = allAssignments
                .GroupBy(a => a.Class)
                .ToDictionary(
                    classGroup => classGroup.Key,
                    classGroup => classGroup
                        .GroupBy(a => a.Section)
                        .ToDictionary(
                            sectionGroup => sectionGroup.Key,
                            sectionGroup => sectionGroup.ToList()
                        )
                );

            AssignmentsByClass = groupedAssignments;
        }

        public async Task<IActionResult> OnPostDeleteFolderAsync(int folderId)
        {
            var folder = await _context.VirtualFolders.FindAsync(folderId);
            if (folder != null)
            {
                var files = await _context.UserFiles.Where(f => f.VirtualFolderId == folderId).ToListAsync();
                _context.UserFiles.RemoveRange(files); // Also delete associated files
                _context.VirtualFolders.Remove(folder);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }
    }
}

