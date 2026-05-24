namespace BossSetup.Controls;

public partial class HomeTopBar : ContentView
{
    public event EventHandler? VerProdutosClicked;
    public event EventHandler? CadastrarProdutoClicked;
    public event EventHandler? GerenciarUsuariosClicked;
    public event EventHandler? SairClicked;

    public HomeTopBar()
    {
        InitializeComponent();
    }

    public void SetAdminMode(bool isAdmin)
    {
        btnCadastrarProduto.IsVisible = isAdmin;
        btnGerenciarUsuarios.IsVisible = isAdmin;
    }

    private void OnVerProdutosClicked(object sender, EventArgs e) =>
        VerProdutosClicked?.Invoke(this, e);

    private void OnCadastrarProdutoClicked(object sender, EventArgs e) =>
        CadastrarProdutoClicked?.Invoke(this, e);

    private void OnGerenciarUsuariosClicked(object sender, EventArgs e) =>
        GerenciarUsuariosClicked?.Invoke(this, e);

    private void OnSairClicked(object sender, EventArgs e) =>
        SairClicked?.Invoke(this, e);
}
