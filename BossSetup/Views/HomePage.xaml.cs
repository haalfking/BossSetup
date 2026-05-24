using BossSetup.Models;
using BossSetup.Services;
using System.Collections.ObjectModel;

namespace BossSetup.Views;

public partial class HomePage : ContentPage
{
    public ObservableCollection<Produto> ProdutosDestaque { get; set; }

    public HomePage()
    {
        InitializeComponent();

        ProdutosDestaque = new ObservableCollection<Produto>();
        BindingContext = this;

        AtualizarSaudacao();
        AtualizarPainelAdmin();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        AtualizarSaudacao();
        AtualizarPainelAdmin();
        CarregarDestaques();
    }

    private void AtualizarSaudacao()
    {
        string nome = App.UsuarioLogado?.Nome ?? "visitante";
        lblSaudacao.Text = $"Ol\u00E1, {nome}!";
    }

    private void AtualizarPainelAdmin()
    {
        painelAdmin.IsVisible = App.UsuarioLogado?.Tipo == "Admin";
    }

    private void CarregarDestaques()
    {
        ProdutosDestaque.Clear();

        foreach (var produto in App.ProdutoDB.Listar().Where(p => p.Destaque))
            ProdutosDestaque.Add(produto);

        bool temDestaques = ProdutosDestaque.Count > 0;
        collectionDestaques.IsVisible = temDestaques;
        lblSemDestaques.IsVisible = !temDestaques;
    }

    private async void OnCarrinhoClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CarrinhoPage());
    }

    private void OnSairClicked(object sender, EventArgs e)
    {
        App.UsuarioLogado = null;
        Application.Current!.MainPage = new NavigationPage(new LoginPage());
    }

    private async void OnVerProdutosClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ProdutosPage());
    }

    private async void OnCadastrarProdutoClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CadastroProdutoPage());
    }

    private async void OnGerenciarUsuariosClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new UsuariosPage());
    }

    private async void OnComprarDestaqueClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var produto = (Produto)button!.CommandParameter;

        Carrinho.Adicionar(produto);
        await DisplayAlert("Carrinho", $"{produto.Nome} adicionado!", "OK");
    }
}
