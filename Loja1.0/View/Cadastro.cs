using Loja1._0.Control;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;

namespace Loja1._0
{
    public partial class Cadastro : Form
    {
        //declaração das variaveis locais referentes ao Control
        Model.Usuarios user = new Model.Usuarios();
        Controle controle = new Controle();
        Email email = new Email();

        //declaração das variaveis locais estaticas
        static int novoPerfil;

        public Cadastro()
        {
            //inicilização do form e definção do campo inicial
            InitializeComponent();
            txtLogin.Focus();
        }

        //Ação do botão exit, omite o formulário atual e apresenta a tela de login/logout
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login form = new Login();
            form.Show();
        }

        //ação do botão confirmar, salva como inativo o usuário cadastrado
        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            //realiza todas as ações determinadas ou passa a função catch
            try
            {
                //verifica o correto preenchimento dos campos do formulário
                if (validaCampos())
                {
                    //instancia novo usuário em memória
                    user = new Model.Usuarios();
                    controle.SalvarUsuario(user);
                    //determina os parametros dessa nova instancia
                    user.nome = txtLogin.Text.ToUpper().Trim();
                    user.senha = txtSenha.Text.Trim();
                    user.num_perfil = novoPerfil;
                    user.status = 0;
                    //salva esta instancia da memória para o banco de dados
                    controle.SalvaAtualiza();
                    //informa ao usuário a criação de novo usuário
                    MessageBox.Show("Criado novo usuário, login : " + txtLogin.Text.Trim().ToUpper() + ", aguardando validação.", "Criação de usuário e senha");
                    //apresenta a tela de login e oculta o formulário atual
                    Login form = new Login();
                    form.Show();
                    this.Hide();
                }
            }
            catch
            {
                //Envio de email como parametro string do método da classe Email
                email.EnviaEmail("Cadastro.cs, linha 36 a 58");

                //mensagem genérica de erro 
                MessageBox.Show("Erro não identificado em Cadastro.cs, linha 36 a 58, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }        

        private void btnCancela_Click(object sender, EventArgs e)
        {
            //Ação do botão cancela, similar ao sair
            Login form = new Login();
            form.Show();
            this.Hide();
        }

        //metodo para validação do preenchimento dos campos do formulário
        private bool validaCampos()
        {
            //verifica se existem caracteres no campo Login
            if (txtLogin.Text.Trim().Equals(""))
            {
                //Mensagem para o usuário realizar a correção caso o erro seja encontrado
                MessageBox.Show("O campo nome não deve estar em branco","alerta");
                return false;
            }

            //verifica se existem caracteres no campo Senha
            else if (txtSenha.Text.Equals(""))
            {
                //Mensagem para o usuário realizar a correção caso o erro seja encontrado
                MessageBox.Show("O campo senha deve ser preenchido", "alerta");
                return false;
            }

            //verifica se a senha respeita os parametros estipulados de 8 digitos e a combinação entre texto e número
            else if (txtSenha.Text.Trim().Length != 8 || txtSenha.Text.Trim().All(char.IsDigit) || txtSenha.Text.Trim().All(char.IsLetter))
            {
                //Mensagem para o usuário realizar a correção caso o erro seja encontrado
                MessageBox.Show("O campo senha deve conter 8 digitos, ser composto por letras e números", "alerta");
                return false;
            }

            //verifica se os campos de senha e confirmação estão preenchidos de forma identica
            else if (!txtSenha.Text.Trim().Equals(txtConfirma.Text.Trim()))
            {
                //Mensagem para o usuário realizar a correção caso o erro seja encontrado
                MessageBox.Show("A senha e a confirmação não conhecidem", "alerta");
                return false;
            }

            //verifica se houve a seleção de perfil para o usuário a ser cadastrado
            else if (novoPerfil == 0)
            {
                //Mensagem para o usuário realizar a correção caso o erro seja encontrado
                MessageBox.Show("Deve ser selecionado o tipo de perfil do usuário", "alerta");
                return false;
            }

            //caso não haja nenhum dos erros determinados a função retorna verdadeiro
            else
            {
                return true;
            }
        }

        //caso o radio button Administrador seja selecionado atribui valor a variável novoPerfil correspondente ao id do perfil 
        private void rdbAdministrador_CheckedChanged(object sender, EventArgs e)
        {
            novoPerfil = 1;
        }

        //caso o radio button Gerente seja selecionado atribui valor a variável novoPerfil correspondente ao id do perfil
        private void rdbGerente_CheckedChanged(object sender, EventArgs e)
        {
            novoPerfil = 2;
        }

        //caso o radio button Operador seja selecionado atribui valor a variável novoPerfil correspondente ao id do perfil
        private void rdbCaixa_CheckedChanged(object sender, EventArgs e)
        {
            novoPerfil = 3;
        }

        //caso o radio button Caixa seja selecionado atribui valor a variável novoPerfil correspondente ao id do perfil
        private void rdbOperador_CheckedChanged(object sender, EventArgs e)
        {
            novoPerfil = 4;
        }
    }
}
