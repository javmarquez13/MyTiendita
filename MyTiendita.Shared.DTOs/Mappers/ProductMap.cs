using MyTiendita.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;


namespace MyTiendita.Shared.DTOs.Mappers
{

    public static class ProductMap
    {
        public static ProductDTO ToDTO(this Product model)
        {
            if (model == null) return null;

            return new ProductDTO(
                model.Barcode,
                model.Description,
                model.Capture);
        }


        public static Product ToModel(this ProductDTO dto)
        {
            if (dto is null) return null;

            return new Product()
            {
                Barcode = dto.Barcode,
                Description = dto.Description,
                Capture = dto.Capture
            };
        }

        public static IEnumerable<ProductDTO> ToDTOs(this IEnumerable<Product> model)
        {
            if (model != null) return model.Select(i => i.ToDTO());
            return Enumerable.Empty<ProductDTO>();
        }
    }
}
