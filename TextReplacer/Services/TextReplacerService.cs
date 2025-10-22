using System;
using System.IO;
using System.Text;

namespace TextReplacer.Services
{
    // Servicio que realiza el reemplazo de texto en los ficheros.
    public class TextReplacerService
    {
        // Método que procesa el fichero de origen, reemplazando todas las ocurrencias del
        // texto de búsqueda por el texto de reemplazo. El método devuelve el número de
        // reemplazos realizados.
        public int ProcessFile(string ficheroOrigen, string ficheroDestino, string textoBusqueda, string textoReemplazo)
        {
            // Se lee el contenido del fichero de origen.
            string contenido = File.ReadAllText(ficheroOrigen, Encoding.UTF8);

            // El número de reemplazos realizados.
            int reemplazos = 0;

            // Se realiza el reemplazo de todas las ocurrencias del texto de búsqueda por el texto de reemplazo,
            // sin distinguir mayúsculas y minúsculas. Se usa un bucle para asegurarse de que se reemplazan todas
            // las ocurrencias. Esto es necesario porque el método IndexOf no permite especificar una comparación
            // sin distinguir mayúsculas y minúsculas directamente en el método Replace.  Por ello, se busca la
            // posición de cada ocurrencia y se realiza el reemplazo manualmente.
            for (int i = 0; i < 2; i++)
            {
                int indice = 0;

                // Mientras se encuentren ocurrencias del texto de búsqueda, se usa StringComparison.OrdinalIgnoreCase
                // para realizar una comparación sin distinguir mayúsculas y minúsculas.

                // El bucle continúa hasta que no se encuentren más ocurrencias.
                while ((indice = contenido.IndexOf(textoBusqueda, indice, StringComparison.OrdinalIgnoreCase)) != -1)
                {
                    // Si se encuentra una ocurrencia, se realiza el reemplazo...
                    contenido = contenido.Remove(indice, textoBusqueda.Length).Insert(indice, textoReemplazo);

                    // ...y se actualiza el índice para continuar la búsqueda.
                    indice += textoReemplazo.Length;

                    // Se incrementa el contador de reemplazos realizados.
                    reemplazos++;
                }
            }

            // Se escribe el contenido modificado en el fichero de destino.
            File.WriteAllText(ficheroDestino, contenido, Encoding.UTF8);
            return reemplazos;
        }
    }
}
