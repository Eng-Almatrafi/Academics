using Academic.DataAccess;
using Academics.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Academics.Controllers
{
    public class InstructorController : Controller
    {


        private readonly AppDbContext _context;
        public InstructorController()
        {

            var context = new AppDbContext();
            _context = context;

        }

        public IActionResult Index(string term="", int currentPage = 1)
        {
            term = string.IsNullOrEmpty(term) ? "" : term.ToLower();
            var ListInstructors = _context.Instructors.Include(x => x.Office).ToList();
            var empData = new PaginatedList<Instructor>();

            var result = (from instructor in ListInstructors
                          where term == "" || instructor.FName.ToLower().StartsWith(term)
                          select new Instructor
                          {
                              FName = instructor.FName,
                              LName = instructor.LName,
                              Id = instructor.Id,
                              OfficeId = instructor.OfficeId,
                              Office = instructor.Office,
                          });



            int totalRecords = result.Count();
            int pageSize = 10;
            int totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
            result = result.Skip((currentPage - 1) * pageSize).Take(pageSize);

            empData.Entity = result;
            empData.CurrentPage = currentPage;
            empData.TotalPages = totalPages;
            empData.PageSize = pageSize;
            empData.Term=term;


            return View(empData);
        }

        public IActionResult AddInstructor()
        {
            List<Office> officesList = _context.Offices.Where(x => x.OfficeEnabled == true).ToList();
            ViewBag.data = officesList;
            return View();
        }
        [HttpPost]
        public IActionResult AddInstructor(Instructor obj)
        {
            if (ModelState.IsValid)
            {
                _context.Add(obj);
                _context.SaveChanges();

                // Update Office status 
                var OfficeId = obj.OfficeId;
                var GetOffice = _context.Offices.FirstOrDefault(x => x.Id == OfficeId);
                GetOffice.OfficeEnabled = false;
                _context.Update(GetOffice);
                _context.SaveChanges();
                TempData["notify"] = "Instructor Added Successfully";

                return RedirectToAction("index");
            }
            else
            {
                List<Office> officesList = _context.Offices.Where(x => x.OfficeEnabled == true).ToList();
                ViewBag.data = officesList;
            }

            return View();
            
        }

        public IActionResult Edit(int Id)
        {
            var Instructor = _context.Instructors.FirstOrDefault(x => x.Id == Id);

            return View(Instructor);

        }

        [HttpPost]
        public IActionResult Edit(Instructor obj)
        {
            _context.Update(obj);
            _context.SaveChanges();
            TempData["notify"] = "Instructor Update Successfully";

            return RedirectToAction("index");
        }


        public IActionResult Delete(int Id)
        {
            var Instructor = _context.Instructors.FirstOrDefault(x => x.Id == Id);

            return View(Instructor);

        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeletePost(int Id)
        {
            var Instructor = _context.Instructors.Single(x => x.Id == Id);
            var OfficeId = Instructor.OfficeId;
            _context.Remove(Instructor);
            _context.SaveChanges();

            // Update Office status 
            
            var GetOffice = _context.Offices.Single(x => x.Id == OfficeId);
            GetOffice.OfficeEnabled = true;
            _context.Update(GetOffice);
            _context.SaveChanges();
            TempData["notify"] = "Instructor Delete Successfully";

            return RedirectToAction("index");
        }


    }
}
