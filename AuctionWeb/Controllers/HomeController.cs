using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using AuctionBLL.Dto;
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

        public async Task<ActionResult> Index()
        {
            var unmappedNews = await _newsService.GetAllNewsAsync();

            var mappedItems = _mapper.Map<IEnumerable<NewsListViewModel>>(unmappedNews);

            return View(mappedItems);
        }

        public async Task<ActionResult> NewsCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> NewsCreate(NewsCreateViewModel model)
        {
            try
            {
                var mappedItem = _mapper.Map<NewsDto>(model);

                var created = await _newsService.CreateNewsAsync(mappedItem);

                return RedirectToAction("NewsDetails", new { created.Id });
            }
            catch (Exception e)
            {
                return View("Error");
            }
        }

        public async Task<ActionResult> NewsDetails(Guid id)
        {
            try
            {
                var unmappedItem = await _newsService.GetNewsByIdAsync(id);

                var mappedItem = _mapper.Map<NewsListViewModel>(unmappedItem);

                return View(mappedItem);
            }
            catch (Exception e)
            {
                return View("Error");
            }
        }
        
        public async Task<ActionResult> NewsEdit(Guid id)
        {
            try
            {
                var unmappedItem = await _newsService.GetNewsByIdAsync(id);

                var mappedItem = _mapper.Map<NewsEditViewModel>(unmappedItem);

                return View(mappedItem);
            }
            catch (Exception e)
            {
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<ActionResult> NewsEdit(NewsEditViewModel model)
        {
            try
            {
                var mappedItem = _mapper.Map<NewsDto>(model);

                await _newsService.UpdateNewsAsync(mappedItem);

                return RedirectToAction("NewsDetails", new {mappedItem.Id});
            }
            catch (Exception e)
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
