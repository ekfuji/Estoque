using Orientacao.Application.UsuarioConnection;
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
    /// Lógica interna para TelaLogin.xaml
    /// </summary>
    public partial class TelaLogin : Window
    {
        private WindowState lastNonMinimizedState = WindowState.Normal;
        public TelaLogin()
        {
            InitializeComponent();
        }

        private void PackIcon_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.lastNonMinimizedState = this.WindowState;
            this.WindowState = WindowState.Minimized;
        }

        private void PackIcon_MouseUp_1(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var conec = new UsuarioConnection();
            string login, senha;

            login = txtLogin.Text;
            senha = boxSenha.Password;

            if (!conec.Logar(login, senha))
            {
                MessageBox.Show("O login ou senha estão incorretos!");
            }
            else
            {
                var logado = new MainWindow();
                logado.ShowDialog();
            }
        }
    }
}
