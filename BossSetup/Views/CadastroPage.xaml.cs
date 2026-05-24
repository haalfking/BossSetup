using BossSetup.Models;

namespace BossSetup.Views;

public partial class CadastroPage : ContentPage
{
    public CadastroPage()
    {
        InitializeComponent();
        topBar.VoltarClicked += async (_, _) => await Navigation.PopAsync();
    }

    private async void OnSalvarClicked(object sender, EventArgs e)
    {
        try
        {
            string nome = txtNome.Text?.Trim() ?? "";
            string email = txtEmail.Text?.Trim() ?? "";
            string senha = txtSenha.Text ?? "";

            if (string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(senha))
            {
                await DisplayAlert("Erro", "Preencha todos os campos.", "OK");
                return;
            }

            if (App.Database.ExisteEmail(email))
            {
                await DisplayAlert("Erro", "Este e-mail j� est� cadastrado.", "OK");
                return;
            }

            Usuario u = new Usuario
            {
                Nome = nome,
                Email = email,
                Senha = senha,
                Tipo = "Cliente"
            };

            App.Database.Salvar(u);

            await DisplayAlert("Sucesso", "Usu�rio cadastrado! Fa�a login para continuar.", "OK");
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "OK");
        }
    }
}