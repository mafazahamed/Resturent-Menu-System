using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Resturent_Menu_System.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resturent_Menu_System.Controllers
{
    [ApiController]
    public class MenuController : ControllerBase
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(MenuController));
        private readonly Context _context;

        public MenuController(Context context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("menu/Category")]
        public JsonResult CreateCategory(Category category)
        {
            log.Info("In the CreateCategory");
            string message = "", status = "";

            _context.Category.Add(category);
            _context.SaveChanges();

            status = "success";
            message = "Category added successfully!";
            log.Info("In the CreateCategory : Category added successfully");

            return new JsonResult(new { status = status, message = message });
        }

        [HttpPost]
        [Route("menu/Category/{parentCategory}/")]
        public JsonResult CreateSubcategory(string parentCategory, Subcategory subcategory)
        {
            log.Info("In the CreateSubcategory");
            string message = "", status = "";

            var category = _context.Category.Where(x => x.Name == parentCategory).FirstOrDefault();
            if (category != null)
            {
                subcategory.CategoryId = category.Id;
                _context.Subcategory.Add(subcategory);
                _context.SaveChanges();

                status = "success";
                message = "Subcategory added successfully!";
                log.Info("In the CreateSubcategory : Subcategory added successfully");
            }
            else
            {
                status = "warning";
                message = "Sub Category not found!";
                log.Info("In the CreateSubcategory : Sub Category not found");
            }

            return new JsonResult(new { status = status, message = message });
        }

        [HttpPost]
        [Route("menu/item/")]
        public JsonResult Createitem(Item item)
        {
            log.Info("In the Createitem");
            string message = "", status = "";

            if (item.Type == "Category")
            {
                var category = _context.Category.Where(x => x.Name == item.TypeName).FirstOrDefault();

                if (category != null)
                {
                    item.TypeMappingId = category.Id;
                }
                else
                {
                    status = "warning";
                    message = "Category not found!";
                    log.Info("In the Createitem : Category not found");
                }
            }
            else if(item.Type == "Subcategory")
            {
                var subcategory = _context.Subcategory.Where(x => x.Name == item.TypeName).FirstOrDefault();

                if (subcategory != null)
                {
                    item.TypeMappingId = subcategory.Id;
                }
                else
                {
                    status = "warning";
                    message = "sub category not found!";
                    log.Info("In the Createitem : sub category not found");
                }
            }
            else
            {
                status = "warning";
                message = "Category not found!";
                log.Info("In the Createitem : type not found");
            }

            if(message == "")
            {
                _context.Item.Add(item);
                _context.SaveChanges();
                status = "success";
                message = "Item added successfully!";
                log.Info("In the Createitem : Item added successfully");
            }
            
            return new JsonResult(new { status = status, message = message });
        }

        [HttpGet]
        [Route("menu/item/{item}")]
        public JsonResult GetItemDetails(string item)
        {
            log.Info("In the GetItemDetails");
            string message = "", status = "";
            int id = 0;

            var containsInt = item.Any(char.IsDigit);
            if (containsInt) { id = Int32.Parse(item); }

            var data = _context.Item.Where(x=>x.Title == item || x.Id == id).ToList();

            if(data == null)
            {
                status = "warning";
                message = "No record found!";
                log.Info("In the GetItemDetails : No record found");
            }
            else
            {
                status = "success";
                message = "Item found!";
                log.Info("In the GetItemDetails : Item found");
            }

            return new JsonResult(new { status = status, message = message, data = data });
        }
    }
}
