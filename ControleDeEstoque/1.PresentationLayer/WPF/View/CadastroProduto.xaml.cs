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
        public CadastroProduto()
        {
            InitializeComponent();
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            produto = new Produto();
            produto.nomeProduto = txtNome.Text;
            produto.qtdeProduto = Convert.ToInt32(txtEstoque.Text);
            produto.valorProduto = Convert.ToDecimal(txtValor.Text);
            produto.FK_idCategoria = (int)boxCategoria.SelectedValue;
            produto.descricaoProduto = txtDescricao.Text;
            application.SalvarProduto(produto);
            dgListaProd.ItemsSource = application.BuscarTodos();
            AlterarBotoes(1);
        }

        private void dgListaProd_Loaded(object sender, RoutedEventArgs e)
        {
            dgListaProd.ItemsSource = application.BuscarTodos();
            dgListaProd.Columns[0].IsReadOnly = true;
            dgListaProd.Columns[0].Header = "id";
            dgListaProd.Columns[1].Header = "Produto";
            dgListaProd.Columns[2].Visibility = Visibility.Hidden;
            AlterarBotoes(1);
        }


        private void btnInserir_Click(object sender, RoutedEventArgs e)
        {
            AlterarBotoes(2);
        }



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

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            produto = new Produto();
            if (dgListaProd.SelectedCells.ToList() != null)
            {
                produto.nomeProduto = txtNome.Text;
                produto.qtdeProduto = Convert.ToInt32(txtEstoque);
                produto.valorProduto = Convert.ToDecimal(txtValor);
                produto.FK_idCategoria = Convert.ToInt32(boxCategoria.Text);
                produto.descricaoProduto = txtDescricao.Text;
                application.SalvarProduto(produto);
                AlterarBotoes(1);
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            AlterarBotoes(2);
        }

        private void btnExcluir_Click(object sender, RoutedEventArgs e)
        {
            Produto p = (Produto)dgListaProd.SelectedItem;
            produto = application.BuscarProduto(x => x.idProduto == p.idProduto);
            application.ExcluirProduto(produto);
            dgListaProd.ItemsSource = application.BuscarTodos();
            AlterarBotoes(1);
        }


        private void boxCategoria_Loaded(object sender, RoutedEventArgs e)
        {
            Categoria categoria = new Categoria();
            boxCategoria.ItemsSource = categoriaApplication.BuscarTodos();

        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
         {
            produto = new Produto();

            produto.nomeProduto = txtNome.Text;
             List<Produto> l = new List<Produto>() { application.BuscarProduto(x => x.nomeProduto == produto.nomeProduto) };
           produto = application.BuscarProduto(x => x.nomeProduto == produto.nomeProduto);
            dgListaProd.ItemsSource = l;
           // dgListaProd.ItemsSource = produto;

        }
    }
  }
