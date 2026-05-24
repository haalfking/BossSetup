using BossSetup.Models;
using System.Collections.ObjectModel;

namespace BossSetup.Views;

public partial class HomePage : ContentPage
{
    public ObservableCollection<Produto> ProdutosDestaque { get; set; }

    public HomePage()
    {
        InitializeComponent();

        ProdutosDestaque = new ObservableCollection<Produto>(
            App.ProdutoDB.Listar().Where(p => p.Destaque));

        BindingContext = this;
        AtualizarPainelAdmin();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        AtualizarPainelAdmin();

        ProdutosDestaque.Clear();
        foreach (var produto in App.ProdutoDB.Listar().Where(p => p.Destaque))
            ProdutosDestaque.Add(produto);
    }

    private void AtualizarPainelAdmin()
    {
        bool isAdmin = App.UsuarioLogado?.Tipo == "Admin";
        painelAdmin.IsVisible = isAdmin;
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
}
