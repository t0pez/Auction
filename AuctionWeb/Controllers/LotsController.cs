using AuctionBLL.Dto;
using AuctionBLL.Services;
using AuctionWeb.ViewModels.Lots;
using AutoMapper;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AuctionWeb.Controllers
{
    public class LotsController : Controller
    {
        private readonly ILotsService _lotsService;
        private readonly IMapper _mapper;
        

        public LotsController(ILotsService lotsService, IMapper mapper)
        {
            _lotsService = lotsService;
            _mapper = mapper;
        }
        
        public async Task<ActionResult> Index()
        {
            var unmapped = await _lotsService.GetAllLotsAsync();

            var mapped = _mapper.Map<IEnumerable<LotListViewModel>>(unmapped).OrderBy(model => model.Status); 
            
            return View(mapped);
        }
        
        public async Task<ActionResult> Details(Guid id)
        {
            try
            {
                var unmapped = await _lotsService.GetLotByIdAsync(id);

                var mapped = _mapper.Map<LotDetailsViewModel>(unmapped);

                return View(mapped);
            }
            catch (Exception)
            {
                return HttpNotFound();
            }
        }

        [Authorize(Roles = "user, admin")]
        public ActionResult Create()
        {
            var currencies = Currency.List.Select(currency => new SelectListItem()
            {
                Value = currency.Value.ToString(), 
                Text = currency.IsoName
            }).ToList();

            var selectList = new SelectList(currencies, "Value", "Text");

            ViewBag.Currencies = selectList;
            return View();
        }

        [Authorize(Roles = "user, admin")]
        [HttpPost]
        public async Task<ActionResult> Create(LotCreateViewModel viewModel)
        {
            try
            {
                var dto = _mapper.Map<LotDto>(viewModel);
                dto.Owner = new UserDto()
                {
                    Id = HttpContext.GetOwinContext().Authentication.User.Identity.GetUserId()
                };

                await _lotsService.CreateLotAsync(dto);

                return RedirectToAction(nameof(Index));
            }
            catch(Exception e)
            {

                return View(nameof(Create), viewModel);
            }
        }

        [Authorize(Roles = "user, admin")]
        [HttpPost]
        public async Task<ActionResult> SetNewPrice(Guid lotId, decimal newPrice)
        {
            try
            {
                var userId = HttpContext.GetOwinContext().Authentication.User.Identity.GetUserId();

                await _lotsService.SetLotActualPriceAsync(lotId, userId, newPrice);

                return RedirectToAction(nameof(Details), new { id = lotId });
            }
            catch(Exception e)
            {
                return View("Error");
            }
        }
        
        [Authorize(Roles = "user, admin")]
        [HttpPost]
        public async Task<ActionResult> SetUserAsParticipant(Guid lotId)
        {
            try
            {
                var userId = HttpContext.GetOwinContext().Authentication.User.Identity.GetUserId();

                await _lotsService.AddParticipantAsync(lotId, userId);

                return RedirectToAction(nameof(Details), new { id = lotId });
            }
            catch(Exception e)
            {
                return View("Error");
            }
        }

        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
    }
}
