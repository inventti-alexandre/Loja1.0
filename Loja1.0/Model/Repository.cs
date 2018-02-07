using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loja1._0.Model
{
    class Repository : DbContext
    {
        #region Repositório de itens de uso Genérico
        
        //Alternar entre BD Produção e BD Teste Local

        //Debug com BD Local
        DbEntitiesLocal dataEntity = new DbEntitiesLocal();

        //Produção com BD no server
        //DbEntities dataEntity = new DbEntities();

        public void SalvaAlteracao()
        {
            dataEntity.SaveChanges();
        }
        #endregion

        #region Repositório do BD de Usuários
        public void SalvarNovoUsuario(Usuarios usuario)
        {
            dataEntity.Usuarios.Add(usuario);
        }

        public void ExcluirUsuarioExistente(Usuarios usuario)
        {
            dataEntity.Usuarios.Remove(usuario);
        }

        public Usuarios PesquisaSimplesUser(string valor)
        {
            return (from usuarios in dataEntity.Usuarios
                    where (usuarios.login.Equals(valor))
                    select usuarios).SingleOrDefault();
        }

        public Usuarios PesquisaUserById(int Id)
        {
            return (from usuarios in dataEntity.Usuarios
                    where (usuarios.id == Id)
                    select usuarios).SingleOrDefault();
        }

        public List<Usuarios> PesquisaCompletaUsers()
        {
            return (from usuarios in dataEntity.Usuarios
                    select usuarios).ToList();
        }

        public Usuarios PesquisaNomeUser(string nomeCompleto)
        {
            return (from usuarios in dataEntity.Usuarios
                    where (usuarios.nome.Equals(nomeCompleto))
                    select usuarios).SingleOrDefault();

        }

        public List<Usuarios> PesquisaUsuariosInvalidos()
        {
            return (from usuarios in dataEntity.Usuarios
                    where (usuarios.status == 0)
                    select usuarios).ToList();
        }

        public List<Usuarios> PesquisaUsersIdPerfil(int idMinus)
        {
            return (from usuarios in dataEntity.Usuarios
                    where usuarios.id_Perfil >= idMinus
                    && usuarios.status == 1
                    select usuarios).ToList();
        }

        public int ContaUserValidos()
        {
            List<Usuarios> lista = (from usuarios in dataEntity.Usuarios
                                    where (usuarios.status == 1)
                                    select usuarios).ToList();
            return lista.Count;
        }
        
        #endregion

        #region Repositório do BD de Clientes

        public Clientes PesquisaClienteId(int id)
        {
            return (from cliente in dataEntity.Clientes
                    where (cliente.id == (id))
                    select cliente).SingleOrDefault();
        }

        public void SalvarNovoCliente(Clientes cliente)
        {
            dataEntity.Clientes.Add(cliente);
        }

        public List<Clientes> PesquisaClienteByCpfOrNome(string pesquisa)
        {
            return (from cliente in dataEntity.Clientes
                    where (cliente.cpf.Equals(pesquisa)
                    || (cliente.nome.Contains(pesquisa)
                    && cliente.status == 1))
                    select cliente).ToList();
        }

        public Clientes PesquisaClienteByCpf(string cpf)
        {
            return (from cliente in dataEntity.Clientes
                    where (cliente.cpf.Equals(cpf))
                    select cliente).SingleOrDefault();
        }
        #endregion

        #region Repositório do BD de Produtos

        public List<Produtos> PesquisaProdutosValidoByName(string pesquisa)
        {
            return (from produto in dataEntity.Produtos
                    where (produto.desc_produto.Contains(pesquisa)
                    && produto.status == 1)
                    orderby produto.desc_produto
                    select produto).ToList();
        }

        public List<Produtos> PesquisaProdutos()
        {
            return (from produto in dataEntity.Produtos
                    where produto.status == 1
                    select produto).ToList();
        }

        public List<Produtos> PesquisaProdutoByNomeId(string busca)
        {
            return (from produto in dataEntity.Produtos
                    where (produto.desc_produto.Contains(busca)
                    || produto.cod_produto == busca)
                    select produto).ToList();
        }

        public Produtos PesquisaProdutoByNome(string pesquisa)
        {
            return (from produtos in dataEntity.Produtos
                    where (produtos.desc_produto.Equals(pesquisa))
                    select produtos).SingleOrDefault();
        }

        public void SalvarNovoProduto(Produtos produto)
        {
            dataEntity.Produtos.Add(produto);
        }

        public Produtos PesquisaProdutoByCodigo(string codigo)
        {
            return (from produtos in dataEntity.Produtos
                    where (produtos.cod_produto.Equals(codigo))
                    select produtos).SingleOrDefault();
        }

        public Produtos PesquisaProdutoById(int pesquisa)
        {
            return (from produto in dataEntity.Produtos
                    where (produto.id == pesquisa)
                    select produto).SingleOrDefault();
        }
        #endregion

        #region Repositório do BD de Estoques

        public List<Estoque> PesquisaEstoqueOrdened()
        {
            return (from estoque in dataEntity.Estoque
                    where estoque.Produtos.status == 1
                    orderby estoque.qnt_atual
                    select estoque).ToList();
        }

        public Estoque PesquisaEstoqueByProdID(int valor)
        {
            return (from estoque in dataEntity.Estoque
                    where (estoque.id_produto == valor)
                    select estoque).SingleOrDefault();
        }

        public void SalvarNovoEstoque(Estoque estoque)
        {
            dataEntity.Estoque.Add(estoque);
        }
        #endregion

        #region Repositório do BD de Contabilidade

        public void SalvarNovaContabilidade(Contabilidade contabilidade)
        {
            dataEntity.Contabilidade.Add(contabilidade);
        }

        public Contabilidade PesquisaContabilidadeId(int v)
        {
            return (from contabil in dataEntity.Contabilidade
                    where contabil.id == v
                    select contabil).Single();
        }

        public List<Contabilidade> PesquisaGeralContabilidade()
        {
            return (from contabil in dataEntity.Contabilidade
                    select contabil).ToList();
        }
        #endregion

        #region Repositório do BD de UnidMedidas
        public List<UnidMedidas> PesquisaMedidas()
        {
            return (from unid in dataEntity.UnidMedidas
                    select unid).ToList();
        }

        public UnidMedidas PesquisaMedidaId(int id)
        {
            return (from unid in dataEntity.UnidMedidas
                    where unid.id == id
                    select unid).SingleOrDefault();
        }

        public UnidMedidas PesquisaMedidaNome(string nome)
        {
            return (from unid in dataEntity.UnidMedidas
                    where unid.medida.Equals(nome)
                    select unid).SingleOrDefault();
        }
        #endregion

        #region Repositório do BD de Cidades
        public List<Cidades> PesquisaCidadesByEstado(int pesquisa)
        {
            return (from cidade in dataEntity.Cidades
                    where (cidade.id_Estado == (pesquisa)
                    || cidade.id_Estado == 0)
                    orderby cidade.cidade
                    select cidade).ToList();
        }

        public Cidades PesquisaCidadeByName(string pesquisa)
        {
            return (from cidade in dataEntity.Cidades
                    where (cidade.cidade.Equals(pesquisa))
                    select cidade).SingleOrDefault();
        }

        internal Cidades PesquisaCidadeId(int pesquisa)
        {
            return (from cidade in dataEntity.Cidades
                    where cidade.id == pesquisa
                    select cidade).SingleOrDefault();
        }

        public void SalvarNovaCidade(Cidades cidade)
        {
            dataEntity.Cidades.Add(cidade);
        }

        public void RemoverCidade(Cidades cidade)
        {
            dataEntity.Cidades.Remove(cidade);
        }

        public List<Cidades> PesquisaCidadeByNameUF(string pesquisa)
        {
            return (from cidade in dataEntity.Cidades
                    where cidade.Estados.estado.Equals(pesquisa)                                       
                    select cidade).ToList();
        }
        #endregion

        #region Repositório do BD Vendas_Produtos

        public List<Vendas_Produtos> PesquisaProdutosVendaById(int busca)
        {
            return (from prodVenda in dataEntity.Vendas_Produtos
                    where prodVenda.id_venda == busca
                    select prodVenda).ToList();
        }

        public List<Vendas_Produtos> PesquisaProdutosVendasByPedido(int id_Venda)
        {
            return (from prodVenda in dataEntity.Vendas_Produtos
                    where (prodVenda.id_venda == id_Venda)
                    select prodVenda).ToList();
        }

        public void SalvarNovoProdutoVendido(Vendas_Produtos prodVendido)
        {
            dataEntity.Vendas_Produtos.Add(prodVendido);
        }

        #endregion

        #region Repositório do BD Tipos_Movimentacao
        public Tipos_Movimentacao PesquisaTipoMovById(int? id_tipo)
        {
            return (from tipos in dataEntity.Tipos_Movimentacao
                    where tipos.id == (id_tipo)
                    select tipos).SingleOrDefault();
        }

        public List<Tipos_Movimentacao> PesquisaSubTiposMovimentoByDesc(string busca)
        {
            return (from tipos in dataEntity.Tipos_Movimentacao
                    where tipos.sub_tipo.Equals(busca)
                    orderby tipos.forma_pag
                    select tipos).ToList();
        }

        public List<Tipos_Movimentacao> PesquisaTiposMov()
        {
            List<Tipos_Movimentacao> lista = (from tipoMov in dataEntity.Tipos_Movimentacao
                                              where tipoMov.mostrar == true
                                              orderby tipoMov.descricao
                                              select tipoMov).ToList();

            List<Tipos_Movimentacao> listaAux = new List<Tipos_Movimentacao>();

            for (int i = 0; i < lista.Count; i++)
            {
                if (i == 0)
                {
                    listaAux.Add(lista[i]);
                }
                else if (!lista[i].descricao.Equals(lista[i - 1].descricao))
                {
                    listaAux.Add(lista[i]);
                }

            }

            return listaAux;
        }

        public List<Tipos_Movimentacao> PesquisaTiposMovimentoByDesc(string busca)
        {

            List<Tipos_Movimentacao> lista = (from tipos in dataEntity.Tipos_Movimentacao
                                              where tipos.descricao.Equals(busca)
                                              && tipos.mostrar == true
                                              orderby tipos.sub_tipo
                                              select tipos).ToList();

            List<Tipos_Movimentacao> listaAux = new List<Tipos_Movimentacao>();

            for (int i = 0; i < lista.Count; i++)
            {
                if (i == 0)
                {
                    listaAux.Add(lista[i]);
                }
                else if (!lista[i].sub_tipo.Equals(lista[i - 1].sub_tipo))
                {
                    listaAux.Add(lista[i]);
                }

            }

            return listaAux;
        }
        #endregion

        #region Repositório do BD Gerenciamento
        public void SalvarNovoGerenciamento(Gerenciamento gerencia)
        {
            dataEntity.Gerenciamento.Add(gerencia);
        }

        public Gerenciamento PesquisaGerenciamento(int pesquisa)
        {
            return (from gerencia in dataEntity.Gerenciamento
                    where gerencia.id == pesquisa
                    select gerencia).SingleOrDefault();
        }
        #endregion

        #region Repositório do BD Estados
        public List<Estados> PesquisaEstados()
        {
            return (from estados in dataEntity.Estados
                    orderby estados.id            
                    select estados).ToList();
        }
        #endregion

        #region Repositório do BD de Movimentos

        public void SalvarNovoMovimento(Movimentos movimento)
        {
            dataEntity.Movimentos.Add(movimento);
        }

        public List<Movimentos> PesquisaMovimentoByTipoId(int id)
        {
            return (from movimento in dataEntity.Movimentos
                    where movimento.id_tipo == id
                    orderby movimento.id descending
                    select movimento).ToList();
        }

        public List<Movimentos> PesquisaMovimentosTotais()
        {
            return (from movimento in dataEntity.Movimentos
                    orderby movimento.id_tipo
                    select movimento).ToList();
        }

        public Movimentos PesquisaMovimentoByID(int id)
        {
            return (from movimento in dataEntity.Movimentos
                    where movimento.id == id
                    select movimento).Single();
        }

        public void ExcluirMovimento(Movimentos movimento)
        {
            dataEntity.Movimentos.Remove(movimento);
        }

        public List<Movimentos> PesquisaMovimentosReferentePagamento(int idPagamento)
        {
            return (from movimento in dataEntity.Movimentos
                    where movimento.desc.Contains(idPagamento.ToString())
                    select movimento).ToList();
        }        

        public List<Movimentos> PesquisaMovimentoIntervalo(DateTime dtInicio, DateTime dtFim)
        {
            return (from movimento in dataEntity.Movimentos
                    where movimento.data >= dtInicio
                    && movimento.data <= dtFim
                    orderby movimento.id_tipo
                    select movimento).ToList();
        }

        public decimal PesquisaMovimentoByDiaTipo(DateTime data, int mov)
        {
            List<Movimentos> listMov = new List<Movimentos>();

            listMov = (from movimento in dataEntity.Movimentos
                       where movimento.data.Year.Equals(data.Year)
                       && movimento.data.Month.Equals(data.Month)
                       && movimento.data.Day.Equals(data.Day)
                       && movimento.id_tipo == mov
                       select movimento).ToList();

            decimal valorMov = 0.00M;

            foreach(Movimentos value in listMov)
            {
                valorMov = valorMov + value.valor;
            }

            return valorMov;
        }

        public Compras PesquisaCompraByIdProduto(int idProduto)
        {
            return (from compra in dataEntity.Compras
                    where compra.status == 1
                    && compra.id_produto == idProduto
                    orderby compra.dt_compra descending
                    select compra).First();
        }

        public Compras PesquisaCompraPendente(int id)
        {
            return (from compra in dataEntity.Compras
                    where compra.qnt_compra > 0
                    && compra.id_produto == id
                    orderby compra.dt_compra ascending
                    select compra).First();
        }        

        public void RemoveProdVenda(Vendas_Produtos prodVend)
        {
            dataEntity.Vendas_Produtos.Remove(prodVend);
        }

        public int PesquisaMovimentoID(string desc, string tipo, string form)
        {
            if (form != null && !form.Equals(""))
            {
                return (from movimento in dataEntity.Tipos_Movimentacao
                        where (movimento.descricao.Equals(desc)
                        && movimento.sub_tipo.Equals(tipo)
                        && movimento.forma_pag.Equals(form))
                        select movimento.id).SingleOrDefault();
            }
            else
            {
                return (from movimento in dataEntity.Tipos_Movimentacao
                        where (movimento.descricao.Equals(desc)
                        && movimento.sub_tipo.Equals(tipo))
                        select movimento.id).SingleOrDefault();
            }

        }

        #endregion

        #region Repositório do BD de Vendas

        public double? PesquisaValorVendasDia(DateTime today)
        {
            return (from venda in dataEntity.Vendas
                    where (venda.data_Venda.Year.Equals(today.Year))
                    && (venda.data_Venda.Month.Equals(today.Month))
                    && (venda.data_Venda.Day.Equals(today.Day))
                    select venda.valor_Venda).Sum();
        }

        public int PesquisaVendasDia(DateTime today)
        {
            return (from venda in dataEntity.Vendas
                    where (venda.data_Venda.Year.Equals(today.Year))
                    && (venda.data_Venda.Month.Equals(today.Month))
                    && (venda.data_Venda.Day.Equals(today.Day))
                    select venda).Count();
        }

        public List<Vendas> PesquisaVendas()
        {
            return (from venda in dataEntity.Vendas
                    select venda).ToList();
        }

        public Vendas PesquisaVendabyID(int valor)
        {
            return (from venda in dataEntity.Vendas
                    where (venda.id == valor)
                    select venda).SingleOrDefault();
        }

        public void SalvarNovaVenda(Vendas venda)
        {
            dataEntity.Vendas.Add(venda);
        }

        #endregion

        #region Repositório do BD de Pagamentos

        public void SalvarNovoPagamento(Pagamentos pagamento)
        {
            dataEntity.Pagamentos.Add(pagamento);
        }

        public bool PesquisaIdPagamento(Pagamentos pagamento)
        {
            List<Pagamentos> pag = (from pagamentos in dataEntity.Pagamentos
                                    where (pagamentos.id == (pagamento.id))
                                    select pagamentos).ToList();
            if (pag.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public List<Pagamentos> PesquisaPagamentosTotais()
        {
            return (from pagamento in dataEntity.Pagamentos
                    where pagamento.tipoPag != null
                    orderby pagamento.dataPagamento
                    select pagamento).ToList();
        }

        public void ExcluirPagamento(Pagamentos pag)
        {
            dataEntity.Pagamentos.Remove(pag);
        }

        public List<Pagamentos> PesquisaPagamentosUltimo()
        {
            Pagamentos ultimoPag = (from pagamento in dataEntity.Pagamentos
                                    orderby pagamento.id descending
                                    select pagamento).First();

            int qntParcelas = Convert.ToInt32(ultimoPag.qntParcelas);

            List<Pagamentos> listaRetorno = new List<Pagamentos>();

            if (ultimoPag.tipoPag.Contains("Entrada +"))
            {
                qntParcelas++;
            }

            for (int i = 0; i <= qntParcelas; i++)
            {
                Pagamentos pagamento = (from pag in dataEntity.Pagamentos
                                        where pag.id == (ultimoPag.id - i)
                                        select pag).Single();
                listaRetorno.Add(pagamento);
            }

            return listaRetorno;
        }
        #endregion

        #region Repositório do BD de Fornecedores
        public Fornecedores PesquisaFornecedorValidoByNome(string nome)
        {
            return (from fornecedor in dataEntity.Fornecedores
                    where (fornecedor.nome.Equals(nome))
                    select fornecedor).SingleOrDefault();
        }

        public Fornecedores PesquisaFornecedoresByCnpj(string cnpj)
        {
            return (from fornecedor in dataEntity.Fornecedores
                    where (fornecedor.cnpj.Equals(cnpj))
                    select fornecedor).SingleOrDefault();
        }

        public List<Fornecedores> PesquisaFornecedoresByNomeCnpjOfStatus(string pesquisa)
        {
            return (from fornecedor in dataEntity.Fornecedores
                    where (fornecedor.cnpj.Equals(pesquisa)
                    || (fornecedor.nome.Contains(pesquisa)
                    && fornecedor.status == 1))
                    select fornecedor).ToList();
        }

        public Fornecedores PesquisaFornecedoresID(int pesquisa)
        {
            return (from fornecedor in dataEntity.Fornecedores
                    where (fornecedor.id == (pesquisa))
                    select fornecedor).SingleOrDefault();
        }

        public List<Fornecedores> PesquisaFornecedoresByNomeCnpj(string pesquisa)
        {
            return (from fornecedor in dataEntity.Fornecedores
                    where (fornecedor.cnpj.Equals(pesquisa)
                    || fornecedor.nome.Contains(pesquisa))
                    && fornecedor.status == 1
                    select fornecedor).ToList();
        }

        public void SalvarNovoFornecedor(Fornecedores fornecedor)
        {
            dataEntity.Fornecedores.Add(fornecedor);
        }
        #endregion

        #region Repositório do BD Pagamentos_Vendas
        public List<Pagamentos_Vendas> PesquisaPagamentoVendaByIdPagamento(int idPagamento)
        {
            return (from pagVend in dataEntity.Pagamentos_Vendas
                    where pagVend.id_Pagamento == idPagamento
                    select pagVend).ToList();
        }

        public void ExcluirPagamento_Venda(Pagamentos_Vendas pagVend)
        {
            dataEntity.Pagamentos_Vendas.Remove(pagVend);
        }

        public void SalvarNovoPagamentoPedido(Pagamentos_Vendas pagamentoPedido)
        {
            dataEntity.Pagamentos_Vendas.Add(pagamentoPedido);
        }

        public bool PesquisaPagamentoVendaByIdVenda(int idVenda)
        {
            bool busca = true;
            int teste = (from pagVenda in dataEntity.Pagamentos_Vendas
                         where pagVenda.id_Venda == idVenda
                         select pagVenda).Count();
            if (teste == 0)
            {
                busca = false;
            }
            return busca;
        }
        #endregion

        #region Repositório do BD Compras

        public void SalvarNovaCompra(Compras compra)
        {
            dataEntity.Compras.Add(compra);
        }
        #endregion

        public List<CtrlPonto> PesquisaPontoPeriodo(int idUser, int month, int year)
        {
            return (from ponto in dataEntity.CtrlPonto
                    where ponto.Dt_Ponto.Month == month
                    && ponto.Dt_Ponto.Year == year
                    && ponto.Id_User == idUser
                    orderby ponto.Dt_Ponto ascending
                    select ponto).ToList();
        }

        public CtrlPonto PesquisaPontoPeriodoDia(int id, int dia, int mes, int ano)
        {
            return (from ponto in dataEntity.CtrlPonto
                    where ponto.Dt_Ponto.Day == dia
                    && ponto.Dt_Ponto.Month == mes
                    && ponto.Dt_Ponto.Year == ano
                    && ponto.Id_User == id
                    orderby ponto.Dt_Ponto ascending
                    select ponto).SingleOrDefault();
        }

        public void SalvarNovoPonto(CtrlPonto ponto)
        {
            dataEntity.CtrlPonto.Add(ponto);
        }

        public void SalvarNovoLog(LogPonto log)
        {
            dataEntity.LogPonto.Add(log);
        }        

        //SCRIPT FECHAMENTO
        #region Repositório do BD Pedidos

        public void SalvarNovoPedido(Fechamento pedido)
        {
            dataEntity.Fechamento.Add(pedido);
        }                 

        public bool PesquisaFechamentoByIdVenda(int idVenda)
        {
            bool busca = true;
            int teste = (from fecha in dataEntity.Fechamento
                         where fecha.id_venda == idVenda
                         select fecha).Count();
            if (teste == 0)
            {
                busca = false;
            }
            return busca;
        }
        #endregion                                                          
    }
}
