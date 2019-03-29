using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModel
{
    class BusinessContextInitializer : DropCreateDatabaseIfModelChanges<BusinessContext>
    {
        public BusinessContextInitializer()
        {
        }

        protected override void Seed(BusinessContext context)
        {
            context.Accounts.AddOrUpdate(new Account[]
            {
                new Account{ AccountName = "ABS LTD", InceptionDate = new DateTime(1965,12,02) },
                new Account{ AccountName = "ABC LTD", InceptionDate = new DateTime(2001,5,22) },
                new Account{ AccountName = "Littlewoods", InceptionDate = new DateTime(1998,4,23) },
            });
            base.Seed(context);
        }
    }
}