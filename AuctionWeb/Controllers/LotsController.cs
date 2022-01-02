using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AuctionBLL.Dto;
using AuctionBLL.Services;
using AuctionWeb.ViewModels.Lots;
using AutoMapper;
using Microsoft.AspNet.Identity;

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


        // GET: Lots
        public async Task<ActionResult> Index()
        {
            var unmapped = await _lotsService.GetAllLotsAsync();

            var mapped = _mapper.Map<IEnumerable<ListModel>>(unmapped); // TODO: change view to new view model
            
            return View(mapped);
        }

        // GET: Lots/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            try
            {
                var unmapped = await _lotsService.GetLotByIdAsync(id);

                var mapped = _mapper.Map<DetailsModel>(unmapped);

                return View(mapped);
            }
            catch (Exception)
            {
                return HttpNotFound();
            }
        }

        // GET: Lots/Create
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

        // POST: Lots/Create
        [Authorize(Roles = "user, admin")]
        [HttpPost]
        public async Task<ActionResult> Create(CreateModel model)
        {
            try
            {
                var dto = _mapper.Map<LotDto>(model);
                dto.Owner = new UserDto()
                {
                    Id = HttpContext.GetOwinContext().Authentication.User.Identity.GetUserId<string>()
                };

                await _lotsService.CreateLotAsync(dto);

                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                return View("Error");
            }
        }

        // GET: Lots/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Lots/Edit/5
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

        // GET: Lots/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Lots/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
