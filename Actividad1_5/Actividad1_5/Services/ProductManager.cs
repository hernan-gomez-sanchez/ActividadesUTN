using Actividad1_5.Data;
using Actividad1_5.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actividad1_5.Services
{
    public class ProductManager
    {
        public IProductoRepositorio oProductoRepositorio;

        public ProductManager()
        {
            oProductoRepositorio = new ProductoRepositorioADO();
        }
        public List<Product> GetAll()
        {
            return oProductoRepositorio.GetAll();
        }
        public Product? GetByID(int id)
        {
            return oProductoRepositorio.GetByID(id);
        }
        public bool Save(Product oProducto)
        {
            return oProductoRepositorio.Save(oProducto);
        }
        public bool Delete(int id)
        {
            return oProductoRepositorio.Delete(id);
        }
    }
}
