using Cinema.Backend.DTOS;
using Cinema.Backend.DTOS.Request;
using Cinema.Backend.DTOS.Response;
using Cinema.BLL.HelperService;
using Cinema.BLL.Services;
using Cinema.DAL;
using Cinema.DAL.Implemantations;
using Cinema.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Cinema.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly ApplicationUserServices _applicationUserServices;
        private readonly JWTService _jwtService;
        private readonly PasswordHash _passwordHash;
        private readonly IHttpContextAccessor _contextAccessor;

        public UserController(UnitOfWork unitOfWork,
            ApplicationUserServices applicationUserServices,
            JWTService jwtService,
            PasswordHash passwordHash,
            IHttpContextAccessor contextAccessor)

        {
            _unitOfWork = unitOfWork;
            _applicationUserServices = applicationUserServices;
            _jwtService = jwtService;
            _passwordHash = passwordHash;
            _contextAccessor = contextAccessor;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<GeneralRequestDTO>> Register([FromBody] RegisterDTO registerDTO)
        {
            try
            {
                if (await _applicationUserServices.CheckEmailExist(registerDTO.Email))
                {
                    return StatusCode(500, new GeneralRequestDTO { IsSuccess = false, Message = $"User with this email is exists" });
                }

                if (ModelState.IsValid)
                {
                    var newUser = new ApplicationUser
                    {
                        Email = registerDTO.Email,
                        FirstName = registerDTO.FirstName,
                        LastName = registerDTO.LastName,
                        PhoneNumber = registerDTO.PhoneNumber,
                        Role = registerDTO.Role,
                        Password = PasswordHash.DoHash(registerDTO.Password)
                    };

                    if (newUser.Role == "string" || newUser.Role.IsNullOrEmpty())                       
                         newUser.Role = SD.Customer;

                    await _unitOfWork.ApplicationUserRepository.InsertAsync(newUser);
                    await _unitOfWork.SaveAsync();
                }
                 return Ok(new GeneralRequestDTO { IsSuccess = true, Message = $"User with {registerDTO.Email} was successfully created!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Something went wrong try again:{ex.Message}");
            }
        }
        [HttpGet("Login")]
        public async Task<ActionResult<LoginRequestDTO>> Login([FromBody] LoginDTO loginDTO)
        {
            var user = await _applicationUserServices.GetUserByEmailAsync(loginDTO.Email);

            if (user != null)
            {
                var result = await _passwordHash.CheckPassword(loginDTO.Password, user.Password);

                if (result)
                {
                    var token = _jwtService.GenerateToken(user);

                    _contextAccessor.HttpContext.Response.Cookies.Append("token", token);
                    return Ok(new LoginRequestDTO { IsSuccess = true, Message = "You successfully login", Token = token });
                }
            }
            return StatusCode(500, "Invalid email or password,try again");
        }



       


        


    }
}
