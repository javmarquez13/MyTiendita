using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyTiendita.Domain
{
    [Table("Stocks")]
    public class Stock : ModelBase
    {
        public int IdProduct { get; set; }

        [ForeignKey(nameof(IdProduct))]
        [JsonIgnore]
        public Product? Product { get; set; }

        public int? Quantity { get; set; }

    }
}
