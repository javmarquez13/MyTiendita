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
    public class ModelBase
    {
        [Key]
        [Column(Order = 1)]
        public int id { get; set; }

        [JsonIgnore]
        public DateTime Created { get; set; }

        [JsonIgnore]
        public DateTime Updated { get; set; }


        [JsonIgnore]
        public int? CreatedById { get; set; }

        [JsonIgnore]
        public int? UpdatedById { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(CreatedById))]
        public User? CreatedBy { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(UpdatedById))]
        public User? UpdatedBy { get; set; }

        [JsonIgnore]
        public bool? IsActive { get; set; } = true;
    }
}
