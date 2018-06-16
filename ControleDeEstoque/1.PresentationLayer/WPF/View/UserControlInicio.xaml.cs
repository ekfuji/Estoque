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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace View
{
    /// <summary>
    /// Interação lógica para UserControlInicio.xam
    /// </summary>
    public partial class UserControlInicio : UserControl
    {
        public UserControlInicio()
        {
            InitializeComponent();
        }
        private void btnProduto_MouseUp(object sender, MouseButtonEventArgs e)
        {
            CadastroProduto cadasProd = new CadastroProduto();
            cadasProd.ShowDialog();
        }



        private void btnPessoa_MouseUp(object sender, MouseButtonEventArgs e)
        {
            CadastroPessoa cadasPessoa = new CadastroPessoa();
            cadasPessoa.ShowDialog();
        }

        private void btnCategoria_MouseUp(object sender, MouseButtonEventArgs e)
        {
            CadastroCategoria cadasCategoria = new CadastroCategoria();
            cadasCategoria.ShowDialog();
        }
    }
}
