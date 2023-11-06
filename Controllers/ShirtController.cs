using Microsoft.AspNetCore.Mvc;
using Whitees.Interfaces;
using Whitees.Models;
using Whitees.ViewModels;

namespace Whitees.Controllers
{
    public class ShirtController : Controller
    {
        private readonly IShirtRepository _shirtRepository;

        public ShirtController(IShirtRepository shirtRepository)
        {
            _shirtRepository = shirtRepository;
        }

        public async Task<IActionResult> Index()
        {
            var shirts = await _shirtRepository.GetAllShirts();

            return View(shirts);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var shirt = await _shirtRepository.GetShirtById(id);
            if (shirt == null) return NotFound();

            return View(shirt);
        }

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateShirtViewModel csVM)
        {

            if (ModelState.IsValid)
            {
                var newShirt = new Shirt { Name = csVM.Name };

                await _shirtRepository.Add(newShirt);

                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Failed to add new whitees");
            return View(csVM);

        }



        public async Task<IActionResult> Edit(int id)
        {
            var shirt = await _shirtRepository.GetShirtById(id);
            if (shirt == null) return NotFound();


            var shirtData = new EditShirtViewModel
            {
                Id = shirt.Id,
                Name = shirt.Name
            };
            return View(shirtData);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditShirtViewModel editShirtVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit whitees");
                return View("Edit", editShirtVM);
            }

            var shirt = await _shirtRepository.GetShirtByIdNoTracking(id);
            if (shirt == null) return NotFound();


            var shirtData = new Shirt
            {
                Id = id,
                Name = editShirtVM.Name
            };

            await _shirtRepository.Update(shirtData);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            var shirt = await _shirtRepository.GetShirtById(id);

            if (shirt == null) return NotFound();


            return View(shirt);

        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteShirt(int id)
        {
            var shirt = await _shirtRepository.GetShirtById(id);

            if (shirt == null) return NotFound();

            await _shirtRepository.Delete(shirt);


            return RedirectToAction("Index");

        }
    }

}