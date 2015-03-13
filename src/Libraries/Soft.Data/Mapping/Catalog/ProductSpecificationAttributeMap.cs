using Soft.Core.Domain.Catalog;

namespace Soft.Data.Mapping.Catalog
{
    public partial class ProductSpecificationAttributeMap : SoftEntityTypeConfiguration<ProductSpecificationAttribute>
    {
        public ProductSpecificationAttributeMap()
        {
            ToTable("Product_SpecificationAttribute_Mapping");
            HasKey(psa => psa.Id);

            Property(psa => psa.CustomValue).HasMaxLength(4000);

            Ignore(psa => psa.AttributeType);

            HasRequired(psa => psa.SpecificationAttributeOption)
                .WithMany(sao => sao.ProductSpecificationAttributes)
                .HasForeignKey(psa => psa.SpecificationAttributeOptionId);


            HasRequired(psa => psa.Product)
                .WithMany(p => p.ProductSpecificationAttributes)
                .HasForeignKey(psa => psa.ProductId);
        }
    }
}