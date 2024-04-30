using Academic.DataAccess;
using Academics.Models;
using Academics.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Academics.Controllers
{
    public class ParticipantController : Controller
    {
        private readonly AppDbContext _context;
        public ParticipantController()
        {

            var context = new AppDbContext();
            _context = context;

        }

        public IActionResult Index(int currentPage=1)
        {
            IEnumerable<Participant> Participant = _context.Particpants.ToList();

            var empData = new PaginatedList<Participant>();

            int totalRecords = Participant.Count();
            int pageSize = 100;
            int totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
            Participant = Participant.Skip((currentPage - 1) * pageSize).Take(pageSize);
          
            empData.Entity = Participant;
            empData.CurrentPage = currentPage;
            empData.TotalPages = totalPages;
            empData.PageSize = pageSize;

            

            return View(empData);
          
        }
        
        public IActionResult Add()
        {
            return View(new ParticipantsVM());
        }

        [HttpPost]
        public IActionResult Add(ParticipantsVM obj)
        {
            if (obj.Individual.Id != 0)
            {
                _context.Individuals.Add(obj.Individual);
            }
            else if (obj.Corporate.Id != 0)
            {
                _context.Corporates.Add(obj.Corporate);
            }
            
            _context.SaveChanges();
            TempData["notify"] = "Participant Added Successfully";

            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            
            var enrollment = _context.Enrollments.Where(x => x.ParticipantId == id);

            var individual = _context.Individuals.Include(x=>x.Sections).ThenInclude(x=>x.Schedule).FirstOrDefault(x => x.Id == id);

            var corporate = _context.Corporates.Include(x => x.Sections).ThenInclude(x => x.Schedule).FirstOrDefault(x => x.Id == id);

            var section =_context.Sections.ToList();

            var participantVM = new ParticipantsVM
            {
                Individual = individual,
                Corporate = corporate,
                Sections = section,
                Enrollment = new Enrollment()
      
            };



            return View(participantVM);
        }
        [HttpPost]
        [ActionName("Details")]
        public IActionResult Enrollment(ParticipantsVM obj)
        {
            if(obj != null)
            {
                _context.Enrollments.Add(obj.Enrollment);
                _context.SaveChanges();
                TempData["notify"] = "Registering Participant Successfully";
            }
            int ParticipantId = obj.Enrollment.ParticipantId;

            var individual = _context.Individuals.Include(x => x.Sections).ThenInclude(x => x.Schedule).FirstOrDefault(x => x.Id == ParticipantId);

            var corporate = _context.Corporates.Include(x => x.Sections).ThenInclude(x => x.Schedule).FirstOrDefault(x => x.Id == ParticipantId);

            var section = _context.Sections.ToList();

            var participantVM = new ParticipantsVM
            {
                Individual = individual,
                Corporate = corporate,
                Sections = section,

            };


            return View(participantVM);
        }


        public IActionResult Edit(int Id)
        {

            var Participates = new ParticipantsVM
            {
                Individual = _context.Individuals.FirstOrDefault(x => x.Id == Id),
                Corporate = _context.Corporates.FirstOrDefault(x => x.Id == Id)
            };


            return View(Participates);
        }

        [HttpPost]
        public IActionResult Edit(ParticipantsVM obj)
        {
            if(obj.Individual != null)
            {
                _context.Update(obj.Individual);
            }
            else if (obj.Corporate != null) 
            {
                _context.Update(obj.Corporate);
            }
           
            _context.SaveChanges();
            TempData["notify"] = "Participant Updated Successfully";

            return RedirectToAction("index");
        }

        public IActionResult Delete(int Id)
        {

            var Participates = new ParticipantsVM
            {
                Individual = _context.Individuals.FirstOrDefault(x => x.Id == Id),
                Corporate = _context.Corporates.FirstOrDefault(x => x.Id == Id)
            };


            return View(Participates);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeletePost(int Id)
        {
            var Individual = _context.Individuals.FirstOrDefault(x => x.Id == Id);
            var Corporate = _context.Corporates.FirstOrDefault(x => x.Id == Id);

            if (Individual != null)
            {
                _context.Remove(Individual);
            }
            else if (Corporate != null)
            {
                _context.Remove(Corporate);
            }

            _context.SaveChanges();
            TempData["notify"] = "Participant Deleted Successfully";

            return RedirectToAction("index");
        }
    }
}
