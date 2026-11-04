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
            btnConvertirPDF = new Button();
            btnConvertirJPG = new Button();
            SuspendLayout();
            // 
            // txtContenido
            // 
            txtContenido.Location = new Point(61, 21);
            txtContenido.Multiline = true;
            txtContenido.Name = "txtContenido";
            txtContenido.ScrollBars = ScrollBars.Vertical;
            txtContenido.Size = new Size(654, 128);
            txtContenido.TabIndex = 0;
            // 
            // btnConvertirPDF
            // 
            btnConvertirPDF.Location = new Point(61, 165);
            btnConvertirPDF.Name = "btnConvertitPDF";
            btnConvertirPDF.Size = new Size(324, 43);
            btnConvertirPDF.TabIndex = 1;
            btnConvertirPDF.Text = "PDF";
            btnConvertirPDF.UseVisualStyleBackColor = true;
            btnConvertirPDF.Click += convertirPDF;
            // 
            // btnConvertirJPG
            // 
            btnConvertirJPG.Location = new Point(391, 165);
            btnConvertirJPG.Name = "btnConvertirJPG";
            btnConvertirJPG.Size = new Size(324, 43);
            btnConvertirJPG.TabIndex = 2;
            btnConvertirJPG.Text = "JPG";
            btnConvertirJPG.UseVisualStyleBackColor = true;
            btnConvertirJPG.Click += convertirJpgAPng;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnConvertirJPG);
            Controls.Add(btnConvertirPDF);
            Controls.Add(txtContenido);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtContenido;
        private Button btnConvertirPDF;
        private Button btnConvertir;    // Boton en estado StandBy por el momento, no se usa a propósito, ignoralo.
        private Button btnConvertirJPG;
    }
}
