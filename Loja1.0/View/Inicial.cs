using Loja1._0.Control;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Loja1._0
{
    public partial class Inicial : Form
    {
        private Model.Usuarios user;
        Controle controle = new Controle();
        int perfil = 0;

        public Inicial(Model.Usuarios user)
        {
            this.user = user;
            InitializeComponent();
            perfil = user.num_perfil;
            if (perfil > 1)
            {
                btnUsuarios.Enabled = false;
                btnGestao.Enabled = false;
                btnRelatorios.Enabled = false;

                if (perfil > 2)
                {
                    btnContabil.Enabled = false;
                    btnFolhaPg.Enabled = false;

                    if (perfil == 3)
                    {
                        btnFornecedores.Enabled = false;
                        btnClientes.Enabled = false;
                        btnProdutos.Enabled = false;
                        btnPdv.Enabled = false;
                    }

                    if (perfil == 4)
                    {
                        btnCaixa.Enabled = false;
                    }
                }
            }
        }
               

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login form = new Login();
            form.Show();            
        }


        private void btnPdv_Click(object sender, EventArgs e)
        {
            PDV form = new PDV(user);
            form.Show();
            this.Hide();
        }

        private void btnCaixa_Click(object sender, EventArgs e)
        {
            Caixa form = new Caixa(user);
            form.Show();
            this.Hide();
        }

        private void btnProdutos_Click(object sender, EventArgs e)
        {
            Produtos form = new Produtos(user);
            form.Show();
            this.Hide();
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            Usuarios form = new Usuarios(user);
            form.Show();
            this.Hide();
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            Clientes form = new Clientes(user);
            form.Show();
            this.Hide();
        }

        private void btnFornecedores_Click(object sender, EventArgs e)
        {
            Fornecedores form = new Fornecedores(user);
            form.Show();
            this.Hide();
        }

        private void btnContabil_Click(object sender, EventArgs e)
        {
            Contabil form = new Contabil(user);
            form.Show();
            this.Hide();
        }

        private void btnGestao_Click(object sender, EventArgs e)
        {
            Gestao form = new Gestao(user);
            form.Show();
            this.Hide();
        }

        private void btnRelatorio_Click(object sender, EventArgs e)
        {
            Relatorios form = new Relatorios(user);
            form.Show();
            this.Hide();
        }

        private void btnFolhaPag_Click(object sender, EventArgs e)
        {
            FolhaPg form = new FolhaPg(user);
            form.Show();
            this.Hide();
        }

    }
}
