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
    
    public partial class Vendas_Produtos
    {
        public int id { get; set; }
        public Nullable<int> id_venda { get; set; }
        public Nullable<int> num_item { get; set; }
        public Nullable<int> id_produto { get; set; }
        public int quantidade { get; set; }
    
        public virtual Vendas Vendas { get; set; }
        public virtual Produtos Produtos { get; set; }
    }
}
