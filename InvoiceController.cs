using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BuggyApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetInvoice()
        {
            var items = new List<Item>
            {
                new Item { name = "Widget A", price = 19.99 },
                new Item { name = "Widget B", price = 29.99 }
            };

            return Ok(new { items });
        }

        public class Item
        {
            public string name { get; set; }
            public double price { get; set; }
        }
    }
}