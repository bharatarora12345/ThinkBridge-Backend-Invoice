using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BuggyApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceController : ControllerBase
    {
        private readonly AppDbContext _context;

        public InvoiceController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/invoice/1
        [HttpGet("{invoiceId}")]
        public IActionResult GetInvoice(int invoiceId)
        {
            var invoice = _context.Invoices
                .Include(i => i.Items)
                .FirstOrDefault(i => i.InvoiceID == invoiceId);

            if (invoice == null)
                return NotFound(new { message = "Invoice not found" });

            // Convert response into your required JSON format
            var response = new
            {
                invoiceId = invoice.InvoiceID,
                customerName = invoice.CustomerName,
                items = invoice.Items.Select(item => new
                {
                    name = item.Name,
                    price = item.Price
                }).ToList()
            };

            return Ok(response);
        }
    }


    public class Invoice
    {
        public int InvoiceID { get; set; }
        public string CustomerName { get; set; }

        public List<InvoiceItem> Items { get; set; }
    }

    public class InvoiceItem
    {
        public int ItemID { get; set; }
        public int InvoiceID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public Invoice Invoice { get; set; }
    }


    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceItem> InvoiceItems { get; set; }
    }
}
