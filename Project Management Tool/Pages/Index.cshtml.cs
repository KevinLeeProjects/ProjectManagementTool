using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Project_Management_Tool.Areas.Identity.Data;

namespace Project_Management_Tool.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(ILogger<IndexModel> logger, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        /*
        public void OnGet()
        {
            ViewData["UserID"] = _userManager.GetUserId(this.User);
           
           ViewData["FirstName"] = firstName;
        }
        */
        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                ViewData["UserID"] = user.Id;
                ViewData["FirstName"] = user.FirstName;
            }

            return Page();
        }
    }
}