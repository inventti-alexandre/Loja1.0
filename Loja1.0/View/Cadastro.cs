using Loja1._0.Control;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Loja1._0
{
    public partial class Cadastro : Form
    {
        Model.Usuarios user = new Model.Usuarios();
        Controle controle = new Controle();
        static int novoPerfil;

        public Cadastro()
        {
            InitializeComponent();
            txtLogin.Focus();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                if (validaCampos())
                {
                    user = new Model.Usuarios();
                    controle.salvarUsuario(user);
                    user.nome = txtLogin.Text;
                    user.senha = txtSenha.Text;
                    user.num_perfil = novoPerfil;
                    user.status = 0;
                    controle.salvaAtualiza();
                    MessageBox.Show("Criado novo usuário, login : " + txtLogin.Text + ", aguardando validação.", "Criação de usuário e senha");
                    Login form = new Login();
                    form.Show();
                    this.Hide();
                }
            }
            catch
            {
                MessageBox.Show("Erro não identificado, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancela_Click(object sender, EventArgs e)
        {
            Login form = new Login();
            form.Show();
            this.Hide();
        }

        private bool validaCampos()
        {
            if (txtLogin.Text.Equals(""))
            {
                MessageBox.Show("O campo nome não deve estar em branco","alerta");
                return false;
            }

            else if (txtSenha.Text.Equals(""))
            {
                MessageBox.Show("O campo senha deve ser preenchido", "alerta");
                return false;
            }

            else if (txtSenha.Text.Length != 8 || txtSenha.Text.All(char.IsDigit) || txtSenha.Text.All(char.IsLetter))
            {
                MessageBox.Show("O campo senha deve conter 8 digitos, ser composto por letras e números", "alerta");
                return false;
            }

            else if (!txtSenha.Text.Equals(txtConfirma.Text))
            {
                MessageBox.Show("A senha e a confirmação não conhecidem", "alerta");
                return false;
            }

            else if (novoPerfil == 0)
            {
                MessageBox.Show("Deve ser selecionado o tipo de perfil do usuário", "alerta");
                return false;
            }

            else
            {
                return true;
            }
        }

        private void rdbAdministrador_CheckedChanged(object sender, EventArgs e)
        {
            novoPerfil = 1;
        }

        private void rdbGerente_CheckedChanged(object sender, EventArgs e)
        {
            novoPerfil = 2;
        }

        private void rdbCaixa_CheckedChanged(object sender, EventArgs e)
        {
            novoPerfil = 3;
        }

        private void rdbOperador_CheckedChanged(object sender, EventArgs e)
        {
            novoPerfil = 4;
        }
    }
}
