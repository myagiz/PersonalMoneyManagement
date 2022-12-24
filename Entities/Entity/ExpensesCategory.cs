using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Entity
{
    public partial class ExpensesCategory
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Explain { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsActive { get; set; }
    }
}
