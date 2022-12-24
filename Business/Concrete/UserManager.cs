using Business.Abstract;
using Business.Constants.Messages;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.DTOs;
using Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager:IUserService
    {
        private readonly IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public async Task<IResult> GenerateUserRefreshToken(int id, string refreshToken, DateTime tokenStartDate, DateTime tokenExpiredDate)
        {
            await _userDal.GenerateUserRefreshToken(id, refreshToken, tokenStartDate, tokenExpiredDate);
            return new SuccessResult(SuccessMessages.SPMM5);
        }

        public async Task<IDataResult<User>> GetUserForLoginAsync(LoginDto model)
        {
            var getUser =await _userDal.GetUserForLoginAsync(model);
            if (getUser != null)
            {
                var getConfirmEmail = _userDal.Get(x => x.EmailAddress == model.EmailAddress && x.IsConfirm == true);
                if (getConfirmEmail!=null)
                {
                    return new SuccessDataResult<User>(getUser,SuccessMessages.SPMM4);

                }
                return new ErrorDataResult<User>(ErrorMessages.EPMM5);
            }
            return new ErrorDataResult<User>(ErrorMessages.EPMM4);
        }

        public async Task<IResult>  RegisterAsync(RegisterDto model)
        {
            await _userDal.RegisterAsync(model);
            return new SuccessResult(SuccessMessages.SPMM1);
        }
    }
}
