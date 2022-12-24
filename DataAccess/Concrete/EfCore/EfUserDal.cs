using Core.Repository.Concrete;
using DataAccess.Abstract;
using Entities.DTOs;
using Entities.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EfCore
{
    public class EfUserDal : EfEntityRepository<User, PersonalMoneyManagementContext>, IUserDal
    {
        public async Task GenerateUserRefreshToken(int id, string refreshToken, DateTime tokenStartDate, DateTime tokenExpiredDate)
        {
            using (var context = new PersonalMoneyManagementContext())
            {
                var getUser = context.Users.FirstOrDefaultAsync(x => x.Id == id && x.IsActive).Result;
                if (getUser != null)
                {
                    getUser.RefreshToken = refreshToken;
                    getUser.TokenStartDate = tokenStartDate;
                    getUser.TokenExpiredDate = tokenExpiredDate;
                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task<User> GetUserForLoginAsync(LoginDto model)
        {
            using (var context = new PersonalMoneyManagementContext())
            {
                var getUser =await context.Users.Where(x => x.EmailAddress == model.EmailAddress && x.Password == model.Password && x.IsActive == true).FirstOrDefaultAsync();
                if (getUser != null)
                {
                    return getUser;
                }
                return null;
            }
        }

        public async Task RegisterAsync(RegisterDto model)
        {
            using (var context = new PersonalMoneyManagementContext())
            {
                User user = new User();
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.EmailAddress = model.EmailAddress;
                user.Password = model.Password;
                user.IsTwoFactor = false;
                user.IsConfirm = false;
                user.CreateDate = DateTime.Now;
                user.IsActive = true;
                context.Users.Add(user);
                await context.SaveChangesAsync();
            }
        }
    }
}
