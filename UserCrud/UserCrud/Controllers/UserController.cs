
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Web.Http;
using UserCrud.Models;
using UserCrud.Services;

namespace UserCrud.Controllers
{
    
    [Microsoft.AspNetCore.Mvc.Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly Dictionary<string, int> otpMap = new Dictionary<string, int>();
        public UserController()
        {
            emailService = new EmailService();
        }

        private readonly UserDbContext _userDbContext;
        private EmailService emailService;

        public UserController(UserDbContext userDbContext) 
        { 
            _userDbContext = userDbContext;
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("employee-login")]
        public async Task<bool> GetUserByEmailAsync([FromQuery]string email, [FromQuery] string password)
        {
            var user = await _userDbContext.User.FirstOrDefaultAsync(u => u.email == email);

            if (user != null && user.password== password)
            {
                // If the user is not found, return a NotFound response
                return true;
            }
            else
            {
                return false;
            }

            
        }


        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.Route("create-employee")]
        //Adding Users
        public async  Task<User> AddUser(User objUser)
        {
            _userDbContext.User.Add(objUser);
            await _userDbContext.SaveChangesAsync();
            return objUser;
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.Route("send-otp")]
        public String SendOTP([FromQuery] string email)
        {
            // Generate a random 4-digit number
            Random random = new Random();
            int otp = 1000 + random.Next(9000);

            // Store the OTP in memory for verification
            otpMap.Clear();
            otpMap[email] = otp;

            string to = email;
            string subject = "Verification For Employment: " + otp;
            string body = "Your OTP is : " + otp;

            emailService.SendEmail(to, subject, body);

            return "Email sent successfully!";
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.Route("verify-otp")]
        public IActionResult VerifyOTP([FromQuery] string email, [FromQuery] int otp)
        {
            if (otpMap.TryGetValue(email, out int storedOTP) && storedOTP == otp)
            {
                // OTP is valid
                otpMap.Remove(email); // Remove the OTP from memory after verification
                return Ok(true);
            }
            else
            {
                // OTP is invalid
                return Ok(false);
            }
        }












    }
}
