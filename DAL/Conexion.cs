using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BAL;

namespace DAL
{
    public class Conexion
    {
        private readonly string StringConexion;

        public Conexion(string pStringCnx)
        {
            StringConexion = pStringCnx;
        }

        // INICIO SISTEMAS
        public bool SistemaExiste(string nombreSistema)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(StringConexion))
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) FROM Sistemas WHERE NombreSistema = @nombre";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@nombre", nombreSistema);

                        int count = (int)command.ExecuteScalar();
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al verificar existencia del sistema: " + ex.Message);
            }
        }

        public List<Sistema> ObtenerSistemas()
        {
            List<Sistema> sistemas = new List<Sistema>();

            try
            {
                using (SqlConnection connection = new SqlConnection(StringConexion))
                {
                    connection.Open();

                    string query = "SELECT IDSistema, NombreSistema FROM Sistemas";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Sistema sistema = new Sistema
                                {
                                    Id = reader.GetInt32(0),
                                    NombreSistema = reader.GetString(1)
                                };
                                sistemas.Add(sistema);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener sistemas: " + ex.Message);
            }

            return sistemas;
        }

        public bool AgregarSistema(Sistema sistema, int idUsuario)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(StringConexion))
                {
                    connection.Open();

                    SqlTransaction transaction = connection.BeginTransaction();

                    try
                    {
                        string query = "INSERT INTO Sistemas (NombreSistema) VALUES (@nombre)";

                        using (SqlCommand command = new SqlCommand(query, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@nombre", sistema.NombreSistema);
                            int filasAfectadas = command.ExecuteNonQuery();

                            if (filasAfectadas > 0)
                            {
                                // Registrar en bitácora
                                string detalle = $"Se agregó el sistema: {sistema.NombreSistema}";
                                string bitacora = "INSERT INTO Bitacora (IdUsuario, Detalles, Fecha) VALUES (@idUsuario, @detalles, GETDATE())";

                                using (SqlCommand cmd = new SqlCommand(bitacora, connection, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                                    cmd.Parameters.AddWithValue("@detalles", detalle);
                                    cmd.ExecuteNonQuery();
                                }
                                transaction.Commit();

                                return true;
                            }
                            else
                            {
                                transaction.Rollback();
                                return false;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception("Error al agregar sistema: " + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar sistema: " + ex.Message);
            }
        }

        public bool ActualizarSistema(Sistema sistema, int idUsuario)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(StringConexion))
                {
                    conn.Open();

                    SqlTransaction transaction = conn.BeginTransaction();

                    try
                    {
                        string query = "UPDATE Sistemas SET NombreSistema = @nombre WHERE IDSistema = @id";

                        using (SqlCommand cmd = new SqlCommand(query, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@nombre", sistema.NombreSistema);
                            cmd.Parameters.AddWithValue("@id", sistema.Id);
                            int filasAfectadas = cmd.ExecuteNonQuery();

                            if (filasAfectadas > 0)
                            {
                                // Registramos en bitácora
                                string detalle = $"Se actualizó el sistema {sistema.NombreSistema} (Id:{sistema.Id})";

                                string bitacora = "INSERT INTO Bitacora (IdUsuario, Detalles, Fecha) VALUES (@idUsuario, @detalles, GETDATE())";

                                using (SqlCommand cmdBitacora = new SqlCommand(bitacora, conn, transaction))
                                {
                                    cmdBitacora.Parameters.AddWithValue("@idUsuario", idUsuario);
                                    cmdBitacora.Parameters.AddWithValue("@detalles", detalle);
                                    cmdBitacora.ExecuteNonQuery();
                                }
                                transaction.Commit();

                                return true;
                            }
                            else
                            {
                                transaction.Rollback();
                                return false;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception("Error al actualizar sistema: " + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar sistema: " + ex.Message);
            }
        }

        public bool EliminarSistema(int idSistema, int idUsuario)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(StringConexion))
                {
                    connection.Open();

                    SqlTransaction transaction = connection.BeginTransaction();

                    try
                    {
                        string query = "DELETE FROM Sistemas WHERE IDSistema = @id";

                        using (SqlCommand cmd = new SqlCommand(query, connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@id", idSistema);
                            int filasAfectadas = cmd.ExecuteNonQuery();

                            if (filasAfectadas > 0)
                            {
                                // Registramos en bitácora
                                string detalle = $"Se eliminó el sistema con Id:{idSistema}";

                                string bitacora = "INSERT INTO Bitacora (IdUsuario, Detalles, Fecha) VALUES (@idUsuario, @detalles, GETDATE())";

                                using (SqlCommand cmdBitacora = new SqlCommand(bitacora, connection, transaction))
                                {
                                    cmdBitacora.Parameters.AddWithValue("@idUsuario", idUsuario);
                                    cmdBitacora.Parameters.AddWithValue("@detalles", detalle);
                                    cmdBitacora.ExecuteNonQuery();
                                }
                                transaction.Commit();

                                return true;
                            }
                            else
                            {
                                transaction.Rollback();
                                return false;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception("Error al eliminar sistema: " + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar sistema: " + ex.Message);
            }
        }


        // FIN SISTEMAS

        // INICIO USUARIOS

        public bool UsuarioExiste(string usuario)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(StringConexion))
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) FROM Usuarios WHERE Usuario = @usuario";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@usuario", usuario);

                        int count = (int)command.ExecuteScalar();
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al verificar existencia del usuario: " + ex.Message);
            }
        }


        public bool ValidarCredenciales(string usuario, string contrasena)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(StringConexion))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_ValidarCredenciales", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@Usuario", usuario);
                        command.Parameters.AddWithValue("@Contrasena", contrasena);

                        int resultado = (int)command.ExecuteScalar();

                        return resultado == 1;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al validar credenciales: " + ex.Message);
            }
        }

        public void InsertarBitacora(int idUsuario, string detalle)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(StringConexion))
                {
                    connection.Open();

                    string query = "INSERT INTO Bitacora (IdUsuario, Detalles, Fecha) VALUES (@IdUsuario, @Detalles, GETDATE())";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
                        cmd.Parameters.AddWithValue("@Detalles", detalle);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar en bitácora: " + ex.Message);
            }
        }


        public int ObtenerIdUsuario(string nombreUsuario)
        {
            int idUsuario = -1;

            try
            {
                using (SqlConnection connection = new SqlConnection(StringConexion))
                {
                    connection.Open();

                    string query = "SELECT IdUsuario FROM Usuarios WHERE Usuario = @usuario";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@usuario", nombreUsuario);

                        object result = cmd.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            idUsuario = Convert.ToInt32(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener IdUsuario: " + ex.Message);
            }
            return idUsuario;
        }


        public List<User> ObtenerUsuarios()
        {
            List<User> usuarios = new List<User>();

            try
            {
                using (SqlConnection connection = new SqlConnection(StringConexion))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SELECT TOP (1000) [IDUsuario], [Usuario], [Contrasena], [Activo] FROM [BDProyecto].[dbo].[Usuarios]", connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
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


        public bool AgregarUsuario(User usuario, int idAdmin)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(StringConexion))
                {
                    connection.Open();
                    SqlTransaction transaction = connection.BeginTransaction();

                    try
                    {
                        string query = "INSERT INTO Usuarios (Usuario, Contrasena, Activo) VALUES (@Usuario, @Contrasena, @Activo)";

                        using (SqlCommand command = new SqlCommand(query, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@Usuario", usuario.Usuario);
                            command.Parameters.AddWithValue("@Contrasena", usuario.Contrasena);
                            command.Parameters.AddWithValue("@Activo", usuario.Activo);

                            int filasAfectadas = command.ExecuteNonQuery();

                            if (filasAfectadas > 0)
                            {
                                string detalle = $"Se agregó el usuario: {usuario.Usuario}";
                                string bitacora = "INSERT INTO Bitacora (IdUsuario, Detalles, Fecha) VALUES (@idUsuario, @detalles, GETDATE())";

                                using (SqlCommand cmdBitacora = new SqlCommand(bitacora, connection, transaction))
                                {
                                    cmdBitacora.Parameters.AddWithValue("@idUsuario", idAdmin);
                                    cmdBitacora.Parameters.AddWithValue("@detalles", detalle);
                                    cmdBitacora.ExecuteNonQuery();
                                }
                                transaction.Commit();
                                return true;
                            }
                            else
                            {
                                transaction.Rollback();
                                return false;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception("Error al agregar usuario: " + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar usuario: " + ex.Message);
            }
        }

        public bool ActualizarUsuario(User user, int idAdmin)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(StringConexion))
                {
                    conn.Open();
                    SqlTransaction transaction = conn.BeginTransaction();

                    try
                    {
                        string query = "UPDATE Usuarios SET Usuario = @usuario, Contrasena = @contrasena, Activo = @activo WHERE IDUsuario = @id";

                        using (SqlCommand cmd = new SqlCommand(query, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@usuario", user.Usuario);
                            cmd.Parameters.AddWithValue("@contrasena", user.Contrasena);
                            cmd.Parameters.AddWithValue("@activo", user.Activo);
                            cmd.Parameters.AddWithValue("@id", user.Id);

                            int filasAfectadas = cmd.ExecuteNonQuery();

                            if (filasAfectadas > 0)
                            {
                                string detalle = $"Se actualizó el usuario: {user.Usuario} (Id:{user.Id})";
                                string bitacora = "INSERT INTO Bitacora (IdUsuario, Detalles, Fecha) VALUES (@idUsuario, @detalles, GETDATE())";

                                using (SqlCommand cmdBitacora = new SqlCommand(bitacora, conn, transaction))
                                {
                                    cmdBitacora.Parameters.AddWithValue("@idUsuario", idAdmin);
                                    cmdBitacora.Parameters.AddWithValue("@detalles", detalle);
                                    cmdBitacora.ExecuteNonQuery();
                                }
                                transaction.Commit();
                                return true;
                            }
                            else
                            {
                                transaction.Rollback();
                                return false;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception("Error al actualizar usuario: " + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar usuario: " + ex.Message);
            }
        }

        public bool EliminarUsuario(int idUsuario, int idAdmin)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(StringConexion))
                {
                    connection.Open();
                    SqlTransaction transaction = connection.BeginTransaction();

                    try
                    {
                        string query = "DELETE FROM Usuarios WHERE IDUsuario = @id";

                        using (SqlCommand command = new SqlCommand(query, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@id", idUsuario);

                            int filasAfectadas = command.ExecuteNonQuery();

                            if (filasAfectadas > 0)
                            {
                                string detalle = $"Se eliminó el usuario con Id: {idUsuario}";
                                string bitacora = "INSERT INTO Bitacora (IdUsuario, Detalles, Fecha) VALUES (@idUsuario, @detalles, GETDATE())";

                                using (SqlCommand cmdBitacora = new SqlCommand(bitacora, connection, transaction))
                                {
                                    cmdBitacora.Parameters.AddWithValue("@idUsuario", idAdmin);
                                    cmdBitacora.Parameters.AddWithValue("@detalles", detalle);
                                    cmdBitacora.ExecuteNonQuery();
                                }
                                transaction.Commit();
                                return true;
                            }
                            else
                            {
                                transaction.Rollback();
                                return false;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception("Error al eliminar usuario: " + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar usuario: " + ex.Message);
            }
        }


        // FIN USUARIOS


        // INICIO ROLES

        // Verifica si un rol ya existe por nombre
        public bool RolExiste(string nombreRol)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(StringConexion))
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) FROM Roles WHERE NombreRol = @nombreRol";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@nombreRol", nombreRol);

                        int count = (int)command.ExecuteScalar();
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al verificar existencia del rol: " + ex.Message);
            }
        }

        // Obtiene todos los roles
        public List<Rol> ObtenerRoles()
        {
            List<Rol> roles = new List<Rol>();

            try
            {
                using (SqlConnection connection = new SqlConnection(StringConexion))
                {
                    connection.Open();

                    string query = "SELECT IDRol, NombreRol, Descripcion FROM Roles";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Rol rol = new Rol
                                {
                                    Id = reader.GetInt32(0),
                                    NombreRol = reader.GetString(1),
                                    Descripcion = reader.IsDBNull(2) ? null : reader.GetString(2)
                                };
                                roles.Add(rol);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener roles: " + ex.Message);
            }

            return roles;
        }

        // Agrega un nuevo rol
        public bool AgregarRol(Rol rol, int idAdmin)
        {
            using (SqlConnection connection = new SqlConnection(StringConexion))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    string query = "INSERT INTO Roles (NombreRol, Descripcion) VALUES (@nombreRol, @descripcion)";

                    using (SqlCommand command = new SqlCommand(query, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@nombreRol", rol.NombreRol);
                        command.Parameters.AddWithValue("@descripcion", (object)rol.Descripcion ?? DBNull.Value);

                        int filasAfectadas = command.ExecuteNonQuery();

                        if (filasAfectadas > 0)
                        {
                            string detalle = $"Rol agregado: {rol.NombreRol}";
                            string bitacora = "INSERT INTO Bitacora (IdUsuario, Detalles, Fecha) VALUES (@idUsuario, @detalles, GETDATE())";

                            using (SqlCommand cmdBitacora = new SqlCommand(bitacora, connection, transaction))
                            {
                                cmdBitacora.Parameters.AddWithValue("@idUsuario", idAdmin);
                                cmdBitacora.Parameters.AddWithValue("@detalles", detalle);
                                cmdBitacora.ExecuteNonQuery();
                            }
                            transaction.Commit();
                            return true;
                        }
                        else
                        {
                            transaction.Rollback();
                            return false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Error al agregar rol: " + ex.Message);
                }
            }
        }

        public bool ActualizarRol(Rol rol, int idAdmin)
        {
            using (SqlConnection connection = new SqlConnection(StringConexion))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    string query = "UPDATE Roles SET NombreRol = @nombreRol, Descripcion = @descripcion WHERE IDRol = @idRol";

                    using (SqlCommand command = new SqlCommand(query, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@nombreRol", rol.NombreRol);
                        command.Parameters.AddWithValue("@descripcion", (object)rol.Descripcion ?? DBNull.Value);
                        command.Parameters.AddWithValue("@idRol", rol.Id);

                        int filasAfectadas = command.ExecuteNonQuery();

                        if (filasAfectadas > 0)
                        {
                            string detalle = $"Rol actualizado: {rol.NombreRol}";
                            string bitacora = "INSERT INTO Bitacora (IdUsuario, Detalles, Fecha) VALUES (@idUsuario, @detalles, GETDATE())";

                            using (SqlCommand cmdBitacora = new SqlCommand(bitacora, connection, transaction))
                            {
                                cmdBitacora.Parameters.AddWithValue("@idUsuario", idAdmin);
                                cmdBitacora.Parameters.AddWithValue("@detalles", detalle);
                                cmdBitacora.ExecuteNonQuery();
                            }
                            transaction.Commit();
                            return true;
                        }
                        else
                        {
                            transaction.Rollback();
                            return false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Error al actualizar rol: " + ex.Message);
                }
            }
        }

        public bool EliminarRol(int idRol, int idAdmin)
        {
            using (SqlConnection connection = new SqlConnection(StringConexion))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    using (SqlCommand command = new SqlCommand("SP_EliminarRolSiNoAsignado", connection, transaction))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@IDRol", idRol);

                        command.ExecuteNonQuery();

                        string detalle = $"Rol eliminado: {idRol}";
                        string bitacora = "INSERT INTO Bitacora (IdUsuario, Detalles, Fecha) VALUES (@idUsuario, @detalles, GETDATE())";

                        using (SqlCommand cmdBitacora = new SqlCommand(bitacora, connection, transaction))
                        {
                            cmdBitacora.Parameters.AddWithValue("@idUsuario", idAdmin);
                            cmdBitacora.Parameters.AddWithValue("@detalles", detalle);
                            cmdBitacora.ExecuteNonQuery();
                        }
                        transaction.Commit();
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Error al eliminar rol: " + ex.Message);
                }
            }
        }

        public void AsignarPermisoARol(int idRol, int idPantalla, int idPermiso, int idAdmin)
        {
            using (SqlConnection connection = new SqlConnection(StringConexion))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    string query = @"
IF NOT EXISTS (
    SELECT 1 FROM Rol_Permiso_Pantalla
    WHERE IDRol = @IDRol AND IDPantalla = @IDPantalla AND IDPermiso = @IDPermiso
)
 INSERT INTO Rol_Permiso_Pantalla (IDRol, IDPantalla, IDPermiso)
 VALUES (@IDRol, @IDPantalla, @IDPermiso)";

                    using (SqlCommand command = new SqlCommand(query, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@IDRol", idRol);
                        command.Parameters.AddWithValue("@IDPantalla", idPantalla);
                        command.Parameters.AddWithValue("@IDPermiso", idPermiso);

                        command.ExecuteNonQuery();

                        string detalle = $"Asignado Permiso {idPermiso} en pantalla {idPantalla} al rol {idRol}";
                        string bitacora = "INSERT INTO Bitacora (IdUsuario, Detalles, Fecha) VALUES (@idUsuario, @detalles, GETDATE())";

                        using (SqlCommand cmdBitacora = new SqlCommand(bitacora, connection, transaction))
                        {
                            cmdBitacora.Parameters.AddWithValue("@idUsuario", idAdmin);
                            cmdBitacora.Parameters.AddWithValue("@detalles", detalle);
                            cmdBitacora.ExecuteNonQuery();
                        }
                        transaction.Commit();
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Error al asignar permiso al rol: " + ex.Message);
                }
            }
        }

        public void EliminarPermisoARol(int idRol, int idPantalla, int idPermiso, int idAdmin)
        {
            using (SqlConnection connection = new SqlConnection(StringConexion))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    string query = @"DELETE FROM Rol_Permiso_Pantalla
                             WHERE IDRol = @IDRol AND IDPantalla = @IDPantalla AND IDPermiso = @IDPermiso";

                    using (SqlCommand command = new SqlCommand(query, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@IDRol", idRol);
                        command.Parameters.AddWithValue("@IDPantalla", idPantalla);
                        command.Parameters.AddWithValue("@IDPermiso", idPermiso);

                        command.ExecuteNonQuery();

                        string detalle = $"Eliminado Permiso {idPermiso} en pantalla {idPantalla} del rol {idRol}";
                        string bitacora = "INSERT INTO Bitacora (IdUsuario, Detalles, Fecha) VALUES (@idUsuario, @detalles, GETDATE())";

                        using (SqlCommand cmdBitacora = new SqlCommand(bitacora, connection, transaction))
                        {
                            cmdBitacora.Parameters.AddWithValue("@idUsuario", idAdmin);
                            cmdBitacora.Parameters.AddWithValue("@detalles", detalle);
                            cmdBitacora.ExecuteNonQuery();
                        }
                        transaction.Commit();
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Error al eliminar permiso del rol: " + ex.Message);
                }
            }
        }


        public List<Rol> ObtenerRolesPorUsuario(int idUsuario)
        {
            List<Rol> roles = new List<Rol>();

            using (SqlConnection connection = new SqlConnection(StringConexion))
            {
                connection.Open();

                string query = @"SELECT R.IDRol, R.NombreRol
                         FROM Usuario_Rol UR
                         INNER JOIN Roles R ON UR.IDRol = R.IDRol
                         WHERE UR.IDUsuario = @IDUsuario";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IDUsuario", idUsuario);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            roles.Add(new Rol
                            {
                                Id = reader.GetInt32(0),
                                NombreRol = reader.GetString(1)
                            });
                        }
                    }
                }
            }

            return roles;
        }

        public void AsignarRolAUsuario(int idUsuario, int idRol, int idAdmin)
        {
            using (SqlConnection connection = new SqlConnection(StringConexion))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    string query = @"
                IF NOT EXISTS (
                    SELECT 1 FROM Usuario_Rol 
                    WHERE IDUsuario = @IDUsuario AND IDRol = @IDRol
                )
                BEGIN
                    INSERT INTO Usuario_Rol (IDUsuario, IDRol)
                    VALUES (@IDUsuario, @IDRol);
                END";

                    using (SqlCommand command = new SqlCommand(query, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@IDUsuario", idUsuario);
                        command.Parameters.AddWithValue("@IDRol", idRol);

                        command.ExecuteNonQuery();
                    }

                    string detalle = $"Se asignó el rol {idRol} al usuario {idUsuario}";
                    string bitacora = "INSERT INTO Bitacora (IdUsuario, Detalles, Fecha) VALUES (@idUsuario, @detalles, GETDATE())";

                    using (SqlCommand cmdBitacora = new SqlCommand(bitacora, connection, transaction))
                    {
                        cmdBitacora.Parameters.AddWithValue("@idUsuario", idAdmin);
                        cmdBitacora.Parameters.AddWithValue("@detalles", detalle);
                        cmdBitacora.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Error al asignar rol al usuario: " + ex.Message);
                }
            }
        }

        public void EliminarRolDeUsuario(int idUsuario, int idRol, int idAdmin)
        {
            using (SqlConnection connection = new SqlConnection(StringConexion))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    string query = @"DELETE FROM Usuario_Rol 
                             WHERE IDUsuario = @IDUsuario AND IDRol = @IDRol";

                    using (SqlCommand command = new SqlCommand(query, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@IDUsuario", idUsuario);
                        command.Parameters.AddWithValue("@IDRol", idRol);

                        command.ExecuteNonQuery();
                    }

                    string detalle = $"Se eliminó el rol {idRol} del usuario {idUsuario}";
                    string bitacora = "INSERT INTO Bitacora (IdUsuario, Detalles, Fecha) VALUES (@idUsuario, @detalles, GETDATE())";

                    using (SqlCommand cmdBitacora = new SqlCommand(bitacora, connection, transaction))
                    {
                        cmdBitacora.Parameters.AddWithValue("@idUsuario", idAdmin);
                        cmdBitacora.Parameters.AddWithValue("@detalles", detalle);
                        cmdBitacora.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Error al eliminar rol del usuario: " + ex.Message);
                }
            }
        }




        // FIN ROLES

        // INICIO PANTALLAS

        public bool PantallaExiste(int idSistema, string nombrePantalla)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(StringConexion))
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) FROM Pantallas WHERE IDSistema = @idSistema AND NombrePantalla = @nombrePantalla";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@idSistema", idSistema);
                        command.Parameters.AddWithValue("@nombrePantalla", nombrePantalla);

                        int count = (int)command.ExecuteScalar();
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al verificar existencia de la pantalla: " + ex.Message);
            }
        }

        public List<Pantalla> ObtenerPantallas()
        {
            List<Pantalla> pantallas = new List<Pantalla>();

            try
            {
                using (SqlConnection connection = new SqlConnection(StringConexion))
                {
                    connection.Open();

                    string query = @"
    SELECT p.IDPantalla, p.IDSistema, s.NombreSistema, p.NombrePantalla
    FROM Pantallas p
    INNER JOIN Sistemas s ON p.IDSistema = s.IdSistema
";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Pantalla pantalla = new Pantalla
                                {
                                    Id = reader.GetInt32(0),
                                    IdSistema = reader.GetInt32(1),
                                    NombreSistema = reader.GetString(2),
                                    NombrePantalla = reader.GetString(3)
                                };
                                pantallas.Add(pantalla);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener pantallas: " + ex.Message);
            }

            return pantallas;
        }


        public bool AgregarPantalla(Pantalla pantalla, int idAdmin)
        {
            using (SqlConnection connection = new SqlConnection(StringConexion))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    string query = "INSERT INTO Pantallas (IDSistema, NombrePantalla) VALUES (@idSistema, @nombrePantalla)";

                    using (SqlCommand command = new SqlCommand(query, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@idSistema", pantalla.IdSistema);
                        command.Parameters.AddWithValue("@nombrePantalla", pantalla.NombrePantalla);

                        int filasAfectadas = command.ExecuteNonQuery();

                        if (filasAfectadas > 0)
                        {
                            string detalle = $"Pantalla agregada: {pantalla.NombrePantalla}";
                            string bitacora = "INSERT INTO Bitacora (IdUsuario, Detalles, Fecha) VALUES (@idUsuario, @detalles, GETDATE())";

                            using (SqlCommand cmdBitacora = new SqlCommand(bitacora, connection, transaction))
                            {
                                cmdBitacora.Parameters.AddWithValue("@idUsuario", idAdmin);
                                cmdBitacora.Parameters.AddWithValue("@detalles", detalle);
                                cmdBitacora.ExecuteNonQuery();
                            }
                            transaction.Commit();
                            return true;
                        }
                        else
                        {
                            transaction.Rollback();
                            return false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Error al agregar pantalla: " + ex.Message);
                }
            }
        }

        public bool ActualizarPantalla(Pantalla pantalla, int idAdmin)
        {
            using (SqlConnection connection = new SqlConnection(StringConexion))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    string query = "UPDATE Pantallas SET NombrePantalla = @nombrePantalla WHERE IDPantalla = @idPantalla";

                    using (SqlCommand cmd = new SqlCommand(query, connection, transaction))
                    {
                        cmd.Parameters.AddWithValue("@nombrePantalla", pantalla.NombrePantalla);
                        cmd.Parameters.AddWithValue("@idPantalla", pantalla.Id);

                        int filasAfectadas = cmd.ExecuteNonQuery();

                        if (filasAfectadas > 0)
                        {
                            string detalle = $"Pantalla actualizada: {pantalla.NombrePantalla}";
                            string bitacora = "INSERT INTO Bitacora (IdUsuario, Detalles, Fecha) VALUES (@idUsuario, @detalles, GETDATE())";

                            using (SqlCommand cmdBitacora = new SqlCommand(bitacora, connection, transaction))
                            {
                                cmdBitacora.Parameters.AddWithValue("@idUsuario", idAdmin);
                                cmdBitacora.Parameters.AddWithValue("@detalles", detalle);
                                cmdBitacora.ExecuteNonQuery();
                            }
                            transaction.Commit();
                            return true;
                        }
                        else
                        {
                            transaction.Rollback();
                            return false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Error al actualizar pantalla: " + ex.Message);
                }
            }
        }

        public bool EliminarPantalla(int idPantalla, int idAdmin)
        {
            using (SqlConnection connection = new SqlConnection(StringConexion))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    string query = "DELETE FROM Pantallas WHERE IDPantalla = @idPantalla";

                    using (SqlCommand cmd = new SqlCommand(query, connection, transaction))
                    {
                        cmd.Parameters.AddWithValue("@idPantalla", idPantalla);

                        int filasAfectadas = cmd.ExecuteNonQuery();

                        if (filasAfectadas > 0)
                        {
                            string detalle = $"Pantalla eliminada: {idPantalla}";
                            string bitacora = "INSERT INTO Bitacora (IdUsuario, Detalles, Fecha) VALUES (@idUsuario, @detalles, GETDATE())";

                            using (SqlCommand cmdBitacora = new SqlCommand(bitacora, connection, transaction))
                            {
                                cmdBitacora.Parameters.AddWithValue("@idUsuario", idAdmin);
                                cmdBitacora.Parameters.AddWithValue("@detalles", detalle);
                                cmdBitacora.ExecuteNonQuery();
                            }
                            transaction.Commit();
                            return true;
                        }
                        else
                        {
                            transaction.Rollback();
                            return false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Error al eliminar pantalla: " + ex.Message);
                }
            }
        }


        // FIN PANTALLAS

        // INICIO PERMISOS

        public List<Permiso> ObtenerPermisos()
        {
            List<Permiso> permisos = new List<Permiso>();

            try
            {
                using (SqlConnection connection = new SqlConnection(StringConexion))
                {
                    connection.Open();

                    string query = "SELECT IDPermiso, NombrePermiso FROM Permisos";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Permiso permiso = new Permiso
                                {
                                    IDPermiso = reader.GetInt32(0),
                                    NombrePermiso = reader.GetString(1)
                                };
                                permisos.Add(permiso);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los permisos: " + ex.Message);
            }

            return permisos;
        }

        public int ObtenerIdPermisoPorNombre(string nombrePermiso)
        {
            int idPermiso = -1;

            try
            {
                using (SqlConnection connection = new SqlConnection(StringConexion))
                {
                    connection.Open();

                    string query = "SELECT IDPermiso FROM Permisos WHERE NombrePermiso = @nombrePermiso";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@nombrePermiso", nombrePermiso);

                        object result = command.ExecuteScalar();

                        if (result != null)
                        {
                            idPermiso = Convert.ToInt32(result);
                        }
                        else
                        {
                            throw new Exception($"No se encontró el permiso con nombre '{nombrePermiso}'.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el ID del permiso: " + ex.Message);
            }

            return idPermiso;
        }

        public List<int> ObtenerPermisosAsignados(int idRol, int idPantalla)
        {
            List<int> permisosAsignados = new List<int>();

            try
            {
                using (SqlConnection connection = new SqlConnection(StringConexion))
                {
                    connection.Open();

                    string query = @"SELECT IDPermiso 
                             FROM Rol_Permiso_Pantalla 
                             WHERE IDRol = @idRol AND IDPantalla = @idPantalla";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@idRol", idRol);
                        command.Parameters.AddWithValue("@idPantalla", idPantalla);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                permisosAsignados.Add(reader.GetInt32(0));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener permisos asignados: " + ex.Message);
            }

            return permisosAsignados;
        }

        public List<int> ObtenerPermisosAsignadosUsuario(int idUsuario, int idPantalla)
        {
            List<int> permisosAsignados = new List<int>();

            try
            {
                using (SqlConnection connection = new SqlConnection(StringConexion))
                {
                    connection.Open();

                    string query = @"SELECT IDPermiso 
                             FROM Usuario_Permiso_Pantalla 
                             WHERE IDUsuario = @idUsuario AND IDPantalla = @idPantalla";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@idUsuario", idUsuario);
                        command.Parameters.AddWithValue("@idPantalla", idPantalla);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                permisosAsignados.Add(reader.GetInt32(0));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener permisos de usuario: " + ex.Message);
            }

            return permisosAsignados;
        }

        public void AsignarPermisoAUsuario(int idUsuario, int idPantalla, int idPermiso, int idAdmin)
        {
            using (SqlConnection connection = new SqlConnection(StringConexion))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    string query = @"INSERT INTO Usuario_Permiso_Pantalla (IDUsuario, IDPantalla, IDPermiso) 
                             VALUES (@idUsuario, @idPantalla, @idPermiso)";

                    using (SqlCommand command = new SqlCommand(query, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@idUsuario", idUsuario);
                        command.Parameters.AddWithValue("@idPantalla", idPantalla);
                        command.Parameters.AddWithValue("@idPermiso", idPermiso);

                        command.ExecuteNonQuery();
                    }

                    string detalle = $"Se asignó permiso {idPermiso} en pantalla {idPantalla} al usuario {idUsuario}";
                    string bitacora = "INSERT INTO Bitacora (IdUsuario, Detalles, Fecha) VALUES (@idUsuarioBitacora, @detalles, GETDATE())";

                    using (SqlCommand cmdBitacora = new SqlCommand(bitacora, connection, transaction))
                    {
                        cmdBitacora.Parameters.AddWithValue("@idUsuarioBitacora", idAdmin);
                        cmdBitacora.Parameters.AddWithValue("@detalles", detalle);
                        cmdBitacora.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Error al asignar permiso al usuario: " + ex.Message);
                }
            }
        }

        public void EliminarPermisoAUsuario(int idUsuario, int idPantalla, int idPermiso, int idAdmin)
        {
            using (SqlConnection connection = new SqlConnection(StringConexion))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    string query = @"DELETE FROM Usuario_Permiso_Pantalla 
                             WHERE IDUsuario = @idUsuario AND IDPantalla = @idPantalla AND IDPermiso = @idPermiso";

                    using (SqlCommand command = new SqlCommand(query, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@idUsuario", idUsuario);
                        command.Parameters.AddWithValue("@idPantalla", idPantalla);
                        command.Parameters.AddWithValue("@idPermiso", idPermiso);

                        command.ExecuteNonQuery();
                    }

                    string detalle = $"Se eliminó permiso {idPermiso} en pantalla {idPantalla} del usuario {idUsuario}";
                    string bitacora = "INSERT INTO Bitacora (IdUsuario, Detalles, Fecha) VALUES (@idUsuarioBitacora, @detalles, GETDATE())";

                    using (SqlCommand cmdBitacora = new SqlCommand(bitacora, connection, transaction))
                    {
                        cmdBitacora.Parameters.AddWithValue("@idUsuarioBitacora", idAdmin);
                        cmdBitacora.Parameters.AddWithValue("@detalles", detalle);
                        cmdBitacora.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Error al eliminar permiso del usuario: " + ex.Message);
                }
            }
        }






        // FIN PERMISOS

    }
}