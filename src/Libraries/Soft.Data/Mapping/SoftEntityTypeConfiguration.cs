using System.Data.Entity.ModelConfiguration;

namespace Soft.Data.Mapping
{
    public abstract class SoftEntityTypeConfiguration<T> : EntityTypeConfiguration<T> where T : class
    {
        protected SoftEntityTypeConfiguration()
        {
            PostInitialize();
        }

        protected void PostInitialize()
        {
            //@todo
          //  throw new System.NotImplementedException();
        }
    }
}