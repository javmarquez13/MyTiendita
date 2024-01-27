using MyTiendita.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTiendita.Shared.DTOs.Mappers;

public static class StockMap
{
    public static StockDTO ToDTO(this Stock model)
    {
        if (model is null) return null;

        return new StockDTO(
                model.Quantity
            );
    }



    public static Stock ToModel(this StockDTO dto)
    {
        if (dto is null) return null;

        return new Stock()
        {
            Quantity = dto.Quantity
        };
    }


    public static IEnumerable<StockDTO> ToDTOs(this IEnumerable<Stock> model)
    {
        if (model is not null) return model.Select(i => i.ToDTO());
        return Enumerable.Empty<StockDTO>();
    }
}
