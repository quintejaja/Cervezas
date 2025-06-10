using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Cervezas
{
    class Program
    {
        static void Main(string[] args)
        {
                List<Bebida> bebidas = new List<Bebida>()
            {
                new Cerveza("Imperial", 6, 5, "Imperial S.A."),
                new Vino("Malbec Reserva", 4, 14),
                new Gaseosa("Coca Cola", 10),
                new Cerveza("Quilmes", 8, 4, "Quilmes S.A."),
                new Cerveza("Imperial", 3, 5, "Imperial S.A."), // duplicada
                new Vino("Malbec Reserva", 2, 14),               // duplicado
                new Gaseosa("Sprite", 5),
                new Gaseosa("Coca Cola", 8),                     // duplicada
                null                                             // Error simulado
            };

            Console.WriteLine("Bebidas cargadas:");
            foreach (var bebida in bebidas)
            {
                if (bebida != null)
                {
                    bebida.MostrarInformacion();

                    if (bebida is IBebidaAlcoholica)
                        Console.WriteLine("-> Esta bebida es alcohólica.");
                    else
                        Console.WriteLine("-> Esta bebida no es alcohólica.");
                }
                else
                {
                    Console.WriteLine("-> Bebida nula encontrada.");
                }

                Console.WriteLine();
            }

            var bebidasConTipo = new List<object>();
            foreach (var bebida in bebidas)
            {
                if (bebida == null) continue;

                if (bebida is Cerveza c)
                    bebidasConTipo.Add(new { Tipo = "Cerveza", c.Nombre, c.Cantidad, c.Alcohol, c.Marca });
                else if (bebida is Vino v)
                    bebidasConTipo.Add(new { Tipo = "Vino", v.Nombre, v.Cantidad, v.Alcohol });
                else if (bebida is Gaseosa g)
                    bebidasConTipo.Add(new { Tipo = "Gaseosa", g.Nombre, g.Cantidad });
            }

            string json = JsonSerializer.Serialize(bebidasConTipo, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("bebidas.json", json);
            Console.WriteLine("Archivo bebidas.json guardado correctamente.\n");

            //lectura y deserialización
            string jsonLeido = File.ReadAllText("bebidas.json");

            var bebidasDesdeArchivo = JsonSerializer.Deserialize<List<Dictionary<string, JsonElement>>>(jsonLeido);

            List<Bebida> bebidasFinales = new List<Bebida>();
            int errores = 0;

            foreach (var bebidaData in bebidasDesdeArchivo)
            {
                try
                {
                    string tipo = bebidaData["Tipo"].GetString();
                    string nombre = bebidaData["Nombre"].GetString();
                    int cantidad = bebidaData["Cantidad"].GetInt32();

                    if (tipo == "Cerveza")
                    {
                        int alcohol = bebidaData["Alcohol"].GetInt32();
                        string marca = bebidaData["Marca"].GetString();
                        bebidasFinales.Add(new Cerveza(nombre, cantidad, alcohol, marca));
                    }
                    else if (tipo == "Vino")
                    {
                        int alcohol = bebidaData["Alcohol"].GetInt32();
                        bebidasFinales.Add(new Vino(nombre, cantidad, alcohol));
                    }
                    else if (tipo == "Gaseosa")
                    {
                        bebidasFinales.Add(new Gaseosa(nombre, cantidad));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al deserializar bebida: " + ex.Message);
                    errores++;
                }
            }

            CervezaBD conexionBD = new CervezaBD();
            int procesadas = bebidasFinales.Count;
            int insertadas = 0;
            int duplicadas = 0;

            HashSet<string> bebidasInsertadas = new HashSet<string>();

            foreach (var bebida in bebidasFinales)
            {
                string key;

                if (bebida is Cerveza cerveza)
                    key = cerveza.Nombre.ToLower() + "_" + cerveza.Marca.ToLower();
                else
                    key = bebida.Nombre.ToLower();

                if (!bebidasInsertadas.Contains(key))
                {
                    conexionBD.InsertarBebida(bebida);
                    bebidasInsertadas.Add(key);
                    insertadas++;
                }
                else
                {
                    duplicadas++;
                }
            }

            Console.WriteLine("\nResumen del proceso:");
            Console.WriteLine("Total bebidas procesadas: " + procesadas);
            Console.WriteLine("Insertadas: " + insertadas);
            Console.WriteLine("Duplicadas: " + duplicadas);
            Console.WriteLine("Errores en deserialización: " + errores);

            Console.ReadKey();
        }

    }
}
