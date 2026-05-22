using BossSetup.Models;

namespace BossSetup.Views;

public partial class ProdutosPage : ContentPage
{
    public ProdutosPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        CarregarProdutos();
    }

    private void CarregarProdutos()
    {
        collectionProdutos.ItemsSource = App.ProdutoDB.Listar();
    }

    // ✏️ EDITAR PRODUTO (clique no card)
    private async void OnItemTapped(object sender, TappedEventArgs e)
    {
        var produto = (Produto)e.Parameter;

        await Navigation.PushAsync(new CadastroProdutoPage(produto));
    }

    // 🗑 EXCLUIR PRODUTO
    private async void OnExcluirClicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert(
            "Excluir",
            "Deseja realmente excluir este produto?",
            "Sim",
            "Não"
        );

        if (!confirm)
            return;

        var button = sender as Button;
        int id = (int)button.CommandParameter;

        App.ProdutoDB.Deletar(id);

        CarregarProdutos();
    }
}