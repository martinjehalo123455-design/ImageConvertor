namespace ConvertToJPG
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
            btnConvertirPNG = new Button();
            btnConvertirBMP = new Button();
            btnConvertirGIF = new Button();
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
            // btnConvertirPNG
            // 
            btnConvertirPNG.Location = new Point(391, 165);
            btnConvertirPNG.Name = "btnConvertirPNG";
            btnConvertirPNG.Size = new Size(324, 43);
            btnConvertirPNG.TabIndex = 2;
            btnConvertirPNG.Text = "PNG";
            btnConvertirPNG.UseVisualStyleBackColor = true;
            btnConvertirPNG.Click += convertirPngAJpg;
            //
            // btnConvertirBMP
            //
            btnConvertirBMP.Location = new Point(61, 214);
            btnConvertirBMP.Name = "btnConvertirBMP";
            btnConvertirBMP.Size = new Size(324, 43);
            btnConvertirBMP.TabIndex = 3;
            btnConvertirBMP.Text = "BMP";
            btnConvertirBMP.UseVisualStyleBackColor = true;
            btnConvertirBMP.Click += convertirBmpAJpg;
            //
            // btnConvertirGIF
            //
            btnConvertirGIF.Location = new Point(391, 214);
            btnConvertirGIF.Name = "btnConvertirGIF";
            btnConvertirGIF.Size = new Size(324, 43);
            btnConvertirGIF.TabIndex = 4;
            btnConvertirGIF.Text = "GIF";
            btnConvertirGIF.UseVisualStyleBackColor = true;
            btnConvertirGIF.Click += convertirGifAJpg;
            //
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnConvertirGIF);
            Controls.Add(btnConvertirBMP);
            Controls.Add(btnConvertirPNG);
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
        private Button btnConvertirPNG;
        private Button btnConvertirBMP;
        private Button btnConvertirGIF;
    }
}
