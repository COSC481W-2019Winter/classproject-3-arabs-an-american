using Authentication2.Models;
using Microsoft.EntityFrameworkCore;

namespace Authentication2.DataAccessLayer
{
    public class RequestContext : DbContext
    {
        public RequestContext(DbContextOptions<RequestContext> options) : base (options) { }

        public DbSet<RequestModel> Requests { get; set; }

    }
}