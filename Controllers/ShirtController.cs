using Microsoft.AspNetCore.Mvc;
using Whitees.Interfaces;
using Whitees.Models;
using Whitees.ViewModels;

namespace Whitees.Controllers
{
    public class ShirtController : Controller
    {
        private readonly IShirtRepository _shirtRepository;
        private readonly IPhotoService _photoService;

        public ShirtController(IShirtRepository shirtRepository,
         IPhotoService photoService)
        {
            _shirtRepository = shirtRepository;
            _photoService = photoService;
        }

        public async Task<IActionResult> Index()
        {
            var shirts = await _shirtRepository.GetAllShirts();

            return View(shirts);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var shirt = await _shirtRepository.GetShirtById(id);
            if (shirt == null) return NotFound("Whitees not found");

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
                var imageResult = await _photoService.AddPhotoAsync(csVM.Image);

                var newShirt = new Shirt
                {
                    Name = csVM.Name,
                    Description = csVM.Description,
                    Price = csVM.Price,
                    Image = imageResult.SecureUrl.AbsoluteUri
                };

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
                Name = shirt.Name,
                Description = shirt.Description,
                Price = shirt.Price,
                ImageUrl = shirt.Image,

            };
            return View(shirtData);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditShirtViewModel esVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit whitees");
                return View("Edit", esVM);
            }

            var shirt = await _shirtRepository.GetShirtByIdNoTracking(id);
            if (shirt != null)
            {

                try
                {
                    var file = new FileInfo(shirt.Image);
                    var publicId = Path.GetFileNameWithoutExtension(file.Name);
                    await _photoService.DeletePhotoAsync(publicId);
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Failed to upload image");
                    return View(esVM);
                }

                var imageResult = await _photoService.AddPhotoAsync(esVM.Image);
                var shirtData = new Shirt
                {
                    Id = id,
                    Name = esVM.Name,
                    Description = esVM.Description,
                    Price = esVM.Price,
                    Image = imageResult.SecureUrl.AbsoluteUri
                };

                await _shirtRepository.Update(shirtData);
                return RedirectToAction("Index");
            }

            return View(esVM);
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

            if (!string.IsNullOrEmpty(shirt.Image))
            {
                await _photoService.DeletePhotoAsync(shirt.Image);
            }

            await _shirtRepository.Delete(shirt);


            return RedirectToAction("Index");

        }
    }

}