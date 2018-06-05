
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
    
public partial class Vendas
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public Vendas()
    {

        this.CtrlEntrega = new HashSet<CtrlEntrega>();

        this.Pagamentos_Vendas = new HashSet<Pagamentos_Vendas>();

        this.Vendas_Produtos = new HashSet<Vendas_Produtos>();

    }


    public int id { get; set; }

    public System.DateTime data_Venda { get; set; }

    public string cpf { get; set; }

    public string cnpj { get; set; }

    public Nullable<double> icms { get; set; }

    public Nullable<double> valor_Venda { get; set; }

    public int id_Usuario { get; set; }

    public Nullable<int> id_Cliente { get; set; }

    public decimal desconto { get; set; }

    public Nullable<decimal> comissao { get; set; }



    public virtual Clientes Clientes { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<CtrlEntrega> CtrlEntrega { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<Pagamentos_Vendas> Pagamentos_Vendas { get; set; }

    public virtual Usuarios Usuarios { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<Vendas_Produtos> Vendas_Produtos { get; set; }

}

}
