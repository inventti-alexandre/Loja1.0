using Loja1._0.Control;
using Loja1._0.View;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Loja1._0
{
    public partial class Login : Form
    {
        Controle controle = new Controle();
        Model.Usuarios user;
        static Abertura tela = new Abertura();
        static int exec = 0;

        public Login()
        {
            try
            {
                if (exec == 0)
                {
                    tela.Show();
                    exec++;
                    var t = Task.Run(async delegate
                    {
                        await Task.Delay(3000);
                        InitializeComponent();
                    });
                    t.Wait();
                }
                else
                {
                    InitializeComponent();
                }
            }
            catch
            {
                MessageBox.Show("Erro não identificado, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            try
            {
                tela.Dispose();

                if (txtLogin.Text.Equals(""))
                {
                    MessageBox.Show("Digite um valor válido para Login e Senha", "Usuário/Senha inválido");
                }
                else
                {
                    user = controle.PesquisaUserLogin(txtLogin.Text.Trim().ToUpper());
                    if (user == null)
                    {
                        MessageBox.Show("Digite um valor válido para Login e Senha", "Usuário/Senha inválido");
                    }
                    else
                    {
                        if (txtSenha.Text == user.senha && user.status != 0)
                        {
                            this.Hide();

                            Inicial form = new Inicial(user);
                            txtLogin.Text = "";
                            txtSenha.Text = "";
                            form.Show();

                        }

                        else if (txtSenha.Text == user.senha && user.status == 0)
                        {
                            if (controle.PesquisaUserValidos() == 0)
                            {
                                this.Hide();

                                user.status = 1;
                                controle.SalvaAtualiza();
                                Inicial form = new Inicial(user);
                                txtLogin.Text = "";
                                txtSenha.Text = "";
                                form.Show();

                            }
                            else
                            {
                                MessageBox.Show("Digite um valor válido para Login e Senha", "Usuário/Senha inválido");
                            }
                        }

                        else
                        {
                            MessageBox.Show("Digite um valor válido para Login e Senha", "Usuário/Senha inválido");
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Erro não identificado, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCadastro_Click(object sender, EventArgs e)
        {
            this.Hide();
            tela.Dispose();
            
            Cadastro form = new Cadastro();
            txtLogin.Text = "";
            txtSenha.Text = "";
            form.Show();
            
        }
    }
}
