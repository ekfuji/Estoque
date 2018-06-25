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
    /// Lógica interna para CadastroFuncionario.xaml
    /// </summary>
    public partial class CadastroFuncionario : Window
    {
        private readonly FuncionarioApplication application = new FuncionarioApplication();
        private readonly PessoaApplication pessoaApplication = new PessoaApplication();
        private Funcionario funcionario;
        private string operacao;

        public CadastroFuncionario()
        {
            InitializeComponent();
            boxFuncPessoa.IsEnabled = false;
            dpContrat.IsEnabled = false;
            txtCTPS.IsEnabled = false;
        }

        #region AtivaCampos
        private void AtivaCampos()
        {
            boxFuncPessoa.IsEnabled = true;
            dpContrat.IsEnabled = true;
            txtCTPS.IsEnabled = true;
        }
        #endregion

        #region LimparCampos
        private void LimparCampos()
        {
            boxFuncPessoa.SelectedIndex = -1;
            dpContrat.SelectedDate = null;
            txtCTPS.Clear();
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

        #region Loaded Grid Lista Pessoas
        private void dgListaFunc_Loaded(object sender, RoutedEventArgs e)
        {
            dgListaFunc.ItemsSource = application.BuscarTodos();
            dgListaFunc.Columns[0].IsReadOnly = true;
            dgListaFunc.Columns[0].Header = "ID";
            dgListaFunc.Columns[1].Header = "Contratação";
            dgListaFunc.Columns[2].Header = "CTPS";
            dgListaFunc.Columns[3].Header = "IDPessoa";
            dgListaFunc.Columns[4].Visibility = Visibility.Hidden;
            dgListaFunc.Columns[5].Visibility = Visibility.Hidden;

            
            AlternarBotoes(1);
        }
        #endregion

        #region EditarGrid
        private void editarGrid()
        {
            dgListaFunc.Columns[0].IsReadOnly = true;
            dgListaFunc.Columns[0].Header = "ID";
            dgListaFunc.Columns[1].Header = "Contratação";
            dgListaFunc.Columns[2].Header = "CTPS";
            dgListaFunc.Columns[3].Header = "IDPessoa";
            dgListaFunc.Columns[4].Visibility = Visibility.Hidden;
            dgListaFunc.Columns[5].Visibility = Visibility.Hidden;
        }
        #endregion

        #region SalvarFuncionario
        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            AtivaCampos();

            if (this.operacao == "inserir")
            {
                funcionario = new Funcionario();
                funcionario.FK_idPessoa = (int)boxFuncPessoa.SelectedValue;
                funcionario.dataContratacao = Convert.ToDateTime(dpContrat.Text);
                funcionario.carteiraTrab = txtCTPS.Text;
                application.SalvarFuncionario(funcionario);
                AlternarBotoes(1);               
                dgListaFunc.ItemsSource = application.BuscarTodos();
                editarGrid();
            }
            if (this.operacao == "alterar")
            {
                if (dgListaFunc.SelectedCells.ToList() != null && boxFuncPessoa.Text != "")
                {
                    Funcionario f = (Funcionario)dgListaFunc.SelectedItem;
                    if (f.idFuncionario != 0)
                    {
                        funcionario = application.BuscarFuncionario(x => x.idFuncionario == f.idFuncionario);
                        funcionario.FK_idPessoa = (int)boxFuncPessoa.SelectedValue;
                        funcionario.dataContratacao = Convert.ToDateTime(dpContrat.Text);
                        funcionario.carteiraTrab = txtCTPS.Text;
                        application.SalvarFuncionario(funcionario);
                    }
                }
                dgListaFunc.ItemsSource = application.BuscarTodos();
                editarGrid();


            }
            editarGrid();
        }

        #endregion

        #region Inserir
        private void btnInserir_Click(object sender, RoutedEventArgs e)
        {
            AtivaCampos();
            this.operacao = "inserir";
            AlternarBotoes(2);
        }
        #endregion

        #region Loaded boxFuncPessoa

        private void boxFuncPessoa_Loaded(object sender, RoutedEventArgs e)
        {
            boxFuncPessoa.ItemsSource = pessoaApplication.BuscarTodos();
        }

        #endregion

        #region Editar Funcionario
        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            AtivaCampos();
            this.operacao = "alterar";
            AlternarBotoes(2);
        }

        #endregion

        #region Cancelar
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            AlternarBotoes(1);
            LimparCampos();
        }

        #endregion

        #region Excluir Funcionario
        private void btnExcluir_Click(object sender, RoutedEventArgs e)
        {
            Funcionario f = (Funcionario)dgListaFunc.SelectedItem;
            funcionario = application.BuscarFuncionario(x => x.idFuncionario == f.idFuncionario);
            application.ExcluirFuncionario(funcionario);
            dgListaFunc.ItemsSource = application.BuscarTodos();
            editarGrid();
            AlternarBotoes(1);
        }

        #endregion

        #region MouseDoubleCLick Lista de Funcionario
        private void dgListaFunc_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            AtivaCampos();
            if (dgListaFunc.SelectedIndex >= 0)
            {
                Pessoa pessoa = new Pessoa();
                Funcionario f = (Funcionario)dgListaFunc.SelectedItem;
                pessoa = pessoaApplication.BuscarPessoa(x => x.idPessoa == f.FK_idPessoa);
                boxFuncPessoa.Text = pessoa.nomePessoa;
                dpContrat.Text = f.dataContratacao.ToString();
                txtCTPS.Text = f.carteiraTrab;
                this.AlternarBotoes(3);

            }
        }

        #endregion

        #region Método que bloqueia a edição se a pessoa der somente um clique
        private void dgListaFunc_MouseUp(object sender, MouseButtonEventArgs e)
        {
            dgListaFunc.Columns[0].IsReadOnly = true;
            dgListaFunc.Columns[1].IsReadOnly = true;
            dgListaFunc.Columns[2].IsReadOnly = true;
            dgListaFunc.Columns[3].IsReadOnly = true;
        }

        #endregion

        #region Buscar Funcionario
        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            AtivaCampos();
            dpContrat.IsEnabled = false;
            boxFuncPessoa.IsEnabled = false;
            funcionario = new Funcionario();

            //buscar por CTPS
            if (txtCTPS.Text.Trim().Count() > 0)
            {
                funcionario.carteiraTrab = txtCTPS.Text;
                dgListaFunc.ItemsSource = application.BuscarPor(x => x.carteiraTrab.Contains(funcionario.carteiraTrab));

            }
            editarGrid();
        }
        #endregion
    }
}
