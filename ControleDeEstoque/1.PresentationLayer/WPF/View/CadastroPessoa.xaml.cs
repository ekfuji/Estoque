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
    /// Lógica interna para CadastroPessoa.xaml
    /// </summary>
    public partial class CadastroPessoa : Window
    {
        private readonly PessoaApplication application = new PessoaApplication();
        private Pessoa pessoa;
        public CadastroPessoa()
        {
            InitializeComponent();
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            pessoa = new Pessoa();
            pessoa.nomePessoa = txtNome.Text;
            pessoa.celPessoa = Convert.ToInt32(txtCelular.Text);
            pessoa.email = txtEmail.Text;
            pessoa.dtaNascimento = Convert.ToDateTime(dpNascim.Text);
        }
    }
}
