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
        public CadastroCategoria()
        {
            InitializeComponent();
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            Categoria categoria = new Categoria();
            categoria.nomeCategoria = txtNome.Text;
            application.SalvarCategoria(categoria);
        }
    }
}
