using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervezas
{
    public interface IBebidaAlcoholica
    {
        int Alcohol { get; set; }
        void MaximoRecomendado();
        bool EsAlcoholica();
    }
}
