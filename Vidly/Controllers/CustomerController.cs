using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity;

namespace Vidly.Controllers
{
    public class CustomerController : Controller
    {
        private ApplicationDbContext _context;

        public CustomerController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: NavBar
        public ActionResult Index()
        {
            return View();
        }

        [Route("customers/display")]
        public ViewResult Customers()
        {
            var customers = _context.Customers.Include(c=>c.MembershipType).ToList();

            return View(customers);
        }

        [Route("customers/details/{id}")]
        public ActionResult Details(int id)
        {

            var customers = _context.Customers.SingleOrDefault(c=>c.Id==id);

            if (customers == null)
                return HttpNotFound();

            var select = new List<Models.Customer>();

            return View(customers);
        }
      
        
    }
}