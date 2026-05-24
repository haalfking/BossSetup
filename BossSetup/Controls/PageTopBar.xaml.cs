namespace BossSetup.Controls;

public partial class PageTopBar : ContentView
{
    public static readonly BindableProperty TituloProperty = BindableProperty.Create(
        nameof(Titulo), typeof(string), typeof(PageTopBar), string.Empty,
        propertyChanged: (b, _, v) => ((PageTopBar)b).lblTitulo.Text = (string)v);

    public static readonly BindableProperty TextoAcaoProperty = BindableProperty.Create(
        nameof(TextoAcao), typeof(string), typeof(PageTopBar), string.Empty,
        propertyChanged: OnTextoAcaoChanged);

    public event EventHandler? VoltarClicked;
    public event EventHandler? AcaoClicked;

    public string Titulo
    {
        get => (string)GetValue(TituloProperty);
        set => SetValue(TituloProperty, value);
    }

    public string TextoAcao
    {
        get => (string)GetValue(TextoAcaoProperty);
        set => SetValue(TextoAcaoProperty, value);
    }

    public PageTopBar()
    {
        InitializeComponent();
    }

    private static void OnTextoAcaoChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var bar = (PageTopBar)bindable;
        var texto = (string)newValue;
        bar.btnAcao.Text = texto;
        bar.btnAcao.IsVisible = !string.IsNullOrEmpty(texto);
    }

    private void OnVoltarClicked(object sender, EventArgs e) =>
        VoltarClicked?.Invoke(this, e);

    private void OnAcaoClicked(object sender, EventArgs e) =>
        AcaoClicked?.Invoke(this, e);
}
