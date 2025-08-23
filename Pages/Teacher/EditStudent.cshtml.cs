using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentClassworkPortal.Areas.Identity.Data;

namespace StudentClassworkPortal.Pages.Teacher
{
    [Authorize(Roles = "Teacher")]
    public class EditStudentModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public EditStudentModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            Input = new InputModel();
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public string Id { get; set; } = string.Empty;

            [Required]
            [Display(Name = "Name")]
            public string Name { get; set; } = string.Empty;

            [Required]
            [Display(Name = "Username")]
            public string Username { get; set; } = string.Empty;

            [Display(Name = "Class")]
            public StudentClass? Class { get; set; }

            [Display(Name = "Section")]
            public StudentSection? Section { get; set; }

            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string? Password { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            Input = new InputModel
            {
                Id = user.Id,
                Name = user.Name,
                Username = user.UserName ?? string.Empty,
                Class = user.Class,
                Section = user.Section
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.FindByIdAsync(Input.Id);

            if (user == null)
            {
                return NotFound();
            }

            user.Name = Input.Name;
            user.UserName = Input.Username;
            user.Class = Input.Class;
            user.Section = Input.Section;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }

            if (!string.IsNullOrEmpty(Input.Password))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var passwordResult = await _userManager.ResetPasswordAsync(user, token, Input.Password);

                if (!passwordResult.Succeeded)
                {
                    foreach (var error in passwordResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return Page();
                }
            }

            return RedirectToPage("./Dashboard");
        }
    }
}