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
        public CadastroCategoria()
        {
            InitializeComponent();
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            categoria = new Categoria();
            categoria.nomeCategoria = txtNome.Text;
            application.SalvarCategoria(categoria);
            dgListaCateg.ItemsSource = application.BuscarTodos();
            AlterarBotoes(1);
        }


        private void dgListaCateg_Loaded(object sender, RoutedEventArgs e)
        {
            dgListaCateg.ItemsSource = application.BuscarTodos();

            AlterarBotoes(1);


        }



        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            categoria = new Categoria();
            if(dgListaCateg.SelectedCells.ToList() != null)
            {
                categoria.nomeCategoria = txtNome.Text;
                application.SalvarCategoria(categoria);
                AlterarBotoes(1);
            }
            dgListaCateg.ItemsSource = application.BuscarTodos();
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

        private void btnInserir_Click(object sender, RoutedEventArgs e)
        {
            AlterarBotoes(2);
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            AlterarBotoes(1);
        }

        private void dgListaCateg_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
            if (dgListaCateg.SelectedIndex >= 0)
            {
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

        private void btnExcluir_Click(object sender, RoutedEventArgs e)
        {
            Categoria c = (Categoria)dgListaCateg.SelectedItem;
            categoria = application.BuscarCategoria(x => x.idCategoria == c.idCategoria);
            application.ExcluirCategoria(categoria);
            dgListaCateg.ItemsSource = application.BuscarTodos();
            AlterarBotoes(1);
        }
    }
}
