using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Resturent_Menu_System.Model
{
    public class Cart
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public string Options { get; set; }
    }

    public class ListOfItems
    {
        public List<Cart> Items { get; set; }
    }
}
