using BossSetup.Database;
using BossSetup.Models;
using System.IO;
using System.Linq;

namespace BossSetup
{
    public partial class App : Application
    {
        public static UsuarioDatabase Database;
        public static ProdutoDatabase ProdutoDB;
        public static Usuario UsuarioLogado;

        public App()
        {
            InitializeComponent();

            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "usuarios.db");
            Database = new UsuarioDatabase(dbPath);

            string dbProdutoPath = Path.Combine(FileSystem.AppDataDirectory, "produtos.db");
            ProdutoDB = new ProdutoDatabase(dbProdutoPath);

            // 🔥 CRIA ADMIN AUTOMÁTICO
            var usuarios = Database.Listar();

            if (!usuarios.Any(u => u.Email == "admin@admin.com"))
            {
                Database.Salvar(new Usuario
                {
                    Nome = "Administrador",
                    Email = "admin@admin.com",
                    Senha = "123",
                    Tipo = "Admin"
                });
            }

            if (!ProdutoDB.Listar().Any())
            {
                SeedProdutosDemo();
            }

            MainPage = new NavigationPage(new Views.LoginPage());
        }

        private static void SeedProdutosDemo()
        {
            var produtos = new[]
            {
                new Produto { Nome = "Mouse Gamer RGB", Descricao = "Sensor 12.000 DPI, 6 botões programáveis.", Preco = 149.90, Categoria = "Periféricos", Estoque = 25, Imagem = "logo.png", Destaque = true, Avaliacao = 4.8 },
                new Produto { Nome = "Teclado Mecânico", Descricao = "Switch blue, retroiluminação rainbow.", Preco = 299.90, Categoria = "Periféricos", Estoque = 15, Imagem = "logo.png", Destaque = true, Avaliacao = 4.7 },
                new Produto { Nome = "Headset 7.1", Descricao = "Som surround, microfone removível.", Preco = 199.90, Categoria = "Áudio", Estoque = 20, Imagem = "logo.png", Destaque = true, Avaliacao = 4.5 },
                new Produto { Nome = "Mousepad XL", Descricao = "Base antiderrapante, superfície speed.", Preco = 59.90, Categoria = "Acessórios", Estoque = 40, Imagem = "logo.png", Destaque = false, Avaliacao = 4.6 },
                new Produto { Nome = "Webcam Full HD", Descricao = "1080p 60fps para streams.", Preco = 249.90, Categoria = "Streaming", Estoque = 10, Imagem = "logo.png", Destaque = false, Avaliacao = 4.4 },
                new Produto { Nome = "Cadeira Gamer", Descricao = "Apoio lombar, reclinável 180°.", Preco = 1299.90, Categoria = "Mobiliário", Estoque = 5, Imagem = "logo.png", Destaque = false, Avaliacao = 4.9 }
            };

            foreach (var p in produtos)
                ProdutoDB.Salvar(p);
        }
    }
}
