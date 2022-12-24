using Core.Repository.Concrete;
using DataAccess.Abstract;
using Entities.DTOs;
using Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EfCore
{
    public class EfUserDal : EfEntityRepository<User, PersonalMoneyManagementContext>, IUserDal
    {
        public async Task RegisterAsync(RegisterDto model)
        {
            using (var context=new PersonalMoneyManagementContext())
            {
                User user = new User();
                user.FirstName= model.FirstName;
                user.LastName= model.LastName;
                user.EmailAddress= model.EmailAddress;
                user.Password= model.Password;
                user.IsTwoFactor = false;
                user.IsConfirm = false;
                user.CreateDate= DateTime.Now;
                user.IsActive= true;
                context.Users.Add(user);
                await context.SaveChangesAsync();
            }
        }
    }
}
