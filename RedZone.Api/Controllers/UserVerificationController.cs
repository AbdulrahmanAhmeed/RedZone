using Azure.Core;
using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RedZone.App.Auth.Commands.Register;
using RedZone.App.Services.Auth.Common;
using RedZone.Contracts.Auth;
using System.ComponentModel.DataAnnotations;


namespace RedZone.Api.Controllers
{
    public class UserVerificationController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public UserVerificationController(IMediator mediator, IMapper mapper)
        {
            _mapper = mapper;
            _mediator = mediator;
            
        }
        public async Task<IActionResult> VerifyEmail(string UserId, string Code)
        {
            var Command = _mapper.Map<RegisterCommand>(new VerifyEmail(UserId,Code) );
            ErrorOr<AuthResult> result = await _mediator.Send(Command);
            return result.Match(
                result => Ok(_mapper.Map<AuthResponse>(result)),
                Errors => Problem()
                );
            //var user = await _userManager.FindByIdAsync(UserId);
            //if (user == null) return BadRequest();
            //var result = await _userManager.ConfirmEmailAsync(user, Code);
            //if (result.Succeeded)
            //{
            //    return View();
            //}
        }

        //[HttpGet]
        //[AllowAnonymous]
        //public IActionResult ResetPassword(string token, string email)
        //{
        //    // If password reset token or email is null, most likely the
        //    // user tried to tamper the password reset link
        //    if (token == null || email == null)
        //    {
        //        ModelState.AddModelError("", "Invalid password reset token");
        //    }
        //    return View();
        //}

        //[HttpPost]
        //[AllowAnonymous]
        //public async Task<IActionResult> ResetPassword(ResetPasswordView model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Find the user by email
        //        var user = await _userManager.FindByEmailAsync(model.Email);

        //        if (user != null)
        //        {
        //            // reset the user password
        //            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
        //            if (result.Succeeded)
        //            {
        //                return View("ResetPasswordConfirmation");
        //            }
        //            // Display validation errors. For example, password reset token already
        //            // used to change the password or password complexity rules not met
        //            foreach (var error in result.Errors)
        //            {
        //                ModelState.AddModelError("", error.Description);
        //            }
        //            return View(model);
        //        }

        //        // To avoid account enumeration and brute force attacks, don't
        //        // reveal that the user does not exist
        //        return View("ResetPasswordConfirmation");
        //    }
        //    // Display validation errors if model state is not valid
        //    return View(model);
        //}
        //public ActionResult ResetPasswordConfirmation()
        //{
        //    return View();
        //}
    }

    
}
