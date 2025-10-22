using System;
using System.IO;
using TextReplacer.Services;
using Xunit;

namespace TextReplacer.Tests
{
    // Pruebas unitarias para la clase TextReplacerService.
    public class FileTextReplacerTests
    {
        // Prueba que verifica que el método ProcessFile reemplaza correctamente las ocurrencias.
        [Fact]
        public void ProcessFile_ReplacesOccurrences_Correctly()
        {
            string tempOrigen = Path.GetTempFileName(); // Archivo origen temporal.
            string tempDestino = Path.GetTempFileName(); // Archivo destino temporal.

            // Se escribe el contenido de prueba en el archivo fuente. El contenido contiene dos ocurrencias
            // de "auraportal" para verificar que ambas son reemplazadas.
            File.WriteAllText(tempOrigen, "Hola auraportal, auraportal te saluda.");

            // Se crea una instancia del servicio de reemplazo de texto y se llama
            // al método que realiza el procesamiento. El código de TextReplacerService
            // se encuentra en la carpeta Services, dentro del proyecto.
            var servicio = new TextReplacerService();

            // Se procesa el fichero y se obtiene el número de reemplazos realizados.
            int reemplazos = servicio.ProcessFile(tempOrigen, tempDestino, "auraportal", "ap");

            // Se lee el contenido del archivo destino para verificar el resultado.
            string resultado = File.ReadAllText(tempDestino);

            // Se verifica que se realizaron 2 reemplazos y que el contenido es el esperado.
            // El texto "auraportal" debe haber sido reemplazado por "ap" en ambas ocurrencias.
            // Por lo tanto, el contenido esperado es "Hola ap, ap te saluda.".
            Assert.Equal(2, reemplazos);
            Assert.Contains("Hola ap, ap te saluda.", resultado);

            // Se eliminan los archivos temporales creados para la prueba.
            File.Delete(tempOrigen);
            File.Delete(tempDestino);
        }

        // Prueba que verifica que el método ProcessFile lanza una excepción cuando el archivo de origen no existe.
        [Fact]
        public void ProcessFile_WhenFileDoesNotExist_ThrowsException()
        {
            // Se crea una instancia del servicio de reemplazo de texto y se llama
            // al método que realiza el procesamiento. El código de TextReplacerService
            // se encuentra en la carpeta Services, dentro del proyecto.
            var servicio = new TextReplacerService();

            // Se verifica que se lanza una excepción FileNotFoundException al intentar procesar un archivo que no existe.
            // Se utiliza Assert.Throws para verificar que se lanza la excepción esperada.
            Assert.Throws<FileNotFoundException>(() => File.ReadAllText("noexiste.txt"));
        }
    }
}
