using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OnePageNet.App.Data.Models;

namespace OnePageNet.App.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILoggerFactory loggerFactory)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = loggerFactory.CreateLogger<AccountController>();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(loginDto.Email, loginDto.Password, loginDto.RememberMe, lockoutOnFailure: false);
         
                if (result.Succeeded)
                {
                    _logger.LogInformation(1, "User logged in");
                    return Ok();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    
                    return BadRequest(loginDto);
                }
            }

            return UnprocessableEntity(loginDto);
        }

        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<string>> Register([FromBody] RegisterDto model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // TODO Add isPersistent as a parameter
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    
                    _logger.LogInformation(3, "User account created successfully");
                    
                    return Ok();
                }
                AddErrors(result);
            }

            return BadRequest("You did not register successfully");
        }

        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation(4, "User logged out");
            return Ok();
        }

        // GET: /Account/ConfirmEmail
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(
            [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Disallow)]string userId,
            [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Disallow)] string code)
        {
            var user = await _userManager.FindByIdAsync(userId);
            
            if (user == null)
            {
                return NotFound(userId);
            }
            
            var result = await _userManager.ConfirmEmailAsync(user, code);

            if (result.Succeeded)
            {
                return Ok();
            }
            
            return BadRequest("Did not confirm email");
        }

        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<ForgotPasswordDto>> ForgotPassword(ForgotPasswordDto forgotPasswordDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(forgotPasswordDto.Email);

                Url.Action("ResetPassword", "Account", 
                        new { userId = user.Id }, HttpContext.Request.Scheme);
                // TODO Code a emailSender
                //await _emailSender.SendEmailAsync(model.Email, "Reset Password",
                //   "Please reset your password by clicking here: <a href=\"" + callbackUrl + "\">link</a>");
                return Ok();
            }

            return forgotPasswordDto;
        }

        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(resetPasswordDto);
            }
            
            var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);
            
            if (user == null)
            {
                return NotFound(resetPasswordDto);
            }
            
            var result = await _userManager.ResetPasswordAsync(user, resetPasswordDto.Code, resetPasswordDto.Password);
            if (result.Succeeded)
            {
                return Ok();
            }
            
            AddErrors(result);
            return BadRequest(result);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}
