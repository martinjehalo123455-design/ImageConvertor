namespace ConvertToPNG
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        public System.ComponentModel.CancelEventHandler openFileDialog1_FileOk = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private OpenFileDialog GetOpenFileDialog11()
        {
            return openFileDialog1;
        }


        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent(OpenFileDialog openFileDialog1)
        {
            this.openFileDialog1 = new OpenFileDialog();
            txtContenido = new TextBox();
            btnAbrir = new Button();
            btnGuardar = new Button();
            
            SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            openFileDialog1.FileOk += openFileDialog1_FileOk;
            // 
            // txtContenido
            // 
            txtContenido.Location = new Point(61, 21);
            txtContenido.Multiline = true;
            txtContenido.Name = "txtContenido";
            txtContenido.ScrollBars = ScrollBars.Vertical;
            txtContenido.Size = new Size(648, 331);
            txtContenido.TabIndex = 0;
            // 
            // btnAbrir
            // 
            btnAbrir.Location = new Point(61, 369);
            btnAbrir.Name = "btnAbrir";
            btnAbrir.Size = new Size(324, 43);
            btnAbrir.TabIndex = 1;
            btnAbrir.Text = "Abrir";
            btnAbrir.UseVisualStyleBackColor = true;
            btnAbrir.Click += btnAbrir_Click;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(391, 369);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(318, 43);
            btnGuardar.TabIndex = 2;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnGuardar);
            Controls.Add(btnAbrir);
            Controls.Add(txtContenido);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private OpenFileDialog openFileDialog1;
        private TextBox txtContenido;
        private Button btnAbrir;
        private Button btnGuardar;
    }
}
