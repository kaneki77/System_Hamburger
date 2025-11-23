
using System.Collections.Generic;
using Hamburgueria.Data;
using System;

namespace Hamburgueria.Domain
{
    public class IngredienteService
    {
        private readonly IngredienteRepository _ingredienteRepository;

        public IngredienteService()
        {
            _ingredienteRepository = new IngredienteRepository();
        }

        // Regra de Negócio: Validação e Adição
        public void Adicionar(Ingrediente ingrediente)
        {
            if (string.IsNullOrWhiteSpace(ingrediente.Nome))
            {
                throw new ArgumentException("O nome do Ingrediente é obrigatório.");
            }
            if (string.IsNullOrWhiteSpace(ingrediente.UnidadeMedida))
            {
                throw new ArgumentException("A unidade de medida é obrigatória.");
            }
            if (ingrediente.EstoqueMinimo < 0)
            {
                throw new ArgumentException("O estoque mínimo não pode ser negativo.");
            }

            _ingredienteRepository.Adicionar(ingrediente);
        }

        public List<Ingrediente> BuscarTodos()
        {
            return _ingredienteRepository.GetAll();
        }

        // Regra de Negócio: Validação e Atualização
        public void Atualizar(Ingrediente ingrediente)
        {
            if (ingrediente.Id <= 0)
            {
                throw new ArgumentException("ID do Ingrediente inválido para atualização.");
            }
            if (string.IsNullOrWhiteSpace(ingrediente.Nome))
            {
                throw new ArgumentException("O nome do Ingrediente é obrigatório.");
            }
            // ... outras validações ...

            _ingredienteRepository.Atualizar(ingrediente);
        }

        public void Remover(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID do Ingrediente inválido para remoção.");
            }
            // Regra de Negócio: Verificar se o ingrediente está sendo usado em alguma receita antes de remover
            // (Esta verificação exigiria um método no ProdutoIngredienteRepository)

            _ingredienteRepository.Remover(id);
        }
    }
}
