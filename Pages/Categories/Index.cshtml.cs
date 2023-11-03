using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Cretu_Ioana_Lab2.Data;
using Cretu_Ioana_Lab2.Models;
using Cretu_Ioana_Lab2.Models.ViewModels;
using System.Security.Policy;

namespace Cretu_Ioana_Lab2.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly Cretu_Ioana_Lab2.Data.Cretu_Ioana_Lab2Context _context;

        public IndexModel(Cretu_Ioana_Lab2.Data.Cretu_Ioana_Lab2Context context)
        {
            _context = context;
        }

        public IList<Category> Category { get;set; } = default!;

        public CategoryIndexData CategoryData { get; set; }
        public int CategoryID { get; set; }
        public int BookID { get; set; }

        public async Task OnGetAsync(int? id, int? bookID)
        {
            CategoryData = new CategoryIndexData();
            CategoryData.Categories = await _context.Category
            .Include(i => i.BookCategories) //ultima modificare, nu stiu daca e ok
                .ThenInclude(i => i.Book)
                    .ThenInclude(i => i.Author)
            .OrderBy(i => i.CategoryName)
            .ToListAsync();

            if (id != null)
            {
                CategoryID = id.Value;
                var category = CategoryData.Categories
                .SingleOrDefault(i => i.ID == id.Value);
                if(category != null)
                {
                    CategoryData.Books = category.BookCategories.Select(b => b.Book).ToList();
                }
                

            }
        }
    }
}
