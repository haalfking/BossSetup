using BossSetup.Models;

namespace BossSetup.Views;

public partial class ProdutosPage : ContentPage
{
    public ProdutosPage()
    {
        InitializeComponent();
        CarregarProdutos();
    }

    private void CarregarProdutos()
    {
        var produtos = App.ProdutoDB.Listar();
        collectionProdutos.ItemsSource = produtos;
    }
}