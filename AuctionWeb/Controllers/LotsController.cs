﻿using AuctionBLL.Dto;
using AuctionBLL.Services;
using AuctionWeb.ViewModels.Lots;
using AutoMapper;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;

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
        public async Task<ActionResult> Create()
        {
            var userId = User.Identity.GetUserId();
            var usersService = HttpContext.GetOwinContext().GetUserManager<IUsersService>();

            var user = await usersService.GetByUserIdAsync(userId);

            var userCurrencies = user.Wallet.Money.Select(money => money.Currency);
            var currencies = userCurrencies.Select(currency => new SelectListItem()
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

                var userId = HttpContext.GetOwinContext().Authentication.User.Identity.GetUserId();

                var created = await _lotsService.CreateLotAsync(dto, userId);

                return RedirectToAction(nameof(Details), new {id = created.Id});
            }
            catch(Exception e)
            {
                ModelState.AddModelError("", e.Message);

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

                return RedirectToAction(nameof(Details), new {id = lotId});
            }
            catch (ValidationException exception)
            {
                ModelState.AddModelError("", exception.Message);

                return RedirectToAction(nameof(Details), new { id = lotId });
            }
            catch(Exception e)
            {


                return View("Error");
            }
        }

    }
}
