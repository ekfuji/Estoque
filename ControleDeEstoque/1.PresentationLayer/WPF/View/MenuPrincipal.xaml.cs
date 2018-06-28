using Orientacao.Application.ApplicationImplementation;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using View;

namespace View
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        private WindowState lastNonMinimizedState = WindowState.Normal;
        public int tipo;
        private readonly TipoApplication application = new TipoApplication();
        public MainWindow()
        {
            InitializeComponent();
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;

        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            ButtonOpenMenu.Visibility = Visibility.Visible;
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserControl usc = null;
            GridMain.Children.Clear();
            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {

                case "ItemCreate":
                    usc = new UserControlInicio();
                    GridMain.Children.Add(usc);
                    break;
                case "ItemMovement":
                    usc = new UserControlEscolha();
                    GridMain.Children.Add(usc);
                    break;
                case "ItemHVendas":
                    usc = new UserControlHVendas();
                    GridMain.Children.Add(usc);
                    break;
                default:
                    break;
            }
        }

        private void btnFechar_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void btnMinimizar_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.lastNonMinimizedState = this.WindowState;
            this.WindowState = WindowState.Minimized;
        }

        public void ControleAcesso(byte tipo)
        {
            if(tipo == 1)
            {
                ItemMovement.IsEnabled = false;
            }
            else if (tipo == 2)
            {
                ItemCreate.IsEnabled = false;
            }
        }

       }
    }

