using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTiendita.Domain;

public class User : ModelBase
{
    public string Name { get; set; }
    public string LastName { get; set; }

    public string WholeName
    {
        get { return string.Join(" ", Name, LastName); }
    }

    public string? Password { get; set; }
}
