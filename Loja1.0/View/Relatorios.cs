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

        }

        private void btnMovimentoData_Click(object sender, EventArgs e)
        {

        }

        private void btnFornecedores_Click(object sender, EventArgs e)
        {

        }

        private void btnPagReceb_Click(object sender, EventArgs e)
        {

        }

        private void btnPagPendente_Click(object sender, EventArgs e)
        {

        }

        private void btnPagEfetuado_Click(object sender, EventArgs e)
        {

        }

        private void btnVendaUser_Click(object sender, EventArgs e)
        {

        }

        private void btnVendaProd_Click(object sender, EventArgs e)
        {

        }

        private void btnVendaPag_Click(object sender, EventArgs e)
        {

        }
    }
}
