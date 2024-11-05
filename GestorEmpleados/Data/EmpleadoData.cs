
using GestorEmpleados.API.Models;
using Microsoft.EntityFrameworkCore;
using MiWebAPI.Models;
using System.Data;
using System.Data.SqlClient;

namespace MiWebAPI.Data
{
    public class EmpleadoData
    {

        private readonly string conexion;
        public EmpleadoData(IConfiguration configuration)
        {
            conexion = configuration.GetConnectionString("CadenaSQL")!;
        }

       
        public async Task<List<Empleado>> GetEmpleados(string filtro)
        {
            List<Empleado> lista = new List<Empleado>();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_compra_selecciona", con);
                cmd.Parameters.AddWithValue("@filtro", filtro);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        lista.Add(new Empleado
                        {
                           
                            Id = Convert.ToInt32(reader["Id"]),
                            Producto = reader["Producto"].ToString(),
                            Canitdad = Convert.ToInt32(reader["Cantidad"]),
                            Precio = Convert.ToDecimal(reader["Precio"]),
                            Total = Convert.ToDecimal(reader["Total"]),
                            Subtotal = Convert.ToDecimal(reader["Subtotal"]),
                            Cliente = reader["Cliente"].ToString()
                        });
                    }
                }
            }
            return lista;
        }


        /// <summary>
        /// Agrega un empleado
        /// </summary>
        /// <param name="objeto"></param>
        /// <returns></returns>
        public async Task<RespuestaDB> AddEmpleado(Empleado objeto)
        {
            var resultado = new RespuestaDB();

            using (var con = new SqlConnection(conexion))
            {

                SqlCommand cmd = new SqlCommand("sp_compra_agrega", con);
                cmd.Parameters.AddWithValue("@Producto", objeto.Producto);
                cmd.Parameters.AddWithValue("@Cantidad", objeto.Canitdad);
                cmd.Parameters.AddWithValue("@Precio", objeto.Precio);
                cmd.Parameters.AddWithValue("@Total", objeto.Total);
                cmd.Parameters.AddWithValue("@Cliente", objeto.Cliente);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {

                        resultado.TipoError = Convert.ToInt32(reader["TipoError"]);
                        resultado.Mensaje = reader["Mensaje"].ToString();
                    }
                }

            }
            return resultado;
        }

        /// <summary>
        /// Actualiza un empleado
        /// </summary>
        /// <param name="objeto"></param>
        /// <returns></returns>
        public async Task<RespuestaDB> UpdateEmpleado(Empleado objeto)
        {
            var resultado = new RespuestaDB();

            using (var con = new SqlConnection(conexion))
            {

                SqlCommand cmd = new SqlCommand("sp_compra_actualizar", con);
                cmd.Parameters.AddWithValue("@id", objeto.Id);
                cmd.Parameters.AddWithValue("@Producto", objeto.Producto);
                cmd.Parameters.AddWithValue("@Cantidad", objeto.Canitdad);
                cmd.Parameters.AddWithValue("@Precio", objeto.Precio);
                cmd.Parameters.AddWithValue("@Total", objeto.Total);
                cmd.Parameters.AddWithValue("@Cliente", objeto.Cliente);    
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {

                        resultado.TipoError = Convert.ToInt32(reader["TipoError"]);
                        resultado.Mensaje = reader["Mensaje"].ToString();
                    }
                }

            }
            return resultado;
        }

        /// <summary>
        /// Elimina un empleado
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<RespuestaDB> DeleteEmpleado(int id)
        {
            var resultado = new RespuestaDB();
            using (var con = new SqlConnection(conexion))
            {

                SqlCommand cmd = new SqlCommand("sp_compra_eliminar", con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
              
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {

                        resultado.TipoError = Convert.ToInt32(reader["TipoError"]);
                        resultado.Mensaje = reader["Mensaje"].ToString();
                    }
                }

            }
            return resultado;
        }
    }
}
