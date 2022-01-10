using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using AuctionBLL.Dto;
using AuctionBLL.Interfaces;
using AuctionBLL.Services;
using AuctionWeb.ViewModels.News;
using AutoMapper;

namespace AuctionWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly INewsService _newsService;
        private readonly IMapper _mapper;

        public HomeController(INewsService newsService, IMapper mapper)
        {
            _newsService = newsService;
            _mapper = mapper;
        }

        /// <summary>
        /// Process GET request for Main page
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Index()
        {
            var unmappedNews = await _newsService.GetAllNewsAsync();

            var mappedItems = _mapper.Map<IEnumerable<NewsListViewModel>>(unmappedNews);

            return View(mappedItems);
        }

        /// <summary>
        /// Process GET request for Creating news
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> NewsCreate()
        {
            return View();
        }

        /// <summary>
        /// Process POST request for Creation news
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> NewsCreate(NewsCreateViewModel model)
        {
            try
            {
                var mappedItem = _mapper.Map<NewsDto>(model);

                var created = await _newsService.CreateNewsAsync(mappedItem);

                return RedirectToAction("NewsDetails", new { created.Id });
            }
            catch (InvalidOperationException e)
            {
                ModelState.AddModelError("", e.Message);
                return View();
            }
        }

        /// <summary>
        /// Process GET request for News Details
        /// </summary>
        /// <param name="id">News id</param>
        /// <returns></returns>
        public async Task<ActionResult> NewsDetails(Guid id)
        {
            if (ModelState.IsValid == false)
                return RedirectToAction("Index");
            try
            {
                var unmappedItem = await _newsService.GetNewsByIdAsync(id);

                var mappedItem = _mapper.Map<NewsListViewModel>(unmappedItem);

                return View(mappedItem);
            }
            catch (InvalidOperationException)
            {
                return View("Error");
            }
        }
        
        /// <summary>
        /// Process GET request for News Edit
        /// </summary>
        /// <param name="id">News id</param>
        /// <returns></returns>
        public async Task<ActionResult> NewsEdit(Guid id)
        {
            try
            {
                var unmappedItem = await _newsService.GetNewsByIdAsync(id);

                var mappedItem = _mapper.Map<NewsEditViewModel>(unmappedItem);

                return View(mappedItem);
            }
            catch (InvalidOperationException)
            {
                return View("Error");
            }
        }

        /// <summary>
        /// Process POST request for News Edit
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> NewsEdit(NewsEditViewModel model)
        {
            try
            {
                var mappedItem = _mapper.Map<NewsDto>(model);

                await _newsService.UpdateNewsAsync(mappedItem);

                return RedirectToAction("NewsDetails", new {mappedItem.Id});
            }
            catch (InvalidOperationException)
            {
                return View("Error");
            }
        }
        
        /// <summary>
        /// Process POST request for News Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> NewsDelete(Guid id)
        {
            try
            {
                await _newsService.DeleteNewsAsync(id);
                
                return RedirectToAction("Index");
            }
            catch (InvalidOperationException)
            {
                return View("Error");
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}
