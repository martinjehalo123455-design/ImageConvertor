namespace ConvertToJPG
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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

        // Pide un archivo al usuario, lo convierte con ProcesarBase64 y guarda el JPG en la misma carpeta
        private void convertirImagenAJpg(string filtro)
        {
            using OpenFileDialog dialogo = new OpenFileDialog();
            dialogo.Title = "Abrir archivo";
            dialogo.Filter = filtro;

            if (dialogo.ShowDialog() != DialogResult.OK) return;
            string origen = dialogo.FileName;
            string destino = Path.ChangeExtension(origen, ".jpg");

            // Pide confirmación antes de reemplazar un JPG existente
            if (File.Exists(destino) &&
                MessageBox.Show($"El archivo {destino} ya existe. ¿Desea reemplazarlo?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            txtContenido.Text = "Origen: " + origen;

            try
            {
                string base64 = Convert.ToBase64String(File.ReadAllBytes(origen));         // Lee el archivo y lo pasa a Base64
                string jpg = ConvertToJpg.ProcesarBase64(base64, Path.GetExtension(origen));             // Convierte a JPG según la extensión del archivo
                File.WriteAllBytes(destino, Convert.FromBase64String(jpg));                 // Guarda el JPG en disco
                MessageBox.Show($"Imagen guardada en {destino}");
                txtContenido.Text += Environment.NewLine;
                txtContenido.Text += "Destino: " + destino;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "No se pudo convertir", MessageBoxButtons.OK, MessageBoxIcon.Error);     // El prefijo va en el título para no duplicarlo
            }
        }
    }
}