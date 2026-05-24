using BossSetup.Models;

namespace BossSetup.Views;

public partial class UsuariosPage : ContentPage
{
    public UsuariosPage()
    {
        InitializeComponent();
        topBar.VoltarClicked += async (_, _) => await Navigation.PopAsync();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (App.UsuarioLogado?.Tipo != "Admin")
        {
            DisplayAlert("Acesso negado", "Apenas administradores podem acessar esta tela.", "OK");
            Navigation.PopAsync();
            return;
        }

        CarregarUsuarios();
    }

    private void CarregarUsuarios()
    {
        collectionUsuarios.ItemsSource = App.Database.Listar();
    }

    private async void OnEditarUsuarioClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var usuario = (Usuario)button!.CommandParameter;

        await Navigation.PushAsync(new EditarUsuarioPage(usuario));
    }

    private async void OnExcluirUsuarioClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var usuario = (Usuario)button!.CommandParameter;

        if (usuario.Email == "admin@admin.com")
        {
            await DisplayAlert("Aviso", "O administrador padrão não pode ser excluído.", "OK");
            return;
        }

        if (usuario.Id == App.UsuarioLogado?.Id)
        {
            await DisplayAlert("Aviso", "Você não pode excluir seu próprio usuário.", "OK");
            return;
        }

        bool confirm = await DisplayAlert(
            "Excluir usuário",
            $"Deseja excluir {usuario.Nome}?",
            "Sim",
            "Não");

        if (!confirm)
            return;

        App.Database.Deletar(usuario.Id);
        CarregarUsuarios();
    }
}
