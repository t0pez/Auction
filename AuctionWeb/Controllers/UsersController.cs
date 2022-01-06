using AuctionBLL.Dto;
using AuctionBLL.Services;
using AuctionWeb.ViewModels.Users;
using AutoMapper;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AuctionWeb.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersService _usersService;
        private readonly IMapper _mapper;
        

        public UsersController(IUsersService usersService, IMapper mapper)
        {
            _usersService = usersService;
            _mapper = mapper;
        }

        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Index()
        {
            var unmapped = await _usersService.GetAllAsync();

            var mapped = _mapper.Map<IEnumerable<UserListModel>>(unmapped);

            return View(mapped);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(RegisterModel registerModel)
        {
            try
            {
                var userDto = new UserDto // TODO: Mapper
                {
                    Id = Guid.NewGuid().ToString(),
                    Password = registerModel.Password,
                    UserName = registerModel.UserName,
                    FirstName = registerModel.FirstName,
                    LastName = registerModel.LastName,
                    Role = "user",
                    Wallet = new WalletDto {Id = Guid.NewGuid()},
                    OwnedLots = new List<LotDto>(),
                    AsParticipant = new List<LotDto>(),
                };

                await _usersService.CreateAsync(userDto);
                
                return View();
            }
            catch (InvalidOperationException)
            {
                return HttpNotFound(); // TODO: show message
            }
        }

        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LogIn(LoginInfoModel loginInfo)
        {
            if (ModelState.IsValid == false) 
                return View();

            var userDto = new UserDto
            {
                UserName = loginInfo.Login,
                Password = loginInfo.Password
            };

            var claim = await _usersService.LoginAsync(userDto);

            if (claim is null)
            {
                ModelState.AddModelError("", "Wrong login or password");
                return View(loginInfo);
            }

            var authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut();
            authManager.SignIn(new AuthenticationProperties
            {
                IsPersistent = true
            }, claim);
                
            return RedirectToAction("Index", "Home");
        }
        
        [HttpGet]
        public ActionResult LogOut()
        {
            var authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut();
            
            return RedirectToAction("Index", "Home");
        }

    }
}
