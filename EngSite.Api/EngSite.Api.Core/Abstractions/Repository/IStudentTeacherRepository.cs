using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngSite.Api.BusinessLogic.Abstractions.Repository
{
    public interface IStudentTeacherRepository : IDatabaseRepository
    {
        Task<bool> AddStudentAsync(string teacherLogin, string studentLogin);
        Task<string[]?> GetStudentsAsync(string teacherLogin);
        Task<string[]?> GetTeachersAsync(string studentLogin);
    }
}
