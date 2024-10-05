using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {

        // from here to..
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;

        }
        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _db.Categories.ToList();        // we are fetching data from database and converting it into a list
            return View(objCategoryList);
        }
        //here we are fetching data from data base and displaying on category index page

        //GET

        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]                              // declaring an http request will be sent as a POST request
        [ValidateAntiForgeryToken]              //this is basically a CSRF token
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The display order cannot exactly match the name.");
            }
            if (ModelState.IsValid)
            {

                _db.Categories.Add(obj);       //dbcontext adds object into database. Basically in obj we have data filled from form
                _db.SaveChanges();      // this is basically logic behind the Save button when we submit the form
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }



        //EDIT
        public IActionResult Edit(int? id)      //here int? means that any data comes can be a null
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.Categories.Find(id);
            //var categoryFromDbFirst = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        //POST
        [HttpPost]                              // declaring an http request will be sent as a POST request
        [ValidateAntiForgeryToken]              //this is basically a CSRF token
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The display order cannot exactly match the name.");
            }
            if (ModelState.IsValid)
            {

                _db.Categories.Update(obj);       //just the change here from POST method is Update function
                _db.SaveChanges();      // this is basically logic behind the Save button when we submit the form
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //DELETE
        public IActionResult Delete(int? id)      //here int? means that any data comes can be a null
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.Categories.Find(id);
            //var categoryFromDbFirst = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        //POST
        [HttpPost]                              // declaring an http request will be sent as a POST request
        [ValidateAntiForgeryToken]              //this is basically a CSRF token
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.Categories.Find(id);

            if (obj == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(obj);       //just the change here from POST method is Update function
            _db.SaveChanges();      // this is basically logic behind the Save button when we submit the form
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
