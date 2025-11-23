
using System.Collections.Generic;
using Hamburgueria.Data;
using System;

namespace Hamburgueria.Domain
{
    public class ProdutoService
    {
        private readonly ProdutoRepository _produtoRepository;

        public ProdutoService()
        {
            _produtoRepository = new ProdutoRepository();
        }

        public List<Produto> BuscarTodos()
        {
            // Lógica de negócio (ex: filtros, ordenação) pode ser adicionada aqui
            return _produtoRepository.GetAll();
        }

        public Produto BuscarPorId(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID do Produto inválido.");
            }
            return _produtoRepository.GetById(id);
        }

        public void Adicionar(Produto produto)
        {
            // Regra de Negócio: Validação mínima
            if (string.IsNullOrWhiteSpace(produto.Nome))
            {
                throw new ArgumentException("O nome do Produto é obrigatório.");
            }

            _produtoRepository.Adicionar(produto);
        }
    }
}
