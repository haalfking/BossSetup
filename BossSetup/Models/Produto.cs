using SQLite;

namespace BossSetup.Models
{
    public class Produto
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public string Descricao { get; set; } = string.Empty;

        public double Preco { get; set; }

        // NOVOS CAMPOS

        public string Categoria { get; set; } = string.Empty;

        public int Estoque { get; set; }

        public string Imagem { get; set; } = string.Empty;

        public bool Destaque { get; set; }

        public double Avaliacao { get; set; }
    }
}
