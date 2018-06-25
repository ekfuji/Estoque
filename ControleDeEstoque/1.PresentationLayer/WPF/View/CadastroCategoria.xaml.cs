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
    /// Lógica interna para CadastroCategoria.xaml
    /// </summary>
    public partial class CadastroCategoria : Window
    {
        private readonly CategoriaApplication application = new CategoriaApplication();
        private Categoria categoria;
        private string operacao;
        public CadastroCategoria()
        {
            InitializeComponent();
            txtNome.IsEnabled = false;
        }

        #region Salvar
        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            AtivaCampos();
            if (this.operacao == "inserir")
            {
                categoria = new Categoria();
                categoria.nomeCategoria = txtNome.Text;
                application.SalvarCategoria(categoria);
                dgListaCateg.ItemsSource = application.BuscarTodos();
                AlterarBotoes(1);
                AlterarColumnGd();
            }
            if (this.operacao == "alterar")
            {
                if (dgListaCateg.SelectedCells.ToList() != null && txtNome.Text != "")
                {
                    Categoria c = (Categoria)dgListaCateg.SelectedItem;
                    if (c.idCategoria != 0)
                    {
                        categoria = application.BuscarCategoria(x => x.idCategoria == c.idCategoria);
                        categoria.nomeCategoria = txtNome.Text;
                        application.SalvarCategoria(categoria);
                    }
                }
                dgListaCateg.ItemsSource = application.BuscarTodos();
                AlterarColumnGd();
            }
        }
        #endregion

        #region Load dgListaCateg
        private void dgListaCateg_Loaded(object sender, RoutedEventArgs e)
        {
            categoria = new Categoria();
            dgListaCateg.ItemsSource = application.BuscarTodos();
            AlterarColumnGd();
            AlterarBotoes(1);
        }
        #endregion

        #region Editar
        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            AtivaCampos();
            this.operacao = "alterar";
            AlterarBotoes(2);
        }
        #endregion

        #region Aleterar Botões
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

        #region Inserir
        private void btnInserir_Click(object sender, RoutedEventArgs e)
        {
            AtivaCampos();
            this.operacao = "inserir";
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

        #region Double_Click dgListaCategorias
        private void dgListaCateg_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            AtivaCampos();
            if (dgListaCateg.SelectedIndex >= 0)
            {
                AlterarColumnGd();
                //contato c = (contato)dgDados.Items[dgDados.SelectedIndex];
                Categoria c = (Categoria)dgListaCateg.SelectedItem;
                txtNome.Text = c.nomeCategoria;

                this.AlterarBotoes(3);
            }
            else
            {
                AlterarBotoes(1);
            }
        }
        #endregion

        #region Excluir
        private void btnExcluir_Click(object sender, RoutedEventArgs e)
        {
            Categoria c = (Categoria)dgListaCateg.SelectedItem;
            categoria = application.BuscarCategoria(x => x.idCategoria == c.idCategoria);
            application.ExcluirCategoria(categoria);
            dgListaCateg.ItemsSource = application.BuscarTodos();
            AlterarColumnGd();
            AlterarBotoes(1);
        }
        #endregion

        #region Buscar
        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            AtivaCampos();
            categoria = new Categoria();
            categoria.nomeCategoria = txtNome.Text;
            dgListaCateg.ItemsSource = application.BuscarPor(x => x.nomeCategoria.Contains(categoria.nomeCategoria));
            AlterarColumnGd();
        }
        #endregion

        #region AlterarColums da Grid
        private void AlterarColumnGd()
        {
            dgListaCateg.IsReadOnly = true;
            dgListaCateg.Columns[0].Header = "id";
            dgListaCateg.Columns[1].Header = "Categoria";
            dgListaCateg.Columns[2].Visibility = Visibility.Hidden;
        }
        #endregion

        #region AtivaCampos
        private void AtivaCampos()
        {
            txtNome.IsEnabled = true;
        }
        #endregion

        #region Limpar campos
        private void LimpaCampos()
        {
            txtNome.Clear();
        }
        #endregion

    }
}
