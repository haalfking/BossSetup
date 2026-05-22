using BossSetup.Models;

namespace BossSetup.Views;

public partial class CadastroProdutoPage : ContentPage
{
    public CadastroProdutoPage()
    {
        InitializeComponent();
    }

    private async void OnSalvarClicked(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(txtNome.Text) || string.IsNullOrEmpty(txtPreco.Text))
            {
                await DisplayAlert("Erro", "Preencha todos os campos obrigatórios!", "OK");
                return;
            }

            var produto = new Produto
            {
                Nome = txtNome.Text,
                Descricao = txtDescricao.Text,
                Preco = Convert.ToDouble(txtPreco.Text)
            };

            App.ProdutoDB.Salvar(produto);

            await DisplayAlert("Sucesso", "Produto cadastrado com sucesso!", "OK");

            // Limpa os campos
            txtNome.Text = string.Empty;
            txtDescricao.Text = string.Empty;
            txtPreco.Text = string.Empty;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "OK");
        }
    }
}