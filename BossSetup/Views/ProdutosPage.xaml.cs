using BossSetup.Models;
using BossSetup.Services;

namespace BossSetup.Views;

public partial class ProdutosPage : ContentPage
{
    public ProdutosPage()
    {
        InitializeComponent();
    }

    // 🔄 sempre recarrega ao voltar pra tela
    protected override void OnAppearing()
    {
        base.OnAppearing();
        CarregarProdutos();
    }

    // 📦 carregar produtos (corrigido)
    private void CarregarProdutos()
    {
        var produtos = App.ProdutoDB.Listar();

        // força refresh da UI (IMPORTANTE)
        collectionProdutos.ItemsSource = null;
        collectionProdutos.ItemsSource = produtos;
    }

    // 🛒 abrir carrinho
    private async void OnIrCarrinhoClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CarrinhoPage());
    }

    // ✏️ editar produto
    private async void OnItemTapped(object sender, TappedEventArgs e)
    {
        var produto = (Produto)e.Parameter;

        await Navigation.PushAsync(new CadastroProdutoPage(produto));
    }

    // 🗑 excluir produto
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

    // 🛒 adicionar ao carrinho
    private async void OnAddCarrinhoClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var produto = (Produto)button.CommandParameter;

        Carrinho.Adicionar(produto);

        await DisplayAlert("Carrinho", "Produto adicionado!", "OK");
    }
}