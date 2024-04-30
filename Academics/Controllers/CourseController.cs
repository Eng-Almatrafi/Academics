using Academic.DataAccess;
using Academics.Models;
using Microsoft.AspNetCore.Mvc;

namespace Academics.Controllers
{
    public class CourseController : Controller
    {
        
        private readonly AppDbContext _context;
        public CourseController()
        {
            
            var context = new AppDbContext();
            _context = context;

        }
        public IActionResult Index(int currentPage=1)
        {
            IEnumerable<Course> course =  _context.Courses.ToList();

            var empData = new PaginatedList<Course>();

            int totalRecords = course.Count();
            int pageSize = 10;
            int totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
            course = course.Skip((currentPage - 1) * pageSize).Take(pageSize);

            empData.Entity = course;
            empData.CurrentPage = currentPage;
            empData.TotalPages = totalPages;
            empData.PageSize = pageSize;
           

            return View(empData);
       
        }

        public IActionResult AddCourse()
        {

            return View(new Course());
        }

        [HttpPost]
        public IActionResult AddCourse(Course ojb)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ojb);
                _context.SaveChanges();
                TempData["notify"] = "Course Added Successfully";
                return RedirectToAction("Index", "Course");

            }
            return View(ojb);
        }

        public IActionResult Edit(int Id)
        {
            var course = _context.Courses.FirstOrDefault(x=>x.Id == Id);

            return View(course);
        }

        [HttpPost]
        public IActionResult Edit(Course obj)
        {
            _context.Update(obj);
            _context.SaveChanges();
            TempData["notify"] = "Course Updated Successfully";
            return RedirectToAction("index");
        }


        public IActionResult Delete(int Id)
        {
            var course = _context.Courses.Single(x=>x.Id==Id);

            return View(course);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeletePost(int Id)
        {
            var course= _context.Courses.Single(x => x.Id == Id);
            _context.Remove(course);
            _context.SaveChanges();
            TempData["notify"] = "Course Delted Successfully";

            return RedirectToAction("index");
        }
    }
}
