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
    public partial class Pedido : Form
    {
        PrintDocument printDocument1 = new PrintDocument();
        private PDV pdv = null;
        private ConsultaPedido pedido = null;
        private Vendas venda;
        Controle controle = new Controle();
        public Gerenciamento gerencia = new Gerenciamento();
        public Model.Usuarios user = new Model.Usuarios();
        decimal valorAux = 0;
        Bitmap memoryImage;
        static Compras compra = new Compras();

        public Pedido(PDV pdv, Model.Vendas venda)
        {
            try
            {
                InitializeComponent();
                gerencia = controle.PesquisaGerenciamento(1);
                this.pdv = pdv;
                this.venda = venda;
                printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
                preenchePedido(venda);
            }
            catch
            {
                MessageBox.Show("Erro não identificado, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //reimpressão de pedido, módulo Pedido
        public Pedido(ConsultaPedido pedido, Model.Vendas venda)
        {
            try
            {
                InitializeComponent();
                gerencia = controle.PesquisaGerenciamento(1);
                this.pedido = pedido;
                this.venda = venda;
                printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
                preenchePedido(venda);
            }
            catch
            {
                MessageBox.Show("Erro não identificado, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void preenchePedido(Vendas venda)
        {
            try
            {
                txtDtPedido.Text = DateTime.Today.ToShortDateString();
                txtDtLonga.Text = DateTime.Today.ToLongDateString() + " - " + DateTime.Now.ToLongTimeString();
                txtUsuario.Text = controle.PesquisaUserId(venda.id_Usuario).nome;
                txtNumPedido.Text = venda.id.ToString();
                if (venda.Clientes != null)
                {
                    txtNomeCliente.Text = venda.Clientes.nome;
                    txtContatoCliente.Text = venda.Clientes.contato;
                    txtTel1Cliente.Text = venda.Clientes.telefone;
                    txtTel2Cliente.Text = venda.Clientes.recado;
                    txtCelCliente.Text = venda.Clientes.celular;
                    txtEmailCliente.Text = venda.Clientes.email;
                    txtEndCliente.Text = venda.Clientes.endereço;
                    txtNumCliente.Text = venda.Clientes.numeral;
                    txtBairroCliente.Text = venda.Clientes.bairro;
                    txtCidadeCliente.Text = venda.Clientes.Cidades.cidade;
                    txtUfCliente.Text = venda.Clientes.Cidades.Estados.sigla;
                }
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
                List<Vendas_Produtos> listaProdutos = new List<Vendas_Produtos>();
                listaProdutos = controle.PesquisaProdutosVenda(venda.id);

                DataTable dtProdutos = new DataTable();
                dtProdutos.Columns.Add("produto", typeof(string));
                dtProdutos.Columns.Add("unidade", typeof(string));
                dtProdutos.Columns.Add("qnt", typeof(string));
                dtProdutos.Columns.Add("preçoUnd", typeof(string));
                dtProdutos.Columns.Add("impostos", typeof(string));
                dtProdutos.Columns.Add("preçototal", typeof(string));

                for (int i = 0; i < listaProdutos.Count; i++)
                {
                    compra = controle.PesquisaCompraAnterior(Convert.ToInt32(listaProdutos[i].id_produto));
                    Compras compraPend = controle.PesquisaIcmsCompra(Convert.ToInt32(listaProdutos[i].id_produto));
                    dtProdutos.Rows.Add(listaProdutos[i].Produtos.desc_produto, listaProdutos[i].Produtos.UnidMedidas.medida, listaProdutos[i].quantidade.ToString(), compra.preco_venda.ToString(), compraPend.icms_pago.ToString(), (listaProdutos[i].quantidade * compra.preco_venda).ToString());
                    valorAux = valorAux + (compra.preco_venda * listaProdutos[i].quantidade);
                    if(compraPend.qnt_compra >= listaProdutos[i].quantidade)
                    {
                        compraPend.qnt_compra = compraPend.qnt_compra - listaProdutos[i].quantidade;
                        controle.SalvaAtualiza();
                    }
                    else
                    {
                        int quant = listaProdutos[i].quantidade - compraPend.qnt_compra;
                        compraPend.qnt_compra = 0;
                        controle.SalvaAtualiza();

                        while (quant > 0)
                        {
                            compraPend = controle.PesquisaIcmsCompra(Convert.ToInt32(listaProdutos[i].id_produto));
                            if (compraPend.qnt_compra >= quant)
                            {
                                compraPend.qnt_compra = compraPend.qnt_compra - quant;
                                controle.SalvaAtualiza();
                            }
                            else
                            {
                                quant = quant - compraPend.qnt_compra;
                                compraPend.qnt_compra = 0;
                                controle.SalvaAtualiza();
                            }
                        }
                        
                    }
                }
                for (int i = listaProdutos.Count; i < 25; i++)
                {
                    dtProdutos.Rows.Add("", "", "", "", "", "");
                }

                dgvProdutos.DataSource = dtProdutos;

                dgvProdutos.Columns[0].Width = 629;
                dgvProdutos.Columns[1].Width = 98;
                dgvProdutos.Columns[2].Width = 84;
                dgvProdutos.Columns[3].Width = 97;
                dgvProdutos.Columns[4].Width = 98;
                dgvProdutos.Columns[5].Width = 101;

                txtSubTotal.Text = valorAux.ToString("0.00");
                txtTotalImp.Text = (valorAux * (gerencia.tributacao / 100)).ToString("0.00");
                txtAcres.Text = "0,00";
                txtDesc.Text = (valorAux * (Convert.ToDecimal(venda.desconto) / 100)).ToString("0.00");
                txtTotalFinal.Text = (Convert.ToDecimal(txtSubTotal.Text) + Convert.ToDecimal(txtAcres.Text) - Convert.ToDecimal(txtDesc.Text)).ToString("0.00");
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
