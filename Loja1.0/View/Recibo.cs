using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using Loja1._0.Model;
using Loja1._0.Control;

namespace Loja1._0.View
{
    public partial class Recibo : Form
    {
        PrintDocument printDocument1 = new PrintDocument();
        public static Model.Clientes clienteRecibo = new Model.Clientes();
        Controle controle = new Controle();
        public Model.Usuarios user = new Model.Usuarios();
        Bitmap memoryImage;

        public Recibo(Model.Clientes cliente, Model.Usuarios user, decimal valor)
        {
            try
            {
                InitializeComponent();
                clienteRecibo = cliente;
                this.user = user;
                preenchePedido(valor);
                printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);                
            }
            catch
            {
                MessageBox.Show("Erro não identificado, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void preenchePedido(decimal valor)
        {
            try
            {
                txtDtPedido.Text = DateTime.Today.ToShortDateString();
                txtDtLonga.Text = DateTime.Today.ToLongDateString() + " - " + DateTime.Now.ToLongTimeString();

                txtUsuario.Text = user.nome;
                txtNomeCliente.Text = clienteRecibo.nome;
                txtContatoCliente.Text = clienteRecibo.contato;
                txtTel1Cliente.Text = clienteRecibo.telefone;
                txtTel2Cliente.Text = clienteRecibo.recado;
                txtCelCliente.Text = clienteRecibo.celular;
                txtEmailCliente.Text = clienteRecibo.email;
                txtEndCliente.Text = clienteRecibo.endereço;
                txtNumCliente.Text = clienteRecibo.numeral;
                txtBairroCliente.Text = clienteRecibo.bairro;
                txtCidadeCliente.Text = clienteRecibo.Cidades.cidade;
                txtUfCliente.Text = clienteRecibo.Cidades.Estados.sigla;

                preencheRecibo(valor);
            }
            catch
            {
                MessageBox.Show("Erro não identificado, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void preencheRecibo(decimal valor)
        {
            try
            {            
                txtTotalFinal.Text = valor.ToString("0.00");
            }
            catch
            {
                MessageBox.Show("Erro não identificado, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ImpressaoPagina(object sender, EventArgs e)
        {
            CaptureScreen();
            printDocument1.Print();
            Dispose();
        }        

        private void CaptureScreen()
        {
            try
            {
                int width = printablePanel1.Size.Width;
                int height = printablePanel1.Size.Height;

                memoryImage = new Bitmap(width, height);
                memoryImage.SetResolution(139.3F, 135.0F);
                printablePanel1.DrawToBitmap(memoryImage, new Rectangle(0, 0, width, height));
            }
            catch
            {
                MessageBox.Show("Erro não identificado, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void printDocument1_PrintPage(System.Object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(memoryImage, 0, 0);
        }
    }
}
