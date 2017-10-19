using Loja1._0.Control;
using System;
using System.Collections.Generic;
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
            try
            {
                this.user = user;
                InitializeComponent();

                perfil = Convert.ToInt32(user.id_Perfil);
                if (perfil > 1)
                {
                    btnUsuarios.Enabled = false;
                    btnPonto.Enabled = false;
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
                else
                {
                    alertaFaltaEstoque();
                }
            }
            catch
            {
                MessageBox.Show("Erro não identificado, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (controle.PesquisaGerenciamento(1) == null)
            {
                MessageBox.Show("Não existem dados gerenciais cadastrados, por favor, cadastre-os e tente novamente", "Ação Inválida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                form.Dispose();
                this.Show();
            }
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
            if (Produtos.listaFornecedores.Count == 0)
            {
                MessageBox.Show("Não existem fornecedores cadastrados, por favor, cadastre-os e tente novamente", "Ação Inválida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                form.Dispose();
                this.Show();                
            }
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

        private void btnConsultaPedidos_Click(object sender, EventArgs e)
        {
            ConsultaPedido form = new ConsultaPedido(user);
            form.Show();
            this.Hide();
        }

        private void alertaFaltaEstoque()
        {
            List<Model.Produtos> relacaoCompleta = controle.PesquisaGeralProd();
            bool faltando = false;

            foreach(Model.Produtos value in relacaoCompleta)
            {
                if (value.Estoque.qnt_atual < value.Estoque.qnt_minima)
                {
                    faltando = true;
                }
            }

            if (faltando)
            {
                faltando = false;
                MessageBox.Show("Existem produtos esgotados ou abaixo da quantidade miníma, favor verificar no módulo \"Relatórios\" a relação de produtos nestas condições","Ação Necessária", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnEntregas_Click(object sender, EventArgs e)
        {
            Entrega form = new Entrega(user);
            form.Show();
            this.Hide();
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            Gestao form = new Gestao(user);
            form.Show();
            this.Hide();
        }

        private void btnPonto_Click(object sender, EventArgs e)
        {
            ControleHoras form = new ControleHoras(user);
            form.Show();
            this.Hide();
        }
    }
}
