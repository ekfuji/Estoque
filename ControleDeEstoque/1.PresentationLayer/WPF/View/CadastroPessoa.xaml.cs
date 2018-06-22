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

        #region Limpar campos
        private void LimpaCampos()
        {
            txtNome.Clear();
            txtCelular.Clear();
            txtEmail.Clear();
        }
        #endregion

        #region Alternar Botões
        private void AlternarBotoes(int op)
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

        #region Salvar Pessoa
        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            pessoa = new Pessoa();
            pessoa.nomePessoa = txtNome.Text;
            pessoa.celPessoa = Convert.ToInt64(txtCelular.Text);
            pessoa.email = txtEmail.Text;
            pessoa.dtaNascimento = Convert.ToDateTime(dpNascim.Text);
            application.SalvarPessoa(pessoa);
            dgListaPessoa.ItemsSource = application.BuscarTodos();
            AlternarBotoes(1);

        }
        #endregion

        #region Loaded Grid Lista Pessoas
        private void dgListaPessoa_Loaded(object sender, RoutedEventArgs e)
        {

            dgListaPessoa.ItemsSource = application.BuscarTodos();
            dgListaPessoa.Columns[0].IsReadOnly = true;
            dgListaPessoa.Columns[0].Header = "id";
            dgListaPessoa.Columns[1].Header = "Pessoa";
            dgListaPessoa.Columns[2].Visibility = Visibility.Hidden;
            AlternarBotoes(1);
        }
        #endregion

        #region Inserir
        private void btnInserir_Click(object sender, RoutedEventArgs e)
        {
            AlternarBotoes(2);
        }
        #endregion

        #region Editar Pessoa
        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (dgListaPessoa.SelectedCells.ToList() != null)
            {
                pessoa = new Pessoa();
                pessoa.nomePessoa = txtNome.Text;
                pessoa.celPessoa = Convert.ToInt32(txtCelular.Text);
                pessoa.email = txtEmail.Text;
                pessoa.dtaNascimento = Convert.ToDateTime(dpNascim.Text);
                application.SalvarPessoa(pessoa);
                AlternarBotoes(1);
            }
        }
        #endregion

        #region Cancelar
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            AlternarBotoes(1);
            LimpaCampos();
        }

        #endregion

        #region Excluir Pessoa
        private void btnExcluir_Click(object sender, RoutedEventArgs e)
        {
            Pessoa p = (Pessoa)dgListaPessoa.SelectedItem;
            pessoa = application.BuscarPessoa(x => x.idPessoa== p.idPessoa);
            application.ExcluirPessoa(pessoa);
            dgListaPessoa.ItemsSource = application.BuscarTodos();
            AlternarBotoes(1);
        }
        #endregion
    }
}
