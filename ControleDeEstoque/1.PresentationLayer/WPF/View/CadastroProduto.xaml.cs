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
        private Produto produto;
        public CadastroProduto()
        {
            InitializeComponent();
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            produto = new Produto();
            produto.nomeProduto = txtNome.Text;
            produto.qtdeProduto = Convert.ToInt32(txtEstoque);
            produto.valorProduto = Convert.ToDecimal(txtValor);
            produto.FK_idCategoria = Convert.ToInt32(boxCategoria.Text);
            produto.descricaoProduto = txtDescricao.Text;
            application.SalvarProduto(produto);
            dgListaProd.ItemsSource = application.BuscarTodos();
        }
    }
}
