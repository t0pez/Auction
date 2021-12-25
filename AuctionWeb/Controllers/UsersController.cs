using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AuctionBLL.Dto;
using AuctionBLL.Services;
using AuctionWeb.ViewModels.Users;
using Microsoft.Owin.Security;

namespace AuctionWeb.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        public async Task<ActionResult> Index()
        {

            return View();
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
                    Id = Guid.NewGuid(),
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
                return Redirect("google.com"); // TODO: show message
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
            if (ModelState.IsValid)
            {
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

            return View();
        }

    }
}
