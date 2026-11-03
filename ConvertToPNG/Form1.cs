using PDFtoImage;

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

        private void btnAbrir_Click(object sender, EventArgs e)
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

        private void btnConvertir_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(rutaFile))
            {
                MessageBox.Show("Primero abre un archivo PDF.");
                return;
            }

            using FolderBrowserDialog carpeta = new FolderBrowserDialog();
            if (carpeta.ShowDialog() != DialogResult.OK) return;

            try
            {
                byte[] pdf = File.ReadAllBytes(rutaFile);
                int paginas = Conversion.GetPageCount(pdf);
                string nombre = Path.GetFileNameWithoutExtension(rutaFile);

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
    }
}
