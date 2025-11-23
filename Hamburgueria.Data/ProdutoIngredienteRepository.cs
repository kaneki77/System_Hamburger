
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Hamburgueria.Domain;
using System;

namespace Hamburgueria.Data
{
    public class ProdutoIngredienteRepository : RepositorioBase<ProdutoIngrediente>
    {
        private readonly DbConnection _dbConnection;

        public ProdutoIngredienteRepository()
        {
            _dbConnection = new DbConnection();
        }

        // Implementação do CRUD: CREATE (Adicionar Receita)
        public void Adicionar(ProdutoIngrediente pi)
        {
            const string query = "INSERT INTO ProdutoIngrediente (id_produto, id_ingrediente, quantidade_necessaria) VALUES (@id_produto, @id_ingrediente, @quantidade_necessaria)";

            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id_produto", pi.IdProduto);
                    command.Parameters.AddWithValue("@id_ingrediente", pi.IdIngrediente);
                    command.Parameters.AddWithValue("@quantidade_necessaria", pi.QuantidadeNecessaria);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Implementação do CRUD: READ (Buscar Receita por Produto)
        public List<ProdutoIngrediente> GetByProdutoId(int idProduto)
        {
            var receita = new List<ProdutoIngrediente>();
            const string query = @"
                SELECT 
                    pi.id_produto, pi.id_ingrediente, pi.quantidade_necessaria,
                    p.nome AS NomeProduto, i.nome AS NomeIngrediente, i.unidade_medida
                FROM ProdutoIngrediente pi
                JOIN Produto p ON pi.id_produto = p.id_produto
                JOIN Ingrediente i ON pi.id_ingrediente = i.id_ingrediente
                WHERE pi.id_produto = @idProduto";

            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@idProduto", idProduto);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var pi = new ProdutoIngrediente
                            {
                                IdProduto = reader.GetInt32("id_produto"),
                                IdIngrediente = reader.GetInt32("id_ingrediente"),
                                QuantidadeNecessaria = reader.GetDecimal("quantidade_necessaria"),
                                NomeProduto = reader.GetString("NomeProduto"),
                                NomeIngrediente = reader.GetString("NomeIngrediente"),
                                UnidadeMedida = reader.GetString("unidade_medida")
                            };
                            receita.Add(pi);
                        }
                    }
                }
            }
            return receita;
        }

        // Implementação do CRUD: DELETE (Remover Ingrediente da Receita)
        public void Remover(int idProduto, int idIngrediente)
        {
            const string query = "DELETE FROM ProdutoIngrediente WHERE id_produto = @idProduto AND id_ingrediente = @idIngrediente";

            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@idProduto", idProduto);
                    command.Parameters.AddWithValue("@idIngrediente", idIngrediente);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
