using System;
using TextReplacer.Services;
using System.IO;

namespace TextReplacer
{
    // Programa principal de la aplicación.
    internal class Program
    {
        static void Main(string[] args)
        {
            // Se comprueba el número de parámetros, y si la cantidad no es exacta,
            // se manda un mensaje al usuario informando de lo que ocurre, además de
            // como debe ser aspecto de la llamada. Finalmente, se cierra la ejecución.
            if (args.Length != 4)
            {
                Console.WriteLine("Número de parámetros incorrecto.");
                Console.WriteLine("Uso: --project TextReplacer.csproj <fichero_origen> <fichero_destino> <texto_buscar> <texto_reemplazo>");
                return;
            }

            // Se asignan los cuatro parámetros solicitados.
            string ficheroOrigen = args[0];
            string ficheroDestino = args[1];
            string textoBusqueda = args[2];
            string textoReemplazo = args[3];

            try
            {
                // Se comprueba la existencia de los ficheros origen y destino.
                // Si alguno de ellos no existe, se informa al usuario y se cierra la ejecución.
                if (!File.Exists(ficheroOrigen) && !File.Exists(ficheroDestino))
                {
                    Console.WriteLine("No existe los ficheros origen y destino.");
                    return;
                }

                if (!File.Exists(ficheroOrigen))
                {
                    Console.WriteLine("No existe el fichero origen.");
                    return;
                }

                if (!File.Exists(ficheroDestino))
                {
                    Console.WriteLine("No existe el fichero destino.");
                    return;
                }

                // Se crea una instancia del servicio de reemplazo de texto y se llama
                // al método que realiza el procesamiento. El código de TextReplacerService
                // se encuentra en la carpeta Services, dentro del proyecto.
                var servicio = new TextReplacerService();

                // Se procesa el fichero y se obtiene el número de reemplazos realizados.
                int reemplazos = servicio.ProcessFile(ficheroOrigen, ficheroDestino, textoBusqueda, textoReemplazo);

                // Finalmente, si no se ha realizado ningún reemplazo, se informa al usuario
                // de que el texto que se quiso buscar no ha aparecido en el fichero origen.
                if (reemplazos == 0)
                {
                    Console.WriteLine($"No se ha encontrado el texto \"{textoBusqueda}\" en el fichero de origen.");
                }

                // En caso contrario, se indica el número de reemplazos realizados.
                else
                {
                    Console.WriteLine($"Se han hecho {reemplazos} reemplazos.");
                }
            }

            // Se capturan las posibles excepciones que se pueden producir durante
            // la ejecución del programa, informando al usuario de lo que ha ocurrido.

            // Excepción lanzada si no se tienen permisos para acceder o crear
            // el fichero destino.
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("No hay permisos para acceder o crear el fichero destino.");
            }

            // Captura genérica de cualquier otra excepción no prevista.
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}