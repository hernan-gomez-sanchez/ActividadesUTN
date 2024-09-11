using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actividad1_5.Domain
{
    public class Factura
    {
        public int Codigo { get; set; }
        public int MetodoPago { get; set; }
        public string Cliente { get; set; }
        public DateTime Fecha { get; set; }

        public List<DetallaFactura> Detalles;
        public List<DetallaFactura> GetDetalles()
        {
            return Detalles;
        }
        public Factura(int metodopago, string cliente, DateTime fecha)
        {
            Detalles = new List<DetallaFactura>();
            MetodoPago = metodopago;
            Cliente = cliente;
            Fecha = fecha;
        }
        public Factura()
        {
            Detalles = new List<DetallaFactura>();
        }
        public void AddDetalle(DetallaFactura detalle)
        {
            if (detalle != null) 
            {
                Detalles.Add(detalle);
            }
        }
        public void RemoverDetalle(int id) 
        {
            Detalles.RemoveAt(id);
        }
        public float Total()
        {
            float total = 0;
            foreach (var detalle in Detalles)
            {
                total += detalle.SubTotal();
            }
            return total;
        }
    }
}
