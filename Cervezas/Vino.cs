using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervezas
{
    public class Vino : Bebida, IBebidaAlcoholica
    {
        public int Alcohol { get; set; }

        public Vino(string nombre, int cantidad, int alcohol) : base(nombre, cantidad)
        {
            this.Alcohol = alcohol;
        }

        public override void MostrarInformacion()
        {
            Console.WriteLine("Nombre: " + Nombre + " - Cantidad: " + Cantidad + " - Alcohol: " + Alcohol + "%");
        }

        public void MaximoRecomendado()
        {
            Console.WriteLine("El máximo permitido es 3 copas por persona.");
        }

        public bool EsAlcoholica()
        {
            return true;
        }
    }
}
