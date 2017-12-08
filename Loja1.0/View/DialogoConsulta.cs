using Loja1._0.Control;
using Loja1._0.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Loja1._0.View
{
    public partial class DialogoConsulta : Form
    {
        private Controle controle = new Controle();
        private Model.Produtos produto = new Model.Produtos();
        private Compras compra = new Compras();
        static PDV pdvOculto;
        static Caixa caixaOculto;
        bool consultaCaixa;

        public DialogoConsulta(PDV pdv)
        {
            InitializeComponent();
            pdvOculto = pdv;
            consultaCaixa = false;
        }

        public DialogoConsulta(Caixa caixa)
        {
            InitializeComponent();
            caixaOculto = caixa;
            consultaCaixa = true;
        }

        private void btnConsulta_Click(object sender, EventArgs e)
        {
            produto = new Model.Produtos();
            compra = new Compras();
            
            txtDescricao.Text = "(Decrição / Nome)";
            txtPreco.Text = "0,00";

            if (txtCodigo.Text.Equals(""))
            {
                MessageBox.Show("Não é possível realizar pesquisa de preço sem o código do produto, tente novamente", "alert", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtCodigo.Focus();
            }
            else if (!txtCodigo.Text.All(char.IsNumber))
            {
                MessageBox.Show("A consulta de preços é feita exclusivamente pelo código de barras do produto, tente novamente", "alert", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtCodigo.Text = "";
                txtCodigo.Focus();
            }
            else if (controle.PesquisaProdutoCod(txtCodigo.Text) == null)
            {
                MessageBox.Show("Não existe item cadastrado com o código do produto informado, tente novamente", "alert", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtCodigo.Text = "";
                txtCodigo.Focus();
            }
            else
            {
                produto = controle.PesquisaProdutoCod(txtCodigo.Text);
                compra = controle.PesquisaCompraAnterior(produto.id);

                txtDescricao.Text = produto.desc_produto;
                txtPreco.Text = compra.preco_venda.ToString("0.00");
            }
        }

        private void btnVolta_Click(object sender, EventArgs e)
        {
            if (consultaCaixa)
            {
                caixaOculto.Show();
            }
            else
            {
                pdvOculto.Show();
            }

            this.Dispose();
        }
    }
}
