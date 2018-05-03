namespace Loja1._0.View
{
    partial class DialogoCheque
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogoCheque));
            this.txtChequePrimeiro = new System.Windows.Forms.TextBox();
            this.txtChequeUltimo = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.lblCheq1 = new System.Windows.Forms.Label();
            this.lblCheq2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtChequePrimeiro
            // 
            this.txtChequePrimeiro.Location = new System.Drawing.Point(12, 65);
            this.txtChequePrimeiro.MaxLength = 50;
            this.txtChequePrimeiro.Name = "txtChequePrimeiro";
            this.txtChequePrimeiro.Size = new System.Drawing.Size(338, 20);
            this.txtChequePrimeiro.TabIndex = 0;
            // 
            // txtChequeUltimo
            // 
            this.txtChequeUltimo.Location = new System.Drawing.Point(12, 127);
            this.txtChequeUltimo.MaxLength = 50;
            this.txtChequeUltimo.Name = "txtChequeUltimo";
            this.txtChequeUltimo.Size = new System.Drawing.Size(338, 20);
            this.txtChequeUltimo.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.button1.Location = new System.Drawing.Point(120, 176);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(116, 32);
            this.button1.TabIndex = 2;
            this.button1.Text = "Confirma";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblCheq1
            // 
            this.lblCheq1.AutoSize = true;
            this.lblCheq1.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCheq1.Location = new System.Drawing.Point(68, 44);
            this.lblCheq1.Name = "lblCheq1";
            this.lblCheq1.Size = new System.Drawing.Size(220, 18);
            this.lblCheq1.TabIndex = 3;
            this.lblCheq1.Text = "Entre o nº do primeiro cheque";
            // 
            // lblCheq2
            // 
            this.lblCheq2.AutoSize = true;
            this.lblCheq2.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCheq2.Location = new System.Drawing.Point(78, 106);
            this.lblCheq2.Name = "lblCheq2";
            this.lblCheq2.Size = new System.Drawing.Size(204, 18);
            this.lblCheq2.TabIndex = 4;
            this.lblCheq2.Text = "Entre o nº do último cheque";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(20, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(321, 21);
            this.label1.TabIndex = 5;
            this.label1.Text = "Registro de numeração de cheques";
            // 
            // DialogoCheque
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Khaki;
            this.ClientSize = new System.Drawing.Size(362, 240);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblCheq2);
            this.Controls.Add(this.lblCheq1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtChequeUltimo);
            this.Controls.Add(this.txtChequePrimeiro);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DialogoCheque";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Registrar  Recebimento de Cheques";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtChequePrimeiro;
        private System.Windows.Forms.TextBox txtChequeUltimo;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblCheq1;
        private System.Windows.Forms.Label lblCheq2;
        private System.Windows.Forms.Label label1;
    }
}