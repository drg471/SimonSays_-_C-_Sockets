namespace Tarea3_SS_Cliente
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
            btnIniciar = new Button();
            lblSalida = new Label();
            pbRed = new PictureBox();
            pbBlue = new PictureBox();
            pbGreen = new PictureBox();
            pbYellow = new PictureBox();
            lblColoresJugador = new Label();
            lblPuntuacion = new Label();
            pbTablero = new PictureBox();
            btnFinalizar = new Button();
            lblINFO = new Label();
            pictureBox1 = new PictureBox();
            label1 = new Label();
            lblInfoColoresDesarr = new Label();
            pbMarco = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pbRed).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbBlue).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbGreen).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbYellow).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbTablero).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbMarco).BeginInit();
            SuspendLayout();
            // 
            // btnIniciar
            // 
            btnIniciar.BackColor = Color.White;
            btnIniciar.FlatAppearance.BorderSize = 3;
            btnIniciar.FlatStyle = FlatStyle.Flat;
            btnIniciar.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnIniciar.Location = new Point(309, 485);
            btnIniciar.Name = "btnIniciar";
            btnIniciar.Size = new Size(131, 38);
            btnIniciar.TabIndex = 0;
            btnIniciar.Text = "INICIAR";
            btnIniciar.UseVisualStyleBackColor = false;
            btnIniciar.Click += btnIniciar_Click;
            // 
            // lblSalida
            // 
            lblSalida.AutoSize = true;
            lblSalida.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lblSalida.ForeColor = Color.IndianRed;
            lblSalida.Location = new Point(77, 628);
            lblSalida.Name = "lblSalida";
            lblSalida.Size = new Size(134, 20);
            lblSalida.TabIndex = 1;
            lblSalida.Text = "Secuencia Servidor";
            // 
            // pbRed
            // 
            pbRed.BackColor = Color.Red;
            pbRed.BorderStyle = BorderStyle.FixedSingle;
            pbRed.Location = new Point(129, 190);
            pbRed.Name = "pbRed";
            pbRed.Size = new Size(121, 111);
            pbRed.TabIndex = 2;
            pbRed.TabStop = false;
            pbRed.Click += pbRed_Click;
            // 
            // pbBlue
            // 
            pbBlue.BackColor = Color.Blue;
            pbBlue.BorderStyle = BorderStyle.FixedSingle;
            pbBlue.Location = new Point(129, 305);
            pbBlue.Name = "pbBlue";
            pbBlue.Size = new Size(120, 110);
            pbBlue.TabIndex = 3;
            pbBlue.TabStop = false;
            pbBlue.Click += pbBlue_Click;
            // 
            // pbGreen
            // 
            pbGreen.BackColor = Color.Green;
            pbGreen.BorderStyle = BorderStyle.FixedSingle;
            pbGreen.Location = new Point(253, 190);
            pbGreen.Name = "pbGreen";
            pbGreen.Size = new Size(120, 111);
            pbGreen.TabIndex = 4;
            pbGreen.TabStop = false;
            pbGreen.Click += pbGreen_Click;
            // 
            // pbYellow
            // 
            pbYellow.BackColor = Color.Yellow;
            pbYellow.BorderStyle = BorderStyle.FixedSingle;
            pbYellow.Location = new Point(253, 304);
            pbYellow.Name = "pbYellow";
            pbYellow.Size = new Size(120, 111);
            pbYellow.TabIndex = 5;
            pbYellow.TabStop = false;
            pbYellow.Click += pbYellow_Click;
            // 
            // lblColoresJugador
            // 
            lblColoresJugador.AutoSize = true;
            lblColoresJugador.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lblColoresJugador.Location = new Point(227, 144);
            lblColoresJugador.Name = "lblColoresJugador";
            lblColoresJugador.Size = new Size(46, 28);
            lblColoresJugador.TabIndex = 6;
            lblColoresJugador.Text = "S J1";
            // 
            // lblPuntuacion
            // 
            lblPuntuacion.AutoSize = true;
            lblPuntuacion.FlatStyle = FlatStyle.Flat;
            lblPuntuacion.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lblPuntuacion.Location = new Point(12, 117);
            lblPuntuacion.Name = "lblPuntuacion";
            lblPuntuacion.Size = new Size(162, 28);
            lblPuntuacion.TabIndex = 7;
            lblPuntuacion.Text = "Puntuacion: 0 pts";
            // 
            // pbTablero
            // 
            pbTablero.BackgroundImage = Properties.Resources.tablero;
            pbTablero.BackgroundImageLayout = ImageLayout.Stretch;
            pbTablero.Location = new Point(115, 175);
            pbTablero.Name = "pbTablero";
            pbTablero.Size = new Size(274, 255);
            pbTablero.TabIndex = 9;
            pbTablero.TabStop = false;
            // 
            // btnFinalizar
            // 
            btnFinalizar.BackColor = Color.White;
            btnFinalizar.FlatAppearance.BorderSize = 3;
            btnFinalizar.FlatStyle = FlatStyle.Flat;
            btnFinalizar.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnFinalizar.Location = new Point(76, 485);
            btnFinalizar.Name = "btnFinalizar";
            btnFinalizar.Size = new Size(131, 38);
            btnFinalizar.TabIndex = 10;
            btnFinalizar.Text = "FINALIZAR";
            btnFinalizar.UseVisualStyleBackColor = false;
            btnFinalizar.Click += btnFinalizar_Click;
            // 
            // lblINFO
            // 
            lblINFO.AutoSize = true;
            lblINFO.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lblINFO.Location = new Point(187, 444);
            lblINFO.Name = "lblINFO";
            lblINFO.Size = new Size(0, 28);
            lblINFO.TabIndex = 11;
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImage = Properties.Resources.titulo;
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox1.Location = new Point(115, 36);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(273, 64);
            pictureBox1.TabIndex = 12;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(27, 566);
            label1.Name = "label1";
            label1.Size = new Size(215, 20);
            label1.TabIndex = 13;
            label1.Text = "Herramienta del Desarrollador:";
            // 
            // lblInfoColoresDesarr
            // 
            lblInfoColoresDesarr.AutoSize = true;
            lblInfoColoresDesarr.Location = new Point(76, 599);
            lblInfoColoresDesarr.Name = "lblInfoColoresDesarr";
            lblInfoColoresDesarr.Size = new Size(321, 20);
            lblInfoColoresDesarr.TabIndex = 14;
            lblInfoColoresDesarr.Text = "Leyenda: R (rojo) B (azul) G (verde) Y (amarillo)";
            // 
            // pbMarco
            // 
            pbMarco.BorderStyle = BorderStyle.FixedSingle;
            pbMarco.Location = new Point(12, 553);
            pbMarco.Name = "pbMarco";
            pbMarco.Size = new Size(496, 112);
            pbMarco.TabIndex = 15;
            pbMarco.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(520, 673);
            Controls.Add(lblInfoColoresDesarr);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            Controls.Add(lblINFO);
            Controls.Add(btnFinalizar);
            Controls.Add(lblPuntuacion);
            Controls.Add(lblColoresJugador);
            Controls.Add(pbYellow);
            Controls.Add(pbGreen);
            Controls.Add(pbBlue);
            Controls.Add(pbRed);
            Controls.Add(lblSalida);
            Controls.Add(btnIniciar);
            Controls.Add(pbTablero);
            Controls.Add(pbMarco);
            Name = "Form1";
            Text = "CLIENTE - Simon Says";
            ((System.ComponentModel.ISupportInitialize)pbRed).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbBlue).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbGreen).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbYellow).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbTablero).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbMarco).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnIniciar;
        private Label lblSalida;
        private PictureBox pbRed;
        private PictureBox pbBlue;
        private PictureBox pbGreen;
        private PictureBox pbYellow;
        private Label lblColoresJugador;
        private Label lblPuntuacion;
        private PictureBox pbTablero;
        private Button btnFinalizar;
        private Label lblINFO;
        private PictureBox pictureBox1;
        private Label label1;
        private Label lblInfoColoresDesarr;
        private PictureBox pbMarco;
    }
}