using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Resturent_Menu_System.Model
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Subcategory> ListSubcategories{ get; set; }
    }
}
