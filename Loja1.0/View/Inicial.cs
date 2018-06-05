﻿using Loja1._0.Control;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Loja1._0
{
    public partial class Inicial : Form
    {
        private static Model.Usuarios usuario;
        Controle controle = new Controle();
        int perfil = 0;

        Email email = new Email();
        public string erro;

        public Inicial(Model.Usuarios user)
        {
            try
            {
                usuario = user;
                InitializeComponent();               

                perfil = Convert.ToInt32(user.id_Perfil);
                if (perfil > 1)
                {
                    btnUsuarios.Enabled = false;                    
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
                //havendo erro na execução das instruções envia email ao desenvolvedor e mensagem de erro desconhecido ao usuário
                erro = "Inicial.cs, no construtor da classe";
                email.EnviaEmail(erro);
                MessageBox.Show("Erro não identificado em" + erro + ", por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            PDV form = new PDV(usuario);
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
            Caixa form = new Caixa(usuario);
            form.Show();
            this.Hide();
        }

        private void btnProdutos_Click(object sender, EventArgs e)
        {
            Produtos form = new Produtos(usuario);
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
            Usuarios form = new Usuarios(usuario);
            form.Show();
            this.Hide();
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            Clientes form = new Clientes(usuario);
            form.Show();
            this.Hide();
        }

        private void btnFornecedores_Click(object sender, EventArgs e)
        {
            Fornecedores form = new Fornecedores(usuario);
            form.Show();
            this.Hide();
        }

        private void btnContabil_Click(object sender, EventArgs e)
        {
            Contabil form = new Contabil(usuario);
            form.Show();
            this.Hide();
        }

        private void btnRelatorio_Click(object sender, EventArgs e)
        {
            Relatorios form = new Relatorios(usuario);
            form.Show();
            this.Hide();
        }

        private void btnFolhaPag_Click(object sender, EventArgs e)
        {
            FolhaPg form = new FolhaPg(usuario);
            form.Show();
            this.Hide();
        }

        private void btnConsultaPedidos_Click(object sender, EventArgs e)
        {
            ConsultaPedido form = new ConsultaPedido(usuario);
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
            Entrega form = new Entrega(usuario);
            form.Show();
            this.Hide();
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            Gestao form = new Gestao(usuario);
            form.Show();
            this.Hide();
        }

        private void btnPonto_Click(object sender, EventArgs e)
        {
            ControleHoras form = new ControleHoras(usuario);
            form.Show();
            this.Hide();
        }

        private void Inicial_Load(object sender, EventArgs e)
        {
            if (usuario.nome == null || usuario.nome.Equals(""))
            {
                this.Hide();
                Usuarios form = new Usuarios(usuario);
                form.Show();
                
            }
        }
    }
}
