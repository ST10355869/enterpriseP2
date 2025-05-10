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
    public class DetailsModel : PageModel
    {
        private readonly enterpriseP2.Data.AppDbContext _context;

        public DetailsModel(enterpriseP2.Data.AppDbContext context)
        {
            _context = context;
        }

        public ProductModel ProductModel { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productmodel = await _context.Product.FirstOrDefaultAsync(m => m.Id == id);
            if (productmodel == null)
            {
                return NotFound();
            }
            else
            {
                ProductModel = productmodel;
            }
            return Page();
        }
    }
}
