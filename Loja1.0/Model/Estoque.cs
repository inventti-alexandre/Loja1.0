
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
    
public partial class Estoque
{

    public int id { get; set; }

    public int id_produto { get; set; }

    public int qnt_atual { get; set; }

    public int qnt_minima { get; set; }

    public Nullable<int> qnt_maxima { get; set; }

    public Nullable<int> num_local { get; set; }

    public string letra_local { get; set; }

    public string ref_local { get; set; }



    public virtual Produtos Produtos { get; set; }

}

}
