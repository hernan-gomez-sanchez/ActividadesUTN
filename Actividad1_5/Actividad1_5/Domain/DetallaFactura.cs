using Actividad1_5.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actividad1_5.Domain
{
    public class DetallaFactura
    {
        public int Codigo { get; set; }
        public Product Producto { get; set; }
        public int Cantidad { get; set; }
        public float Precio { get; set; }

        public float SubTotal()
        {
            return Cantidad * Precio;
        }
        public DetallaFactura(Product producto, int cantidad, float precio)
        {
            Producto = producto;
            Cantidad = cantidad;
            Precio = precio;
        }
        public DetallaFactura()
        {
        }
        

    }
}
