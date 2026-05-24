using BossSetup.Models;

namespace BossSetup.Views;

public partial class EditarUsuarioPage : ContentPage
{
    private readonly Usuario _usuarioEditando;
    private readonly bool _emailBloqueado;

    public EditarUsuarioPage(Usuario usuario)
    {
        InitializeComponent();
        topBar.VoltarClicked += async (_, _) => await Navigation.PopAsync();

        _usuarioEditando = usuario;
        _emailBloqueado = usuario.Email == "admin@admin.com";

        pickerTipo.ItemsSource = new List<string> { "Cliente", "Admin" };

        txtNome.Text = usuario.Nome;
        txtEmail.Text = usuario.Email;
        pickerTipo.SelectedItem = usuario.Tipo;

        if (_emailBloqueado)
        {
            txtEmail.IsEnabled = false;
            pickerTipo.IsEnabled = false;
        }
    }

    private async void OnSalvarClicked(object sender, EventArgs e)
    {
        try
        {
            string nome = txtNome.Text?.Trim() ?? "";
            string email = txtEmail.Text?.Trim() ?? "";
            string senha = txtSenha.Text ?? "";
            string tipo = pickerTipo.SelectedItem?.ToString() ?? _usuarioEditando.Tipo;

            if (string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(email))
            {
                await DisplayAlert("Erro", "Nome e e-mail são obrigatórios.", "OK");
                return;
            }

            if (!_emailBloqueado && App.Database.ExisteEmail(email, _usuarioEditando.Id))
            {
                await DisplayAlert("Erro", "Este e-mail já está cadastrado.", "OK");
                return;
            }

            if (string.IsNullOrEmpty(senha))
                senha = _usuarioEditando.Senha;

            var usuarioAtualizado = new Usuario
            {
                Id = _usuarioEditando.Id,
                Nome = nome,
                Email = _emailBloqueado ? _usuarioEditando.Email : email,
                Senha = senha,
                Tipo = _emailBloqueado ? "Admin" : tipo
            };

            App.Database.Atualizar(usuarioAtualizado);

            if (App.UsuarioLogado?.Id == usuarioAtualizado.Id)
                App.UsuarioLogado = usuarioAtualizado;

            await DisplayAlert("Sucesso", "Usuário atualizado!", "OK");
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "OK");
        }
    }
}
