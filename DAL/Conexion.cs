using BAL;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Data;


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
                using (OracleConnection connection = new OracleConnection(StringConexion))
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) FROM Sistemas WHERE NombreSistema = :nombre";

                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        command.Parameters.Add(new OracleParameter("nombre", nombreSistema));

                        int count = Convert.ToInt32(command.ExecuteScalar());
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
                using (OracleConnection connection = new OracleConnection(StringConexion))
                {
                    connection.Open();

                    string query = "SELECT IDSistema, NombreSistema FROM Sistemas";

                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        using (OracleDataReader reader = command.ExecuteReader())
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


        public List<Sistema> ObtenerSistemasSistema()
        {
            List<Sistema> sistemas = new List<Sistema>();

            try
            {
                using (OracleConnection connection = new OracleConnection(StringConexion))
                {
                    connection.Open();

                    string query = "SELECT IDSistema, NombreSistema FROM Sistemas WHERE NombreSistema <> :nombreExcluido";

                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        command.Parameters.Add(new OracleParameter("nombreExcluido", "Seguridad"));

                        using (OracleDataReader reader = command.ExecuteReader())
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
                using (OracleConnection connection = new OracleConnection(StringConexion))
                {
                    connection.Open();

                    OracleTransaction transaction = connection.BeginTransaction();

                    try
                    {
                        string query = "INSERT INTO Sistemas (NombreSistema) VALUES (:nombre)";

                        using (OracleCommand command = new OracleCommand(query, connection))
                        {
                            command.Transaction = transaction;
                            command.Parameters.Add(new OracleParameter("nombre", sistema.NombreSistema));
                            int filasAfectadas = command.ExecuteNonQuery();

                            if (filasAfectadas > 0)
                            {
                                // Registrar en bitácora
                                string detalle = $"Se agregó el sistema: {sistema.NombreSistema}";
                                string bitacora = "INSERT INTO Bitacora (IdUsuario, Detalles, Fecha) VALUES (:idUsuario, :detalles, SYSDATE)";

                                using (OracleCommand cmd = new OracleCommand(bitacora, connection))
                                {
                                    cmd.Transaction = transaction;
                                    cmd.Parameters.Add(new OracleParameter("idUsuario", idUsuario));
                                    cmd.Parameters.Add(new OracleParameter("detalles", detalle));
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
                using (OracleConnection conn = new OracleConnection(StringConexion))
                {
                    conn.Open();

                    OracleTransaction transaction = conn.BeginTransaction();

                    try
                    {
                        string query = "UPDATE Sistemas SET NombreSistema = :nombre WHERE IDSistema = :id";

                        using (OracleCommand cmd = new OracleCommand(query, conn))
                        {
                            cmd.Transaction = transaction;
                            cmd.Parameters.Add(new OracleParameter("nombre", sistema.NombreSistema));
                            cmd.Parameters.Add(new OracleParameter("id", sistema.Id));
                            int filasAfectadas = cmd.ExecuteNonQuery();

                            if (filasAfectadas > 0)
                            {
                                // Registrar en bitácora
                                string detalle = $"Se actualizó el sistema {sistema.NombreSistema} (Id:{sistema.Id})";

                                string bitacora = "INSERT INTO Bitacora (IdUsuario, Detalles, Fecha) VALUES (:idUsuario, :detalles, SYSDATE)";

                                using (OracleCommand cmdBitacora = new OracleCommand(bitacora, conn))
                                {
                                    cmdBitacora.Transaction = transaction;
                                    cmdBitacora.Parameters.Add(new OracleParameter("idUsuario", idUsuario));
                                    cmdBitacora.Parameters.Add(new OracleParameter("detalles", detalle));
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
                using (OracleConnection connection = new OracleConnection(StringConexion))
                {
                    connection.Open();

                    using (OracleCommand cmd = new OracleCommand("SP_EliminarSistema", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("p_idSistema", OracleDbType.Int32).Value = idSistema;
                        cmd.Parameters.Add("p_idUsuario", OracleDbType.Int32).Value = idUsuario;
                        cmd.Parameters.Add("p_resultado", OracleDbType.Int32).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("p_mensaje", OracleDbType.Varchar2, 4000).Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();

                        int resultado = int.Parse(cmd.Parameters["p_resultado"].Value.ToString());
                        string mensaje = cmd.Parameters["p_mensaje"].Value.ToString();

                        if (resultado == 1)
                        {
                            return true;
                        }
                        else
                        {
                            throw new Exception(mensaje);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar sistema: " + ex.Message);
            }
        }








        public bool UsuarioExiste(string usuario)
        {
            try
            {
                using (OracleConnection connection = new OracleConnection(StringConexion))
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) FROM Usuarios WHERE Usuario = :usuario";

                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        command.Parameters.Add(new OracleParameter("usuario", usuario));

                        int count = Convert.ToInt32(command.ExecuteScalar());
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
                using (OracleConnection connection = new OracleConnection(StringConexion))
                {
                    connection.Open();

                    using (OracleCommand command = new OracleCommand("SP_ValidarCredenciales", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add(new OracleParameter("Usuario", OracleDbType.Varchar2, usuario, ParameterDirection.Input));
                        command.Parameters.Add(new OracleParameter("Contrasena", OracleDbType.Varchar2, contrasena, ParameterDirection.Input));

                        OracleParameter resultadoParam = new OracleParameter("Resultado", OracleDbType.Int32);
                        resultadoParam.Direction = ParameterDirection.Output;
                        command.Parameters.Add(resultadoParam);

                        command.ExecuteNonQuery();

                        int resultado = Convert.ToInt32(resultadoParam.Value.ToString());

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
                using (OracleConnection connection = new OracleConnection(StringConexion))
                {
                    connection.Open();

                    string query = "INSERT INTO Bitacora (IdUsuario, Detalles, Fecha) VALUES (:IdUsuario, :Detalles, SYSDATE)";

                    using (OracleCommand cmd = new OracleCommand(query, connection))
                    {
                        cmd.Parameters.Add(new OracleParameter("IdUsuario", idUsuario));
                        cmd.Parameters.Add(new OracleParameter("Detalles", detalle));

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
                using (OracleConnection connection = new OracleConnection(StringConexion))
                {
                    connection.Open();

                    string query = "SELECT IdUsuario FROM Usuarios WHERE Usuario = :usuario";

                    using (OracleCommand cmd = new OracleCommand(query, connection))
                    {
                        cmd.Parameters.Add("usuario", OracleDbType.Varchar2).Value = nombreUsuario;

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
                using (OracleConnection connection = new OracleConnection(StringConexion))
                {
                    connection.Open();

                    string query = "SELECT IdUsuario, Usuario, Contrasena, Activo FROM Usuarios WHERE ROWNUM <= 1000";

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

        public bool AgregarUsuario(User usuario, int idAdmin)
        {
            try
            {
                using (OracleConnection connection = new OracleConnection(StringConexion))
                {
                    connection.Open();
                    OracleTransaction transaction = connection.BeginTransaction();

                    try
                    {
                        string query = "INSERT INTO Usuarios (Usuario, Contrasena, Activo) VALUES (:usuario, :contrasena, :activo)";

                        using (OracleCommand command = new OracleCommand(query, connection))
                        {
                            command.Transaction = transaction;

                            command.Parameters.Add("usuario", OracleDbType.Varchar2).Value = usuario.Usuario;
                            command.Parameters.Add("contrasena", OracleDbType.Varchar2).Value = usuario.Contrasena;
                            command.Parameters.Add("activo", OracleDbType.Int32).Value = usuario.Activo;

                            int filasAfectadas = command.ExecuteNonQuery();

                            if (filasAfectadas > 0)
                            {
                                string detalle = $"Se agregó el usuario: {usuario.Usuario}";
                                string bitacora = "INSERT INTO Bitacora (IdUsuario, Detalles, Fecha) VALUES (:idUsuario, :detalles, SYSDATE)";

                                using (OracleCommand cmdBitacora = new OracleCommand(bitacora, connection))
                                {
                                    cmdBitacora.Transaction = transaction;

                                    cmdBitacora.Parameters.Add("idUsuario", OracleDbType.Int32).Value = idAdmin;
                                    cmdBitacora.Parameters.Add("detalles", OracleDbType.Varchar2).Value = detalle;

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
                using (OracleConnection conn = new OracleConnection(StringConexion))
                {
                    conn.Open();
                    OracleTransaction transaction = conn.BeginTransaction();

                    try
                    {
                        string query = "UPDATE Usuarios SET Usuario = :usuario, Contrasena = :contrasena, Activo = :activo WHERE IDUsuario = :id";

                        using (OracleCommand cmd = new OracleCommand(query, conn))
                        {
                            cmd.Transaction = transaction;

                            cmd.Parameters.Add("usuario", OracleDbType.Varchar2).Value = user.Usuario;
                            cmd.Parameters.Add("contrasena", OracleDbType.Varchar2).Value = user.Contrasena;
                            cmd.Parameters.Add("activo", OracleDbType.Int32).Value = user.Activo;
                            cmd.Parameters.Add("id", OracleDbType.Int32).Value = user.Id;

                            int filasAfectadas = cmd.ExecuteNonQuery();

                            if (filasAfectadas > 0)
                            {
                                string detalle = $"Se actualizó el usuario: {user.Usuario} (Id:{user.Id})";
                                string bitacora = "INSERT INTO Bitacora (IdUsuario, Detalles, Fecha) VALUES (:idUsuario, :detalles, SYSDATE)";

                                using (OracleCommand cmdBitacora = new OracleCommand(bitacora, conn))
                                {
                                    cmdBitacora.Transaction = transaction;

                                    cmdBitacora.Parameters.Add("idUsuario", OracleDbType.Int32).Value = idAdmin;
                                    cmdBitacora.Parameters.Add("detalles", OracleDbType.Varchar2).Value = detalle;

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

        public bool EliminarUsuario(int idUsuarioEliminar, int idAdmin, out string mensaje)
        {
            mensaje = "";

            using (OracleConnection connection = new OracleConnection(StringConexion))
            {
                connection.Open();

                using (OracleCommand cmd = new OracleCommand("SP_EliminarUsuario", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Parámetros de entrada
                    cmd.Parameters.Add("p_idUsuario", OracleDbType.Int32).Value = idUsuarioEliminar;
                    cmd.Parameters.Add("p_idAdmin", OracleDbType.Int32).Value = idAdmin;

                    // Parámetros de salida
                    OracleParameter p_resultado = new OracleParameter("p_resultado", OracleDbType.Int32)
                    {
                        Direction = ParameterDirection.Output
                    };
                    OracleParameter p_mensaje = new OracleParameter("p_mensaje", OracleDbType.Varchar2, 500)
                    {
                        Direction = ParameterDirection.Output
                    };

                    cmd.Parameters.Add(p_resultado);
                    cmd.Parameters.Add(p_mensaje);

                    cmd.ExecuteNonQuery();

                    int resultado = Convert.ToInt32(p_resultado.Value.ToString());
                    mensaje = p_mensaje.Value.ToString();

                    return resultado == 1;
                }
            }
        }


        // FIN USUARIOS

        // INICIO ROLES

        // Verifica si un rol ya existe por nombre
        public bool RolExiste(string nombreRol)
        {
            try
            {
                using (OracleConnection connection = new OracleConnection(StringConexion))
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) FROM Roles WHERE NombreRol = :nombreRol";

                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        command.Parameters.Add("nombreRol", OracleDbType.Varchar2).Value = nombreRol;

                        int count = Convert.ToInt32(command.ExecuteScalar());
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
                using (OracleConnection connection = new OracleConnection(StringConexion))
                {
                    connection.Open();

                    string query = "SELECT IDRol, NombreRol, Descripcion FROM Roles";

                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        using (OracleDataReader reader = command.ExecuteReader())
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


        public bool AgregarRol(Rol rol, int idAdmin)
        {
            using (OracleConnection connection = new OracleConnection(StringConexion))
            {
                connection.Open();
                OracleTransaction transaction = connection.BeginTransaction();

                try
                {
                    string query = "INSERT INTO Roles (NombreRol, Descripcion) VALUES (:nombreRol, :descripcion)";

                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        command.Transaction = transaction;
                        command.Parameters.Add(":nombreRol", rol.NombreRol);
                        command.Parameters.Add(":descripcion", rol.Descripcion ?? (object)DBNull.Value);

                        int filasAfectadas = command.ExecuteNonQuery();

                        if (filasAfectadas > 0)
                        {
                            string detalle = $"Rol agregado: {rol.NombreRol}";
                            string bitacora = "INSERT INTO Bitacora (IdUsuario, Detalles, Fecha) VALUES (:idUsuario, :detalles, SYSDATE)";

                            using (OracleCommand cmdBitacora = new OracleCommand(bitacora, connection))
                            {
                                cmdBitacora.Transaction = transaction;
                                cmdBitacora.Parameters.Add(":idUsuario", idAdmin);
                                cmdBitacora.Parameters.Add(":detalles", detalle);
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
            using (OracleConnection connection = new OracleConnection(StringConexion))
            {
                connection.Open();
                OracleTransaction transaction = connection.BeginTransaction();

                try
                {
                    string query = "UPDATE Roles SET NombreRol = :nombreRol, Descripcion = :descripcion WHERE IDRol = :idRol";

                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        command.Transaction = transaction;
                        command.Parameters.Add(":nombreRol", rol.NombreRol);
                        command.Parameters.Add(":descripcion", rol.Descripcion ?? (object)DBNull.Value);
                        command.Parameters.Add(":idRol", rol.Id);

                        int filasAfectadas = command.ExecuteNonQuery();

                        if (filasAfectadas > 0)
                        {
                            string detalle = $"Rol actualizado: {rol.NombreRol}";
                            string bitacora = "INSERT INTO Bitacora (IdUsuario, Detalles, Fecha) VALUES (:idUsuario, :detalles, SYSDATE)";

                            using (OracleCommand cmdBitacora = new OracleCommand(bitacora, connection))
                            {
                                cmdBitacora.Transaction = transaction;
                                cmdBitacora.Parameters.Add(":idUsuario", idAdmin);
                                cmdBitacora.Parameters.Add(":detalles", detalle);
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





        public bool EliminarRol(int idRol, int idAdmin, out string mensaje)
        {
            mensaje = "";

            try
            {
                using (OracleConnection connection = new OracleConnection(StringConexion))
                {
                    connection.Open();

                    using (OracleCommand command = new OracleCommand("SP_EliminarRol", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("p_idRol", OracleDbType.Int32).Value = idRol;
                        command.Parameters.Add("p_idUsuario", OracleDbType.Int32).Value = idAdmin;

                        OracleParameter p_resultado = new OracleParameter("p_resultado", OracleDbType.Int32)
                        {
                            Direction = ParameterDirection.Output
                        };

                        OracleParameter p_mensaje = new OracleParameter("p_mensaje", OracleDbType.Varchar2, 500)
                        {
                            Direction = ParameterDirection.Output
                        };

                        command.Parameters.Add(p_resultado);
                        command.Parameters.Add(p_mensaje);

                        command.ExecuteNonQuery();

                        int resultado = ((OracleDecimal)p_resultado.Value).ToInt32();
                        mensaje = p_mensaje.Value.ToString();

                        return resultado == 1;
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = "Error al eliminar rol: " + ex.Message;
                return false; // <- IMPORTANTE: esto soluciona el error de compilación
            }
        }




        public void AsignarPermisoARol(int idRol, int idPantalla, int idPermiso, int idAdmin)
        {
            using (OracleConnection connection = new OracleConnection(StringConexion))
            {
                connection.Open();
                OracleTransaction transaction = connection.BeginTransaction();

                try
                {
                    string query = @"
DECLARE
    v_count NUMBER;
BEGIN
    SELECT COUNT(*) INTO v_count 
    FROM Rol_Permiso_Pantalla 
    WHERE IDRol = :IDRol AND IDPantalla = :IDPantalla AND IDPermiso = :IDPermiso;

    IF v_count = 0 THEN
        INSERT INTO Rol_Permiso_Pantalla (IDRol, IDPantalla, IDPermiso)
        VALUES (:IDRol, :IDPantalla, :IDPermiso);
    END IF;
END;";

                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        command.Transaction = transaction;
                        command.Parameters.Add(new OracleParameter("IDRol", idRol));
                        command.Parameters.Add(new OracleParameter("IDPantalla", idPantalla));
                        command.Parameters.Add(new OracleParameter("IDPermiso", idPermiso));
                        command.ExecuteNonQuery();
                    }

                    string detalle = $"Asignado Permiso {idPermiso} en pantalla {idPantalla} al rol {idRol}";
                    string bitacora = "INSERT INTO Bitacora (IdUsuario, Detalles, Fecha) VALUES (:idUsuario, :detalles, SYSDATE)";

                    using (OracleCommand cmdBitacora = new OracleCommand(bitacora, connection))
                    {
                        cmdBitacora.Transaction = transaction;
                        cmdBitacora.Parameters.Add(new OracleParameter("idUsuario", idAdmin));
                        cmdBitacora.Parameters.Add(new OracleParameter("detalles", detalle));
                        cmdBitacora.ExecuteNonQuery();
                    }

                    transaction.Commit();
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
            using (OracleConnection connection = new OracleConnection(StringConexion))
            {
                connection.Open();
                OracleTransaction transaction = connection.BeginTransaction();

                try
                {
                    string query = @"DELETE FROM Rol_Permiso_Pantalla
                             WHERE IDRol = :IDRol AND IDPantalla = :IDPantalla AND IDPermiso = :IDPermiso";

                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        command.Transaction = transaction;
                        command.Parameters.Add(new OracleParameter("IDRol", idRol));
                        command.Parameters.Add(new OracleParameter("IDPantalla", idPantalla));
                        command.Parameters.Add(new OracleParameter("IDPermiso", idPermiso));

                        command.ExecuteNonQuery();
                    }

                    string detalle = $"Eliminado Permiso {idPermiso} en pantalla {idPantalla} del rol {idRol}";
                    string bitacora = "INSERT INTO Bitacora (IdUsuario, Detalles, Fecha) VALUES (:idUsuario, :detalles, SYSDATE)";

                    using (OracleCommand cmdBitacora = new OracleCommand(bitacora, connection))
                    {
                        cmdBitacora.Transaction = transaction;
                        cmdBitacora.Parameters.Add(new OracleParameter("idUsuario", idAdmin));
                        cmdBitacora.Parameters.Add(new OracleParameter("detalles", detalle));
                        cmdBitacora.ExecuteNonQuery();
                    }

                    transaction.Commit();
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

            using (OracleConnection connection = new OracleConnection(StringConexion))
            {
                connection.Open();

                string query = @"SELECT R.IDRol, R.NombreRol
                         FROM Usuario_Rol UR
                         INNER JOIN Roles R ON UR.IDRol = R.IDRol
                         WHERE UR.IDUsuario = :IDUsuario";

                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("IDUsuario", idUsuario));

                    using (OracleDataReader reader = command.ExecuteReader())
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
            using (OracleConnection connection = new OracleConnection(StringConexion))
            {
                connection.Open();
                OracleTransaction transaction = connection.BeginTransaction();

                try
                {
                    string query = @"
DECLARE
    v_count NUMBER;
BEGIN
    SELECT COUNT(*) INTO v_count 
    FROM Usuario_Rol 
    WHERE IDUsuario = :IDUsuario AND IDRol = :IDRol;

    IF v_count = 0 THEN
        INSERT INTO Usuario_Rol (IDUsuario, IDRol)
        VALUES (:IDUsuario, :IDRol);
    END IF;
END;";

                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        command.Transaction = transaction;
                        command.Parameters.Add(new OracleParameter("IDUsuario", idUsuario));
                        command.Parameters.Add(new OracleParameter("IDRol", idRol));
                        command.ExecuteNonQuery();
                    }

                    string detalle = $"Se asignó el rol {idRol} al usuario {idUsuario}";
                    string bitacora = "INSERT INTO Bitacora (IdUsuario, Detalles, Fecha) VALUES (:idUsuario, :detalles, SYSDATE)";

                    using (OracleCommand cmdBitacora = new OracleCommand(bitacora, connection))
                    {
                        cmdBitacora.Transaction = transaction;
                        cmdBitacora.Parameters.Add(new OracleParameter("idUsuario", idAdmin));
                        cmdBitacora.Parameters.Add(new OracleParameter("detalles", detalle));
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
            using (OracleConnection connection = new OracleConnection(StringConexion))
            {
                connection.Open();
                OracleTransaction transaction = connection.BeginTransaction();

                try
                {
                    string query = @"DELETE FROM Usuario_Rol 
                             WHERE IDUsuario = :IDUsuario AND IDRol = :IDRol";

                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        command.Transaction = transaction;
                        command.Parameters.Add(new OracleParameter("IDUsuario", idUsuario));
                        command.Parameters.Add(new OracleParameter("IDRol", idRol));

                        command.ExecuteNonQuery();
                    }

                    string detalle = $"Se eliminó el rol {idRol} del usuario {idUsuario}";
                    string bitacora = "INSERT INTO Bitacora (IdUsuario, Detalles, Fecha) VALUES (:idUsuario, :detalles, SYSDATE)";

                    using (OracleCommand cmdBitacora = new OracleCommand(bitacora, connection))
                    {
                        cmdBitacora.Transaction = transaction;
                        cmdBitacora.Parameters.Add(new OracleParameter("idUsuario", idAdmin));
                        cmdBitacora.Parameters.Add(new OracleParameter("detalles", detalle));
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






        // ----------- PANTALLAS -----------

        public bool PantallaExiste(int idSistema, string nombrePantalla)
        {
            using (OracleConnection connection = new OracleConnection(StringConexion))
            {
                connection.Open();
                try
                {
                    using (OracleCommand command = new OracleCommand(
                        "SELECT COUNT(*) FROM Pantallas WHERE IDSistema = :idSistema AND NombrePantalla = :nombrePantalla", connection))
                    {
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add("idSistema", OracleDbType.Int32).Value = idSistema;
                        command.Parameters.Add("nombrePantalla", OracleDbType.Varchar2).Value = nombrePantalla;

                        int count = Convert.ToInt32(command.ExecuteScalar());
                        return count > 0;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al verificar existencia de la pantalla: " + ex.Message);
                }
            }
        }

        public List<Pantalla> ObtenerPantallas()
        {
            List<Pantalla> pantallas = new List<Pantalla>();

            using (OracleConnection connection = new OracleConnection(StringConexion))
            {
                connection.Open();
                try
                {
                    string query = @"
                SELECT p.IDPantalla, p.IDSistema, s.NombreSistema, p.NombrePantalla
                FROM Pantallas p
                INNER JOIN Sistemas s ON p.IDSistema = s.IDSistema";

                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        command.CommandType = CommandType.Text;

                        using (OracleDataReader reader = command.ExecuteReader())
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
                catch (Exception ex)
                {
                    throw new Exception("Error al obtener pantallas: " + ex.Message);
                }
            }

            return pantallas;
        }

  public List<Pantalla> ObtenerPantallasPorSistema(int idSistema)
{
    List<Pantalla> pantallas = new List<Pantalla>();

    using (OracleConnection connection = new OracleConnection(StringConexion))
    {
        connection.Open();
        try
        {
            string query = @"
                SELECT p.IDPantalla, p.IDSistema, s.NombreSistema, p.NombrePantalla
                FROM Pantallas p
                INNER JOIN Sistemas s ON p.IDSistema = s.IDSistema
                WHERE p.IDSistema = :idSistema";

            using (OracleCommand command = new OracleCommand(query, connection))
            {
                command.CommandType = CommandType.Text;
                command.Parameters.Add(new OracleParameter("idSistema", idSistema));

                using (OracleDataReader reader = command.ExecuteReader())
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
        catch (Exception ex)
        {
            throw new Exception("Error al obtener pantallas por sistema: " + ex.Message);
        }
    }

    return pantallas;
}






        public List<Pantalla> ObtenerPantallasPantalla()
        {
            List<Pantalla> pantallas = new List<Pantalla>();

            using (OracleConnection connection = new OracleConnection(StringConexion))
            {
                connection.Open();
                try
                {
                    string query = @"
                SELECT p.IDPantalla, p.IDSistema, s.NombreSistema, p.NombrePantalla
                FROM Pantallas p
                INNER JOIN Sistemas s ON p.IDSistema = s.IDSistema
                WHERE NOT (s.NombreSistema = :nombreSistema AND p.NombrePantalla = :nombrePantalla)";

                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new OracleParameter("nombreSistema", "Seguridad"));
                        command.Parameters.Add(new OracleParameter("nombrePantalla", "MenuSeguridad"));

                        using (OracleDataReader reader = command.ExecuteReader())
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
                catch (Exception ex)
                {
                    throw new Exception("Error al obtener pantallas: " + ex.Message);
                }
            }

            return pantallas;
        }




        public bool AgregarPantalla(Pantalla pantalla, int idAdmin)
        {
            using (OracleConnection connection = new OracleConnection(StringConexion))
            {
                connection.Open();
                OracleTransaction transaction = connection.BeginTransaction();

                try
                {
                    using (OracleCommand command = new OracleCommand(
                        "INSERT INTO Pantallas (IDSistema, NombrePantalla) VALUES (:idSistema, :nombrePantalla)", connection))
                    {
                        command.Transaction = transaction;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add("idSistema", OracleDbType.Int32).Value = pantalla.IdSistema;
                        command.Parameters.Add("nombrePantalla", OracleDbType.Varchar2).Value = pantalla.NombrePantalla;

                        int filasAfectadas = command.ExecuteNonQuery();

                        if (filasAfectadas > 0)
                        {
                            string detalle = $"Pantalla agregada: {pantalla.NombrePantalla}";

                            using (OracleCommand cmdBitacora = new OracleCommand(
                                "INSERT INTO Bitacora (IdUsuario, Detalles, Fecha) VALUES (:idUsuario, :detalles, SYSDATE)", connection))
                            {
                                cmdBitacora.Transaction = transaction;
                                cmdBitacora.CommandType = CommandType.Text;
                                cmdBitacora.Parameters.Add("idUsuario", OracleDbType.Int32).Value = idAdmin;
                                cmdBitacora.Parameters.Add("detalles", OracleDbType.Varchar2).Value = detalle;
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
            using (OracleConnection connection = new OracleConnection(StringConexion))
            {
                connection.Open();
                OracleTransaction transaction = connection.BeginTransaction();

                try
                {
                    using (OracleCommand cmd = new OracleCommand(
                        "UPDATE Pantallas SET NombrePantalla = :nombrePantalla WHERE IDPantalla = :idPantalla", connection))
                    {
                        cmd.Transaction = transaction;
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add("nombrePantalla", OracleDbType.Varchar2).Value = pantalla.NombrePantalla;
                        cmd.Parameters.Add("idPantalla", OracleDbType.Int32).Value = pantalla.Id;

                        int filasAfectadas = cmd.ExecuteNonQuery();

                        if (filasAfectadas > 0)
                        {
                            string detalle = $"Pantalla actualizada: {pantalla.NombrePantalla}";

                            using (OracleCommand cmdBitacora = new OracleCommand(
                                "INSERT INTO Bitacora (IdUsuario, Detalles, Fecha) VALUES (:idUsuario, :detalles, SYSDATE)", connection))
                            {
                                cmdBitacora.Transaction = transaction;
                                cmdBitacora.CommandType = CommandType.Text;
                                cmdBitacora.Parameters.Add("idUsuario", OracleDbType.Int32).Value = idAdmin;
                                cmdBitacora.Parameters.Add("detalles", OracleDbType.Varchar2).Value = detalle;
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

        public bool EliminarPantalla(int idPantalla, int idAdmin, out string mensaje)
        {
            mensaje = string.Empty;
            bool resultado = false;

            using (OracleConnection connection = new OracleConnection(StringConexion))
            {
                connection.Open();

                using (OracleCommand cmd = new OracleCommand("SP_EliminarPantalla", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Parámetros de entrada
                    cmd.Parameters.Add("p_idPantalla", OracleDbType.Int32).Value = idPantalla;
                    cmd.Parameters.Add("p_idUsuario", OracleDbType.Int32).Value = idAdmin;

                    // Parámetros de salida
                    var p_resultado = new OracleParameter("p_resultado", OracleDbType.Int32);
                    p_resultado.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(p_resultado);

                    var p_mensaje = new OracleParameter("p_mensaje", OracleDbType.Varchar2, 500);
                    p_mensaje.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(p_mensaje);

                    cmd.ExecuteNonQuery();

                    // Leer resultados
                    int valorResultado = Convert.ToInt32(p_resultado.Value.ToString());
                    mensaje = p_mensaje.Value.ToString();

                    resultado = (valorResultado == 1);
                }
            }

            return resultado;
        }



        // ----------- PERMISOS -----------

        public List<Permiso> ObtenerPermisos()
        {
            List<Permiso> permisos = new List<Permiso>();

            using (OracleConnection connection = new OracleConnection(StringConexion))
            {
                connection.Open();
                try
                {
                    string query = "SELECT IDPermiso, NombrePermiso FROM Permisos";

                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        command.CommandType = CommandType.Text;

                        using (OracleDataReader reader = command.ExecuteReader())
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
                catch (Exception ex)
                {
                    throw new Exception("Error al obtener los permisos: " + ex.Message);
                }
            }

            return permisos;
        }


        public int ObtenerIdPermisoPorNombre(string nombrePermiso)
        {
            int idPermiso = -1;

            try
            {
                using (OracleConnection connection = new OracleConnection(StringConexion))
                {
                    connection.Open();

                    string query = "SELECT IDPermiso FROM Permisos WHERE NombrePermiso = :nombrePermiso";

                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        command.Parameters.Add(new OracleParameter("nombrePermiso", nombrePermiso));

                        object result = command.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
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

            using (OracleConnection connection = new OracleConnection(StringConexion))
            {
                connection.Open();

                try
                {
                    string query = @"SELECT IDPermiso 
                             FROM Rol_Permiso_Pantalla 
                             WHERE IDRol = :idRol AND IDPantalla = :idPantalla";

                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add("idRol", OracleDbType.Int32).Value = idRol;
                        command.Parameters.Add("idPantalla", OracleDbType.Int32).Value = idPantalla;

                        using (OracleDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                permisosAsignados.Add(reader.GetInt32(0));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al obtener permisos asignados: " + ex.Message);
                }
            }

            return permisosAsignados;
        }

        public List<int> ObtenerPermisosAsignadosUsuario(int idUsuario, int idPantalla)
        {
            List<int> permisosAsignados = new List<int>();

            using (OracleConnection connection = new OracleConnection(StringConexion))
            {
                connection.Open();

                try
                {
                    string query = @"SELECT IDPermiso 
                             FROM Usuario_Permiso_Pantalla 
                             WHERE IDUsuario = :idUsuario AND IDPantalla = :idPantalla";

                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add("idUsuario", OracleDbType.Int32).Value = idUsuario;
                        command.Parameters.Add("idPantalla", OracleDbType.Int32).Value = idPantalla;

                        using (OracleDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                permisosAsignados.Add(reader.GetInt32(0));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al obtener permisos de usuario: " + ex.Message);
                }
            }

            return permisosAsignados;
        }

        public bool AsignarPermisoAUsuario(int idUsuario, int idPantalla, int idPermiso, int idAdmin)
        {
            using (OracleConnection connection = new OracleConnection(StringConexion))
            {
                connection.Open();
                OracleTransaction transaction = connection.BeginTransaction();

                try
                {
                    string query = @"INSERT INTO Usuario_Permiso_Pantalla (IDUsuario, IDPantalla, IDPermiso) 
                             VALUES (:idUsuario, :idPantalla, :idPermiso)";

                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        command.Transaction = transaction;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add("idUsuario", OracleDbType.Int32).Value = idUsuario;
                        command.Parameters.Add("idPantalla", OracleDbType.Int32).Value = idPantalla;
                        command.Parameters.Add("idPermiso", OracleDbType.Int32).Value = idPermiso;

                        command.ExecuteNonQuery();
                    }

                    string detalle = $"Se asignó permiso {idPermiso} en pantalla {idPantalla} al usuario {idUsuario}";

                    using (OracleCommand cmdBitacora = new OracleCommand(
                        "INSERT INTO Bitacora (IdUsuario, Detalles, Fecha) VALUES (:idUsuarioBitacora, :detalles, SYSDATE)", connection))
                    {
                        cmdBitacora.Transaction = transaction;
                        cmdBitacora.CommandType = CommandType.Text;
                        cmdBitacora.Parameters.Add("idUsuarioBitacora", OracleDbType.Int32).Value = idAdmin;
                        cmdBitacora.Parameters.Add("detalles", OracleDbType.Varchar2).Value = detalle;
                        cmdBitacora.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Error al asignar permiso al usuario: " + ex.Message);
                }
            }
        }

        public bool EliminarPermisoAUsuario(int idUsuario, int idPantalla, int idPermiso, int idAdmin)
        {
            using (OracleConnection connection = new OracleConnection(StringConexion))
            {
                connection.Open();
                OracleTransaction transaction = connection.BeginTransaction();

                try
                {
                    string query = @"DELETE FROM Usuario_Permiso_Pantalla 
                             WHERE IDUsuario = :idUsuario AND IDPantalla = :idPantalla AND IDPermiso = :idPermiso";

                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        command.Transaction = transaction;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add("idUsuario", OracleDbType.Int32).Value = idUsuario;
                        command.Parameters.Add("idPantalla", OracleDbType.Int32).Value = idPantalla;
                        command.Parameters.Add("idPermiso", OracleDbType.Int32).Value = idPermiso;

                        command.ExecuteNonQuery();
                    }

                    string detalle = $"Se eliminó permiso {idPermiso} en pantalla {idPantalla} del usuario {idUsuario}";

                    using (OracleCommand cmdBitacora = new OracleCommand(
                        "INSERT INTO Bitacora (IdUsuario, Detalles, Fecha) VALUES (:idUsuarioBitacora, :detalles, SYSDATE)", connection))
                    {
                        cmdBitacora.Transaction = transaction;
                        cmdBitacora.CommandType = CommandType.Text;
                        cmdBitacora.Parameters.Add("idUsuarioBitacora", OracleDbType.Int32).Value = idAdmin;
                        cmdBitacora.Parameters.Add("detalles", OracleDbType.Varchar2).Value = detalle;
                        cmdBitacora.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Error al eliminar permiso del usuario: " + ex.Message);
                }
            }
        }


    }
}



