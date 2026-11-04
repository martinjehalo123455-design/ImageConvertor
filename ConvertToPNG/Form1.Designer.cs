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
            button1 = new Button();
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
            btnAbrir.Location = new Point(61, 165);
            btnAbrir.Name = "btnConvertitPDF";
            btnAbrir.Size = new Size(324, 43);
            btnAbrir.TabIndex = 1;
            btnAbrir.Text = "PDF";
            btnAbrir.UseVisualStyleBackColor = true;
            btnAbrir.Click += convertirPDF;
            // 
            // btnConvertirJPG
            // 
            button1.Location = new Point(391, 165);
            button1.Name = "btnConvertirJPG";
            button1.Size = new Size(324, 43);
            button1.TabIndex = 2;
            button1.Text = "JPG";
            button1.UseVisualStyleBackColor = true;
            button1.Click += convertirJPG;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button1);
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
        private Button button1;
    }
}
