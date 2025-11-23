
using System;

namespace Hamburgueria.Domain
{
    public class ProdutoIngrediente : EntidadeBase
    {
        // Esta entidade não tem um ID próprio, a chave primária é composta
        public int IdProduto { get; set; }
        public int IdIngrediente { get; set; }
        public decimal QuantidadeNecessaria { get; set; }
        
        // Propriedades de navegação (opcional, mas útil para a UI)
        public string NomeProduto { get; set; }
        public string NomeIngrediente { get; set; }
        public string UnidadeMedida { get; set; }
    }
}
