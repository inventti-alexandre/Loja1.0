
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace Loja1._0.Model
{

using System;
    using System.Collections.Generic;
    
public partial class CtrlEntrega
{

    public int id { get; set; }

    public int id_Venda { get; set; }

    public Nullable<int> id_Cliente { get; set; }

    public int id_Produto { get; set; }

    public System.DateTime DataVenda { get; set; }

    public string EndEntrega { get; set; }



    public virtual Clientes Clientes { get; set; }

    public virtual Produtos Produtos { get; set; }

    public virtual Vendas Vendas { get; set; }

}

}
