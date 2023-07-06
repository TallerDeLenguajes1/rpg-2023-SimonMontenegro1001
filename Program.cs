using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

namespace EspacioApi
{
    internal class Program
    {
        static readonly HttpClient client = new HttpClient();

        static async Task Main()
        {
            Console.Clear();
            bool salir = false;

            while (!salir)
            {
                Console.WriteLine("Seleccione una operación:");
                Console.WriteLine("1. Simplificar expresión");
                Console.WriteLine("2. Factorizar expresión");
                Console.WriteLine("3. Derivar expresión");
                Console.WriteLine("4. Integrar expresión");
                Console.WriteLine("5. Encontrar ceros de la función");
                Console.WriteLine("6. Calcular la tangente");
                Console.WriteLine("7. Calcular área bajo la curva");
                Console.WriteLine("8. Calcular logaritmo");
                Console.WriteLine("9. Salir");

                Console.Write("Ingrese el número de la operación deseada: ");
                string opcion = Console.ReadLine();

                Console.Clear();
                string operacion = "";
                switch (opcion)
                {
                    case "1":
                        Console.WriteLine(" ejemplo de uso : (entrada ->) x+x = (salida ->) 2 x ");
                        operacion = "simplify";
                        break;
                    case "2":
                        Console.WriteLine(" ejemplo de uso : (entrada ->) x^2 + 2x = (salida ->) x (x + 2)");
                        operacion = "factor";
                        break;
                    case "3":
                        Console.WriteLine(" ejemplo de uso : (entrada ->) x^2 + 2x = (salida ->) 2 x + 2 ");
                        operacion = "derive";
                        break;
                    case "4":
                        Console.WriteLine(" ejemplo de uso : (entrada ->) x^2 + 2x = (salida ->) 1/3 x^3 + x^2 ");
                        operacion = "integrate";
                        break;
                    case "5":
                        Console.WriteLine(" ejemplo de uso : (entrada ->) x^2 + 2x = (salida ->) [-2, 0]");
                        operacion = "zeroes";
                        break;
                    case "6":
                        Console.WriteLine(" ejemplo de uso : (entrada ->) 2|x^3 = (salida ->) 12 x + -16 ");
                        operacion = "tangent";
                        break;
                    case "7":
                        Console.WriteLine(" ejemplo de uso : (entrada ->) 2:4|x^3 = (salida ->) 60 ");
                        operacion = "area";
                        break;
                    case "8":
                        Console.WriteLine(" ejemplo de uso : (entrada ->) 2|8 = (salida ->) 3 ");
                        operacion = "log";
                        break;
                    case "9":
                        salir = true;
                        Console.WriteLine("¡Hasta luego!");
                        break;
                    default:
                        Console.WriteLine("Opción inválida. Por favor, ingrese un número válido.");
                        break;
                }

                if (!salir)
                {
                    Console.WriteLine();
                    Console.Write("Ingrese la expresión: ");
                    string expresion = Console.ReadLine();

                    await Calcular(expresion, operacion);

                    Console.WriteLine();
                }
            }
        }

        static async Task Calcular(string expresion, string operacion)
        {
            try
            {
                using HttpResponseMessage response = await client.GetAsync("https://newton.now.sh/api/v2/" + operacion + "/" + HttpUtility.UrlEncode(expresion));
                response.EnsureSuccessStatusCode();
                using Stream strReader = await response.Content.ReadAsStreamAsync();
                using StreamReader objReader = new StreamReader(strReader);
                string responseBody = await objReader.ReadToEndAsync();
                Ecuaciones ecuacion = JsonSerializer.Deserialize<Ecuaciones>(responseBody);
                Console.WriteLine("");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ecuacion.result);
                Console.ResetColor();
                Console.WriteLine($"\nPresione una tecla para continuar...");
                Console.ReadKey();
                Console.Clear();
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message: {0}", e.Message);
            }
        }
    }
}
