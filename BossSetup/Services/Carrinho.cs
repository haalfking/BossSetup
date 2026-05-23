using BossSetup.Models;
using System.Collections.ObjectModel;

namespace BossSetup.Services
{
    public static class Carrinho
    {
        public static ObservableCollection<Produto> Itens { get; set; }
            = new ObservableCollection<Produto>();

        public static void Adicionar(Produto produto)
        {
            Itens.Add(produto);
        }

        public static void Remover(Produto produto)
        {
            Itens.Remove(produto);
        }

        public static decimal Total =>
            (decimal)Itens.Sum(p => (decimal)p.Preco);
    }
}
