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
    /// Lógica interna para Window1.xaml
    /// </summary>
    public partial class CadastroProduto : Window
    {
        private readonly ProdutoApplication application = new ProdutoApplication();
        private readonly CategoriaApplication categoriaApplication = new CategoriaApplication();
        private Produto produto;
        private string operacao;

        public CadastroProduto()
        {
                InitializeComponent();
                DesativaCampos();
        }

        #region Botão Salvar
        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            AtivaCampos();

            if (this.operacao == "inserir")
            {
                produto = new Produto();
                produto.nomeProduto = txtNome.Text;
                produto.qtdeProduto = Convert.ToInt32(txtEstoque.Text);
                produto.valorProduto = Convert.ToDecimal(txtValor.Text);
                produto.FK_idCategoria = (int)boxCategoria.SelectedValue;
                produto.descricaoProduto = txtDescricao.Text;
                application.SalvarProduto(produto);
                AlterarBotoes(1);
               
                dgListaProd.ItemsSource = application.BuscarTodos();
            }
            if (this.operacao == "alterar")
            {
                if (dgListaProd.SelectedCells.ToList() != null && txtNome.Text != "")
                {
                    Produto p = (Produto)dgListaProd.SelectedItem;
                    if (p.idProduto != 0)
                    {
                        produto = application.BuscarProduto(x => x.idProduto == p.idProduto);
                        produto.nomeProduto = txtNome.Text;
                        produto.qtdeProduto = Convert.ToInt32(txtEstoque.Text);
                        produto.valorProduto = Convert.ToDecimal(txtValor.Text);
                        produto.FK_idCategoria = (int)boxCategoria.SelectedValue;
                        produto.descricaoProduto = txtDescricao.Text;
                        application.SalvarProduto(produto);
                    }
                }
                dgListaProd.ItemsSource = application.BuscarTodos();
            }
            AlterarColumnGd();
        }
        #endregion

        #region Carregando a dgListaProd
        private void dgListaProd_Loaded(object sender, RoutedEventArgs e)
        {
            dgListaProd.ItemsSource = application.BuscarTodos();
            AlterarColumnGd();
            AlterarBotoes(1);
        }
        #endregion

        #region Botão inserir
        private void btnInserir_Click(object sender, RoutedEventArgs e)
        {
            AtivaCampos();
            this.operacao = "inserir";
            AlterarBotoes(2);
        }
        #endregion

        #region Aletera os Botões
        private void AlterarBotoes(int op)
        {
            btnEditar.IsEnabled = false;
            btnInserir.IsEnabled = false;
            btnExcluir.IsEnabled = false;
            btnCancelar.IsEnabled = false;
            btnBuscar.IsEnabled = false;
            btnSalvar.IsEnabled = false;
            if (op == 1)
            {
                //ativar opções iniciais
                btnInserir.IsEnabled = true;
                btnBuscar.IsEnabled = true;
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

        #region Ativa os Campos
        private void AtivaCampos()
        {
            txtNome.IsEnabled = true;
            txtEstoque.IsEnabled = true;
            txtValor.IsEnabled = true;
            txtDescricao.IsEnabled = true;
            boxCategoria.IsEnabled = true;
        }
        #endregion

        #region Limpa Campos
        private void LimpaCampos()
        {
            txtNome.Clear();
            txtEstoque.Clear();
            txtValor.Clear();
            txtDescricao.Clear();
            boxCategoria.SelectedIndex = -1;
        }
        #endregion

        #region Botão Editar
        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            AtivaCampos();
            this.operacao = "alterar";
            AlterarBotoes(2);
        }
        #endregion

        #region Botão Cancelar
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            AlterarBotoes(1);
            DesativaCampos();
            LimpaCampos();
        }
        #endregion

        #region Botâo Excluir
        private void btnExcluir_Click(object sender, RoutedEventArgs e)
        {
            Produto p = (Produto)dgListaProd.SelectedItem;
            produto = application.BuscarProduto(x => x.idProduto == p.idProduto);
            application.ExcluirProduto(produto);
            dgListaProd.ItemsSource = application.BuscarTodos();
            AlterarColumnGd();
            AlterarBotoes(1);
        }
        #endregion

        #region Carregar boxCategoria
        private void boxCategoria_Loaded(object sender, RoutedEventArgs e)
        {
           
            boxCategoria.ItemsSource = categoriaApplication.BuscarTodos();
            AlterarColumnGd();
        }
        #endregion

        #region Botão Buscar
        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            AtivaCampos();
            produto = new Produto();
            txtNome.IsEnabled = true;
            txtEstoque.IsEnabled = true;
            txtValor.IsEnabled = true;
            boxCategoria.IsEnabled = true;
            //buscar por nome
            if (txtNome.Text.Trim().Count() > 0)
            {
                produto.nomeProduto = txtNome.Text;
                dgListaProd.ItemsSource = application.BuscarPor(x => x.nomeProduto.Contains(produto.nomeProduto));

            }
            //buscar por qtde do Produto
            if (txtEstoque.Text.Trim().Count() > 0)
            {
                produto.qtdeProduto = Convert.ToInt32(txtEstoque.Text);
                dgListaProd.ItemsSource = application.BuscarPor(x => x.qtdeProduto.ToString().Contains(produto.qtdeProduto.ToString()));

            }
            //buscar por Valor
            if (txtValor.Text.Trim().Count() > 0)
            {
                produto.valorProduto = Convert.ToDecimal(txtValor.Text);
                dgListaProd.ItemsSource = application.BuscarPor(x => x.valorProduto.ToString().Contains(produto.valorProduto.ToString()));

            }

            //buscar por Categoria
            if (boxCategoria.Text.Trim().Count() > 0)
            {
                produto.FK_idCategoria = (int)boxCategoria.SelectedValue;
                dgListaProd.ItemsSource = application.BuscarPor(x => x.FK_idCategoria.ToString().Contains(produto.FK_idCategoria.ToString()));
            }
            
            //Buscar pro Descrição
            if (txtDescricao.Text.Trim().Count() > 0)
            {
                produto.descricaoProduto = txtDescricao.Text;
                dgListaProd.ItemsSource = application.BuscarPor(x => x.descricaoProduto.Contains(produto.descricaoProduto));

            }

            AlterarColumnGd();
            // dgListaProd.ItemsSource = produto;
        }
        #endregion

        #region Alterar Colunas Da Grid
        private void AlterarColumnGd()
        {
            dgListaProd.IsReadOnly = true;
            dgListaProd.Columns[0].Header = "id";
            dgListaProd.Columns[1].Header = "Produto";
            dgListaProd.Columns[2].Visibility = Visibility.Hidden;
            dgListaProd.Columns[3].Header = "Valor";
            dgListaProd.Columns[4].Header = "Estoque";
            dgListaProd.Columns[5].Header = "Cod. Categoria";
            dgListaProd.Columns[6].Visibility = Visibility.Hidden;
        }
        #endregion

        #region Double_Click na dgListaProd
        private void dgListaProd_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgListaProd.SelectedIndex >= 0)
            {
                AlterarColumnGd();
                Categoria categoria = new Categoria();
                //contato c = (contato)dgDados.Items[dgDados.SelectedIndex];
                Produto p = (Produto)dgListaProd.SelectedItem;
                txtNome.Text = p.nomeProduto;
                txtEstoque.Text = Convert.ToString(p.qtdeProduto);
                txtValor.Text = Convert.ToString(p.valorProduto);
                categoria = categoriaApplication.BuscarCategoria(x => x.idCategoria == p.FK_idCategoria);
                boxCategoria.Text = categoria.nomeCategoria;
                txtDescricao.Text = p.descricaoProduto;
                this.AlterarBotoes(3);
            }
            else
            {
                AlterarBotoes(1);
            }
        }
        #endregion

        #region Desativa Campos
        private void DesativaCampos()
        {
            txtDescricao.IsEnabled = false;
            txtNome.IsEnabled = false;
            txtValor.IsEnabled = false;
            boxCategoria.IsEnabled = false;
            txtEstoque.IsEnabled = false;
        }
        #endregion
    }
  }
