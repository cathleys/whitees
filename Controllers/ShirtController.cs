using Microsoft.AspNetCore.Mvc;
using Whitees.Extensions;
using Whitees.Helpers;
using Whitees.Interfaces;
using Whitees.Models;
using Whitees.ViewModels;

namespace Whitees.Controllers
{
    public class ShirtController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPhotoService _photoService;

        public ShirtController(IUnitOfWork uow,
        IHttpContextAccessor httpContextAccessor,
         IPhotoService photoService)
        {
            _uow = uow;
            _httpContextAccessor = httpContextAccessor;
            _photoService = photoService;
        }

        public async Task<IActionResult> Index(UserParams userParams)
        {

            var shirts = await _uow.ShirtRepository.GetPaginatedShirts(userParams);

            return View(shirts);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var shirt = await _uow.ShirtRepository.GetShirtById(id);
            if (shirt == null) return View("Error");

            return View(shirt);
        }

        public IActionResult Create()
        {
            var userId = _httpContextAccessor.HttpContext.User.GetUserId();

            var creatVM = new CreateShirtViewModel
            {
                AppUserId = userId
            };

            return View(creatVM);
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
                    Image = imageResult.SecureUrl.AbsoluteUri,
                    AppUserId = csVM.AppUserId
                };

                await _uow.ShirtRepository.Add(newShirt);

                return RedirectToAction("Index", "Dashboard");
            }
            ModelState.AddModelError("", "Failed to add new whitees");
            return View(csVM);

        }



        public async Task<IActionResult> Edit(int id)
        {
            var userId = _httpContextAccessor.HttpContext.User.GetUserId();
            var shirt = await _uow.ShirtRepository.GetShirtById(id);
            if (shirt == null) return View("Error");


            var shirtData = new EditShirtViewModel
            {
                Id = shirt.Id,
                Name = shirt.Name,
                Description = shirt.Description,
                Price = shirt.Price,
                ImageUrl = shirt.Image,
                AppUserId = userId,

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

            var shirt = await _uow.ShirtRepository.GetShirtByIdNoTracking(id);
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
                    Image = imageResult.SecureUrl.AbsoluteUri,
                    AppUserId = esVM.AppUserId
                };

                await _uow.ShirtRepository.Update(shirtData);
                return RedirectToAction("Index", "Dashboard");
            }

            return View(esVM);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var shirt = await _uow.ShirtRepository.GetShirtById(id);

            if (shirt == null) return NotFound();


            return View(shirt);

        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteShirt(int id)
        {
            var shirt = await _uow.ShirtRepository.GetShirtById(id);

            if (shirt == null) return NotFound();

            if (!string.IsNullOrEmpty(shirt.Image))
            {
                await _photoService.DeletePhotoAsync(shirt.Image);
            }

            await _uow.ShirtRepository.Delete(shirt);


            return RedirectToAction("Index", "Dashboard");

        }
    }

}