using DAL.ModeloDeDados;
using Orientacao.Application.ApplicationImplementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace View
{
    /// <summary>
    /// Lógica interna para Vendas.xaml
    /// </summary>
    public partial class Vendas : Window
    {
        private string operacao;
        private readonly VendaApplication vendaApplication = new VendaApplication();
        private readonly CarrinhoApplication carrinhoApplication = new CarrinhoApplication();
        private readonly ProdutoApplication produtoApplication = new ProdutoApplication();
        private readonly FuncionarioApplication funcionarioApp = new FuncionarioApplication();
        private Venda venda;
        private Carrinho carrinho;
        int i = 0;
        public Vendas()
        {
            InitializeComponent();
            DesativaCampos();
        }

        #region btn Adicionar qtde
        private void btnAdicionar_Click(object sender, RoutedEventArgs e)
        {
            int adicionar = 0;
            if(txtQuantidade.Text != "")
            {
                adicionar = Convert.ToInt32(txtQuantidade.Text);
                adicionar += 1;
                txtQuantidade.Text = Convert.ToString(adicionar);
            }

        }
        #endregion

        #region btn Reduzir qtde
        private void btnReduzir_Click(object sender, RoutedEventArgs e)
        {
            int redu = 0;
            if (txtQuantidade.Text != "" && Convert.ToInt32(txtQuantidade.Text) > 0)
            {
                
                redu = Convert.ToInt32(txtQuantidade.Text);
                redu -= 1;
                txtQuantidade.Text = Convert.ToString(redu);
            }
        }
        #endregion

        #region Ativa Campos Venda
        public void AtivaCamposV()
        {
            dpData.IsEnabled = true;
            boxFuncPessoa.IsEnabled = true;
        }
        #endregion

        #region Ativa os Campos Carrinho
        private void AtivaCamposC()
        {
            boxProduto.IsEnabled = true;
            txtQuantidade.IsEnabled = true;
            btnAdicionar.IsEnabled = true;
            btnReduzir.IsEnabled = true;
        }
        #endregion

        #region Desativa Campos
        private void DesativaCampos()
        {
            dpData.IsEnabled = false;
            boxFuncPessoa.IsEnabled = false;
            boxProduto.IsEnabled = false;
            txtValorUnit.IsEnabled = false;
            txtValorTot.IsEnabled = false;
            txtQuantidade.IsEnabled = false;
            btnAdicionar.IsEnabled = false;
            btnReduzir.IsEnabled = false;
        }
        #endregion

        #region Desativa Campos Carrinho
        private void DesativaCamposCar()
        {
            boxProduto.IsEnabled = true;
            txtQuantidade.IsEnabled = true;
            btnAdicionar.IsEnabled = true;
            btnReduzir.IsEnabled = true;
        }
        #endregion

        #region Desativa Campos Venda
        private void DesativaCamposV()
        {
            dpData.IsEnabled = false;
            boxFuncPessoa.IsEnabled = false;
        }
        #endregion

        #region Aletera os Botões
        private void AlterarBotoes(int op)
        {
            btnEditar.IsEnabled = false;
            btnInserir.IsEnabled = false;
            btnExcluir.IsEnabled = false;
            btnCancelar.IsEnabled = false;
            btnFinalizar.IsEnabled = false;
            btnSalvar.IsEnabled = false;
            if (op == 1)
            {
                //ativar opções iniciais
                btnInserir.IsEnabled = true;
                btnFinalizar.IsEnabled = true;
            }
            if (op == 2)
            {
                //inserir um valor
                btnSalvar.IsEnabled = true;
                btnCancelar.IsEnabled = true;
            }
            if (op == 3)
            {
                btnEditar.IsEnabled = true;
                btnExcluir.IsEnabled = true;
            }
        }
        #endregion

        #region Limpa Campos
        private void LimpaCampos()
        {
            dpData.SelectedDate = null;
            boxFuncPessoa.SelectedIndex = -1; 
            boxProduto.SelectedIndex = -1;
            txtValorUnit.Clear();
            txtValorTot.Clear();
            txtQuantidade.Clear();
        }
        #endregion

        #region Inserir
        private void btnInserir_Click(object sender, RoutedEventArgs e)
        {
            if (i <= 0)
            {
                AtivaCamposV();
                i++;
            }
            else
            {
                AtivaCamposC();
            }
            this.operacao = "inserir";
            AlterarBotoes(2);
        }
        #endregion

        #region Salvar Venda
        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            carrinho = new Carrinho();
            if(this.operacao == "inserir" && i <= 0)
            {
                venda = new Venda();
                venda.FK_idFuncionario = (int)boxFuncPessoa.SelectedValue;
                venda.dtaVenda = Convert.ToDateTime(dpData.Text);
                vendaApplication.Salvar(venda);
                dgListaV.ItemsSource = vendaApplication.BuscarTodos();
                AlterarBotoes(1);
            }
            else if (this.operacao == "inserir" && i > 0)
            {
                Produto produto = new Produto();
                carrinho.idProduto = (int)boxProduto.SelectedValue;
                produto.idProduto = carrinho.idProduto;
                produto = produtoApplication.BuscarProduto(x => x.idProduto == produto.idProduto);
                carrinho.valorParcial = produto.valorProduto;
                txtValorUnit.Text = Convert.ToString(carrinho.valorParcial);
                venda.valorTotal += carrinho.valorParcial * carrinho.qtdeItensVenda;
                txtValorTot.Text = Convert.ToString(venda.valorTotal);
                carrinhoApplication.SalvarCarrinho(carrinho);
                dgListaV.ItemsSource = carrinhoApplication.BuscarTodos();
            }

            if(this.operacao == "alterar" && i <= 0)
            {
                if(dgListaV.SelectedCells.ToList() != null)
                {
                    Venda v = (Venda)dgListaV.SelectedItem;
                    if(v.idVenda != 0)
                    {
                        vendaApplication.BuscarVenda(x => x.idVenda == v.idVenda);
                        venda.valorTotal = Convert.ToInt32(txtValorTot.Text);
                        venda.FK_idFuncionario = (int)boxFuncPessoa.SelectedValue;
                        venda.dtaVenda = Convert.ToDateTime(dpData.Text);
                        vendaApplication.Salvar(venda);
                        dgListaV.ItemsSource = vendaApplication.BuscarTodos();
                        i--;
                    }
                }
            }

            else if (this.operacao == "alterar" && i > 0)
            {
                if(dgListaV.SelectedCells.ToList() != null)
                {
                    Carrinho c = (Carrinho)dgListaV.SelectedItem;
                    if(c.idCarrinho != 0)
                    {
                        Produto produto = new Produto();
                        carrinhoApplication.BuscarCarrinho(x => x.idCarrinho == c.idCarrinho);
                        carrinho.idProduto = (int)boxProduto.SelectedValue;
                        produto.idProduto = carrinho.idProduto;
                        produto = produtoApplication.BuscarProduto(x => x.idProduto == produto.idProduto);
                        carrinho.valorParcial = produto.valorProduto;
                        carrinho.qtdeItensVenda = Convert.ToInt32(txtQuantidade.Text);
                        txtValorUnit.Text = Convert.ToString(carrinho.valorParcial);
                        venda.valorTotal += carrinho.valorParcial * carrinho.qtdeItensVenda;
                        txtValorTot.Text = Convert.ToString(venda.valorTotal);
                        carrinhoApplication.SalvarCarrinho(carrinho);
                        carrinhoApplication.BuscarTodos();
                    }
                }
            }

        }

        #endregion

        #region Editar
        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (i <= 0)
            {
                AtivaCamposC();
                i++;
            }
            else
            {
                AtivaCamposC();
            }
            this.operacao = "alterar";
            AlterarBotoes(2);
        }
        #endregion

        #region Cancelar
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            AlterarBotoes(1);
            LimpaCampos();
        }

        #endregion

        #region Excluir
        private void btnExcluir_Click(object sender, RoutedEventArgs e)
        {
            if(i <= 0)
            {
                Venda v = (Venda)dgListaV.SelectedItem;
                vendaApplication.ExcluirVenda(venda);
                dgListaV.ItemsSource = vendaApplication.BuscarTodos();

                AlterarBotoes(1);
            }
            else if (i > 0)
            {
                Carrinho c = (Carrinho)dgListaV.SelectedItem;
                carrinhoApplication.ExcluirCarrinho(carrinho);
            }
        }
        #endregion


        #region Funcinario Grid Box

        private void boxFuncPessoa_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Funcionario func = new Funcionario();
            Pessoa pessoa = new Pessoa();
            pessoa.idPessoa = (int)boxFuncPessoa.SelectedValue;
            func = funcionarioApp.BuscarFuncionario(x => x.FK_idPessoa == pessoa.idPessoa);
            dgListaV.ItemsSource = vendaApplication.BuscarPor(x => x.FK_idFuncionario == func.idFuncionario);
        }
        #endregion

        #region Carregar boxFuncionario
        private void boxFuncPessoa_Loaded(object sender, RoutedEventArgs e)
        {
            boxFuncPessoa.ItemsSource = funcionarioApp.BuscarTodos();
        }
        #endregion
    }
}
