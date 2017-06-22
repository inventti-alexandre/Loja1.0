using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Loja1._0.Control
{
    class CupomFiscal
    {
        public string formaPagamento;
        public string valorPagamento;
        public string acrescimo;
        public string desconto;

        public CupomFiscal()// string formaPagamento, string valorPagamento, string valorTotal, string acrescimo, string desconto)
        {
            formaPagamento = "";
            valorPagamento = "0";
            acrescimo = "0";
            desconto = "0";
        }
    }
}