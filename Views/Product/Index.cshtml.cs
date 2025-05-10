using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using enterpriseP2.Data;
using enterpriseP2.Models;

namespace enterpriseP2.Views.Farmer
{
    public class IndexModel : PageModel
    {
        private readonly enterpriseP2.Data.AppDbContext _context;

        public IndexModel(enterpriseP2.Data.AppDbContext context)
        {
            _context = context;
        }

        public IList<ProductModel> ProductModel { get;set; } = default!;

        public async Task OnGetAsync()
        {
            ProductModel = await _context.Product.ToListAsync();
        }
    }
}
