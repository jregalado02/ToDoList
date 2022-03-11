using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.Data;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class ToDoListCollectionController : Controller
    {
        /// <summary>
        /// Database Context
        /// </summary>
        private readonly ApplicationDBContext _dbContext;

        public ToDoListCollectionController(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateToDoListCollection()
        {
            return View();
        }

        /// <summary>
        /// Create a new To Do Collection
        /// </summary>
        /// <param name="toDoCollection">To Do Collection being created</param>
        /// <returns>Creation Result</returns>
        [HttpPost]
        public async Task<IActionResult> CreateToDoListCollection(ToDoCollection toDoCollection)
        {
            if (ModelState.IsValid)
            {
                var newModel = _dbContext.AddAsync(toDoCollection);
                return Ok(newModel);
            }

            return BadRequest(ModelState);
        }

        /// <summary>
        /// Get a To Do List Collection by id.
        /// </summary>
        /// <param name="id">ID of the To Do List Collection</param>
        /// <returns>Matching To Do Collection</returns>
        [HttpGet]
        public async Task<IActionResult> GetToDoListCollection(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var toDoCollection = await _dbContext.ToDoCollection.FirstOrDefaultAsync(c => c.Id == id);

            return Ok(toDoCollection);
        }

        /// <summary>
        /// Update a specific To Do List Collection
        /// </summary>
        /// <param name="toDoCollection">Collection to update</param>
        /// <returns>Updated Collection</returns>
        [HttpPatch]
        public IActionResult UpdateToDoListCollection(ToDoCollection toDoCollection)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedCollection = _dbContext.ToDoCollection.Update(toDoCollection);
            return Ok(updatedCollection);
        }

        /// <summary>
        /// Delete the todolist collection.
        /// </summary>
        /// <param name="toDoCollection">Collection to be deleted</param>
        /// <returns>Delete Result</returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteToDoListCollection(ToDoCollection toDoCollection)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var exists = (await _dbContext.ToDoCollection.FirstOrDefaultAsync(c => c.Id == toDoCollection.Id)) is not null;
            if (!exists)
                return BadRequest("Provided Model was not found and could not be deleted");

            _dbContext.ToDoCollection.Remove(toDoCollection);

            return Ok();
        }
    }
}
