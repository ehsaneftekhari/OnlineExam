using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace OnlineExam.Infrastructure.Contexts
{
    internal class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<OnlineExamContext>
    {
        public OnlineExamContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<OnlineExamContext>();
            builder.UseSqlServer("data source=.;initial catalog=data source=EHSANROGSTRIX;initial catalog=OnlineExam;integrated security=true;integrated security=true");
            return new OnlineExamContext(builder.Options);
        }
    }
}
