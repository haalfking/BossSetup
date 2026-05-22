using BossSetup.Models;
using SQLite;

namespace BossSetup.Database
{
    public class ProdutoDatabase
    {
        private readonly SQLiteConnection _db;

        public ProdutoDatabase(string dbPath)
        {
            _db = new SQLiteConnection(dbPath);

            // cria tabela se não existir
            _db.CreateTable<Produto>();
        }

        // INSERT ou UPDATE automático
        public int Salvar(Produto produto)
        {
            if (produto.Id != 0)
                return _db.Update(produto);
            else
                return _db.Insert(produto);
        }

        public List<Produto> Listar()
        {
            return _db.Table<Produto>().ToList();
        }

        public Produto ObterPorId(int id)
        {
            return _db.Table<Produto>().FirstOrDefault(p => p.Id == id);
        }

        public int Deletar(int id)
        {
            return _db.Delete<Produto>(id);
        }

        public void LimparTabela()
        {
            _db.DeleteAll<Produto>();
        }
    }
}
