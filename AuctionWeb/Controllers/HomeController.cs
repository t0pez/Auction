using AuctionBLL.Dto;
using AuctionBLL.Interfaces;
using AuctionWeb.ViewModels.News;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

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

            var sortedItems = mappedItems.OrderByDescending(model => model.DateOfCreation);

            return View(sortedItems);
        }

        public ActionResult NewsCreate()
        {
            return View(new NewsCreateViewModel());
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
            catch (InvalidOperationException)
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
            catch (InvalidOperationException)
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
            catch (InvalidOperationException)
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
            catch (InvalidOperationException)
            {
                return View("Error");
            }
        }
        
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
