using PDFtoImage;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
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
                byte[] pdf = File.ReadAllBytes(origen);                         // Lee el archivo en bites
                int paginas = Conversion.GetPageCount(pdf);                     // Obtiene el numero de páginas que contiene el archivo
                string nombre = Path.GetFileNameWithoutExtension(origen);       // Obtiene el nombre del archivo sin su extención

                for (int i = 0; i < paginas; i++)
                {
                    string destino = Path.Combine(carpeta.SelectedPath, $"{nombre}_pagina{i + 1}.png");
                    Conversion.SavePng(destino, pdf, page: i, options: new(Dpi: 300));
                }

                MessageBox.Show($"Se convirtieron {paginas} página(s) en {carpeta.SelectedPath}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo convertir: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Convierte JPG a PNG
        private void convertirJpgAPng(object sender, EventArgs e)
        {
            using OpenFileDialog dialogo = new OpenFileDialog();
            dialogo.Title = "Abrir imagen";
            dialogo.Filter = "Imágenes JPG (*.jpg;*.jpeg)|*.jpg;*.jpeg|Todos los archivos (*.*)|*.*";
            
            if (dialogo.ShowDialog() != DialogResult.OK) return;
            string origen = dialogo.FileName;
            string destino = Path.ChangeExtension(origen, ".png");
            txtContenido.Text = origen;

            try
            {
                using SixLabors.ImageSharp.Image image = SixLabors.ImageSharp.Image.Load(origen);
                image.Save(destino);
                MessageBox.Show($"Imagen guardada en {destino}");
                txtContenido.Text += Environment.NewLine;
                txtContenido.Text += destino;
            }catch(Exception ex)
            {
                MessageBox.Show($"No se pudo convertir: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}