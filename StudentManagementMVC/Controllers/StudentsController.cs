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
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddStudentViewModel reqModel)
        {
            if(ModelState.IsValid)
            {
                Student stu = new Student()
                {
                    ID = Guid.NewGuid(),
                    Name = reqModel.Name,
                    Email = reqModel.Email,
                    Subed = reqModel.Subed,
                };
                _context.Students.Add(stu);
            }
            return View();
        }
    }
}
