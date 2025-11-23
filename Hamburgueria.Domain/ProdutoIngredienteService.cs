
using System.Collections.Generic;
using Hamburgueria.Data;
using System;

namespace Hamburgueria.Domain
{
    public class ProdutoIngredienteService
    {
        private readonly ProdutoIngredienteRepository _piRepository;

        public ProdutoIngredienteService()
        {
            _piRepository = new ProdutoIngredienteRepository();
        }

        // Regra de Negócio: Validação e Adição de Ingrediente à Receita
        public void AdicionarIngrediente(ProdutoIngrediente pi)
        {
            if (pi.IdProduto <= 0 || pi.IdIngrediente <= 0)
            {
                throw new ArgumentException("O Produto e o Ingrediente devem ser selecionados.");
            }
            if (pi.QuantidadeNecessaria <= 0)
            {
                throw new ArgumentException("A quantidade necessária deve ser maior que zero.");
            }

            _piRepository.Adicionar(pi);
        }

        public List<ProdutoIngrediente> BuscarReceitaPorProduto(int idProduto)
        {
            if (idProduto <= 0)
            {
                throw new ArgumentException("ID do Produto inválido.");
            }
            return _piRepository.GetByProdutoId(idProduto);
        }

        public void RemoverIngrediente(int idProduto, int idIngrediente)
        {
            if (idProduto <= 0 || idIngrediente <= 0)
            {
                throw new ArgumentException("IDs de Produto e Ingrediente inválidos para remoção.");
            }
            _piRepository.Remover(idProduto, idIngrediente);
        }
    }
}
