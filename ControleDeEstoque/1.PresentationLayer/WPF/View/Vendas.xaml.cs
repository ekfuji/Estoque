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
        public Vendas()
        {
            InitializeComponent();
        }

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
    }
}
