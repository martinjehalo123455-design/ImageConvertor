using PDFtoImage;   // Extension para convertir PDF a JPG
using SkiaSharp;    // Extension para convertir archivos PNG, BMP y GIF a JPG

namespace ConvertToJPG
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Recibe una imagen en Base64 y su formato (.jpg, .png, .bmp, .gif o .pdf) y devuelve el JPG en Base64
        public string procesarImagen(string bitsImagen, string formato)
        {
            string extension = (formato ?? "").Trim().TrimStart('.').ToLowerInvariant();                  // Acepta ".PNG", "png", etc.

            if (extension == "jpg") return bitsImagen;                                                      // Ya es JPG, devuelve el mismo Base64

            byte[] datos = Convert.FromBase64String(bitsImagen);                                            // Decodifica el Base64 a bytes

            using SKBitmap? imagen = extension switch
            {
                "png" or "bmp" or "gif" => SKBitmap.Decode(datos),                                          // En GIF solo el primer cuadro
                "pdf" => Conversion.ToImage(new MemoryStream(datos), page: 0, options: new(Dpi: 300)),      // Solo la primera página
                _ => throw new ArgumentException("Formato no válido. Solo se aceptan: .jpg, .png, .bmp, .gif o .pdf")
            };

            if (imagen == null) throw new ArgumentException("El contenido no es una imagen válida.");

            using SKBitmap fondo = new SKBitmap(imagen.Width, imagen.Height);
            using (SKCanvas lienzo = new SKCanvas(fondo))
            {
                lienzo.Clear(SKColors.White);                                                               // Fondo blanco (JPG no tiene transparencia)
                lienzo.DrawBitmap(imagen, 0, 0, SKSamplingOptions.Default);
            }

            using SKData jpg = fondo.Encode(SKEncodedImageFormat.Jpeg, 90);
            return Convert.ToBase64String(jpg.ToArray());                                                   // Devuelve el JPG en Base64
        }

            

        // Convierte PDF a JPG (solo la primera página)
        private void convertirPDF(object sender, EventArgs e)
        {
            convertirImagenAJpg("Archivos PDF (*.pdf)|*.pdf|Todos los archivos (*.*)|*.*");
        }

        // Convierte PNG a JPG
        private void convertirPngAJpg(object sender, EventArgs e)
        {
            convertirImagenAJpg("Imágenes PNG (*.png;)|*.png|Todos los archivos (*.*)|*.*");
        }

        // Convierte BMP a JPG
        private void convertirBmpAJpg(object sender, EventArgs e)
        {
            convertirImagenAJpg("Imágenes BMP (*.bmp)|*.bmp|Todos los archivos (*.*)|*.*");
        }

        // Convierte GIF a JPG (solo el primer cuadro si es animado)
        private void convertirGifAJpg(object sender, EventArgs e)
        {
            convertirImagenAJpg("Imágenes GIF (*.gif)|*.gif|Todos los archivos (*.*)|*.*");
        }

        // Pide un archivo al usuario, lo convierte con procesarImagen y guarda el JPG en la misma carpeta
        private void convertirImagenAJpg(string filtro)
        {
            using OpenFileDialog dialogo = new OpenFileDialog();
            dialogo.Title = "Abrir archivo";
            dialogo.Filter = filtro;

            if (dialogo.ShowDialog() != DialogResult.OK) return;
            string origen = dialogo.FileName;
            string destino = Path.ChangeExtension(origen, ".jpg");
            txtContenido.Text = "Origen: " + origen;

            try
            {
                string base64 = Convert.ToBase64String(File.ReadAllBytes(origen));         // Lee el archivo y lo pasa a Base64
                string jpg = procesarImagen(base64, Path.GetExtension(origen));             // Convierte a JPG según la extensión del archivo
                File.WriteAllBytes(destino, Convert.FromBase64String(jpg));                 // Guarda el JPG en disco
                MessageBox.Show($"Imagen guardada en {destino}");
                txtContenido.Text += Environment.NewLine;
                txtContenido.Text += "Destino: " + destino;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo convertir: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}