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
    /// Lógica interna para Vendas.xaml
    /// </summary>
    public partial class Vendas : Window
    {
        private string operacao;
        private readonly VendaApplication vendaApplication = new VendaApplication();
        private readonly CarrinhoApplication carrinhoApplication = new CarrinhoApplication();
        private Venda venda;
        private Carrinho carrinho;
        public Vendas()
        {
            InitializeComponent();
            DesativaCampos();
        }

        #region btn Adicionar qtde
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
        #endregion

        #region btn Reduzir qtde
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
        #endregion

        #region Ativa os Campos
        private void AtivaCampos()
        {
            dpData.IsEnabled = true;
            boxFuncPessoa.IsEnabled = true;
            boxProduto.IsEnabled = true;
            txtValorUnit.IsEnabled = true;
            txtValorTot.IsEnabled = true;
            txtQuantidade.IsEnabled = true;
            btnAdicionar.IsEnabled = true;
            btnReduzir.IsEnabled = true;
        }
        #endregion

        #region Desativa Campos
        private void DesativaCampos()
        {
            dpData.IsEnabled = false;
            boxFuncPessoa.IsEnabled = false;
            boxProduto.IsEnabled = false;
            txtValorUnit.IsEnabled = false;
            txtValorTot.IsEnabled = false;
            txtQuantidade.IsEnabled = false;
            btnAdicionar.IsEnabled = false;
            btnReduzir.IsEnabled = false;
        }
        #endregion

        #region Aletera os Botões
        private void AlterarBotoes(int op)
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

        #region Limpa Campos
        private void LimpaCampos()
        {
            dpData.SelectedDate = null;
            boxFuncPessoa.SelectedIndex = -1; 
            boxProduto.SelectedIndex = -1;
            txtValorUnit.Clear();
            txtValorTot.Clear();
            txtQuantidade.Clear();
        }
        #endregion

        #region Inserir
        private void btnInserir_Click(object sender, RoutedEventArgs e)
        {
            AtivaCampos();
            this.operacao = "inserir";
            AlterarBotoes(2);
        }

        #endregion

        #region Salvar Venda
        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            AtivaCampos();

            }
        #endregion
    }
}
