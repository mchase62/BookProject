using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookProject.Models;

namespace BookProject.Components
{
    public class TypesViewComponent : ViewComponent
    {
        private IBookProjectRepository repo { get; set; }

        public TypesViewComponent (IBookProjectRepository temp)
        {
            repo = temp;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedType = RouteData?.Values["category"]; // ? means it's ok if there is no category in the url, it's nullable
            var types = repo.Books
                .Select(x => x.Category)
                .Distinct()
                .OrderBy(x => x);
            return View(types);
        }
    }
}
