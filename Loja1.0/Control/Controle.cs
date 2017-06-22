using Loja1._0.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loja1._0.Control
{
    class Controle
    {
        Repository dbRepository = new Repository();
        public void salvaAtualiza()
        {
            dbRepository.salvaAlteracao();
        }

        public void salvarUsuario(Model.Usuarios usuario)
        {
            dbRepository.salvarNovoUsuario(usuario);
        }

        public void excluiUsuario(Model.Usuarios usuario)
        {
            dbRepository.excluirUsuarioExistente(usuario);
        }

        public Model.Usuarios pesquisaUserLogin(string user)
        {
            string pesquisa = user;

            return dbRepository.pesquisaSimplesUser(pesquisa);
        }

        public List<Model.Usuarios> pesquisaGeralUser()
        {
            return dbRepository.pesquisaCompletaUsers();
        }

        public List<Model.Usuarios> usuariosInvalidos()
        {
            return dbRepository.pesquisaUsuariosInvalidos();
        }

        public Model.Produtos pesquisaProdutoNome(string selectedValue)
        {
            string pesquisa = selectedValue;

            return dbRepository.pesquisaProdutoByNome(pesquisa);
        }

        public List<Estados> pesquisaGeralEstados()
        {
            return dbRepository.pesquisaEstados();
        }

        public void salvarFornecedor(Model.Fornecedores fornecedor)
        {
            dbRepository.salvarNovoFornecedor(fornecedor);
        }

        public Model.Usuarios pesquisaUserId(int user)
        {
            int pesquisa = user;

            return dbRepository.pesquisaUserById(pesquisa);
        }

        public Model.Produtos pesquisaProdutoCod(string cod)
        {
            string codigo = cod;

            return dbRepository.pesquisaProdutoByCodigo(codigo);
        }

        public List<Estoque> pesquisaEstoqueRelatorio()
        {
            return dbRepository.pesquisaEstoqueOrdened();
        }

        public int pesquisaUserValidos()
        {
            return dbRepository.contaUserValidos();
        }

        public List<Tipos_Movimentacao> pesquisaTiposMovimento()
        {
            return dbRepository.pesquisaTiposMov();
        }

        public List<Cidades> pesquisaCidadesPorNomeUF(string estado)
        {
            string pesquisa = estado;

            return dbRepository.pesquisaCidadeByNameUF(pesquisa);
        }

        public List<Vendas_Produtos> pesquisaProdutosVenda(int id)
        {
            int busca = id;

            return dbRepository.pesquisaProdutosVendaById(busca);
        }

        public Model.Contabilidade pesquisaContabilidadeById(int v)
        {
            return dbRepository.pesquisaContabilidadeId(v);
        }

        public List<UnidMedidas> pesquisaUnidades(string v)
        {
            return dbRepository.pesquisaMedidas();
        }

        public void salvarProduto(Model.Produtos produto)
        {
            dbRepository.salvarNovoProduto(produto);
        }

        public Model.Produtos pesquisaProdutoId(int id)
        {
            int pesquisa = id;

            return dbRepository.pesquisaProdutoById(pesquisa);
        }

        public List<Model.Produtos> pesquisaProdutosNomeId(string valor)
        {
            string busca = valor;

            return dbRepository.pesquisaProdutoByNomeId(busca);
        }

        public List<Model.Produtos> pesquisaGeralProd()
        {
            return dbRepository.pesquisaProdutos();
        }

        public List<Tipos_Movimentacao> pesquisaTiposMovimento(string pesquisa)
        {
            string busca = pesquisa;

            return dbRepository.pesquisaTiposMovimentoByDesc(busca);
        }

        public Gerenciamento pesquisaGerenciamento(int id)
        {
            int pesquisa = id;

            return dbRepository.pesquisaGerenciamento(pesquisa);
        }

        public List<Model.Fornecedores> pesquisaFornecedores(string pesquisa)
        {
            string busca = pesquisa;

            return dbRepository.pesquisaFornecedoresByNomeCnpj(busca);
        }

        public Cidades pesquisaCidade(string cidade)
        {
            string pesquisa = cidade;

            return dbRepository.pesquisaCidadeByName(pesquisa);
        }

        public List<Cidades> pesquisaCidadesPorEstado(int idEstado)
        {
            int pesquisa = idEstado;

            return dbRepository.pesquisaCidadesByEstado(pesquisa);
        }

        public List<Tipos_Movimentacao> pesquisaSubTipoMov(string valor)
        {
            string busca = valor;

            return dbRepository.pesquisaSubTiposMovimentoByDesc(busca);
        }

        public List<Vendas_Produtos> pesquisaProdutosPedido(int VendaId)
        {
            int id_Venda = VendaId;

            return dbRepository.pesquisaProdutosVendasByPedido(id_Venda);
        }

        public void salvarCidade(Cidades cidade)
        {
            dbRepository.salvarNovaCidade(cidade);
        }

        public void excluirCidade(Cidades cidade)
        {
            dbRepository.removerCidade(cidade);
        }

        public Model.Fornecedores pesquisaFornecedorById(int Id)
        {
            int valor = Id;

            return dbRepository.pesquisaFornecedoresID(valor);
        }

        public List<Movimentos> pesquisaMovPeriodo(DateTime txtDtInicio, DateTime txtDtFim)
        {
            DateTime dtInicio = txtDtInicio;
            DateTime dtFim = txtDtFim;

            return dbRepository.pesquisaMovimentoIntervalo(dtInicio, dtFim);
        }

        public Model.Fornecedores pesquisaFornecedorCpnj(string pesquisa)
        {
            string cnpj = pesquisa;

            return dbRepository.pesquisaFornecedoresByCnpj(cnpj);
        }

        public List<Model.Fornecedores> pesquisaFornecedoresCompleta(string pesquisa)
        {
            string busca = pesquisa;

            return dbRepository.pesquisaFornecedoresByNomeCnpjOfStatus(busca);
        }

        public Model.Clientes pesquisaClienteCpf(string pesquisa)
        {
            string cpf = pesquisa;

            return dbRepository.pesquisaClienteByCpf(cpf);
        }

        public void salvarCliente(Model.Clientes cliente)
        {
            dbRepository.salvarNovoCliente(cliente);
        }

        public Model.Clientes pesquisaClienteById(int selectedIndex)
        {
            int id = selectedIndex;

            return dbRepository.pesquisaClienteId(id);
        }

        public int pesquisaCompletaIDTipoMov(string descricao, string subTipo, string formaPg)
        {
            string desc = descricao;
            string tipo = subTipo;
            string form = formaPg;

            return dbRepository.pesquisaMovimentoID(desc, tipo, form);
        }

        public List<Model.Clientes> pesquisaClientesCompleta(string busca)
        {
            string pesquisa = busca;

            return dbRepository.pesquisaClienteByCpfOrNome(pesquisa);
        }

        public List<Model.Produtos> pesquisaProdutosValidoNome(string busca)
        {
            string pesquisa = busca;

            return dbRepository.pesquisaProdutosValidoByName(pesquisa);
        }

        public void salvarVenda(Vendas venda)
        {
            dbRepository.salvarNovaVenda(venda);
        }

        public void salvarPagamento(Pagamentos pagamento)
        {
            dbRepository.salvarNovoPagamento(pagamento);
        }

        public void salvaProdutosVendidos(Vendas_Produtos prodVendido)
        {
            dbRepository.salvarNovoProdutoVendido(prodVendido);
        }

        public bool pesquisaPagamentoId(Pagamentos pagamento)
        {
            return dbRepository.pesquisaIdPagamento(pagamento);
        }

        public Vendas pesquisaVendaID(int id)
        {
            int valor = id;

            return dbRepository.pesquisaVendabyID(valor);
        }

        public void salvarEstoque(Estoque estoque)
        {
            dbRepository.salvarNovoEstoque(estoque);
        }

        public Estoque pesquisaProdEstoqueId(int id)
        {
            int valor = id;

            return dbRepository.pesquisaEstoqueByProdID(valor);
        }

        public void salvarMovimento(Movimentos movimento)
        {
            dbRepository.salvarNovoMovimento(movimento);
        }

        public Tipos_Movimentacao pesquisaTiposMovimentoId(int? id_tipo)
        {
            return dbRepository.pesquisaTipoMovById(id_tipo);
        }

        public List<Vendas> pesquisaVendasGeral()
        {
            return dbRepository.pesquisaVendas();
        }

        public List<Pagamentos> pesquisaPagamentosGeral()
        {
            return dbRepository.pesquisaPagamentosTotais();
        }

        public List<Movimentos> pesquisaMovimentosGeral()
        {
            return dbRepository.pesquisaMovimentosTotais();
        }

        public Model.Fornecedores pesquisaFornecedorByNome(string pesquisa)
        {
            string nome = pesquisa;

            return dbRepository.pesquisaFornecedorValidoByNome(nome);
        }

        public UnidMedidas pesquisaMedidaByDesc(string desc)
        {
            string nome = desc;

            return dbRepository.pesquisaMedidaNome(nome);
        }

        public UnidMedidas pesquisaMedidaById(int id_medida)
        {
            int id = id_medida;

            return dbRepository.pesquisaMedidaId(id);
        }

        public void salvaPagamentoPedido(Pagamentos_Vendas pagamentoPedido)
        {
            dbRepository.salvarNovoPagamentoPedido(pagamentoPedido);
        }

        public bool pesquisaPagamentoIdVenda(string numPedido)
        {
            int idVenda = Convert.ToInt32(numPedido);

            return dbRepository.pesquisaPagamentoVendaByIdVenda(idVenda);
        }

        //SCRIPT FECHAMENTO
        public void salvarFechamento(Fechamento pedido)
        {
            dbRepository.salvarNovoPedido(pedido);
        }

        public List<Movimentos> pesquisaMovimentoTipo(int id_tipo)
        {
            int id = id_tipo;

            return dbRepository.pesquisaMovimentoByTipoId(id);
        }

        public List<Pagamentos> pesquisaUltimoPagamento()
        {
            return dbRepository.pesquisaPagamentosUltimo();
        }

        public Movimentos pesquisaMovimentoId(int? id_movimento)
        {
            int id = Convert.ToInt32(id_movimento);

            return dbRepository.pesquisaMovimentoByID(id);
        }

        public void removeMovimento(Movimentos movimento)
        {
            dbRepository.excluirMovimento(movimento);
        }

        public List<Movimentos> pesquisaMovimentoReferIdPagamento(int id)
        {
            int idPagamento = id;

            return dbRepository.pesquisaMovimentosReferentePagamento(idPagamento);
        }

        public List<Pagamentos_Vendas> pesquisaPagVendaIdPagamento(int id)
        {
            int idPagamento = id;

            return dbRepository.pesquisaPagamentoVendaByIdPagamento(idPagamento);
        }

        public void removePagamentoVenda(Pagamentos_Vendas pagVend)
        {
            dbRepository.excluirPagamento_Venda(pagVend);
        }

        public void removePagamento(Pagamentos pag)
        {
            dbRepository.excluirPagamento(pag);
        }

        //SCRIPT FECHAMENTO
        public bool pesquisaFechamentoIdVenda(string numPedido)
        {
            int idVenda = Convert.ToInt32(numPedido);

            return dbRepository.pesquisaFechamentoByIdVenda(idVenda);
        }
    }
}
