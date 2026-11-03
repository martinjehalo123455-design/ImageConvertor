using PDFtoImage;

//string rutaPDF = "documento.pdf";
namespace ConvertToPNG
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnAbrir_Click(object sender, EventArgs e)
        {
            using OpenFileDialog dialogo = new OpenFileDialog();
            dialogo.Title = "Abrir archivo";
            dialogo.Filter = "Archivo de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";

            if (dialogo.ShowDialog() == DialogResult.OK)
            {
                txtContenido.Text = dialogo.FileName;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            using SaveFileDialog dialogo = new SaveFileDialog();
            dialogo.Title = "Guardar archivo";
            dialogo.Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
            dialogo.FileName = "nota.txt";

            if (dialogo.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(dialogo.FileName, txtContenido.Text);
                MessageBox.Show("Archivo guardado.");
            }
        }
    }
}
