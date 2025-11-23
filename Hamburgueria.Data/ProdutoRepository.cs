
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Hamburgueria.Domain;
using System;

namespace Hamburgueria.Data
{
    public class ProdutoRepository : RepositorioBase<Produto>
    {
        private readonly DbConnection _dbConnection;

        public ProdutoRepository()
        {
            _dbConnection = new DbConnection();
        }

        // Implementação do CRUD: READ (GetAll)
        public List<Produto> GetAll()
        {
            var produtos = new List<Produto>();
            // A query foi simplificada para o novo escopo (sem preco_venda)
            const string query = "SELECT id_produto, nome, descricao, ativo FROM Produto";

            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var produto = new Produto
                            {
                                Id = reader.GetInt32("id_produto"),
                                Nome = reader.GetString("nome"),
                                // Trata valores nulos para a descrição
                                Descricao = reader.IsDBNull(reader.GetOrdinal("descricao")) ? null : reader.GetString("descricao"),
                                Ativo = reader.GetBoolean("ativo")
                            };
                            produtos.Add(produto);
                        }
                    }
                }
            }
            return produtos;
        }

        // Implementação do CRUD: READ (GetById) - Essencial para a lógica de SaidaProduto
        public Produto GetById(int id)
        {
            const string query = "SELECT id_produto, nome, descricao, ativo FROM Produto WHERE id_produto = @id";

            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Produto
                            {
                                Id = reader.GetInt32("id_produto"),
                                Nome = reader.GetString("nome"),
                                Descricao = reader.IsDBNull(reader.GetOrdinal("descricao")) ? null : reader.GetString("descricao"),
                                Ativo = reader.GetBoolean("ativo")
                            };
                        }
                        return null;
                    }
                }
            }
        }

        // Implementação do CRUD: CREATE (Adicionar)
        public void Adicionar(Produto produto)
        {
            const string query = "INSERT INTO Produto (nome, descricao, ativo) VALUES (@nome, @descricao, @ativo)";

            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nome", produto.Nome);
                    command.Parameters.AddWithValue("@descricao", produto.Descricao);
                    command.Parameters.AddWithValue("@ativo", produto.Ativo);
                    command.ExecuteNonQuery();
                }
            }
        }
        
        // Outros métodos CRUD (Atualizar e Remover) podem ser implementados conforme a necessidade da UI.
    }
}
