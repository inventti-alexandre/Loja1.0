
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
    
public partial class Pagamentos
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public Pagamentos()
    {

        this.Pagamentos_Vendas = new HashSet<Pagamentos_Vendas>();

    }


    public int id { get; set; }

    public string tipoPag { get; set; }

    public string formaPag { get; set; }

    public Nullable<decimal> valorTotal { get; set; }

    public Nullable<int> qntParcelas { get; set; }

    public Nullable<System.DateTime> dataPagamento { get; set; }

    public Nullable<int> numParcela { get; set; }

    public Nullable<decimal> valorParcela { get; set; }

    public string numChequePrimeiro { get; set; }

    public string numChequeUltimo { get; set; }

    public Nullable<int> id_movimento { get; set; }

    public int status { get; set; }



    public virtual Movimentos Movimentos { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<Pagamentos_Vendas> Pagamentos_Vendas { get; set; }

}

}
