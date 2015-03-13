using Soft.Core.Domain.Catalog;

namespace Soft.Data.Mapping.Catalog
{
    public partial class ProductPictureMap : SoftEntityTypeConfiguration<ProductPicture>
    {
        public ProductPictureMap()
        {
            ToTable("Product_Picture_Mapping");
            HasKey(pp => pp.Id);
            
            HasRequired(pp => pp.Picture)
                .WithMany(p => p.ProductPictures)
                .HasForeignKey(pp => pp.PictureId);


            HasRequired(pp => pp.Product)
                .WithMany(p => p.ProductPictures)
                .HasForeignKey(pp => pp.ProductId);
        }
    }
}