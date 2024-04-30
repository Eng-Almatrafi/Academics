using Academic.DataAccess;
using Academics.Models;
using Academics.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace Academics.Controllers
{
    public class SectionController : Controller
    {
        private readonly AppDbContext _context;
        public SectionController()
        {

            var context = new AppDbContext();
            _context = context;

        }
        public IActionResult Index(int currentPage=1)
        {
            IEnumerable<Section> sections = _context.Sections.Include(c=>c.Course)
                .Include(i=>i.Instructor).Include(s=>s.Schedule).ToList();

            var empData = new PaginatedList<Section>();

            int totalRecords = sections.Count();
            int pageSize = 10;
            int totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
            sections = sections.Skip((currentPage - 1) * pageSize).Take(pageSize);

            empData.Entity = sections;
            empData.CurrentPage = currentPage;
            empData.TotalPages = totalPages;
            empData.PageSize = pageSize;


            return View(empData);
          
        }

        public IActionResult Add()
        {
            var sectionVM = new SectionVM
            {
                Section = new Section(),
                Course=_context.Courses.ToList(),
                Instructors=_context.Instructors.ToList(),  
                Schedules=_context.Schedules.ToList(),  
            };
            return View(sectionVM);
        }

        [HttpPost]
        public IActionResult Add(SectionVM sectionVM)
        {
            if(sectionVM.Section != null)
            {
                _context.Add(sectionVM.Section);
                _context.SaveChanges();
                TempData["notify"] = "Section Added Successfully";

            }

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int Id)
        {
            if(Id != 0)
            {
                var section = _context.Sections.FirstOrDefault(x => x.Id == Id);
                var sectionVM = new SectionVM
                {
                    Section = section,
                    Course = _context.Courses.ToList(),
                    Instructors = _context.Instructors.ToList(),
                    Schedules = _context.Schedules.ToList(),
                };
                return View(sectionVM);
            }

            return NotFound();
        }
        [HttpPost]
        public IActionResult Edit(SectionVM obj)
        {
            if (obj.Section != null)
            {
                _context.Update(obj.Section);
                _context.SaveChanges();
                TempData["notify"] = "Section Updated Successfully";

            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int Id)
        {
            if (Id != 0)
            {
                var section = _context.Sections.Include(c => c.Course)
                .Include(i => i.Instructor).Include(s => s.Schedule).FirstOrDefault(y => y.Id == Id);

                return View(section);
            }
            else
            {
                return NotFound();
            }
        }


        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeletePost(int Id)
        {
            if(Id != 0)
            {
                var section = _context.Sections.FirstOrDefault(y => y.Id == Id);
                if (section != null)
                {
                    _context.Remove(section);
                    _context.SaveChanges();
                    TempData["notify"] = "Section Deleted Successfully";

                }

            }
            else
            {
                return NotFound();
            }

            return RedirectToAction("Index");
        }

        public IActionResult Details(int Id,int participatnId,string flag="")
        {
            // The flag is for determined if the request for registering participant or delete the participant 

            if (participatnId != 0)
            {
                if (flag == "register")
                {
                    var enrollment = new Enrollment
                    {
                        ParticipantId = participatnId,
                        SectionId = Id,
                    };
                    _context.Add(enrollment);
                    _context.SaveChanges();
                    TempData["notify"] = "Register Participant Successfully";
                }
                if (flag == "remove")
                {
                    var enrollment = _context.Enrollments.Where(s => s.SectionId == Id).ToList();

                    foreach (var item in enrollment)
                    {
                        if (item.ParticipantId == participatnId)
                        {
                            _context.Remove(item);
                            _context.SaveChanges();
                            TempData["notify"] = "Remove Participant Successfully";
                        }
                    }
                }
                
            }
            if(Id!=0)
            {
                var section = _context.Sections.Include(c => c.Course)
                    .Include(i=>i.Instructor).Include(s => s.Schedule)
                    .Include(p=>p.Participants)
                    .FirstOrDefault(x => x.Id == Id);
                return View(section);
            }
            else
            {
               return NotFound();
            }
        }
    }
}
