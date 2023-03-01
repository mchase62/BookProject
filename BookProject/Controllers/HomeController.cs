using BookProject.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookProject.Models.ViewModels;

namespace BookProject.Controllers
{
    public class HomeController : Controller
    {
        private IBookProjectRepository repo;
        
        public HomeController (IBookProjectRepository temp)
        {
            repo = temp;
        }

        public IActionResult Index(int pageNum=1)
        {
            int pageSize = 10;

            var x = new BooksViewModel
            {
                Books = repo.Books
                .OrderBy(b => b.Title)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize),

                PageInfo = new PageInfo
                {
                    TotalNumBooks = repo.Books.Count(),
                    BooksPerPage = pageSize,
                    CurrentPage = pageNum
                }
            };

            return View(x);
        }
    }
}
