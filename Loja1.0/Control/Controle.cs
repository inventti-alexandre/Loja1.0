﻿using Loja1._0.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loja1._0.Control
{
    class Controle
    {
        #region Controles de uso geral

        Repository dbRepository = new Repository();

        public void SalvaAtualiza()
        {
            dbRepository.SalvaAlteracao();
        }

        #endregion

        #region Controle do BD de Usuários

        public int PesquisaUserValidos()
        {
            return dbRepository.ContaUserValidos();
        }

        public Model.Usuarios PesquisaUserId(int user)
        {
            int pesquisa = user;

            return dbRepository.PesquisaUserById(pesquisa);
        }

        public void SalvarUsuario(Model.Usuarios usuario)
        {
            dbRepository.SalvarNovoUsuario(usuario);
        }

        public void ExcluiUsuario(Model.Usuarios usuario)
        {
            dbRepository.ExcluirUsuarioExistente(usuario);
        }

        public Model.Usuarios PesquisaUserLogin(string user)
        {
            string pesquisa = user;

            return dbRepository.PesquisaSimplesUser(pesquisa);
        }

        public Model.Usuarios PesquisaUserNome(string nome)
        {
            string nomeCompleto = nome;

            return dbRepository.PesquisaNomeUser(nomeCompleto);
        }

        internal List<Model.Produtos> PesquisaVendasProduto(string produto)
        {
            string prod = produto;

            return dbRepository.PesquisaVendasByProduto(prod);
        }

        internal List<Model.Usuarios> PesquisaUserNomeRelação(string ParteNome)
        {
            string nome = ParteNome;

            return dbRepository.PesquisaRelaçãoUsuarios(nome);
        }

        internal List<Model.Fornecedores> PesquisaListaFornecedores(string ParteNome)
        {
            string nome = ParteNome;

            return dbRepository.PesquisaListaForn(nome);
        }

        public List<Model.Usuarios> PesquisaGeralUser()
        {
            return dbRepository.PesquisaCompletaUsers();
        }

        public List<Model.Usuarios> UsuariosInvalidos()
        {
            return dbRepository.PesquisaUsuariosInvalidos();
        }

        public List<Model.Usuarios> PesquisaUserPerfilId(int id_Perfil)
        {
            int idMinus = id_Perfil;

            return dbRepository.PesquisaUsersIdPerfil(idMinus);
        }

        public List<Compras> PesquisaComprasPeriodo(DateTime dataInicio, DateTime dataFim)
        {
            DateTime inicio = dataInicio;
            DateTime fim = dataFim;

            return dbRepository.PesquisaComprasByDate(inicio, fim);
        }

        #endregion

        #region Controle do BD Produtos

        public Model.Produtos PesquisaProdutoNome(string selectedValue)
        {
            string pesquisa = selectedValue;

            return dbRepository.PesquisaProdutoByNome(pesquisa);
        }

        public Model.Produtos PesquisaProdutoCod(string cod)
        {
            string codigo = cod;

            return dbRepository.PesquisaProdutoByCodigo(codigo);
        }

        internal List<Pagamentos> PesquisaPagamentoRecebidoPeriodo(DateTime dataInicio, DateTime dataFim)
        {
            return dbRepository.PesquisaPagamentosPeriodo(dataInicio, dataFim);
        }

        internal List<Movimentos> PesquisaMovSaidaPeriodo(DateTime dataInicio, DateTime dataFim)
        {
            return dbRepository.PesquisaSaidaPagamentosPeriodo(dataInicio, dataFim);
        }

        internal List<CtrlEntrega> PesquisaEntregaIdVenda(int id)
        {
            int Id = id;

            return dbRepository.PesquisaEntregaByIdVenda(Id);
        }

        internal List<Vendas_Produtos> PesquisaVendasProdutoNome(string produto)
        {
            string produtoNome = produto;

            return dbRepository.PesquisaVendas_ProdutoByProduto(produtoNome);
        }

        public void SalvarProduto(Model.Produtos produto)
        {
            dbRepository.SalvarNovoProduto(produto);
        }

        internal List<Vendas> PesquisaVendasUser(string usuario)
        {
            string user = usuario;

            return dbRepository.PesquisaVendasByUser(usuario);
        }

        public Model.Produtos PesquisaProdutoId(int id)
        {
            int pesquisa = id;

            return dbRepository.PesquisaProdutoById(pesquisa);
        }

        public List<Model.Produtos> PesquisaProdutosNomeId(string valor)
        {
            string busca = valor;

            return dbRepository.PesquisaProdutoByNomeId(busca);
        }

        public List<Model.Produtos> PesquisaGeralProd()
        {
            return dbRepository.PesquisaProdutos();
        }

        public List<Model.Produtos> PesquisaProdutosValidoNome(string busca)
        {
            string pesquisa = busca;

            return dbRepository.PesquisaProdutosValidoByName(pesquisa);
        }

        internal List<Model.Produtos> PesquisaProdutosFornecedor(string fornecedorNome)
        {
            return dbRepository.PesquisaProdutosPorFornecedor(fornecedorNome);
        }

        #endregion

        #region Controle do BD de Estados

        public List<Estados> PesquisaGeralEstados()
        {
            return dbRepository.PesquisaEstados();
        }        

        #endregion

        #region Controle do BD Contabilidade

        public List<Contabilidade> PesquisaContabilidade()
        {
            return dbRepository.PesquisaGeralContabilidade();
        }

        public void SalvaContabilidade(Contabilidade contabilidade)
        {
            dbRepository.SalvarNovaContabilidade(contabilidade);
        }

        public Model.Contabilidade PesquisaContabilidadeById(int v)
        {
            return dbRepository.PesquisaContabilidadeId(v);
        }

        internal Compras pesquisaProdutoCompra(int produtoid)
        {
            return dbRepository.PesquisaCompraAtualProduto(produtoid);
        }

        #endregion

        #region Controle do BD de Cidades

        public List<Cidades> PesquisaCidadesPorNomeUF(string estado)
        {
            string pesquisa = estado;

            return dbRepository.PesquisaCidadeByNameUF(pesquisa);
        }

        internal Cidades PesquisaCidadeById(int id_Cidade)
        {
            int pesquisa = id_Cidade;

            return dbRepository.PesquisaCidadeId(pesquisa);
        }

        public Cidades PesquisaCidade(string cidade)
        {
            string pesquisa = cidade;

            return dbRepository.PesquisaCidadeByName(pesquisa);
        }

        public List<Vendas> PesquisaVendasDia(DateTime today)
        {
            return dbRepository.PesquisaVendasByDate(today);
        }

        public List<Cidades> PesquisaCidadesPorEstado(int idEstado)
        {
            int pesquisa = idEstado;

            return dbRepository.PesquisaCidadesByEstado(pesquisa);
        }

        public void SalvarCidade(Cidades cidade)
        {
            dbRepository.SalvarNovaCidade(cidade);
        }

        public void ExcluirCidade(Cidades cidade)
        {
            dbRepository.RemoverCidade(cidade);
        }

        #endregion

        #region Controle do BD de gerenciamento

        public void SalvaGerenciamento(Gerenciamento gerencia)
        {
            dbRepository.SalvarNovoGerenciamento(gerencia);
        }

        public Gerenciamento PesquisaGerenciamento(int id)
        {
            int pesquisa = id;

            return dbRepository.PesquisaGerenciamento(pesquisa);
        }

        #endregion

        #region Controle do BD de Clientes

        public Model.Clientes PesquisaClienteCpf(string pesquisa)
        {
            string cpf = pesquisa;

            return dbRepository.PesquisaClienteByCpf(cpf);
        }

        public void SalvarCliente(Model.Clientes cliente)
        {
            dbRepository.SalvarNovoCliente(cliente);
        }

        public Model.Clientes PesquisaClienteById(int selectedIndex)
        {
            int id = selectedIndex;

            return dbRepository.PesquisaClienteId(id);
        }

        public List<Model.Clientes> PesquisaClientesCompleta(string busca)
        {
            string pesquisa = busca;

            return dbRepository.PesquisaClienteByCpfOrNome(pesquisa);
        }

        #endregion

        #region Controle do BD Vendas_Produtos

        public List<Vendas_Produtos> PesquisaProdutosVenda(int id)
        {
            int busca = id;

            return dbRepository.PesquisaProdutosVendaById(busca);
        }

        public List<Vendas_Produtos> PesquisaProdutosPedido(int VendaId)
        {
            int id_Venda = VendaId;

            return dbRepository.PesquisaProdutosVendasByPedido(id_Venda);
        }

        public void SalvaProdutosVendidos(Vendas_Produtos prodVendido)
        {
            dbRepository.SalvarNovoProdutoVendido(prodVendido);
        }

        #endregion

        #region Controle do BD de Estoques

        public List<Estoque> PesquisaEstoqueRelatorio()
        {
            return dbRepository.PesquisaEstoqueOrdened();
        }

        public void SalvarEstoque(Estoque estoque)
        {
            dbRepository.SalvarNovoEstoque(estoque);
        }

        public Estoque PesquisaProdEstoqueId(int id)
        {
            int valor = id;

            return dbRepository.PesquisaEstoqueByProdID(valor);
        }

        #endregion    

        #region Controle do BD Tipos de Movimentos

        public List<Tipos_Movimentacao> PesquisaTiposMovimento()
        {
            return dbRepository.PesquisaTiposMov();
        }

        public List<Tipos_Movimentacao> PesquisaTiposMovimento(string pesquisa)
        {
            string busca = pesquisa;

            return dbRepository.PesquisaTiposMovimentoByDesc(busca);
        }

        public List<Tipos_Movimentacao> PesquisaSubTipoMov(string valor)
        {
            string busca = valor;

            return dbRepository.PesquisaSubTiposMovimentoByDesc(busca);
        }

        public int PesquisaCompletaIDTipoMov(string descricao, string subTipo, string formaPg)
        {
            string desc = descricao;
            string tipo = subTipo;
            string form = formaPg;

            return dbRepository.PesquisaMovimentoID(desc, tipo, form);
        }

        public Tipos_Movimentacao PesquisaTiposMovimentoId(int? id_tipo)
        {
            return dbRepository.PesquisaTipoMovById(id_tipo);
        }

        #endregion

        #region Controle do BD de Fornecedores

        public void SalvarFornecedor(Model.Fornecedores fornecedor)
        {
            dbRepository.SalvarNovoFornecedor(fornecedor);
        }

        public List<Model.Fornecedores> PesquisaFornecedores(string pesquisa)
        {
            string busca = pesquisa;

            return dbRepository.PesquisaFornecedoresByNomeCnpj(busca);
        }

        internal void SalvaEntrega(CtrlEntrega entrega)
        {
            dbRepository.SalvarNovaEntrega(entrega);
        }

        public Model.Fornecedores PesquisaFornecedorById(int Id)
        {
            int valor = Id;

            return dbRepository.PesquisaFornecedoresID(valor);
        }

        public Model.Fornecedores PesquisaFornecedorCpnj(string pesquisa)
        {
            string cnpj = pesquisa;

            return dbRepository.PesquisaFornecedoresByCnpj(cnpj);
        }

        public List<Model.Fornecedores> PesquisaFornecedoresCompleta(string pesquisa)
        {
            string busca = pesquisa;

            return dbRepository.PesquisaFornecedoresByNomeCnpjOfStatus(busca);
        }

        public Model.Fornecedores PesquisaFornecedorByNome(string pesquisa)
        {
            string nome = pesquisa;

            return dbRepository.PesquisaFornecedorValidoByNome(nome);
        }

        #endregion

        #region Controle do BD de Unidades de Medida

        public List<UnidMedidas> PesquisaUnidades(string v)
        {
            return dbRepository.PesquisaMedidas();
        }

        public UnidMedidas PesquisaMedidaByDesc(string desc)
        {
            string nome = desc;

            return dbRepository.PesquisaMedidaNome(nome);
        }

        public UnidMedidas PesquisaMedidaById(int id_medida)
        {
            int id = id_medida;

            return dbRepository.PesquisaMedidaId(id);
        }

        #endregion

        #region Controle do BD de Vendas

        public void SalvarVenda(Vendas venda)
        {
            dbRepository.SalvarNovaVenda(venda);
        }

        public Vendas PesquisaVendaID(int id)
        {
            int valor = id;

            return dbRepository.PesquisaVendabyID(valor);
        }

        public List<Vendas> PesquisaVendasGeral()
        {
            return dbRepository.PesquisaVendas();
        }

        public int QuantidadeVendasDia(DateTime today)
        {
            return dbRepository.PesquisaVendasDia(today);
        }

        public double? ValorTotalVendasDia(DateTime today)
        {
            return dbRepository.PesquisaValorVendasDia(today);
        }

        #endregion

        #region Controle do BD de Movimentos

        public void SalvarMovimento(Movimentos movimento)
        {
            dbRepository.SalvarNovoMovimento(movimento);
        }

        public List<Movimentos> PesquisaMovPeriodo(DateTime txtDtInicio, DateTime txtDtFim)
        {
            DateTime dtInicio = txtDtInicio;
            DateTime dtFim = txtDtFim;

            return dbRepository.PesquisaMovimentoIntervalo(dtInicio, dtFim);
        }

        public List<Movimentos> PesquisaMovimentosGeral()
        {
            return dbRepository.PesquisaMovimentosTotais();
        }

        public List<Movimentos> PesquisaMovimentoTipo(int id_tipo)
        {
            int id = id_tipo;

            return dbRepository.PesquisaMovimentoByTipoId(id);
        }

        public Movimentos PesquisaMovimentoId(int? id_movimento)
        {
            int id = Convert.ToInt32(id_movimento);

            return dbRepository.PesquisaMovimentoByID(id);
        }

        public void RemoveMovimento(Movimentos movimento)
        {
            dbRepository.ExcluirMovimento(movimento);
        }

        public List<Movimentos> PesquisaMovimentoReferIdPagamento(int id)
        {
            int idPagamento = id;

            return dbRepository.PesquisaMovimentosReferentePagamento(idPagamento);
        }

        public decimal PesquisaMovDiaTipo(DateTime today, int mov)
        {
            return dbRepository.PesquisaMovimentoByDiaTipo(today, mov);
        }

        #endregion

        #region Controle do BD Pagamentos_Vendas

        public void SalvaPagamentoPedido(Pagamentos_Vendas pagamentoPedido)
        {
            dbRepository.SalvarNovoPagamentoPedido(pagamentoPedido);
        }

        public bool PesquisaPagamentoIdVenda(string numPedido)
        {
            int idVenda = Convert.ToInt32(numPedido);

            return dbRepository.PesquisaPagamentoVendaByIdVenda(idVenda);
        }

        public List<Pagamentos_Vendas> PesquisaPagVendaIdPagamento(int id)
        {
            int idPagamento = id;

            return dbRepository.PesquisaPagamentoVendaByIdPagamento(idPagamento);
        }

        public void RemovePagamentoVenda(Pagamentos_Vendas pagVend)
        {
            dbRepository.ExcluirPagamento_Venda(pagVend);
        }

        #endregion

        #region Controle do BD de Pagamentos

        public Pagamentos PesquisaPagamentoId(int id)
        {
            return dbRepository.PesquisaIdPagamento(id);
        }

        public void SalvarPagamento(Pagamentos pagamento)
        {
            dbRepository.SalvarNovoPagamento(pagamento);
        }

        public List<Pagamentos> PesquisaPagamentosGeral()
        {
            return dbRepository.PesquisaPagamentosTotais();
        }

        internal List<Vendas> pesquisaVendasPeriodo(DateTime inicio, DateTime final)
        {
            return dbRepository.PesquisaVendasByDate(inicio, final);
        }

        public void RemovePagamento(Pagamentos pag)
        {
            dbRepository.ExcluirPagamento(pag);
        }        

        public List<Pagamentos> PesquisaUltimoPagamento()
        {
            return dbRepository.PesquisaPagamentosUltimo();
        }

        #endregion

        //SCRIPT FECHAMENTO
        #region Controle do BD Pedido

        public bool PesquisaFechamentoIdVenda(string numPedido)
        {
            int idVenda = Convert.ToInt32(numPedido);

            return dbRepository.PesquisaFechamentoByIdVenda(idVenda);
        }

        public void SalvarFechamento(Fechamento pedido)
        {
            dbRepository.SalvarNovoPedido(pedido);
        }
        #endregion

        public void removeProdutoVenda(Vendas_Produtos prodVend)
        {
            dbRepository.RemoveProdVenda(prodVend);
        }

        public void SalvarCompras(Compras compra)
        {
            dbRepository.SalvarNovaCompra(compra);
        }

        public Compras PesquisaCompraAnterior(int id)
        {
            int idProduto = id;

            return dbRepository.PesquisaCompraByIdProduto(idProduto);
        }

        public Compras PesquisaIcmsCompra(int id_produto)
        {
            int id = id_produto;

            return dbRepository.PesquisaCompraPendente(id);
        }

        public void salvarLog(LogPonto log)
        {
            dbRepository.SalvarNovoLog(log);
        }

        public CtrlPonto PesquisaPontoDia(int id, string Mes, string Ano, string Dia)
        {
            int idUser = id;
            int dia = Convert.ToInt32(Dia);
            int mes = Convert.ToInt32(Mes);
            int ano = Convert.ToInt32(Ano);

            return dbRepository.PesquisaPontoPeriodoDia(id, dia, mes, ano);
        }

        internal void SalvaPonto(CtrlPonto ponto)
        {
            dbRepository.SalvarNovoPonto(ponto);
        }

        public List<CtrlPonto> PesquisaPonto(int id, string mes, string ano)
        {
            int idUser = id;
            int month = Convert.ToInt32(mes);
            int year = Convert.ToInt32(ano);

            return dbRepository.PesquisaPontoPeriodo(idUser, month, year);
        }

        internal void ExcluirCtrlPonto(CtrlPonto ponto)
        {
            dbRepository.ExcluirCtrlPonto(ponto);
        }
    }
}
