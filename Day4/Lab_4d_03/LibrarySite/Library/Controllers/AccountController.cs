﻿using Library.Models;
using Library.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Library.Controllers
{
    public class AccountController : Controller
    {
        private SignInManager<User> _signInManager;
        private UserManager<User> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public AccountController(SignInManager<User> signInManager, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Login()
        {
            if (this.User.Identity.IsAuthenticated)
                RedirectToAction("Index", "Library");
            return View();
        }

        [HttpPost]
        [ActionName("Login")]
        public async Task<IActionResult> LoginPost(LoginViewModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(loginModel.UserName, loginModel.Password, loginModel.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Library");
                }
            }
            ModelState.AddModelError("", "Failed to Login");
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Library");
        }

        public IActionResult Register()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [ActionName("Register")]
        public async Task<IActionResult> RegisterPost(RegisterViewModel registerModel)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    FirstName = registerModel.FirstName,
                    LastName = registerModel.LastName,
                    UserName = registerModel.UserName,
                    PhoneNumber = registerModel.PhoneNumber,
                    Email = registerModel.Email,
                };
                var result = await _userManager.CreateAsync(user, registerModel.Password);
                if (result.Succeeded)
                {
                    var roleExists = await _roleManager.RoleExistsAsync(registerModel.RoleName);
                    if (!roleExists)
                    {
                        await _roleManager.CreateAsync(new IdentityRole(registerModel.RoleName));
                    }
                    if (!await _userManager.IsInRoleAsync(user, registerModel.RoleName))
                    {
                        await _userManager.AddToRoleAsync(user, registerModel.RoleName);
                    }
                    if (!string.IsNullOrWhiteSpace(user.Email))
                    {
                        var claim = new Claim(ClaimTypes.Email, user.Email);
                        await _userManager.AddClaimAsync(user, claim);
                    }
                    var resultSignIn = await _signInManager.PasswordSignInAsync(registerModel.UserName, registerModel.Password, registerModel.RememberMe, false);
                    if (resultSignIn.Succeeded)
                    {
                        return RedirectToAction("Index", "Library");
                    }
                }
            }
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
