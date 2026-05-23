using BossSetup.Services;
using BossSetup.Models;

namespace BossSetup.Views;

public partial class CarrinhoPage : ContentPage
{
    public CarrinhoPage()
    {
        InitializeComponent();
        CarregarCarrinho();
    }

    private void CarregarCarrinho()
    {
        collectionCarrinho.ItemsSource = Carrinho.Itens;

        lblTotal.Text = $"Total: R$ {Carrinho.Total:F2}";
    }

    private void OnRemoverClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var produto = (Produto)button.CommandParameter;

        Carrinho.Remover(produto);

        CarregarCarrinho();
    }
}