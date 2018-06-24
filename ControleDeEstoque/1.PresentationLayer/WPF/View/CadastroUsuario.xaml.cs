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
    /// Lógica interna para CadastroUsuario.xaml
    /// </summary>
    public partial class CadastroUsuario : Window
    {
        private readonly UsuarioApplication application = new UsuarioApplication();
        private readonly PessoaApplication pessoaApplication = new PessoaApplication();
        private Usuario usuario;
        private string operacao;

        public CadastroUsuario()
        {
            InitializeComponent();
            boxUserPessoa.IsEnabled = false;
            txtLogin.IsEnabled = false;
            boxSenha.IsEnabled = false;

        }

        #region AtivaCampos
        private void AtivaCampos()
        {
            boxUserPessoa.IsEnabled = true;
            txtLogin.IsEnabled = true;
            boxSenha.IsEnabled = true;

        }
        #endregion

        #region LimparCampos
        private void LimparCampos()
        {
            boxUserPessoa.SelectedIndex = -1;
            txtLogin.Clear();
            boxSenha.Clear();
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

        #region Loaded Lista de Usuários
        private void dgListaUser_Loaded(object sender, RoutedEventArgs e)
        {
            dgListaUser.ItemsSource = application.BuscarTodos();
            dgListaUser.Columns[0].IsReadOnly = true;
            dgListaUser.Columns[0].Header = "ID";
            dgListaUser.Columns[1].Header = "Usuário";
            dgListaUser.Columns[2].Header = "Senha";
            dgListaUser.Columns[3].Header = "Tipo";
            dgListaUser.Columns[4].Header = "IDPessoa";
            dgListaUser.Columns[5].Visibility = Visibility.Hidden;
            AlternarBotoes(1);

        }
        #endregion

        #region Loaded BoxUserPessoa
        private void boxUserPessoa_Loaded(object sender, RoutedEventArgs e)
        {
            boxUserPessoa.ItemsSource = pessoaApplication.BuscarTodos();
        }
        #endregion

        #region EditarGrid
        private void editarGrid()
        {
            dgListaUser.Columns[0].IsReadOnly = true;
            dgListaUser.Columns[0].Header = "ID";
            dgListaUser.Columns[1].Header = "Usuário";
            dgListaUser.Columns[2].Header = "Senha";
            dgListaUser.Columns[3].Header = "Tipo";
            dgListaUser.Columns[4].Header = "IDPessoa";
            dgListaUser.Columns[5].Visibility = Visibility.Hidden;
        }

        #endregion

        #region Salvar Usuário
        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            AtivaCampos();

            if (this.operacao == "inserir")
            {
                usuario = new Usuario();
                usuario.FK_idPessoa = (int)boxUserPessoa.SelectedValue;
                usuario.loginUsuario = txtLogin.Text;
                usuario.senhaUsuario = boxSenha.Password;
                int tipo;
                if(RadioBtnAdm.IsChecked == true)
                {
                    tipo = 1;
                    usuario.tipoUsuario = Convert.ToByte(tipo);
                }
                if (RadioBtnVend.IsChecked == true)
                {
                    tipo = 2;
                    usuario.tipoUsuario = Convert.ToByte(tipo);
                }
                application.SalvarUsuario(usuario);
                AlternarBotoes(1);
                dgListaUser.ItemsSource = application.BuscarTodos();
                editarGrid();
            }
            if (this.operacao == "alterar")
            {
                if (dgListaUser.SelectedCells.ToList() != null && boxUserPessoa.Text != "")
                {
                    Usuario u = (Usuario)dgListaUser.SelectedItem;
                    if (u.idUsuario != 0)
                    {
                        usuario = application.BuscarUsuario(x => x.idUsuario == u.idUsuario);
                        usuario = new Usuario();
                        usuario.FK_idPessoa = (int)boxUserPessoa.SelectedValue;
                        usuario.loginUsuario = txtLogin.Text;
                        usuario.senhaUsuario = boxSenha.Password;
                        int tipo;
                        if (RadioBtnAdm.IsChecked == true)
                        {
                            tipo = 1;
                            usuario.tipoUsuario = Convert.ToByte(tipo);
                        }
                        if (RadioBtnVend.IsChecked == true)
                        {
                            tipo = 2;
                            usuario.tipoUsuario = Convert.ToByte(tipo);
                        }
                        application.SalvarUsuario(usuario);
                    }
                }
                dgListaUser.ItemsSource = application.BuscarTodos();
                editarGrid();


            }

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

        #region Editar Usuario
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

        #region Excluir Usuario
        private void btnExcluir_Click(object sender, RoutedEventArgs e)
        {
            Usuario u = (Usuario)dgListaUser.SelectedItem;
            usuario = application.BuscarUsuario(x => x.idUsuario == u.idUsuario);
            application.ExcluirUsuario(usuario);
            dgListaUser.ItemsSource = application.BuscarTodos();
            editarGrid();
            AlternarBotoes(1);
        }
        #endregion

        #region Duplo clique na Lista de Usuario
        private void dgListaUser_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            AtivaCampos();
            if (dgListaUser.SelectedIndex >= 0)
            {
                Pessoa pessoa = new Pessoa();
                Usuario u = (Usuario)dgListaUser.SelectedItem;
                pessoa = pessoaApplication.BuscarPessoa(x => x.idPessoa == u.FK_idPessoa);
                boxUserPessoa.Text = pessoa.nomePessoa;
                txtLogin.Text = u.loginUsuario;
                boxSenha.Password = u.senhaUsuario;
                if(u.tipoUsuario == 1)
                {
                    RadioBtnAdm.IsChecked = true;
                }
                if (u.tipoUsuario == 2)
                {
                    RadioBtnVend.IsChecked = true;
                }
                this.AlternarBotoes(3);
            }
        }
        #endregion

        #region Método que bloqueia a edição se a pessoa der somente um clique
        private void dgListaUser_MouseUp(object sender, MouseButtonEventArgs e)
        {
            dgListaUser.Columns[0].IsReadOnly = true;
            dgListaUser.Columns[1].IsReadOnly = true;
            dgListaUser.Columns[2].IsReadOnly = true;
            dgListaUser.Columns[3].IsReadOnly = true;
            dgListaUser.Columns[4].IsReadOnly = true;
        }

        #endregion

        #region Buscar Usuario
        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            AtivaCampos();
            boxUserPessoa.IsEnabled = false;
            boxSenha.IsEnabled = false;
            usuario = new Usuario();

            //buscar por login
            if (txtLogin.Text.Trim().Count() > 0)
            {
                usuario.loginUsuario = txtLogin.Text;
                dgListaUser.ItemsSource = application.BuscarPor(x => x.loginUsuario.Contains(usuario.loginUsuario));

            }
            editarGrid();
        }
        #endregion
    }
}
