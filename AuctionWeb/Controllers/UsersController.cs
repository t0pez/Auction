using AuctionBLL.Dto;
using AuctionBLL.Services;
using AuctionWeb.ViewModels.Users;
using AutoMapper;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;

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

        public async Task<ActionResult> Details(string userId)
        {
            var unmapped = await _usersService.GetByUserIdAsync(userId);

            var mapped = _mapper.Map<UserDetailsViewModel>(unmapped);

            var userService = HttpContext.GetOwinContext().GetUserManager<IUsersService>();
            var roles = await userService.GetUserRolesAsync(userId);
            var role = String.Join(" ", roles);

            mapped.Role = role;

            return View(mapped);
        }

        [HttpPost]
        public async Task<ActionResult> Create(UserCreateViewModel userCreateViewModel)
        {
            try
            {
                var mapped = _mapper.Map<UserDto>(userCreateViewModel);

                mapped.Role = "user";

                await _usersService.CreateAsync(mapped);
                
                return RedirectToAction("Index", "Home");
            }
            catch (InvalidOperationException)
            {
                ModelState.AddModelError("", "User with this username already exists");
                return View();
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult> AddUserToAdminRole(string userId)
        {
            await _usersService.AddRoleAsync(userId, "admin");
            

            return RedirectToAction("Index", "Users");
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult> RemoveUserFromAdminRole(string userId)
        {
            await _usersService.RemoveRoleAsync(userId, "admin");

            return RedirectToAction("Index", "Users");
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

            ClaimsIdentity claim;

            try
            {
                claim = await _usersService.LoginAsync(userDto);
            }
            catch (InvalidOperationException)
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

        
        public ActionResult CreateUserWallet()
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
        
        [HttpPost]
        public async Task<ActionResult> CreateUserWallet(string userId, MoneyCreateViewModel money)
        {
            var mapped = _mapper.Map<MoneyDto>(money);

            await _usersService.CreateUserMoneyAsync(userId, mapped);
            
            return RedirectToAction("Details", new{userId = userId});
        }
    }
}
