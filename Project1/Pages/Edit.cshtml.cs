using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Project1.Data;
using Project1.Models;

#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS0472
#pragma warning disable CS8618

namespace Project1.Pages
{
    public class EditModel : PageModel
    {
        private readonly Context _context;
        private IDataProtector Protector;

        [BindProperty]
        public int Identifier { get; set; }

        public EditModel(Context context, IDataProtectionProvider provider)
        {
            _context = context;
            Protector = provider.CreateProtector(nameof(UserLogin));
        }


        [BindProperty]
        public UserLogin UserLogin { get; set; } = default!;

        [TempData]
        public string Message { get; set; }


        public async Task<IActionResult> OnGetAsync(string bogus)
        {

            /*
             * This needs to be done because out of three possible post
             * two are protected while one is not protected.
             */
            var id = TempData.Count == 1 && TempData.ContainsKey("normal")
                ? Convert.ToInt32(bogus)
                : Convert.ToInt32(Protector.Unprotect(HttpContext.Session.GetString("Id")));


            TempData.Clear();

            if (id == null || _context.UserLogin == null)
            {
                Message = "Missing id";

                return new RedirectToPageResult("/Index");
            }

            Identifier = id;

            var userlogin = await _context.UserLogin.FirstOrDefaultAsync(user => user.Id == id);

            if (userlogin == null)
            {
                Message = $"No user with the provided Id {id} exists";

                return new RedirectToPageResult("/Index");

            }

            UserLogin = userlogin;

            TempData.Clear();
            return Page();

        }

        public async Task<IActionResult> OnPostAsync()
        {
            
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(UserLogin).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserLoginExists(UserLogin.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");

        }

        private bool UserLoginExists(int id)
        {
            return (_context.UserLogin?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public RedirectToPageResult OnPostGoHome() => new("Index");
    }
}
