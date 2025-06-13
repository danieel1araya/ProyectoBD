using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using BAL;

namespace DAL
{
    public class ConexionOracle
    {
        private readonly string StringConexion;

        public ConexionOracle(string pStringCnx)
        {
            StringConexion = pStringCnx;
        }

        public List<User> ObtenerUsuarios()
        {
            List<User> usuarios = new List<User>();

            try
            {
                using (OracleConnection connection = new OracleConnection(StringConexion))
                {
                    connection.Open();

                    string query = "SELECT Id, Usuario, Contrasena, Activo FROM Usuarios WHERE ROWNUM <= 1000";

                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        using (OracleDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                User usuario = new User
                                {
                                    Id = reader.GetInt32(0),
                                    Usuario = reader.GetString(1),
                                    Contrasena = reader.GetString(2),
                                    Activo = reader.GetInt32(3)
                                };
                                usuarios.Add(usuario);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener usuarios: " + ex.Message);
            }

            return usuarios;
        }
    }
}
