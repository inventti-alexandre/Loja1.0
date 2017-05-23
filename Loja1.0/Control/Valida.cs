using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Loja1._0.Control
{
    public class Valida
    {
        public bool validaTipoCpfCnpj(string documento)
        {
            if(documento.Length == 11)
            {
                return testaCpf(documento);
            }
            else if(documento.Length == 14)
            {
                return testaCnpj(documento);
            }
            else
            {
                MessageBox.Show("Por favor insira somente números no campo CNPJ/CPF", "Ação Inválida");
                return false;
            }
        }

        private bool testaCnpj(string documento)
        {
            char[] cnpjArray = documento.ToCharArray();
            int[] calcCnpj = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2};
            int cnpjAux = 0, resto;
            for (int i = 0; i < 12; i++)
            {
                cnpjAux = cnpjAux + (Convert.ToInt32(cnpjArray[i].ToString()) * (calcCnpj[i + 1]));
            }
            resto = cnpjAux / 11;
            cnpjAux = cnpjAux - (resto * 11);
            if (cnpjAux < 2)
            {
                cnpjAux = 0;
            }
            else
            {
                cnpjAux = 11 - cnpjAux;
            }
            if (cnpjAux == Convert.ToInt32(cnpjArray[12].ToString()))
            {
                cnpjAux = 0;
                for (int i = 0; i < 13; i++)
                {
                    cnpjAux = cnpjAux + (Convert.ToInt32(cnpjArray[i].ToString()) * (calcCnpj[i]));
                }
                resto = cnpjAux / 11;
                cnpjAux = cnpjAux - (resto * 11);
                if (cnpjAux < 2)
                {
                    cnpjAux = 0;
                }
                else
                {
                    cnpjAux = 11 - cnpjAux;
                }
                if (cnpjAux == Convert.ToInt32(cnpjArray[13].ToString()))
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("O número informado não corresponde a um CPF válido, por favor confira e tente novamente");
                    return false;
                }
            }
            else
            {
                MessageBox.Show("O número informado não corresponde a um CPF válido, por favor confira e tente novamente");
                return false;
            }
        }

        private bool testaCpf(string documento)
        {
            char[] cpfArray = documento.ToCharArray();
            int cpfAux = 0, resto;
            for(int i = 0; i < 9; i++)
            {
                cpfAux = cpfAux + (Convert.ToInt32(cpfArray[i].ToString()) * (10 - i));
            }
            resto = cpfAux / 11;
            cpfAux = cpfAux - (resto * 11);
            if(cpfAux < 2)
            {
                cpfAux = 0;
            }
            else
            {
                cpfAux = 11 - cpfAux;
            }
            if (cpfAux == Convert.ToInt32(cpfArray[9].ToString()))
            {
                cpfAux = 0;
                for (int i = 0; i < 10; i++)
                {
                    cpfAux = cpfAux + (Convert.ToInt32(cpfArray[i].ToString()) * (11 - i));
                }
                resto = cpfAux / 11;
                cpfAux = cpfAux - (resto * 11);
                if (cpfAux < 2)
                {
                    cpfAux = 0;
                }
                else
                {
                    cpfAux = 11 - cpfAux;
                }
                if (cpfAux == Convert.ToInt32(cpfArray[10].ToString()))
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("O número informado não corresponde a um CPF válido, por favor confira e tente novamente");
                    return false;
                }
            }
            else
            {
                MessageBox.Show("O número informado não corresponde a um CPF válido, por favor confira e tente novamente");
                return false;
            }
        }
    }
}
