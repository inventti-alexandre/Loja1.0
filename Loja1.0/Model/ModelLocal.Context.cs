﻿

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
using System.Data.Entity;
using System.Data.Entity.Infrastructure;


public partial class DbEntitiesLocal : DbContext
{
    public DbEntitiesLocal()
        : base("name=DbEntitiesLocal")
    {

    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        throw new UnintentionalCodeFirstException();
    }


    public virtual DbSet<Cidades> Cidades { get; set; }

    public virtual DbSet<Clientes> Clientes { get; set; }

    public virtual DbSet<Compras> Compras { get; set; }

    public virtual DbSet<Contabilidade> Contabilidade { get; set; }

    public virtual DbSet<CtrlEntrega> CtrlEntrega { get; set; }

    public virtual DbSet<CtrlPonto> CtrlPonto { get; set; }

    public virtual DbSet<Estados> Estados { get; set; }

    public virtual DbSet<Estoque> Estoque { get; set; }

    public virtual DbSet<Fechamento> Fechamento { get; set; }

    public virtual DbSet<Fornecedores> Fornecedores { get; set; }

    public virtual DbSet<Gerenciamento> Gerenciamento { get; set; }

    public virtual DbSet<LogPonto> LogPonto { get; set; }

    public virtual DbSet<Movimentos> Movimentos { get; set; }

    public virtual DbSet<Pagamentos> Pagamentos { get; set; }

    public virtual DbSet<Pagamentos_Vendas> Pagamentos_Vendas { get; set; }

    public virtual DbSet<Perfis> Perfis { get; set; }

    public virtual DbSet<Tipos_Movimentacao> Tipos_Movimentacao { get; set; }

    public virtual DbSet<Usuarios> Usuarios { get; set; }

    public virtual DbSet<Vendas> Vendas { get; set; }

    public virtual DbSet<Vendas_Produtos> Vendas_Produtos { get; set; }

    public virtual DbSet<UnidMedidas> UnidMedidas { get; set; }

    public virtual DbSet<Produtos> Produtos { get; set; }

}

}

