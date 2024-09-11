using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actividad1_5.Domain
{
    public class Product
    {
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public float Precio { get; set; }
        public int Stock { get; set; }
        public bool Activo { get; set; }

        public Product(string nombre, float precio, int stock, bool activo)
        {
            Nombre = nombre;
            Precio = precio;
            Stock = stock;
            Activo = activo;
        }
        public Product() { }

        public override string ToString()
        {
            return Codigo.ToString() +") "+ Nombre;
        }
    }
    
}
