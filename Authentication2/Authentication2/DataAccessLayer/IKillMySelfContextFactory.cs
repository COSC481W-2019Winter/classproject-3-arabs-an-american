using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication2.DataAccessLayer
{
    public class IKillMySelfContextFactory : IDesignTimeDbContextFactory<MyIdentityContext>
    {
        public MyIdentityContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MyIdentityContext>();
            var connectionString = "DataSource =C:\\Users\\Public\\DatabaseLite.db";
            optionsBuilder.UseSqlite(connectionString);
            return new MyIdentityContext(optionsBuilder.Options);

        }
    }
}
