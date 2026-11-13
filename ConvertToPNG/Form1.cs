using PDFtoImage;   // Utiliza SkiaSharp internamente para procesar PDF pero es una librería independiente
using SkiaSharp;    // Extención de NuGET para procesamiento de imagenes 2D
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace ConvertToJPG
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // (PDFtoImage) Convierte PDF a JPG
        private void convertirPDF(object sender, EventArgs e)
        {
            string filtro = "Archivos PDF (*.pdf)|*.pdf|Todos los archivos (*.*)|*.*";
            using OpenFileDialog dialogo = new OpenFileDialog();
            dialogo.Title = "Abrir archivo";
            dialogo.Filter = filtro;

            if (dialogo.ShowDialog() != DialogResult.OK) return;
            
            string origen = dialogo.FileName;
            txtContenido.Text = dialogo.FileName;
            
            using FolderBrowserDialog carpeta = new FolderBrowserDialog();
            if (carpeta.ShowDialog() != DialogResult.OK) return;
            
            try
            {
                using FileStream pdf = File.OpenRead(origen);                                       // Lee el archivo PDF como stream para soportar archivos grandes.
                string nombre = Path.GetFileNameWithoutExtension(origen);                           // Obtiene el nombre del archivo sin su extensión.
                string destino = Path.Combine(carpeta.SelectedPath, $"{nombre}_pagina_1.jpg");      // Se crea la ruta donde se va a guardar el JPG.
                
                Conversion.SaveJpeg(destino, pdf, page: 0, options: new(Dpi: 300));                 // Extensión PDFtoImage para convertir el PDF a JPG.
                MessageBox.Show($"Se convirtió la primera página en {destino}");
                // MessageBox.Show($"Se convirtió la primera página en {carpeta.SelectedPath}");       // Un MessageBox de validación.
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo convertir: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // (skiaSharp) Pide una imagen al usuario y la guarda como JPG en la misma carpeta.
        private void convertirImagenAJpg(string filtro)
        {
            using OpenFileDialog dialogo = new OpenFileDialog();
            dialogo.Title = "Abrir imagen";
            dialogo.Filter = filtro;

            if (dialogo.ShowDialog() != DialogResult.OK) return;
            string origen = dialogo.FileName;
            string destino = Path.ChangeExtension(origen, ".jpg");
            txtContenido.Text = origen;

            try
            {
                using SKBitmap? imagen = SKBitmap.Decode(origen);                    // Lee la imagen (en GIF solo el primer cuadro).
                if (imagen == null)
                {
                    MessageBox.Show("El archivo no es una imagen válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using SKBitmap fondo = new SKBitmap(imagen.Width, imagen.Height);   // Imagen nueva del mismo tamaño.
                using (SKCanvas lienzo = new SKCanvas(fondo))
                {
                    lienzo.Clear(SKColors.White);                                   // Pinta el fondo de blanco.
                    lienzo.DrawBitmap(imagen, 0, 0, SKSamplingOptions.Default);     // Dibuja la imagen original encima.
                }

                using SKData jpg = fondo.Encode(SKEncodedImageFormat.Jpeg, 90);     // Codifica la imagen como JPG con calidad 90 (recomendado).
                File.WriteAllBytes(destino, jpg.ToArray());                         // Guarda el JPG en disco.
                MessageBox.Show($"Imagen guardada en {destino}");
                txtContenido.Text += Environment.NewLine;
                txtContenido.Text += destino;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo convertir: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
    }
}