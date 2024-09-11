using Actividad1_5.Data.Utils;
using Actividad1_5.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actividad1_5.Data
{
    internal class ProductoRepositorioADO : IProductoRepositorio
    {
        public bool Delete(int id)
        {
            var parametros = new List<ParametroSQL>();
            parametros.Add(new ParametroSQL("@codigo",id));
            int rows = DataHelper.GetInstance().ExecuteSPDML("SP_BorrarProducto",parametros);
            return (rows == 1);
        }        

        public List<Product> GetAll()
        {
            List<Product> list = new List<Product>();
            DataTable tabla = DataHelper.GetInstance().ExecuteSPQuery("SP_RecuperarProductos", null);
            foreach (DataRow row in tabla.Rows) 
            {
                Product oProducto = new Product()
                {
                    Codigo = Convert.ToInt32(row["ID_Producto"]),
                    Nombre = row["Nombre"].ToString(),
                    Precio = (float)Convert.ToSingle(row["Precio"]),
                    Activo = Convert.ToBoolean(row["Activo"]),
                    Stock = Convert.ToInt32(row["Stock"])
                };
                list.Add(oProducto);
            }
            return list;
        }

        public Product GetByID(int id)
        {
            var parametros = new List<ParametroSQL>();
            parametros.Add(new ParametroSQL("@codigo", id));
            DataTable tabla = DataHelper.GetInstance().ExecuteSPQuery("SP_RecuperarProducto_Codigo", parametros);
            if (tabla != null && tabla.Rows.Count == 1)
            {
                DataRow row = tabla.Rows[0];
                Product oProducto = new Product()
                {
                    Codigo = Convert.ToInt32(row["ID_Producto"]),
                    Nombre = row["Nombre"].ToString(),
                    Precio = (float)Convert.ToSingle(row["Precio"]),
                    Activo = Convert.ToBoolean(row["Activo"]),
                    Stock = Convert.ToInt32(row["Stock"])
                };
                return oProducto;
            }
            else
            {
                return null;
            }
            
        }

        public bool Save(Product oProducto)
        {
            var parametros = new List<ParametroSQL>()
            {
                new ParametroSQL("@nombre",oProducto.Nombre),
                new ParametroSQL("@precio",oProducto.Precio),
                new ParametroSQL("@stock",oProducto.Stock),
                new ParametroSQL("@activo",oProducto.Activo),
            };
            int rows = DataHelper.GetInstance().ExecuteSPDML("SP_GuardarProducto", parametros);
            return (rows == 1);
        }
    }
}
