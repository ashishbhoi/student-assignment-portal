using StudentClassworkPortal.Models;
using System.Collections.Generic;

namespace StudentClassworkPortal.Pages.Teacher.ViewModels
{
    public class StudentViewModel
    {
        public string Email { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Class { get; set; } = string.Empty;
        public string Section { get; set; } = string.Empty;
        public List<UserFile> UserFiles { get; set; } = new List<UserFile>();
    }
}
