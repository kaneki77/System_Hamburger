
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Hamburgueria.Domain;
using System;

namespace Hamburgueria.Data
{
    public class IngredienteRepository : RepositorioBase<Ingrediente>
    {
        private readonly DbConnection _dbConnection;

        public IngredienteRepository()
        {
            _dbConnection = new DbConnection();
        }

        // Implementação do CRUD: CREATE (Adicionar)
        public void Adicionar(Ingrediente ingrediente)
        {
            const string query = "INSERT INTO Ingrediente (nome, unidade_medida, estoque_minimo) VALUES (@nome, @unidade_medida, @estoque_minimo)";

            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nome", ingrediente.Nome);
                    command.Parameters.AddWithValue("@unidade_medida", ingrediente.UnidadeMedida);
                    command.Parameters.AddWithValue("@estoque_minimo", ingrediente.EstoqueMinimo);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Implementação do CRUD: READ (GetAll)
        public List<Ingrediente> GetAll()
        {
            var ingredientes = new List<Ingrediente>();
            const string query = "SELECT id_ingrediente, nome, unidade_medida, estoque_minimo FROM Ingrediente";

            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var ingrediente = new Ingrediente
                            {
                                Id = reader.GetInt32("id_ingrediente"),
                                Nome = reader.GetString("nome"),
                                UnidadeMedida = reader.GetString("unidade_medida"),
                                EstoqueMinimo = reader.GetDecimal("estoque_minimo")
                            };
                            ingredientes.Add(ingrediente);
                        }
                    }
                }
            }
            return ingredientes;
        }

        // Implementação do CRUD: UPDATE (Atualizar)
        public void Atualizar(Ingrediente ingrediente)
        {
            const string query = "UPDATE Ingrediente SET nome = @nome, unidade_medida = @unidade_medida, estoque_minimo = @estoque_minimo WHERE id_ingrediente = @id";

            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", ingrediente.Id);
                    command.Parameters.AddWithValue("@nome", ingrediente.Nome);
                    command.Parameters.AddWithValue("@unidade_medida", ingrediente.UnidadeMedida);
                    command.Parameters.AddWithValue("@estoque_minimo", ingrediente.EstoqueMinimo);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Implementação do CRUD: DELETE (Remover)
        public void Remover(int id)
        {
            const string query = "DELETE FROM Ingrediente WHERE id_ingrediente = @id";

            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
