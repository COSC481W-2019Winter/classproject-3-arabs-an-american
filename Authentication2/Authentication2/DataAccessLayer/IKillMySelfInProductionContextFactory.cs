using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication2.DataAccessLayer
{
    public class IKillMySelfInProductionContextFactory : IDesignTimeDbContextFactory<MyProductionDbContext>
    {
        public MyProductionDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MyProductionDbContext>();
            var connectionString = "Server = tcp:oshipdbserver.database.windows.net,1433; Initial Catalog = ohship_db; Persist Security Info = False; User ID = zafer; Password = !@#123anas;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            optionsBuilder.UseSqlServer(connectionString);
            return new MyProductionDbContext(optionsBuilder.Options);

        }
    }
}
