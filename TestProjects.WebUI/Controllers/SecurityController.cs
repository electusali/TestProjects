using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestProjects.WebUI.Identity;
using TestProjects.WebUI.Models.Security;
using TestProjects.WebUI.Services;

namespace TestProjects.WebUI.Controllers
{
    public class SecurityController : Controller
    {
        private UserManager<AppIdentityUser> _userManager;
        private RoleManager<AppIdentityRole> _roleManager;
        private SignInManager<AppIdentityUser> _signInManager;
        private IConfiguration _configuration;
        private IMailServices _mailServices;

        public SecurityController(UserManager<AppIdentityUser> userManager,
            RoleManager<AppIdentityRole> roleManager,SignInManager<AppIdentityUser> signInManager,
            IConfiguration configuration,IMailServices mailServices)
        {
            _userManager= userManager;
            _roleManager= roleManager;
            _signInManager= signInManager;
            _configuration= configuration;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModal loginViewModal,string returnUrl="")
        {
            if (ModelState.IsValid)
            {
                var user =await _userManager.FindByEmailAsync(loginViewModal.Email);
                if (user != null)
                {
                    if (!await _userManager.IsEmailConfirmedAsync(user))
                    {
                        ModelState.AddModelError(string.Empty, "Confirm your email please!");
                        return View(loginViewModal);
                    }
                    var result = await _signInManager.PasswordSignInAsync(user, loginViewModal.Password, false, true);
                    if (result.Succeeded)
                    {
                        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            return View(loginViewModal);
                        }
                    }
                    ModelState.AddModelError(string.Empty, "login faied");
                    return View(loginViewModal); 
                }
                return View(loginViewModal);
            }
            return View(loginViewModal);
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
        public IActionResult AccesDenied()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModal registerViewModal)
        {
            if (ModelState.IsValid)
            {
                var user = new AppIdentityUser
                {
                    Email = registerViewModal.Email,
                    UserName = registerViewModal.UserName
                };
                var result=await _userManager.CreateAsync(user,registerViewModal.Password);
                if (result.Succeeded)
                {
                    var confirmationCode = _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var projectUrl = _configuration.GetSection("ProjectSettings").GetSection("ProjectUrl").Value;
                    var callBackUrl=projectUrl +Url.Action("ConfirmEmail", "Security",new {userId=user.Id,code=confirmationCode.Result});

                    /// <summary>
                    /// Kullanicaya mail gonderme
                    /// </summary>
                    var emailAddresTo = new List<EmailAddress>();
                    emailAddresTo.Add(new EmailAddress { Name=registerViewModal.UserName, Address=registerViewModal.Email});
                    var emailAddresFrom=new List<EmailAddress>();
                    emailAddresFrom.Add(new EmailAddress { Name="Test proje bilgilendirme ",Address=_configuration.GetSection("EmailConfiguration").GetSection("EmailFrom").Value});
                    _mailServices.Send(new EmailMessage { Content = callBackUrl,ToAddresses=emailAddresTo,Subject=registerViewModal.UserName, FromAddresses=emailAddresFrom}); 


                    return RedirectToAction("ConfirmEmailInfo", "Security", new { email = user.Email });
                }
                return View(registerViewModal);
            }
            return View(registerViewModal);
        }
        public IActionResult ConfirmEmailInfo(string email)
        {
            TempData["email"] = email;
            return View();
        }
        public async Task<IActionResult> ConfirmEmail(string userId,string code)
        {
            if (userId==null || code==null)
            {
                RedirectToAction("Index", "Home");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user==null)
            {
                throw new ApplicationException("Unable find to user !");
            }
            var result=await _userManager.ConfirmEmailAsync(user,code);
            if (result.Succeeded)
            {
                return RedirectToAction("Login");
            }
            return RedirectToAction("Index", "Home");
        }
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModal forgotPasswordViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(forgotPasswordViewModel);
            }
            var user = await _userManager.FindByEmailAsync(forgotPasswordViewModel.Email);
            if (user == null)
            {
                return View(forgotPasswordViewModel);
            }
            var confirmationCode = await _userManager.GeneratePasswordResetTokenAsync(user);
            var projectUrl = _configuration.GetSection("ProjectSettings").GetSection("ProjectUrl").Value;
            var callBack = projectUrl + Url.Action("ResetPassword", "Security", new { userId = user.Id, code = confirmationCode });

            //Send email

            return RedirectToAction("ConfirmForgotPasswordInfo", new { email = user.Email });
        }
        public IActionResult ConfirmForgotPasswordInfo(string email)
        {
            TempData["email"] = email;
            return View();
        }
        public IActionResult ResetPassword(string userId,string code)
        {
            if (userId==null && code==null)
            {
                throw new ApplicationException("user id or code must to be supplide for password reset");
            }
            var resetPasswordViewModal = new ResetPasswordViewModal
            {
                Code = code
            };
            return View(resetPasswordViewModal);
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModal resetPasswordViewModal)
        {
            if (ModelState.IsValid)
            {
                var user=await _userManager.FindByEmailAsync(resetPasswordViewModal.Email);
                if (user==null)
                {
                    throw new ApplicationException("User Not founf!!");
                }
                var result = await _userManager.ResetPasswordAsync(user, resetPasswordViewModal.Code, resetPasswordViewModal.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Login");
                }
                return View(resetPasswordViewModal);
            }
            return View(resetPasswordViewModal);
        }
    }
}
