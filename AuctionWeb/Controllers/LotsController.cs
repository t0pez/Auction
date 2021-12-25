using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AuctionBLL.Dto;
using AuctionBLL.Services;
using AuctionWeb.ViewModels.Lots;
using Microsoft.AspNet.Identity;

namespace AuctionWeb.Controllers
{
    public class LotsController : Controller
    {
        private readonly ILotsService _lotsService;

        public LotsController(ILotsService lotsService)
        {
            _lotsService = lotsService;
        }


        // GET: Lots
        public ActionResult Index()
        {
            return View();
        }

        // GET: Lots/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Lots/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Lots/Create
        [Authorize(Roles = "user, admin")]
        [HttpPost]
        public async Task<ActionResult> Create(CreateModel model)
        {
            try
            {

                var dto = new LotDto
                {
                    Id = Guid.NewGuid(),

                    Name = model.Name,
                    Description = model.Description,

                    StartPrice = model.StartPrice,
                    ActualPrice = model.StartPrice,
                    MinStepPrice = model.MinStep,

                    DateOfCreation = DateTime.Now,
                    StartDate = model.StartTime,
                    ProlongationTime = model.ProlongationTime,
                    TimeForStep = model.TimeForStep,

                    OwnerId = HttpContext.GetOwinContext().Authentication.User.Identity.GetUserId<string>()
                };

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
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
