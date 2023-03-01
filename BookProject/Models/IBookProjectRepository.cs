using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookProject.Models
{
    public interface IBookProjectRepository
    {
        IQueryable<Book> Books { get; }
    }
}
