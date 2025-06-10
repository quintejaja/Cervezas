using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervezas
{
    public class Bebida
    {
        public string Nombre { get; set; }
        public int Cantidad { get; set; }

        public Bebida(string nombre, int cantidad)
        {
            this.Nombre = nombre;
            this.Cantidad = cantidad;
        }

        public virtual void MostrarInformacion()
        {
            Console.WriteLine("Nombre de la bebida: " + Nombre + " - Cantidad: " + Cantidad);
        }

        public void Consumir(int cantidadConsumida)
        {
            this.Cantidad -= cantidadConsumida;
        }
    }
}
