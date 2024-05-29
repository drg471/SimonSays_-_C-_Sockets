namespace Tarea3_SS_Servidor
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
            txtSalidaMensajes = new TextBox();
            btnConectar = new Button();
            btnDesconectar = new Button();
            lblTitulo = new Label();
            lblMensajes = new Label();
            SuspendLayout();
            // 
            // txtSalidaMensajes
            // 
            txtSalidaMensajes.BackColor = SystemColors.Desktop;
            txtSalidaMensajes.BorderStyle = BorderStyle.FixedSingle;
            txtSalidaMensajes.ForeColor = Color.Aqua;
            txtSalidaMensajes.Location = new Point(150, 99);
            txtSalidaMensajes.Multiline = true;
            txtSalidaMensajes.Name = "txtSalidaMensajes";
            txtSalidaMensajes.Size = new Size(609, 265);
            txtSalidaMensajes.TabIndex = 0;
            // 
            // btnConectar
            // 
            btnConectar.BackColor = SystemColors.ActiveCaptionText;
            btnConectar.FlatStyle = FlatStyle.Flat;
            btnConectar.Font = new Font("Consolas", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btnConectar.ForeColor = Color.Aqua;
            btnConectar.Location = new Point(500, 391);
            btnConectar.Name = "btnConectar";
            btnConectar.Size = new Size(229, 41);
            btnConectar.TabIndex = 1;
            btnConectar.Text = "CONECTAR";
            btnConectar.UseVisualStyleBackColor = false;
            btnConectar.Click += btnConectar_Click;
            // 
            // btnDesconectar
            // 
            btnDesconectar.BackColor = SystemColors.ActiveCaptionText;
            btnDesconectar.FlatStyle = FlatStyle.Flat;
            btnDesconectar.Font = new Font("Consolas", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btnDesconectar.ForeColor = Color.Aqua;
            btnDesconectar.Location = new Point(181, 391);
            btnDesconectar.Name = "btnDesconectar";
            btnDesconectar.Size = new Size(229, 41);
            btnDesconectar.TabIndex = 2;
            btnDesconectar.Text = "DESCONECTAR";
            btnDesconectar.UseVisualStyleBackColor = false;
            btnDesconectar.Click += btnDesconectar_Click;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.BackColor = Color.Transparent;
            lblTitulo.Font = new Font("Consolas", 18F, FontStyle.Bold, GraphicsUnit.Point);
            lblTitulo.ForeColor = Color.Aqua;
            lblTitulo.Location = new Point(232, 19);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(447, 36);
            lblTitulo.TabIndex = 3;
            lblTitulo.Text = "SERVIDOR - Juego Simon says";
            // 
            // lblMensajes
            // 
            lblMensajes.AutoSize = true;
            lblMensajes.BackColor = Color.Transparent;
            lblMensajes.Font = new Font("Consolas", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            lblMensajes.ForeColor = Color.Aqua;
            lblMensajes.Location = new Point(150, 73);
            lblMensajes.Name = "lblMensajes";
            lblMensajes.Size = new Size(90, 22);
            lblMensajes.TabIndex = 5;
            lblMensajes.Text = "MENSAJES";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.bg2;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(918, 467);
            Controls.Add(lblMensajes);
            Controls.Add(lblTitulo);
            Controls.Add(btnDesconectar);
            Controls.Add(btnConectar);
            Controls.Add(txtSalidaMensajes);
            Name = "Form1";
            Text = "SERVIDOR - juego simon says";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtSalidaMensajes;
        private Button btnConectar;
        private Button btnDesconectar;
        private Label lblTitulo;
        private Label lblMensajes;
    }
}