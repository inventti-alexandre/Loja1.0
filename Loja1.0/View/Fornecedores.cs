﻿using System;
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
    public partial class Fornecedores : Form
    {
        private Model.Usuarios user;
        Controle controle = new Controle();
        Valida validaDoc = new Valida();
        static public Model.Fornecedores fornecedor = new Model.Fornecedores();
        List<Estados> listaEstado = new List<Estados>();
        List<Cidades> listaCidade = new List<Cidades>();
        List<Model.Fornecedores> listaFornecedores = new List<Model.Fornecedores>();
        bool flagNovo = true;

        public Fornecedores(Model.Usuarios user)
        {
            try
            {
                this.user = user;
                InitializeComponent();

                listaFornecedores = controle.PesquisaFornecedores("");
                preencheRelacao(listaFornecedores);
            }
            catch
            {
                MessageBox.Show("Erro não identificado, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }

        private void preencheRelacao(List<Model.Fornecedores> listaFornecedores)
        {
            try
            {
                DataTable dtFornecedores = new DataTable();
                dtFornecedores.Columns.Add("Fornecedor", typeof(string));
                dtFornecedores.Columns.Add("CNPJ / CPF", typeof(string));
                dtFornecedores.Columns.Add("Contato", typeof(string));
                dtFornecedores.Columns.Add("Telefone 1", typeof(string));
                dtFornecedores.Columns.Add("Telefone 2", typeof(string));
                dtFornecedores.Columns.Add("Celular", typeof(string));

                for (int i = 0; i < listaFornecedores.Count; i++)
                {
                    dtFornecedores.Rows.Add(listaFornecedores[i].nome, listaFornecedores[i].cnpj, listaFornecedores[i].contato, listaFornecedores[i].telefone, listaFornecedores[i].recado, listaFornecedores[i].celular);
                }
                dgvFornecedores.DataSource = dtFornecedores;

                dgvFornecedores.Columns[0].Width = 250;
                dgvFornecedores.Columns[1].Width = 150;
                dgvFornecedores.Columns[2].Width = 150;
                dgvFornecedores.Columns[3].Width = 90;
                dgvFornecedores.Columns[4].Width = 90;
                dgvFornecedores.Columns[5].Width = 95;

                dgvFornecedores.RowsDefaultCellStyle.SelectionBackColor = Color.White;
                dgvFornecedores.RowsDefaultCellStyle.SelectionForeColor = Color.Black;
            }
            catch
            {
                MessageBox.Show("Erro não identificado, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Inicial form = new Inicial(user);
            form.Show();
            Dispose();
        }

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtFornecedor.Text.Trim().Length < 3 && txtCnpj.Text.Trim().Equals(""))
                {
                    MessageBox.Show("É necessário ao menos 3 caracteres para realizar uma busca por nome", "Ação inválida");
                    btnLimpar_Click(sender, e);
                }

                else if ((txtCnpj.Text.Trim().Length < 11 || txtCnpj.Text.Trim().Length > 14) && txtFornecedor.Text.Trim().Equals(""))
                {
                    MessageBox.Show("É necessário 13 caracteres para realizar uma busca por CNPJ/CPF", "Ação inválida");
                    btnLimpar_Click(sender, e);
                }
                else
                {
                    if (txtFornecedor.Text.Trim().Equals("") && !txtCnpj.Text.Trim().Equals(""))
                    {
                        if (controle.PesquisaFornecedorCpnj(txtCnpj.Text.Trim()) != null)
                        {
                            txtFornecedor.Enabled = false;
                            txtCnpj.Enabled = false;
                            lblMensagem.Text = "Fornecedor com o CNPJ: " + txtCnpj.Text;
                            fornecedor = controle.PesquisaFornecedorCpnj(txtCnpj.Text);
                            preencheDados(fornecedor);
                        }
                        else
                        {
                            MessageBox.Show("Não existem Fornecedores com o CNPJ/CPF " + txtCnpj.Text.ToUpper().Trim() + ", Por favor verifique e refaça sua pesquisa", "Pesquisa Inválida");
                            btnLimpar_Click(sender, e);
                        }
                    }
                    else if (!txtFornecedor.Text.Trim().Equals("") && txtCnpj.Text.Trim().Equals(""))
                    {
                        if (controle.PesquisaFornecedores(txtFornecedor.Text.ToUpper().Trim()).Count > 0)
                        {
                            txtFornecedor.Enabled = false;
                            txtCnpj.Enabled = false;
                            lblMensagem.Text = "Fornecedores que contém a expressão: " + txtFornecedor.Text.ToUpper().Trim() + " no nome";
                            listaFornecedores = controle.PesquisaFornecedores(txtFornecedor.Text.ToUpper().Trim());
                            preencheRelacao(listaFornecedores);
                        }
                        else
                        {
                            MessageBox.Show("Não existem fornecedores ativos com a expressão \"" + txtFornecedor.Text.ToUpper().Trim() + "\", Por favor verifique e refaça sua pesquisa", "Pesquisa Inválida");
                            btnLimpar_Click(sender, e);
                        }
                    }
                    else if (!txtFornecedor.Text.Trim().Equals("") && !txtCnpj.Text.Trim().Equals(""))
                    {
                        MessageBox.Show("Para pesquisa de fornecedor preencha somente um campo de busca, ou CNPJ/CPF, ou Nome", "Ação Inválida");
                        btnLimpar_Click(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("Para pesquisa de fornecedor preencha um campo de busca, ou CNPJ/CPF, ou Nome", "Ação Inválida");
                        btnLimpar_Click(sender, e);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Erro não identificado, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNovo_Click(object sender, EventArgs e) 
        {
            try
            {
                limpaForm();
                listaEstado = controle.PesquisaGeralEstados();
                cmbUf.DataSource = listaEstado;
                cmbUf.ValueMember = "id";
                cmbUf.DisplayMember = "sigla";
                cmbUf.SelectedText = "";

                flagNovo = true;

                btnNovo.Enabled = false;
                btnSalvar.Enabled = true;
                btnPesquisa.Enabled = false;

                chkAtivo.Enabled = true;
                pnlTelefone.Enabled = true;
                cmbUf.Enabled = true;
                cmbCidade.Enabled = true;
                txtBairro.Enabled = true;
                txtCnpj.Enabled = true;
                txtContato.Enabled = true;
                txtEndereco.Enabled = true;
                txtFornecedor.Enabled = true;
                txtNum.Enabled = true;
                txtTel1.Enabled = true;
                txtTel2.Enabled = true;
                txtEmail.Enabled = true;
                dgvFornecedores.Enabled = false;
            }
            catch
            {
                MessageBox.Show("Erro não identificado, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            flagNovo = false;
            btnAlterar.Enabled = false;
            btnSalvar.Enabled = true;
            chkAtivo.Enabled = true;
            pnlTelefone.Enabled = true;
            cmbUf.Enabled = true;
            cmbCidade.Enabled = true;
            txtBairro.Enabled = true;
            txtCnpj.Enabled = true;
            txtContato.Enabled = true;
            txtEndereco.Enabled = true;
            txtFornecedor.Enabled = true;
            txtNum.Enabled = true;
            txtTel1.Enabled = true;
            txtTel2.Enabled = true;
            txtEmail.Enabled = true;
            dgvFornecedores.Enabled = false;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                //Condicionais para validação do preenchimento
                //campos preenchidos
                if (!validaCampos())
                {
                    MessageBox.Show("Todos os campos devem ser preenchidos com valores válidos.");
                }
                else if (validaCampos())
                {
                    //se flag novo = false, novo elemento
                    if (flagNovo)
                    {
                        if (validaDoc.validaTipoCpfCnpj(txtCnpj.Text.Trim()))
                        {
                            btnPesquisa.Enabled = true;
                            btnSalvar.Enabled = false;
                            dgvFornecedores.Enabled = true;
                            flagNovo = false;

                            Model.Fornecedores fornecedor = new Model.Fornecedores();
                            controle.SalvarFornecedor(fornecedor);

                            fornecedor.cnpj = txtCnpj.Text;
                            fornecedor.nome = txtFornecedor.Text.ToUpper().Trim();
                            if (chkAtivo.Checked)
                            {
                                fornecedor.status = 1;
                            }
                            else
                            {
                                fornecedor.status = 0;
                            }
                            fornecedor.contato = txtContato.Text.ToUpper().Trim();
                            fornecedor.endereço = txtEndereco.Text.ToUpper().Trim();
                            fornecedor.numeral = txtNum.Text.ToUpper().Trim();
                            fornecedor.bairro = txtBairro.Text.ToUpper().Trim();
                            fornecedor.id_Cidade = controle.PesquisaCidade(cmbCidade.Text).id;
                            fornecedor.telefone = txtTel1.Text;
                            fornecedor.recado = txtTel2.Text;
                            fornecedor.celular = txtCelular.Text;
                            fornecedor.email = txtEmail.Text;
                            if (rdbClaro.Checked)
                            {
                                fornecedor.operadora = "Claro";
                            }
                            else if (rdbTim.Checked)
                            {
                                fornecedor.operadora = "Tim";
                            }
                            else if (rdbOi.Checked)
                            {
                                fornecedor.operadora = "Oi";
                            }
                            else if (rdbVivo.Checked)
                            {
                                fornecedor.operadora = "Vivo";
                            }
                            controle.SalvaAtualiza();
                            btnLimpar_Click(sender, e);
                            MessageBox.Show("Inclusão realizada com sucesso", "Ação bem sucedida");
                            listaFornecedores = controle.PesquisaFornecedores("");
                            preencheRelacao(listaFornecedores);
                        }
                    }

                    //alteração de elemento existente na base de dados
                    else if (!flagNovo)
                    {
                        if (validaDoc.validaTipoCpfCnpj(txtCnpj.Text))
                        {
                            btnPesquisa.Enabled = true;
                            btnSalvar.Enabled = false;
                            dgvFornecedores.Enabled = true;

                            fornecedor.cnpj = txtCnpj.Text;
                            fornecedor.nome = txtFornecedor.Text;
                            if (chkAtivo.Checked)
                            {
                                fornecedor.status = 1;
                            }
                            else
                            {
                                fornecedor.status = 0;
                            }
                            fornecedor.contato = txtContato.Text;
                            fornecedor.endereço = txtEndereco.Text;
                            fornecedor.numeral = txtNum.Text;
                            fornecedor.bairro = txtBairro.Text;
                            fornecedor.id_Cidade = controle.PesquisaCidade(cmbCidade.Text).id;
                            fornecedor.telefone = txtTel1.Text;
                            fornecedor.recado = txtTel2.Text;
                            fornecedor.celular = txtCelular.Text;
                            fornecedor.email = txtEmail.Text;
                            if (rdbClaro.Checked)
                            {
                                fornecedor.operadora = "Claro";
                            }
                            else if (rdbTim.Checked)
                            {
                                fornecedor.operadora = "Tim";
                            }
                            else if (rdbOi.Checked)
                            {
                                fornecedor.operadora = "Oi";
                            }
                            else if (rdbVivo.Checked)
                            {
                                fornecedor.operadora = "Vivo";
                            }
                            controle.SalvaAtualiza();
                            limpaForm();
                            btnLimpar_Click(sender, e);
                            MessageBox.Show("Alteração realizada com sucesso", "Ação bem sucedida");
                            listaFornecedores = controle.PesquisaFornecedores("");
                            preencheRelacao(listaFornecedores);
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Erro não identificado, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool validaCampos()
        {
            if (!txtCnpj.Text.Equals("") && !txtFornecedor.Text.Equals("") && !txtContato.Text.Equals("") && (!txtCelular.Text.Equals("") || !txtTel1.Text.Equals("") || !txtTel2.Text.Equals("")))
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            try
            {
                flagNovo = true;
                limpaForm();
                listaFornecedores = controle.PesquisaFornecedores("");
                preencheRelacao(listaFornecedores);
                lblMensagem.Text = "Relação de Fornecedores";
            }
            catch
            {
                MessageBox.Show("Erro não identificado, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void limpaForm()
        {
            dgvFornecedores.Enabled = true;
            chkAtivo.Enabled = false;
            btnSalvar.Enabled = false;
            btnNovo.Enabled = true;
            btnAlterar.Enabled = false;
            btnPesquisa.Enabled = true;
            pnlTelefone.Enabled = false;
            cmbCidade.Enabled = false;
            cmbUf.Enabled = false;
            txtBairro.Enabled = false;
            txtCnpj.Enabled = true;
            txtContato.Enabled = false;
            txtEndereco.Enabled = false;
            txtFornecedor.Enabled = true;
            txtEmail.Enabled = false;
            txtNum.Enabled = false;
            rdbOi.Checked = false;
            rdbTim.Checked = false;
            rdbVivo.Checked = false;
            rdbClaro.Checked = false;
            pnlTelefone.Text = "";
            txtBairro.Text = "";
            txtCnpj.Text = "";
            txtContato.Text = "";
            txtEndereco.Text = "";
            txtFornecedor.Text = "";
            txtNum.Text = "";
            txtTel1.Text = "";
            txtTel2.Text = "";
            txtCelular.Text = "";
            cmbUf.Text = "";
            cmbCidade.Text = "";
            txtEmail.Text = "";
        }

        private void cmbUf_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cmbCidade.Enabled = true;
                listaCidade = controle.PesquisaCidadesPorEstado(cmbUf.SelectedIndex + 1);
                cmbCidade.DisplayMember = "cidade";
                cmbCidade.ValueMember = "id";
                cmbCidade.DataSource = listaCidade;
            }
            catch
            {
                MessageBox.Show("Erro não identificado, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gdvFornecedores_Click(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                fornecedor = controle.PesquisaFornecedorCpnj(dgvFornecedores.Rows[e.RowIndex].Cells[1].Value.ToString());
                btnLimpar_Click(sender, e);
                btnPesquisa.Enabled = false;
                preencheDados(fornecedor);
            }
            catch
            {
                MessageBox.Show("Erro não identificado, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void preencheDados(Model.Fornecedores fornecedor)
        {
            try
            {
                txtCnpj.Enabled = false;
                txtFornecedor.Enabled = false;
                if (fornecedor != null)
                {
                    btnNovo.Enabled = false;
                    btnPesquisa.Enabled = false;
                    btnAlterar.Enabled = true;
                    txtFornecedor.Text = fornecedor.nome;
                    txtCnpj.Text = fornecedor.cnpj;
                    txtContato.Text = fornecedor.contato;
                    txtEndereco.Text = fornecedor.endereço;
                    txtNum.Text = fornecedor.numeral;
                    txtBairro.Text = fornecedor.bairro;
                    txtCelular.Text = fornecedor.celular;
                    txtTel1.Text = fornecedor.telefone;
                    txtTel2.Text = fornecedor.recado;
                    txtEmail.Text = fornecedor.email;
                    cmbUf.SelectedText = fornecedor.Cidades.Estados.sigla;
                    cmbCidade.SelectedText = fornecedor.Cidades.cidade;

                    if (fornecedor.status == 1)
                    {
                        chkAtivo.Checked = true;
                    }
                    else
                    {
                        chkAtivo.Checked = false;
                    }

                    if (fornecedor.operadora == null)
                    {
                        rdbOi.Checked = false;
                        rdbTim.Checked = false;
                        rdbVivo.Checked = false;
                        rdbClaro.Checked = false;
                    }
                    else if (fornecedor.operadora.Equals("Oi"))
                    {
                        rdbOi.Checked = true;
                    }
                    else if (fornecedor.operadora.Equals("Tim"))
                    {
                        rdbTim.Checked = true;
                    }
                    else if (fornecedor.operadora.Equals("Vivo"))
                    {
                        rdbVivo.Checked = true;
                    }
                    else if (fornecedor.operadora.Equals("Claro"))
                    {
                        rdbClaro.Checked = true;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Erro não identificado, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
