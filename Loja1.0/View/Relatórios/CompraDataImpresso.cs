using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using Loja1._0.Model;
using Loja1._0.Control;



namespace Loja1._0.View.Relatórios
{
    public partial class CompraDataImpresso : Form
    {              
        PrintDocument printDocument1 = new PrintDocument();
        
        Controle controle = new Controle();
        Bitmap memoryImage;

        public static List<Compras> RelatorioCompras;

        public CompraDataImpresso(List<Compras> listaCompras)
        {
            try
            {                
                InitializeComponent();
                RelatorioCompras = listaCompras;

                printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
                
                preencheRelaçãoProdutos();

            }
            catch
            {
                MessageBox.Show("Erro não identificado, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }        
        }

        private void preencheRelaçãoProdutos()
        {
            try
            {
                DataTable dtCompra = new DataTable();
                dtCompra.Columns.Add("Data Compra", typeof(string));
                dtCompra.Columns.Add("Descrição", typeof(string));
                dtCompra.Columns.Add("Fornecedor", typeof(string));
                dtCompra.Columns.Add("Qnt Adquirida", typeof(string));
                dtCompra.Columns.Add("Custo (R$)", typeof(string));
                dtCompra.Columns.Add("ICMS (R$)", typeof(string));
                dtCompra.Columns.Add("Preço (R$)", typeof(string));
                dtCompra.Columns.Add("Qnt Atual", typeof(string));
                dtCompra.Columns.Add("Qnt Min.", typeof(string));
                dtCompra.Columns.Add("Local Nº", typeof(string));
                dtCompra.Columns.Add("Local Sigla", typeof(string));
                dtCompra.Columns.Add("Local Referência", typeof(string));

                foreach (Compras value in RelatorioCompras)
                {
                    dtCompra.Rows.Add(value.dt_compra.ToShortDateString(), value.Produtos.desc_produto, value.Produtos.UnidMedidas.medida, value.qnt_compra.ToString(), "R$" + value.preco_compra.ToString(), "R$" + value.icms_pago.ToString(), "R$" + value.preco_venda.ToString(), value.Produtos.Estoque.qnt_atual.ToString(), value.Produtos.Estoque.qnt_minima.ToString(), value.Produtos.Estoque.num_local.ToString(), value.Produtos.Estoque.letra_local.ToString(), value.Produtos.Estoque.ref_local.ToString());
                }

                dgvRelatorio.DataSource = dtCompra;

                dgvRelatorio.Columns[0].Width = 80;
                dgvRelatorio.Columns[1].Width = 410;
                dgvRelatorio.Columns[2].Width = 70;
                dgvRelatorio.Columns[3].Width = 70;
                dgvRelatorio.Columns[4].Width = 100;
                dgvRelatorio.Columns[5].Width = 100;
                dgvRelatorio.Columns[6].Width = 100;
                dgvRelatorio.Columns[7].Width = 95;
                dgvRelatorio.Columns[8].Width = 95;
                dgvRelatorio.Columns[9].Width = 30;
                dgvRelatorio.Columns[10].Width = 40;
                dgvRelatorio.Columns[11].Width = 300;
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
                memoryImage.SetResolution(163.0F, 120.0F);
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
