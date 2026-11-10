using PDFtoImage;
using SkiaSharp;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace ConvertToPNG
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Convierte PDF a PNG
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
                byte[] pdf = File.ReadAllBytes(origen);                         // Lee el archivo PDF en bites
                int paginas = Conversion.GetPageCount(pdf);                     // Obtiene el numero de páginas que contiene el archivo
                string nombre = Path.GetFileNameWithoutExtension(origen);       // Obtiene el nombre del archivo sin su extensión

                string destino = Path.Combine(carpeta.SelectedPath, $"{nombre}_pagina_1.png");
                Conversion.SavePng(destino, pdf, page: 1, options: new(Dpi: 300));

                //for (int i = 0; i < paginas; i++)
                //{
                //    string destino = Path.Combine(carpeta.SelectedPath, $"{nombre}_pagina{i + 1}.png");
                //    Conversion.SavePng(destino, pdf, page: i, options: new(Dpi: 300));
                //}

                // MessageBox.Show($"Se convirtieron {paginas} página(s) en {carpeta.SelectedPath}");
                MessageBox.Show($"Se convirtió la primera página en {carpeta.SelectedPath}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo convertir: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Convierte JPG a PNG
        private void convertirJpgAPng(object sender, EventArgs e)
        {
            convertirImagenAPng("Imágenes JPG (*.jpg;*.jpeg)|*.jpg;*.jpeg|Todos los archivos (*.*)|*.*");
        }

        // Convierte BMP a PNG
        private void convertirBmpAPng(object sender, EventArgs e)
        {
            convertirImagenAPng("Imágenes BMP (*.bmp)|*.bmp|Todos los archivos (*.*)|*.*");
        }

        // Convierte GIF a PNG (solo el primer cuadro si es animado)
        private void convertirGifAPng(object sender, EventArgs e)
        {
            convertirImagenAPng("Imágenes GIF (*.gif)|*.gif|Todos los archivos (*.*)|*.*");
        }

        // Pide una imagen al usuario y la guarda como PNG en la misma carpeta
        private void convertirImagenAPng(string filtro)
        {
            using OpenFileDialog dialogo = new OpenFileDialog();
            dialogo.Title = "Abrir imagen";
            dialogo.Filter = filtro;

            if (dialogo.ShowDialog() != DialogResult.OK) return;
            string origen = dialogo.FileName;
            string destino = Path.ChangeExtension(origen, ".png");
            txtContenido.Text = origen;

            try
            {
                using SKBitmap? imagen = SKBitmap.Decode(origen);                   // Lee la imagen (en GIF solo el primer cuadro)
                if (imagen == null)
                {
                    MessageBox.Show("El archivo no es una imagen válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using SKData png = imagen.Encode(SKEncodedImageFormat.Png, 100);   // Codifica la imagen como PNG
                File.WriteAllBytes(destino, png.ToArray());                         // Guarda el PNG en disco
                MessageBox.Show($"Imagen guardada en {destino}");
                txtContenido.Text += Environment.NewLine;
                txtContenido.Text += destino;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo convertir: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}