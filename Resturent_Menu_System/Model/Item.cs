using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Resturent_Menu_System.Model
{
    public class Item
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string Type { get; set; }
        public int TypeMappingId { get; set; }
        [NotMapped]
        public string TypeName { get; set; }
    }
}
