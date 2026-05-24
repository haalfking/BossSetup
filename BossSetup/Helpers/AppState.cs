namespace BossSetup.Helpers;

public static class AppState
{
    public static bool IsAdmin => App.UsuarioLogado?.Tipo == "Admin";
}
