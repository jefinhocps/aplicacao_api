using MySql.Data.MySqlClient;
using UserApi.Models;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace UserApi.Repositories
{
    public class UsuarioRepository
    {
        private readonly string _connectionString;

        public UsuarioRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Insere Usuário
        public void CreateUsuario(Usuario usuario)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "Insert into usuario (nome_completo, telefone, endereco, numero, email, login, senha, status)" +
                    "values (@nome_completo, @telefone, @endereco, @numero, @email, @login, @senha, 'A');";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@nome_completo", usuario.NomeCompleto);
                    cmd.Parameters.AddWithValue("@telefone", usuario.Telefone);
                    cmd.Parameters.AddWithValue("@endereco", usuario.Endereco);
                    cmd.Parameters.AddWithValue("@numero", usuario.Numero);
                    cmd.Parameters.AddWithValue("@email", usuario.Email);
                    cmd.Parameters.AddWithValue("@login", usuario.Login);
                    cmd.Parameters.AddWithValue("@senha", usuario.Senha);

                    cmd.ExecuteNonQuery();

                }
            }
        }

        // Retorna usuários
        public List<Usuario> GetUsuarios() 
        {
            var usuarios = new List<Usuario>();

            using (MySqlConnection conn = new MySqlConnection(_connectionString)) 
            {
                conn.Open();
                string consultaUsuarios = "select * from usuario";

                using (MySqlCommand cmd = new MySqlCommand(consultaUsuarios, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader()) 
                    {
                        while (reader.Read())
                        {
                            usuarios.Add(new Usuario { 
                                
                                Id = reader.GetInt32("id"),
                                NomeCompleto = reader.GetString("nome_completo"),
                                Telefone = reader.GetString("telefone"),
                                Endereco = reader.GetString("endereco"),
                                Numero = reader.GetString("numero"),
                                Email = reader.GetString("email"),
                                Login = reader.GetString("login"),
                                Status = reader.GetString("status")
                            });
                        }
                    }
                }                
            }
            return usuarios;
        }

        // Retorna usuários pelo ID
        public Usuario GetUsuariosById(int id)
        {
            Usuario usuario = null;

            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string consultaUsuarios = "select * from usuario where id = @id;";

                using (MySqlCommand cmd = new MySqlCommand(consultaUsuarios, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                       if (reader.Read())
                        {
                            usuario = new Usuario
                            {
                                Id = reader.GetInt32("id"),
                                NomeCompleto = reader.GetString("nome_completo"),
                                Telefone = reader.GetString("telefone"),
                                Endereco = reader.GetString("endereco"),
                                Numero = reader.GetString("numero"),
                                Email = reader.GetString("email"),
                                Login = reader.GetString("login"),
                                Status = reader.GetString("status")
                            };
                        }
                    }
                }
            }
            return usuario;
        }

        // Delete usuário
        public bool DeleteUsuario(int id)
        {
            using (MySqlConnection con = new MySqlConnection(_connectionString))
            {
                con.Open();
                string query = "delete from usuario where id = @id;";

                using (MySqlCommand cmd = new MySqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0; // retorna true se o usuário for deletado com sucesso
                }
            }
        }
    }
}
