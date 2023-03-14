using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BookProject.Models;
using BookProject.Infrastructure;

namespace BookProject.Pages
{
    public class ShopModel : PageModel
    {
        private IBookProjectRepository repo { get; set; }
        public ShopModel (IBookProjectRepository temp, Cart c)
        {
            repo = temp;
            cart = c;
        }
        public Cart cart { get; set; }
        public string ReturnUrl { get; set; }
        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
            // cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart(); being handled in startup instead
        }

        public IActionResult OnPost(int bookId, string returnUrl)
        {
            Book b = repo.Books.FirstOrDefault(x => x.BookId == bookId);
            // cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart(); // if cart already exists, else make a new one
            cart.AddItem(b, 1);

            // HttpContext.Session.SetJson("cart", cart);

            return RedirectToPage(new { ReturnUrl = returnUrl });
        }

        public IActionResult OnPostRemove(int bookId, string returnUrl)
        {
            cart.RemoveItem(cart.Items.First(x => x.Book.BookId == bookId).Book);

            return RedirectToPage ( new { ReturnUrl = returnUrl });
        }
    }
}
