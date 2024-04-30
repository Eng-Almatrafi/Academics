using Academic.DataAccess;
using Academics.Models;
using Microsoft.AspNetCore.Mvc;

namespace Academics.Controllers
{
    public class OfficeController : Controller
    {
        private readonly AppDbContext _context;
        public OfficeController()
        {

            var context = new AppDbContext();
            _context = context;

        }
        public IActionResult Index(int currentPage=1)
        {
            IEnumerable<Office> office= _context.Offices.ToList();


            var empData = new PaginatedList<Office>();

            int totalRecords = office.Count();
            int pageSize = 10;
            int totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
            office = office.Skip((currentPage - 1) * pageSize).Take(pageSize);

            empData.Entity = office;
            empData.CurrentPage = currentPage;
            empData.TotalPages = totalPages;
            empData.PageSize = pageSize;
            return View(empData);
        }

        public IActionResult Add()
        {

            return View(new Office());
        }

        [HttpPost]
        public IActionResult Add(Office ojb)
        {
            _context.Add(ojb);
            _context.SaveChanges();
            TempData["notify"] = "Office Added Successfully";

            return RedirectToAction("Index", "Office");
        }

        public IActionResult Edit(int Id)
        {
            var Office = _context.Offices.FirstOrDefault(x => x.Id == Id);

            return View(Office);


        }

        [HttpPost]
        public IActionResult Edit(Office obj)
        {
            _context.Update(obj);
            _context.SaveChanges();
            TempData["notify"] = "Office Updated Successfully";

            return RedirectToAction("index");
        }


        public IActionResult Delete(int Id)
        {
            var Office = _context.Offices.Single(x => x.Id == Id);

            return View(Office);


        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeletePost(int Id)
        {
            var Offices = _context.Offices.Single(x => x.Id == Id);
            _context.Remove(Offices);
            _context.SaveChanges();
            TempData["notify"] = "Office Deleted Successfully";

            return RedirectToAction("index");
        }
    }
}
