using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Loja1._0.Control;
using Loja1._0.Model;

namespace Loja1._0
{
    public partial class Usuarios : Form
    {
        private Model.Usuarios user;
        public static Model.Usuarios usuario;
        Controle controle = new Controle();
        Valida validacao = new Valida();
        public static List<Model.Usuarios> listaUser = new List<Model.Usuarios>();

        public Usuarios(Model.Usuarios user)
        {
            this.user = user;
            InitializeComponent();
            carregaListaUsuarios();
        }

        private void carregaListaUsuarios()
        {
            try
            {
                listaUser = controle.PesquisaGeralUser();

                DataTable dtUsers = new DataTable();
                dtUsers.Columns.Add("Registro", typeof(string));
                dtUsers.Columns.Add("Login", typeof(string));
                dtUsers.Columns.Add("Nome", typeof(string));
                dtUsers.Columns.Add("CPF", typeof(string));
                dtUsers.Columns.Add("RG", typeof(string));
                dtUsers.Columns.Add("Salário", typeof(string));
                dtUsers.Columns.Add("Perfil", typeof(string));
                dtUsers.Columns.Add("Status", typeof(string));

                foreach (Model.Usuarios value in listaUser)
                {
                    string perfil = "";
                    string status = "";
                    string registro = "";

                    if (value.registro == null)
                    {
                        registro = "000000";
                    }
                    else
                    {
                        registro = (value.registro).ToString();
                    }

                    if (value.id_Perfil == 1)
                    {
                        perfil = "Administrador";
                    }
                    else if (value.id_Perfil == 2)
                    {
                        perfil = "Gerente";
                    }
                    else if (value.id_Perfil == 4)
                    {
                        perfil = "Operador";
                    }
                    else if (value.id_Perfil == 3)
                    {
                        perfil = "Caixa";
                    }

                    if (value.status == 1)
                    {
                        status = "Ativo";
                    }
                    else if (value.status == 0)
                    {
                        status = "Inativo";
                    }

                    dtUsers.Rows.Add(registro, value.login, value.nome, value.cpf, value.rg, value.salario.ToString(), perfil, status);
                }

                dgvUsuarios.DataSource = dtUsers;

                dgvUsuarios.Columns[0].Width = 100;
                dgvUsuarios.Columns[1].Width = 150;
                dgvUsuarios.Columns[2].Width = 400;
                dgvUsuarios.Columns[3].Width = 150;
                dgvUsuarios.Columns[4].Width = 150;
                dgvUsuarios.Columns[5].Width = 120;
                dgvUsuarios.Columns[6].Width = 120;
                dgvUsuarios.Columns[7].Width = 120;
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

        private void dgvUsuarios_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (Convert.ToInt32(e.RowIndex) >= 0)
                {
                    usuario = controle.PesquisaUserLogin(dgvUsuarios.Rows[e.RowIndex].Cells[1].EditedFormattedValue.ToString());
                    carregaUser(usuario);
                }
            }
            catch
            {
                MessageBox.Show("Erro não identificado, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void carregaUser(Model.Usuarios usuario)
        {
            try
            {
                btnAlterar.Enabled = true;
                pnlDetalhe.Enabled = false;
                txtLogin.Text = usuario.login;
                txtRegistro.Text = usuario.registro;
                txtSalario.Text = (usuario.salario).ToString();
                txtCpf.Text = usuario.cpf;
                txtRg.Text = usuario.rg;
                txtNome.Text = usuario.nome;
                txtBancoHoras.Text = usuario.bancoHoras.ToString();

                if (usuario.status == 1)
                {
                    chkStatus.Checked = true;
                }
                else
                {
                    chkStatus.Checked = false;
                }

                if (usuario.id_Perfil == 1)
                {
                    rdbAdministrador.Checked = true;
                }
                else if (usuario.id_Perfil == 2)
                {
                    rdbGerente.Checked = true;
                }
                else if (usuario.id_Perfil == 4)
                {
                    rdbOperador.Checked = true;
                }
                else if (usuario.id_Perfil == 3)
                {
                    rdbCaixa.Checked = true;
                }
            }
            catch
            {
                MessageBox.Show("Erro não identificado, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            try
            {
                pnlDetalhe.Enabled = true;
                txtLogin.Enabled = true;
                txtRegistro.Enabled = true;
                txtNome.Enabled = true;
                txtCpf.Enabled = true;
                txtRg.Enabled = true;
                txtSalario.Enabled = true;
                chkStatus.Enabled = true;

                btnSalvar.Enabled = true;
                btnCancelar.Enabled = true;
                btnAlterar.Enabled = false;
                usuario = controle.PesquisaUserLogin(txtLogin.Text);
            }
            catch
            {
                MessageBox.Show("Erro não identificado, por favor, verifique os campos e tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (validaCampos())
                {
                    usuario.login = txtLogin.Text.ToUpper().Trim();
                    usuario.registro = txtRegistro.Text.ToUpper().Trim();
                    usuario.rg = txtRg.Text.ToUpper().Trim();
                    usuario.salario = Convert.ToDecimal(txtSalario.Text);
                    usuario.nome = txtNome.Text.ToUpper().Trim();

                    if (validacao.validaTipoCpfCnpj(txtCpf.Text))
                    {
                        usuario.cpf = txtCpf.Text;
                    }

                    if (chkStatus.Checked)
                    {
                        usuario.status = 1;
                    }
                    else
                    {
                        usuario.status = 0;
                    }

                    if (rdbAdministrador.Checked)
                    {
                        usuario.id_Perfil = 1;
                    }
                    else if (rdbGerente.Checked)
                    {
                        usuario.id_Perfil = 2;
                    }
                    else if (rdbOperador.Checked)
                    {
                        usuario.id_Perfil = 4;
                    }
                    else if (rdbCaixa.Checked)
                    {
                        usuario.id_Perfil = 3;
                    }

                    controle.SalvaAtualiza();
                    limpaCampos();
                }
                else
                {
                    MessageBox.Show("Todos os campos são de preenchimento obrigatório, por favor verifique e tente novamente", "Ação Inválida");
                }
            }
            catch
            {
                MessageBox.Show("Erro não identificado, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void limpaCampos()
        {
            btnAlterar.Enabled = false;
            btnSalvar.Enabled = false;
            btnCancelar.Enabled = false;
            pnlDetalhe.Enabled = false;

            txtLogin.Enabled = false;
            txtRegistro.Enabled = false;
            txtNome.Enabled = false;
            txtCpf.Enabled = false;
            txtRg.Enabled = false;
            txtSalario.Enabled = false;
            txtLogin.Text = "";
            txtRegistro.Text = "";
            txtNome.Text = "";
            txtCpf.Text = "";
            txtRg.Text = "";
            txtSalario.Text = "";
            chkStatus.Checked = false;
            rdbAdministrador.Checked = false;
            rdbCaixa.Checked = false;
            rdbGerente.Checked = false;
            rdbOperador.Checked = false;
            usuario = new Model.Usuarios();
            carregaListaUsuarios();
        }

        private bool validaCampos()
        {
            if (!txtLogin.Text.Equals("") && !txtRegistro.Text.Equals(""))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            limpaCampos();
        }
    }
}
