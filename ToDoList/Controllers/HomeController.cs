using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ToDoList.Models;
using ToDoList.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ToDoList.Controllers
{
    /*HOW DOES APPLICATIONDBCONTEXT WORK?
      LOOK UP DEPENDENCY INJECTION
    */
    public class HomeController : Controller
    {
        private readonly ApplicationDBContext _db;

        public HomeController(ApplicationDBContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            //.FirstorDefault searches db and looks for the first ToDoCollection.
            var objToDoList = await _db.ToDoCollection.Include(toDoCollection => toDoCollection.ToDoList).FirstOrDefaultAsync();
            return View(objToDoList);
        }

        

        //GET
        public IActionResult CreateTask()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateTask(ToDo obj)
        {
            //DETERMINES IF THE INPUT IS CORRECT
            if (ModelState.IsValid)
            {
                _db.ToDos.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

       public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            //_db.ToDos.Find(id) searches through the DB to find the corresponing id.
            var toDoFromDB = _db.ToDos.Find(id);

            if(toDoFromDB == null)
            {
                return NotFound();
            }
            return View(toDoFromDB);
        }

        [HttpPost]
        public IActionResult Edit(ToDo obj)
        {
            //DETERMINES IF THE INPUT IS CORRECT
            if (ModelState.IsValid)
            {
                _db.ToDos.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public async Task<IActionResult> ChangeName(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //_db.ToDos.Find(id) searches through the DB to find the corresponing id.
            var nameFromDB = await _db.ToDoCollection.Include(c => c.ToDoList).FirstAsync(c => c.Id == id);

            if (nameFromDB == null)
            {
                return NotFound();
            }
            return View(nameFromDB);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeName(ToDoCollection obj)
        {
            //DETERMINES IF THE INPUT IS CORRECT
            if (ModelState.IsValid)
            {
                _db.ToDoCollection.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(obj);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //_db.ToDos.Find(id) searches through the DB to find the corresponing id.
            var toDoFromDB = _db.ToDos.Find(id);

            if (toDoFromDB == null)
            {
                return NotFound();
            }
            return View(toDoFromDB);
        }

        [HttpPost]
        public IActionResult DeletePost(int? id)
        {
            //DETERMINES IF THE INPUT IS CORRECT
            var obj = _db.ToDos.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            {
                _db.ToDos.Remove(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            
        }

        


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}