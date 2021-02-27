using Microsoft.EntityFrameworkCore;
using WebApiCore.Model;

namespace WebApiCore
{
    public class ApiDbcontext: DbContext
    {
        public ApiDbcontext(DbContextOptions options): base(options)
        {
                
        }

        public DbSet<Fornecedor> Fornecedores { get; set;}
    }
}