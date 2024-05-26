using Microsoft.AspNetCore.Mvc;
using StudentManagementMVC.Data;
using StudentManagementMVC.Models;
using StudentManagementMVC.Models.Entities;

namespace StudentManagementMVC.Controllers
{
    public class StudentsController : Controller
    {
        private readonly AppDbContext _context;

        public StudentsController(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Add(string? id)
        {
            //string? qID = Request.Query["id"];
            TempData["status"] = id is null ? true : false;

            if (id is not null)
            {
                Student? stu = new Student();
                if (Guid.TryParse(id, out Guid gID))
                {
                    stu = _context.Students.Where(x => x.ID == gID).FirstOrDefault();
                }
                AddStudentViewModel vm = new AddStudentViewModel()
                {
                    ID = stu.ID,
                    Name = stu.Name,
                    Phone = stu.Phone,
                    Email = stu.Email,
                    Subed = stu.Subed
                };
                return View("Add", vm);
            }
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddStudentViewModel reqModel)
        {
            if (ModelState.IsValid)
            {
                if (reqModel.ID == Guid.Empty || reqModel.ID is null)
                {
                    Student stu = new Student()
                    {
                        ID = Guid.NewGuid(),
                        Name = reqModel.Name,
                        Phone = reqModel.Phone,
                        Email = reqModel.Email,
                        Subed = reqModel.Subed,
                    };
                    _context.Students.Add(stu);
                    _context.SaveChanges();
                }
                else
                {
                    Student? stu = _context.Students.Where(x => x.ID == reqModel.ID).FirstOrDefault();
                    if(stu is not null)
                    {
                        stu.Name = reqModel.Name;
                        stu.Phone = reqModel.Phone;
                        stu.Email = reqModel.Email;
                        stu.Subed = reqModel.Subed;
                    };
                    _context.SaveChanges();
                    return RedirectToAction("AllStudents");
                }
            }
            TempData["status"] = true;
            ModelState.Clear();
            return View();
        }

        [HttpGet]
        public IActionResult AllStudents()
        {
            var students = _context.Students.ToList();
            return View(students);
        }

        [HttpGet("{id}")]
        public IActionResult Delete(Guid id)
        {
            Student? stu = _context.Students.Where(x => x.ID == id).FirstOrDefault();
            if (stu is null)
            {
                return View("Error");
            }
            _context.Students.Remove(stu);
            _context.SaveChanges();
            return RedirectToAction("AllStudents");
        }

        [HttpGet("Edit/{id}")]
        public IActionResult Edit(Guid id)
        {
            Student? stu = _context.Students.Where(x => x.ID == id).FirstOrDefault();
            AddStudentViewModel vm = new AddStudentViewModel()
            {
                Name = stu.Name,
                Phone = stu.Phone,
                Email = stu.Email,
                Subed = stu.Subed
            };
            return View("Add", vm);
        }
    }
}
