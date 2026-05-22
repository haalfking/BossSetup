namespace BossSetup.Views;

public partial class HomePage : ContentPage
{
    public HomePage()
    {
        InitializeComponent();

        // ?? Controle de acesso
        if (App.UsuarioLogado?.Tipo != "Admin")
        {
            btnCadastrarProduto.IsVisible = false;
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