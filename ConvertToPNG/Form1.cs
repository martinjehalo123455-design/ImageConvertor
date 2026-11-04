using PDFtoImage;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

//string rutaPDF = "documento.pdf";
namespace ConvertToPNG
{
    public partial class Form1 : Form
    {
        string filtro = "Archivos PDF (*.pdf)|*.pdf|Todos los archivos (*.*)|*.*";
        string rutaFile = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void showOpenFileDialog()
        {
            using OpenFileDialog dialogo = new OpenFileDialog();
            dialogo.Title = "Abrir archivo";
            dialogo.Filter = filtro;

            if (dialogo.ShowDialog() == DialogResult.OK)
            {
                txtContenido.Text = dialogo.FileName;
                rutaFile = dialogo.FileName;
            }
        }

        private void convertirPDF(object sender, EventArgs e)
        {
            showOpenFileDialog();

            if (string.IsNullOrEmpty(rutaFile))
            {
                MessageBox.Show("Primero abre un archivo PDF.");
                return;
            }

            using FolderBrowserDialog carpeta = new FolderBrowserDialog();
            if (carpeta.ShowDialog() != DialogResult.OK) return;
            try
            {
                byte[] pdf = File.ReadAllBytes(rutaFile);                       // Lee el archivo en bites
                int paginas = Conversion.GetPageCount(pdf);                     // Obtiene el numero de páginas que contiene el archivo
                string nombre = Path.GetFileNameWithoutExtension(rutaFile);     // Obtiene el nombre del archivo sin su extención

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

        private void convertirJPG(object sender, EventArgs e)
        {

        }
    }
}