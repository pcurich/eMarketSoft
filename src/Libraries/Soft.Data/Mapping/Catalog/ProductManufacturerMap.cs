using Soft.Core.Domain.Catalog;

namespace Soft.Data.Mapping.Catalog
{
    public partial class ProductManufacturerMap : SoftEntityTypeConfiguration<ProductManufacturer>
    {
        public ProductManufacturerMap()
        {
            ToTable("Product_Manufacturer_Mapping");
            HasKey(pm => pm.Id);
            
            HasRequired(pm => pm.Manufacturer)
                .WithMany()
                .HasForeignKey(pm => pm.ManufacturerId);


            HasRequired(pm => pm.Product)
                .WithMany(p => p.ProductManufacturers)
                .HasForeignKey(pm => pm.ProductId);
        }
    }
}