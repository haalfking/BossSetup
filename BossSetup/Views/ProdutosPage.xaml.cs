using BossSetup.Models;
using BossSetup.Services;

namespace BossSetup.Views;

public partial class ProdutosPage : ContentPage
{
    public ProdutosPage()
    {
        InitializeComponent();

        topBar.VoltarClicked += async (_, _) => await Navigation.PopAsync();
        topBar.AcaoClicked += async (_, _) => await Navigation.PushAsync(new CarrinhoPage());
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

    // ✏️ editar produto
    private async void OnItemTapped(object sender, TappedEventArgs e)
    {
        if (App.UsuarioLogado?.Tipo != "Admin")
            return;

        var produto = (Produto)e.Parameter!;
        await Navigation.PushAsync(new CadastroProdutoPage(produto));
    }

    // 🗑 excluir produto
    private async void OnExcluirClicked(object sender, EventArgs e)
    {
        if (App.UsuarioLogado?.Tipo != "Admin")
        {
            await DisplayAlert("Acesso negado", "Apenas administradores podem excluir produtos.", "OK");
            return;
        }

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