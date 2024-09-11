using Actividad1_5.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actividad1_5.Data
{
    public interface IFacturaRepositorio
    {
        bool Save(Factura oFactura);
        List<Factura> GetAll();
        Factura? GetByID(int id);


    }
}
