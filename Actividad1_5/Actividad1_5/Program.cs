// See https://aka.ms/new-console-template for more information
using Actividad1_5.Domain;
using Actividad1_5.Services;
using System.Runtime.CompilerServices;

FacturasManager oFacturaManager = new FacturasManager();
ProductManager oProductManager = new ProductManager();

string ingProducto;
int precio;
int stock;



while (true)
{
    Console.WriteLine("Ingrese un producto(no ingrese nada para continuar)");
    ingProducto = Console.ReadLine();
    if(ingProducto == "")
    {
        break;
    }

    while (true)
    {
        Console.WriteLine("Igrese el precio");
        try
        {
            precio = Convert.ToInt32(Console.ReadLine());
            break;
        }
        catch
        {
            Console.WriteLine("ERROR: Debe ingresar un numero");
        }
    }
    while (true)
    {
        Console.WriteLine("Igrese el stock");
        try
        {
            stock = Convert.ToInt32(Console.ReadLine());
            break;
        }
        catch
        {
            Console.WriteLine("ERROR: Debe ingresar un numero");
        }
    }
    oProductManager.Save(new Product(ingProducto, precio, stock, true));
}


Factura oFactura = new Factura();
while (true)
{
    Console.WriteLine("Ingrese el nombre del cliente");
    try
    {
        oFactura.Cliente = Console.ReadLine();
        break;
    }
    catch
    {
        Console.WriteLine("ERROR: El cliente no puede ser nullo");
    }
}  
    

while (true)
{
    Console.WriteLine("Ingrese metodo de pago(0-Efectivo,1-Tarjeta)");
    try
    {
        oFactura.MetodoPago = Convert.ToInt32(Console.ReadLine());
        if (oFactura.MetodoPago == 0 || oFactura.MetodoPago == 1)
        {
            break;
        }
        else
        {
            Console.WriteLine("ERROR: Elija una opcion valida");
        }
    }
    catch
    {
        Console.WriteLine("ERROR: Debe ingresar un numero");
    }
}


oFactura.Fecha = DateTime.Now;

Console.WriteLine("Lista de productos:");
int contador = 0;
foreach (Product producto in oProductManager.GetAll())
{
    Console.WriteLine(producto.ToString());
    contador++;
}
while(true)
{
    Console.WriteLine("Ingrese el ID del articulo del detalle(0 para dejar de ingresar)");
    try
    {
        var id = Convert.ToInt32(Console.ReadLine());
        bool idrepetido = false;
        if (contador < id||id<0)
        {
            Console.WriteLine("ERROR: Debe ingresar una opcion valida");
        }
        else
        {
            if (id == 0)
            {
                break;
            }
            else
            {
                foreach (DetallaFactura detalle in oFactura.Detalles)
                {
                    if (detalle.Producto.Codigo == id)
                    {
                        idrepetido = true;
                        Console.WriteLine("Ingrese la cantidad a sumar");
                        detalle.Cantidad += Convert.ToInt32(Console.ReadLine());
                    }
                }
            }
            if (!idrepetido)
            {
                DetallaFactura oDetalle = new DetallaFactura();
                oDetalle.Producto = oProductManager.GetByID(id);
                oDetalle.Precio = oDetalle.Producto.Precio;
                Console.WriteLine("Ingrese la cantidad");
                oDetalle.Cantidad = Convert.ToInt32(Console.ReadLine());
                oFactura.AddDetalle(oDetalle);
            }
        }
        
        
    }
    catch
    {
        Console.WriteLine("ERROR: error inesperado, vuelva a cargar los campos");
    }
     
}
if (oFacturaManager.Save(oFactura))
{
    Console.WriteLine("Se cargo de manera correcta la factura");
}
else 
{
    Console.WriteLine("error al cargar la factura");
}


