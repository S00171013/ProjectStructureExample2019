using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModel
{
    public class BusinessContext : DbContext
    {
        public BusinessContext() : base("DefaultConnection")
        {
            Database.SetInitializer(new BusinessContextInitializer());
            Database.Initialize(true);
        }

        public DbSet<Account> Accounts { get; set; }
    }

public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AccountID { get; set; }

        [Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy HH:mm}", ApplyFormatInEditMode = false)]
        public DateTime? InceptionDate { get; set; }

        public string AccountName { get; set; }

        public string AccountManagerID { get; set; }
    }
}