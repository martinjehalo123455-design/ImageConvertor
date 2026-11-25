using PDFtoImage;   // Extension para convertir PDF a JPG
using SkiaSharp;    // Extension para convertir archivos PNG, BMP y GIF a JPG

// Clase para convertir imagen a JPG
namespace ConvertToJPG
{
    internal class ConvertToJpg
    {
        // Método que recibe una imagen en Base64 y su formato (.png, .bmp, .gif o .pdf) y devuelve el JPG en Base64
        public static string ProcesarBase64(string bitsImagen, string formato)
        {
            // Acepta ".PNG", "png", etc.
            string extension = (formato ?? "").Trim().TrimStart('.').ToLowerInvariant();                        

            // Ya es JPG, devuelve el mismo Base64
            if (extension == "jpg" || extension == "jpeg") return bitsImagen;                                  

            try
            {
                // Decodifica el Base64 a bytes
                byte[] datos = Convert.FromBase64String(bitsImagen);                                                        

                using SKBitmap? imagen = extension switch
                {
                    "png" or "bmp" or "gif" => SKBitmap.Decode(datos),                                                      // En GIF solo el primer cuadro
                    "pdf" => Conversion.ToImage(new MemoryStream(datos), page: 0, options: new(Dpi: 300)),                  // Solo la primera página (page:0)
                    _ => throw new ArgumentException("Formato no válido. Solo se aceptan formatos: .png, .bmp, .gif o .pdf")
                };

                if (imagen == null) throw new ArgumentException("El contenido no es una imagen válida.");

                using SKBitmap fondo = new SKBitmap(imagen.Width, imagen.Height);
                using (SKCanvas lienzo = new SKCanvas(fondo))
                {
                    // Fondo blanco (JPG no tiene transparencia)
                    lienzo.Clear(SKColors.White);                                                                             
                    lienzo.DrawBitmap(imagen, 0, 0, SKSamplingOptions.Default);
                }
               
                using SKData jpg = fondo.Encode(SKEncodedImageFormat.Jpeg, 90);
                
                // Devuelve el JPG en Base64
                return Convert.ToBase64String(jpg.ToArray());
            } 
            catch(Exception ex) when (ex is not ArgumentException)
            {
                throw new InvalidOperationException($"No se pudo convertir: {ex.Message}", ex);
            }                                                 
        }
    }
}
