using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Project1.Data;
using Project1.Models;
using System.ComponentModel.DataAnnotations;
using RazorLibrary.Classes;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace Project1.Pages;
public class IndexModel : PageModel
{

    public IDataProtector Protector;
    private readonly Context _context;

    public IndexModel(Context context, IDataProtectionProvider provider)
    {
        _context = context;
        Protector = provider.CreateProtector(nameof(UserLogin));
    }

    [TempData]
    public string Message { get; set; }

    [BindProperty]
    [DataType(DataType.Text)]
    public int? Identifier { get; set; }

    public List<SelectListItem> UserOptions { get; set; }

    public string EncryptedId { get; set; }

    public void OnGet()
    {

        ViewData["UserId"] = new SelectList(_context.UserLogin.AsNoTracking(), "Id", "EmailAddress");

    }

    /// <summary>
    /// Value going to edit page is encrypted and not visible in the address bar of the browser
    /// </summary>
    public Task<IActionResult> OnPostHiddenExample()
    {

        UserLogin userLogin = _context.UserLogin.FirstOrDefault(x => x.Id == Identifier)!;

        if (userLogin == null)
        {
            return Task.FromResult<IActionResult>(Page());
        }

        EncryptedId = Protector.Protect(Identifier.ToString()!);
        HttpContext.Session.SetString("Id", EncryptedId);
        
        return Task.FromResult<IActionResult>(RedirectToPage("./Edit"));

    }

    /// <summary>
    /// 
    /// </summary>
    public Task<IActionResult> OnPostNormalExample()
    {

        UserLogin userLogin = _context.UserLogin.FirstOrDefault(x => x.Id == Identifier)!;

        if (userLogin == null)
        {
            return Task.FromResult<IActionResult>(Page());
        }

        /*
         * This needs to be done because out of three possible post
         * two are protected while one is not protected. normal, normal
         * has zero significant meaning other than an identifier.
         */
        TempData.Put("normal", "normal");

        /*
         * bogus is meant to throw off hackers rather than using id.
         */
        return Task.FromResult<IActionResult>(
            RedirectToPage("./Edit", new { bogus = Identifier!.Value.ToString() }));

    }

    /// <summary>
    /// Value going to edit page is encrypted and visible in the address bar of the browser
    /// </summary>
    public Task<IActionResult> OnPostVisibleExample()
    {

        UserLogin userLogin = _context.UserLogin.FirstOrDefault(x => x.Id == Identifier)!;

        if (userLogin == null)
        {
            return Task.FromResult<IActionResult>(Page());
        }

        EncryptedId = Protector.Protect(Identifier.ToString()!);
        HttpContext.Session.SetString("Id", EncryptedId);

        return Task.FromResult<IActionResult>(
            RedirectToPage("./Edit", new { bogus = EncryptedId }));
        
    }
}
