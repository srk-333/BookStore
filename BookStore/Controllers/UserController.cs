namespace BookStore.Controllers
{   
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BusinessLayer.Interface;
    using CommonLayer.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// User Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// The user interface from business layer
        /// </summary>
        private readonly IUserBL userBL;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="userBL">The user interface.</param>
        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
        }

        /// <summary>
        /// Adds the user.
        /// </summary>
        /// <param name="userRegistration">The user registration.</param>
        /// <returns>Registered User Details</returns>
        [HttpPost("Register")]
        public IActionResult AddUser(UserModel userRegistration)
        {
            try
            {
                var user = this.userBL.Register(userRegistration);
                if (user != null)
                {
                    return this.Ok(new { Success = true, message = "User Added Sucessfully", Response = user });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Enter Different Email" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Users the login.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns>Login Token</returns>
        [HttpPost("Login")]
        public IActionResult UserLogin(string email, string password)
        {
            try
            {
                var loginToken = this.userBL.UserLogin(email, password);
                if (loginToken != null)
                {
                    return this.Ok(new { Success = true, message = "Logged In Sucessfully", Response = loginToken });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Login Failed" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Forgot the password.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>Forgot Password Token</returns>
        [HttpPost("ForgotPassword")]
        public IActionResult ForgotPassword(string email)
        {
            try
            {
                var forgotPasswordToken = this.userBL.ForgotPassword(email);
                if (forgotPasswordToken != null)
                {
                    return this.Ok(new { Success = true, message = " Token Sent on Mail To Reset The Password", Response = forgotPasswordToken });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Forgot Password Token Generation Failed" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Reset the password.
        /// </summary>
        /// <param name="newPassword">The new password.</param>
        /// <param name="confirmPassword">The confirm password.</param>
        /// <returns>Password change Confirmation </returns>
        [Authorize]
        [HttpPut("ResetPassword")]
        public IActionResult ResetPassword(string newPassword, string confirmPassword)
        {
            try
            {
                var email = User.FindFirst("Email").Value.ToString();
                if (this.userBL.ResetPassword(email, newPassword, confirmPassword))
                {
                    return this.Ok(new { Success = true, message = " Password Changed Sucessfully " });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = " Password Change Failed ! Try Again " });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }
    }
}
