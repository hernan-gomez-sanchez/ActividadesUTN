using Actividad1_5.Data;
using Actividad1_5.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actividad1_5.Services
{
    public class FacturasManager
    {
        IFacturaRepositorio oFacturaRepositorio = new FacturaRepositorioADO();
        public FacturasManager() 
        {
            oFacturaRepositorio = new FacturaRepositorioADO();
        }
        public bool Save(Factura oFactura)
        {
            return oFacturaRepositorio.Save(oFactura);
        }
        public List<Factura> GetAll()
        {
            return oFacturaRepositorio.GetAll();
        }
        public Factura? GetByID(int id)
        {
            return oFacturaRepositorio.GetByID(id);
        }
        
    }
}
