using EngSite.Api.BusinessLogic.Abstractions.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngSite.Api.DataAccess.Repository
{
    public class StudentTeacherRepository(EnglishSiteContext dbContext,
        ILogger<StudentTeacherRepository> logger) : IStudentTeacherRepository
    {
        public async Task<bool> AddStudentAsync(string teacherLogin, string studentLogin)
        {
            try
            {
                await dbContext.TeacherStudents.AddAsync(new Models.Entities.TeacherStudent()
                {
                    Studentlogin = studentLogin,
                    Teacherlogin = teacherLogin
                });
                await dbContext.SaveChangesAsync();
                logger.LogInformation($"Added in database teacherLogin:{teacherLogin};studentLogin:{studentLogin}");
                return true;
            }
            catch (Exception)
            {
                logger.LogError($"Failed to add in database teacherLogin:{teacherLogin};studentLogin:{studentLogin}");
                return false;
            }
        }

        public async Task<string[]?> GetStudentsAsync(string teacherLogin)
        {
            try
            {
                return await dbContext.TeacherStudents.Where(e => e.Teacherlogin==teacherLogin).Select(e => e.Studentlogin).ToArrayAsync();
            }
            catch (Exception)
            {
                logger.LogError($"Failed to get students by teacherLogin:{teacherLogin}");
                return null;
            }
        }

        public async Task<string[]?> GetTeachersAsync(string studentLogin)
        {
            try
            {
                return await dbContext.TeacherStudents.Where(e => e.Studentlogin==studentLogin).Select(e => e.Teacherlogin).ToArrayAsync();
            }
            catch (Exception)
            {
                logger.LogError($"Failed to get teachers by studentLogin:{studentLogin}");
                return null;
            }
        }

        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
