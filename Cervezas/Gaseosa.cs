using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervezas
{
    public class Gaseosa : Bebida
    {
        public Gaseosa(string nombre, int cantidad) : base(nombre, cantidad)
        {
        }

        public override void MostrarInformacion()
        {
            Console.WriteLine("Nombre: " + Nombre + " - Cantidad: " + Cantidad);
        }

        public bool EsAlcoholica()
        {
            return false;
        }
    }
}
