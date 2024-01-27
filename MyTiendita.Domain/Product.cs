
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyTiendita.Domain;

//[Table("Product", Schema = "Cat")]
[Table("Product")]
public class Product : ModelBase
{
    public string? Barcode { get; set; }
    public string? Description { get; set; }
    public Byte[]? Capture { get; set; }
}
