namespace ConvertToPNG
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtContenido = new TextBox();
            btnAbrir = new Button();
            // btnConvertir = new Button();
            SuspendLayout();
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
            btnAbrir.Name = "btnConvertirPDF";
            btnAbrir.Size = new Size(324, 43);
            btnAbrir.TabIndex = 1;
            btnAbrir.Text = "PDF";
            btnAbrir.UseVisualStyleBackColor = true;
            btnAbrir.Click += convertirPDF;
            // 
            // btnConvertir
            // 
            //btnConvertir.Location = new Point(391, 369);
            //btnConvertir.Name = "btnConvertir";
            //btnConvertir.Size = new Size(318, 43);
            //btnConvertir.TabIndex = 2;
            //btnConvertir.Text = "Convertir";
            //btnConvertir.UseVisualStyleBackColor = true;
            //btnConvertir.Click += btnConvertir_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnConvertir);
            Controls.Add(btnAbrir);
            Controls.Add(txtContenido);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtContenido;
        private Button btnAbrir;
        private Button btnConvertir;
    }
}
