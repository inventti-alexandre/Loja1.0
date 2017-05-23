using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Loja1._0.Model;
using Loja1._0.Control;

namespace Loja1._0
{
    public partial class PDV : Form
    {
        private Model.Usuarios user;
        Controle controle = new Controle();
        public List<Model.Produtos> listaProdutos = new List<Model.Produtos>();

        public PDV(Model.Usuarios user)
        {
            this.user = user;
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Dispose();
            Inicial form = new Inicial(user);
            form.Show();            
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {            
            if (txtCodigo.Text.Equals(""))
            {
                MessageBox.Show("Para pesquisa de produto insira parte do nome/descrição","Ação Inválida");
            }
            else
            {
                lblCodigo.Text = "Produtos : ";
                txtCodigo.Visible = false;
                btnPesquisar.Visible = false;
                cmbListaProdutos.Visible = true;
                btnCancelPesquisa.Visible = true;
                btnOkPesquisa.Visible = true;
                listaProdutos = controle.pesquisaProdutosValidoNome(txtCodigo.Text);
                cmbListaProdutos.DataSource = listaProdutos;
                cmbListaProdutos.ValueMember = "cod_produto";
                cmbListaProdutos.DisplayMember = "desc_produto";
                cmbListaProdutos.Text = "";
                txtCodigo.Text = "";
            }
        }

        private void btnCancelPesquisa_Click(object sender, EventArgs e)
        {
            lblCodigo.Text = "Código Produto : ";
            txtCodigo.Visible = true;
            btnPesquisar.Visible = true;
            cmbListaProdutos.Visible = false;
            btnCancelPesquisa.Visible = false;
            btnOkPesquisa.Visible = false;
        }

        private void btnOkPesquisa_Click(object sender, EventArgs e)
        {
            lblCodigo.Text = "Código Produto : ";
            txtCodigo.Visible = true;
            btnPesquisar.Visible = true;
            cmbListaProdutos.Visible = false;
            btnCancelPesquisa.Visible = false;
            btnOkPesquisa.Visible = false;
            txtCodigo.Text = cmbListaProdutos.SelectedValue.ToString();            
        }
    }
}
