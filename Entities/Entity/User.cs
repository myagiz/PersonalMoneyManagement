using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Entity
{
    public partial class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public bool IsConfirm { get; set; }
        public bool IsTwoFactor { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? TokenStartDate { get; set; }
        public DateTime? TokenExpiredDate { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsActive { get; set; }
    }
}
