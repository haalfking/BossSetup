using BossSetup.Models;
using System.Collections.ObjectModel;

namespace BossSetup.Views;

public partial class HomePage : ContentPage
{
    public ObservableCollection<Produto> ProdutosDestaque { get; set; }

    public HomePage()
    {
        InitializeComponent();

        ProdutosDestaque = new ObservableCollection<Produto>
        (
            App.ProdutoDB.Listar()
            .Where(p => p.Destaque)
        );

        BindingContext = this;

        // Controle de acesso
        if (App.UsuarioLogado?.Tipo != "Admin")
        {
            btnCadastrarProduto.IsVisible = false;
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        ProdutosDestaque.Clear();

        var produtos = App.ProdutoDB.Listar()
            .Where(p => p.Destaque);

        foreach (var produto in produtos)
        {
            ProdutosDestaque.Add(produto);
        }
    }

    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        // limpa usuário logado
        App.UsuarioLogado = null;

        await Navigation.PopToRootAsync();
    }

    private async void OnCadastrarProdutoClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CadastroProdutoPage());
    }

    private async void OnVerProdutosClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ProdutosPage());
    }
}