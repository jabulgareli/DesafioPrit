using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Prit.Portal.Identity;
using Prit.Portal.Models;

namespace Prit.Portal.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private readonly SignInManager<PritPortalUser> _signInManager;
        private readonly UserManager<PritPortalUser> _userManager;
        private readonly ILogger<AuthController> _logger;

        public AuthController(SignInManager<PritPortalUser> signInManager,
                              UserManager<PritPortalUser> userManager,
                              ILogger<AuthController> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(NewUserViewModel newUser)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View();

                var userPortal = newUser.AsPritPortalUser();
                var result = await _userManager.CreateAsync(userPortal, newUser.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View(nameof(Create));
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel login)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(nameof(Index));

                var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, false, lockoutOnFailure: true);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(nameof(login.Email), "Email e senha não encontrados ou inválidos");
                    return View(nameof(Index));
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return View(nameof(Index));
            }
        }

        [HttpGet]
        public async Task<ActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}