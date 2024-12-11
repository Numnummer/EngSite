using EngSite.Api.BusinessLogic.Abstractions.Repository;
using EngSite.Api.Models.StudentTeacher;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EngSite.Api.Web.Controllers
{
    [ApiController]
    [Route("/studentTeacher")]
    public class StudentTeacherController(ILogger<StudentTeacherController> logger,
        IStudentTeacherRepository studentTeacherRepository) : ControllerBase
    {
        /// <summary>
        /// Добавить студента в ученики
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("addStudent")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> AddStudent([FromBody] AddStudentRequest request)
        {
            logger.LogInformation($"Got request body: {request.Login}");
            var login = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
            if (login==null)
            {
                logger.LogInformation($"Login not found");
                return Unauthorized("Login not found");
            }
            if (await studentTeacherRepository.AddStudentAsync(login, request.Login))
            {
                logger.LogInformation("Ok response");
                return Ok();
            }
            logger.LogInformation("400 error");
            return BadRequest();
        }

        /// <summary>
        /// Получить список логинов учеников
        /// </summary>
        /// <returns></returns>
        [HttpGet("getStudents")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> GetStudents()
        {
            var login = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
            if (login==null)
            {
                logger.LogInformation($"Login not found");
                return Unauthorized("Login not found");
            }
            var students = await studentTeacherRepository.GetStudentsAsync(login);
            if (students==null)
            {
                logger.LogError($"400 error");
                return BadRequest();
            }
            return Ok(students);
        }

        /// <summary>
        /// Получить список логинов учителей
        /// </summary>
        /// <returns></returns>
        [HttpGet("getTeachers")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetTeachers()
        {
            var login = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
            if (login==null)
            {
                logger.LogInformation($"Login not found");
                return Unauthorized("Login not found");
            }
            var teachers = await studentTeacherRepository.GetTeachersAsync(login);
            if (teachers==null)
            {
                logger.LogError($"400 error");
                return BadRequest();
            }
            return Ok(teachers);
        }
    }
}
