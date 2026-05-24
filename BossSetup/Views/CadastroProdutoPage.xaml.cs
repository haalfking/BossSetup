using BossSetup.Models;

namespace BossSetup.Views;

public partial class CadastroProdutoPage : ContentPage
{
    Produto produtoEditando;

    public CadastroProdutoPage()
    {
        InitializeComponent();
        topBar.VoltarClicked += async (_, _) => await Navigation.PopAsync();
    }

    // ✏️ MODO EDIÇÃO
    public CadastroProdutoPage(Produto produto)
    {
        InitializeComponent();
        topBar.VoltarClicked += async (_, _) => await Navigation.PopAsync();
        topBar.Titulo = "Editar produto";

        produtoEditando = produto;

        txtNome.Text = produto.Nome;
        txtDescricao.Text = produto.Descricao;
        txtPreco.Text = produto.Preco.ToString();
        txtCategoria.Text = produto.Categoria;
        txtEstoque.Text = produto.Estoque.ToString();
        txtImagem.Text = produto.Imagem;
        switchDestaque.IsToggled = produto.Destaque;
    }

    private async void OnSalvarClicked(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text) ||
                string.IsNullOrWhiteSpace(txtPreco.Text))
            {
                await DisplayAlert("Erro", "Nome e Preço são obrigatórios!", "OK");
                return;
            }

            if (!double.TryParse(txtPreco.Text, out double preco))
            {
                await DisplayAlert("Erro", "Preço inválido!", "OK");
                return;
            }

            if (!int.TryParse(txtEstoque.Text, out int estoque))
            {
                estoque = 0;
            }

            var produto = new Produto
            {
                Id = produtoEditando?.Id ?? 0, // 🔥 ESSENCIAL PARA UPDATE

                Nome = txtNome.Text.Trim(),
                Descricao = txtDescricao.Text?.Trim() ?? string.Empty,
                Preco = preco,

                Categoria = txtCategoria.Text?.Trim() ?? string.Empty,
                Estoque = estoque,
                Imagem = txtImagem.Text?.Trim() ?? string.Empty,

                Destaque = switchDestaque.IsToggled,
                Avaliacao = 0
            };

            App.ProdutoDB.Salvar(produto);

            await DisplayAlert("Sucesso", "Produto salvo com sucesso!", "OK");

            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "OK");
        }
    }
}