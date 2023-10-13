using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineExam.Model;

namespace OnlineExam.Infrastructure.Abstraction
{
    public abstract class BaseModelEntityTypeConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseModel
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(x => x.Id);

            ConcreteConfigure(builder);
        }

        public abstract void ConcreteConfigure(EntityTypeBuilder<TEntity> builder);
    }

}
