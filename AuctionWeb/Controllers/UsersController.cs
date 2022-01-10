using AuctionBLL.Dto;
using AuctionBLL.Services;
using AuctionWeb.ViewModels.Money;
using AuctionWeb.ViewModels.Users;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AuctionBLL.Interfaces;

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
        
        public async Task<ActionResult> Details(string userId)
        {
            try
            {
                var unmapped = await _usersService.GetByUserIdAsync(userId);
                var mapped = _mapper.Map<UserDetailsViewModel>(unmapped);

                var userService = HttpContext.GetOwinContext().GetUserManager<IUsersService>();
                var userRoles = await userService.GetUserRolesAsync(userId);
                var role = String.Join(" ", userRoles);

                mapped.Role = role;

                return View(mapped);
            }
            catch (InvalidOperationException e)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(UserCreateViewModel userCreateViewModel)
        {
            try
            {
                var mapped = _mapper.Map<UserDto>(userCreateViewModel);

                mapped.Role = "user";

                await _usersService.CreateAsync(mapped);

                return RedirectToAction("Login", "Users");
            }
            catch (InvalidOperationException e)
            {
                ModelState.AddModelError("", e.Message);
                return View();
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult> AddUserToAdminRole(string userId)
        {
            try
            {
                await _usersService.AddRoleAsync(userId, "admin");

                return RedirectToAction("Details", "Users", new { userId });
            }
            catch (InvalidOperationException e)
            {
                return RedirectToAction("Index");
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult> RemoveUserFromAdminRole(string userId)
        {
            await _usersService.RemoveRoleAsync(userId, "admin");

            return RedirectToAction("Details", "Users", new {userId});
        }

        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LogIn(LoginInfoModel loginInfo)
        {
            try
            {
                var userDto = _mapper.Map<UserDto>(loginInfo);

                var claim = await _usersService.LoginAsync(userDto);

                var authManager = HttpContext.GetOwinContext().Authentication;
                authManager.SignOut();
                authManager.SignIn(new AuthenticationProperties
                {
                    IsPersistent = true
                }, claim);

                return RedirectToAction("Index", "Home");
            }
            catch (InvalidOperationException e)
            {
                ModelState.AddModelError("", e.Message);
                return View(loginInfo);
            }
        }

        [HttpGet]
        public ActionResult LogOut()
        {
            var authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut();

            return RedirectToAction("Index", "Home");
        }


        public async Task<ActionResult> CreateUserWallet()
        {
            var userId = User.Identity.GetUserId();
            var user = await _usersService.GetByUserIdAsync(userId);

            var userCurrencies = user.Wallet.Money.Select(money => money.Currency);
            var availableCurrencies = Currency.List.Except(userCurrencies);

            var currencies = availableCurrencies.Select(currency => new SelectListItem()
            {
                Value = currency.Value.ToString(),
                Text = currency.IsoName
            }).ToList();

            var selectList = new SelectList(currencies, "Value", "Text");

            ViewBag.Currencies = selectList;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateUserWallet(string userId, MoneyCreateViewModel money)
        {
            
            var mapped = _mapper.Map<MoneyDto>(money);

            await _usersService.CreateUserMoneyAsync(userId, mapped);

            return RedirectToAction("Details", new {userId = userId});
        }

        public async Task<ActionResult> TopUpUserBalance(Guid moneyId)
        {
            return View(new MoneyTopUpViewModel(){MoneyId = moneyId});
        }

        [HttpPost]
        public async Task<ActionResult> TopUpUserBalance(MoneyTopUpViewModel topUpViewModel)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                await _usersService.TopUpUserBalanceAsync(userId, topUpViewModel.MoneyId, topUpViewModel.AddedAmount);

                return RedirectToAction("Details", new { userId });
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError("", e.Message);
                return View();
            }
        }
    }
}
