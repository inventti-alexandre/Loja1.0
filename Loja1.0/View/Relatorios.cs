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

namespace Loja1._0
{
    public partial class Relatorios : Form
    {
        private Model.Usuarios user;

        public Relatorios(Model.Usuarios user)
        {
            this.user = user;
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Inicial form = new Inicial(user);
            form.Show();
            Dispose();
        }

        private void btnVendaData_Click(object sender, EventArgs e)
        {
            VendaData form = new VendaData(user);
            form.Show();
            this.Hide();
        }

        private void btnCompraData_Click(object sender, EventArgs e)
        {
            ComprasData form = new ComprasData(user);
            form.Show();
            this.Hide();
        }

        private void btnMovimentoData_Click(object sender, EventArgs e)
        {
            MovimentoData form = new MovimentoData(user);
            form.Show();
            this.Hide();
        }

        private void btnFornecedores_Click(object sender, EventArgs e)
        {
            ProdutosFornecedor form = new ProdutosFornecedor(user);
            form.Show();
            this.Hide();
        }

        private void btnPagReceb_Click(object sender, EventArgs e)
        {
            PagamentosRecebidos form = new PagamentosRecebidos(user);
            form.Show();
            this.Hide();
        }

        private void btnPagPendente_Click(object sender, EventArgs e)
        {
            PagamentosPendentes form = new PagamentosPendentes(user);
            form.Show();
            this.Hide();
        }

        private void btnVendaUser_Click(object sender, EventArgs e)
        {
            VendaUser form = new VendaUser(user);
            form.Show();
            this.Hide();
        }

        private void btnVendaProd_Click(object sender, EventArgs e)
        {
            VendaProduto form = new VendaProduto(user);
            form.Show();
            this.Hide();
        }

        private void btnVendaPag_Click(object sender, EventArgs e)
        {
            VendaPagamento form = new VendaPagamento(user);
            form.Show();
            this.Hide();
        }

        private void btnPedidosAbertosData_Click(object sender, EventArgs e)
        {
            PedidosAbertos form = new PedidosAbertos(user);
            form.Show();
            this.Hide();
        }

        private void btnProdQuant_Click(object sender, EventArgs e)
        {
            ProdutoCompleta form = new ProdutoCompleta(user);
            form.Show();
            this.Hide();
        }

        private void btnSaidaPagamentos_Click(object sender, EventArgs e)
        {
            PagamentosSaida form = new PagamentosSaida(user);
            form.Show();
            this.Hide();
        }
    }
}
