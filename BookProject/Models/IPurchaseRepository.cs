using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookProject.Models
{
    public interface IPurchaseRepository
    {
        public IQueryable<Purchase> Purchases { get; set; }

        public void SavePurchase(Purchase purchase);
    }
}
