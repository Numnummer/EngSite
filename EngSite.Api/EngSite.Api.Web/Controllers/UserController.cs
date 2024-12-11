using AutoMapper;
using EngSite.Api.BusinessLogic.Abstractions.Repository;
using EngSite.Api.BusinessLogic.Abstractions.Services;
using EngSite.Api.BusinessLogic.Abstractions.UnitsOfWork;
using EngSite.Api.Models.User.GetUserData;
using EngSite.Api.Models.User.PostUserPhoto;
using EngSite.Api.Models.User.Registrate;
using EngSite.Api.Models.User.SignIn;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EngSite.Api.Web.Controllers
{
    [ApiController]
    [Route("/")]
    public class UserController(IUserService registrationService,
        IUserRepository userRepository, IMapper mapper,
        IRegistrationUnitOfWork registrationUnitOfWork,
        ILogger<UserController> logger) : ControllerBase
    {
        /// <summary>
        /// Зарегистрироваться
        /// </summary>
        /// <param name="requestBody"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<IActionResult> RegistrateUser(RegistrationUserData requestBody)
        {
            logger.LogInformation($"Got request at RegistrateUser");
            var user = await registrationService.GetValidUserAsync(requestBody);
            if (user!=null &&
                await registrationUnitOfWork.RegistrateUserAsync(user))
            {
                return Ok();
            }
            return BadRequest();
        }

        /// <summary>
        /// Войти
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        [HttpPost("enter")]
        public async Task<IActionResult> SignInUser(SignInUserData userData)
        {
            logger.LogInformation($"Got request at SignInUser. Login: {userData.Login}");
            var token = await registrationService.SignInUserAsync(userData);
            if (token != null)
            {
                logger.LogInformation($"Returned 200 OK and jwt token as response");
                return Ok(token);
            }
            logger.LogError($"Returned 400");
            return BadRequest();
        }

        /// <summary>
        /// Получение данных пользователя
        /// </summary>
        /// <returns>UserProfileData</returns>
        [HttpGet("getUserData")]
        [Authorize(Roles = "User, Teacher")]
        public async Task<IActionResult> GetUserData()
        {
            var login = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
            if (login==null)
            {
                return Unauthorized("Login not found");
            }
            var user = await userRepository.GetUserDataAsync(login);
            var response = mapper.Map<UserProfileData>(user);
            return Ok(response);
        }

        /// <summary>
        /// Отправить картинку в бд
        /// </summary>
        /// <param name="photoBlob"></param>
        /// <returns></returns>
        [HttpPost("postUserPhoto")]
        [Authorize(Roles = "User, Teacher")]
        public async Task<IActionResult> PostUserPhoto([FromBody] PhotoRequest photo)
        {
            var photoBlob = Convert.FromBase64String(photo.PhotoBase64);
            var login = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
            if (login==null)
            {
                return Unauthorized("Login not found");
            }
            if (await userRepository.PostPhotoToUser(login, photoBlob))
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}