using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervezas
{
    public class Cerveza : Bebida, IBebidaAlcoholica
    {
        public int Alcohol { get; set; }
        public string Marca { get; set; }

        public Cerveza(string nombre, int cantidad, int alcohol, string marca) : base(nombre, cantidad)
        {
            this.Alcohol = alcohol;
            this.Marca = marca;
        }

        public override void MostrarInformacion()
        {
            Console.WriteLine("Nombre de la cerveza: " + Nombre + " - Marca: " + Marca + " - Cantidad: " + Cantidad + " - Alcohol: " + Alcohol + "%");
        }

        public void MaximoRecomendado()
        {
            Console.WriteLine("El máximo permitido es 5 unidades por persona.");
        }

        public bool EsAlcoholica()
        {
            return true;
        }
    }
}
