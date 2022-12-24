using Core.Utilities.Results;
using Entities.DTOs;
using Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserService
    {
        Task<IResult> RegisterAsync(RegisterDto model);
        Task<IDataResult<User>> GetUserForLoginAsync(LoginDto model);
        Task<IResult> GenerateUserRefreshToken(int id, string refreshToken, DateTime tokenStartDate, DateTime tokenExpiredDate);
    }
}
