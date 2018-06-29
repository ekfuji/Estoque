using DAL.ModeloDeDados;
using Orientacao.Application.ApplicationImplementation;

using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
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
    /// Interaction logic for UserControlHVendas.xaml
    /// </summary>
    public partial class UserControlHVendas : UserControl
    {
        Venda venda = new Venda();
        public UserControlHVendas()
        {
            InitializeComponent();
        }

        #region ListarVendas
        private void dgListaVenda_Loaded(object sender, RoutedEventArgs e)
        {
            EstoqueEntities ct = new EstoqueEntities();
            IQueryable<Venda> venda = ct.Venda;

            var query = from Venda in venda
                        where Venda.FK_idFuncionario >= 0
                        orderby Venda.dtaVenda
                        select new { Venda.idVenda, Venda.dtaVenda, Venda.Funcionario.Pessoa.nomePessoa, Venda.valorTotal };
            dgListaVenda.ItemsSource = query.ToList();
            dgListaVenda.Columns[0].IsReadOnly = true;
            dgListaVenda.Columns[1].IsReadOnly = true;
            dgListaVenda.Columns[2].IsReadOnly = true;
            dgListaVenda.Columns[3].IsReadOnly = true;
            dgListaVenda.Columns[0].Header = "ID";
            dgListaVenda.Columns[1].Header = "Data";
            dgListaVenda.Columns[2].Header = "Funcionário";
            dgListaVenda.Columns[3].Header = "Total";
        }
        #endregion

        #region BuscarPorFuncionario
        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            string nome;
            if (txtNome.Text.Trim().Count() > 0)
            {
                nome= txtNome.Text;

                EstoqueEntities ct = new EstoqueEntities();
                IQueryable<Venda> vend = ct.Venda;

                var query = from Venda in vend
                            where Venda.Funcionario.Pessoa.nomePessoa.Contains(nome)
                            orderby Venda.dtaVenda
                            select new { Venda.idVenda, Venda.dtaVenda, Venda.Funcionario.Pessoa.nomePessoa, Venda.valorTotal };
                dgListaVenda.ItemsSource = query.ToList();
                dgListaVenda.Columns[0].IsReadOnly = true;
                dgListaVenda.Columns[1].IsReadOnly = true;
                dgListaVenda.Columns[2].IsReadOnly = true;
                dgListaVenda.Columns[3].IsReadOnly = true;
                dgListaVenda.Columns[0].Header = "ID";
                dgListaVenda.Columns[1].Header = "Data";
                dgListaVenda.Columns[2].Header = "Funcionário";
                dgListaVenda.Columns[3].Header = "Total";


            }
        }
        #endregion
    }
}
