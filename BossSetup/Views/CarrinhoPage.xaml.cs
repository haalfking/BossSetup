using BossSetup.Services;
using BossSetup.Models;

namespace BossSetup.Views;

public partial class CarrinhoPage : ContentPage
{
    public CarrinhoPage()
    {
        InitializeComponent();
        topBar.VoltarClicked += async (_, _) => await Navigation.PopAsync();
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
        var produto = (Produto)button!.CommandParameter;

        Carrinho.Remover(produto);

        CarregarCarrinho();
    }

    private async void OnFinalizarClicked(object sender, EventArgs e)
    {
        if (Carrinho.Itens.Count == 0)
        {
            await DisplayAlert("Carrinho vazio", "Adicione produtos antes de finalizar.", "OK");
            return;
        }

        int qtd = Carrinho.Itens.Count;
        decimal total = Carrinho.Total;

        bool confirm = await DisplayAlert(
            "Finalizar compra",
            $"Confirmar {qtd} item(ns) por R$ {total:F2}?\n\n(Pagamento simulado para demonstração.)",
            "Confirmar",
            "Cancelar");

        if (!confirm)
            return;

        Carrinho.Itens.Clear();
        CarregarCarrinho();

        await DisplayAlert("Sucesso", "Compra finalizada com sucesso! Obrigado por comprar na Boss Setup.", "OK");
        await Navigation.PopAsync();
    }
}