using Microsoft.AspNetCore.Mvc;

using System.Diagnostics;
using ToDoList.Models;
using ToDoList.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ToDoList.Controllers
{
    public class MyListsController : Controller

    {

        private readonly ApplicationDBContext _db;

        public MyListsController(ApplicationDBContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Lists()
        {
            var query = _db.ToDoCollection.Include(toDoCollection => toDoCollection.ToDoList);
            var toDoCollections = await query.ToListAsync();
            return View(toDoCollections);
        }


        //GET
        

        [HttpPost]
        public IActionResult CreateList(ToDoCollection obj)
        {
            //DETERMINES IF THE INPUT IS CORRECT
            if (ModelState.IsValid)
            {
                _db.ToDoCollection.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Lists");
            }
            return View(obj);
        }

        [HttpPost]
        public IActionResult DeleteList(int? id)
        {
            //DETERMINES IF THE INPUT IS CORRECT
            var obj = _db.ToDoCollection.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            {
                _db.ToDoCollection.Remove(obj);
                _db.SaveChanges();
                return RedirectToAction("Lists");
            }
            

        }



    }
}
