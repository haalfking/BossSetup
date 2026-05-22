namespace BossSetup.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        try
        {
            string email = txtEmail.Text;
            string senha = txtSenha.Text;

            // Validação dos campos
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(senha))
            {
                lblMensagem.Text = "Preencha todos os campos!";
                return;
            }

            // Buscar usuário no banco/lista
            var usuario = App.Database.ValidarLogin(email, senha);

            if (usuario != null)
            {
                App.UsuarioLogado = usuario;

                await DisplayAlert("Sucesso", "Login realizado com sucesso!", "OK");

                // 🔥 Navegação para próxima tela (vamos criar depois)
                await Navigation.PushAsync(new HomePage());
            }
            else
            {
                lblMensagem.Text = "Email ou senha inválidos!";
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "OK");
        }
    }

    private async void OnCadastroClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CadastroPage());
    }
}