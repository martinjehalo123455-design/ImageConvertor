using PDFtoImage;

//string rutaPDF = "documento.pdf";
namespace ConvertToPNG
{
    public partial class Form1 : Form
    {
        static string fileName = "converion.png";
        // string filtro = "Archivo de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
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
            Conversion.SavePng(fileName, rutaFile, page: 1, options: new(Dpi: 300));
            //using SaveFileDialog dialogo = new SaveFileDialog();
            //dialogo.Title = "Guardar archivo";
            //dialogo.Filter = filtro;
            //dialogo.FileName = fileName;

            //if (dialogo.ShowDialog() == DialogResult.OK)
            //{
            //    File.(dialogo.FileName, txtContenido.Text);
            //    MessageBox.Show("Archivo guardado.");
            //}
        }
    }
}
