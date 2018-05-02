using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;


namespace GeoreferenciasAD
{
    public class AD_Recursos
    {

        //public static SqlConnection ObtenerConexion()
        //{      
        //    SqlConnection Conn = new SqlConnection("Data Source=179.43.118.18\\sql2012;Initial Catalog=GEOREFERENCIA;Persist Security Info=True;User ID=GEO;Password=GEO");
        //    Conn.Open();
        //    return Conn;
        //}
        //public static int AgregarArbol1(Arbol pArbol)
        //{
        //    int retorno = 0;
        //    using (SqlConnection conn = AD_Recursos.ObtenerConexion())
        //    {
        //        SqlCommand Comando = new SqlCommand(string.Format("Insert into Referencias (Descripcion, Titulo, Latitud, Longitud, Imagen) values ('{0}','{1}','{2}','{3}','{4}')",
        //            pArbol.Descripcion, pArbol.Especie, pArbol.Latitud, pArbol.Longitud,""), conn);

        //        retorno = Comando.ExecuteNonQuery();    
        //    }
        //    return retorno;
        //}
        public static void AgregarArbol(Arbol pArbol)
        {
            SqlConnection Conn = new SqlConnection("Data Source=179.43.118.18\\sql2012;Initial Catalog=GEOREFERENCIA;Persist Security Info=True;User ID=GEO;Password=GEO");
            SqlCommand cmd = new SqlCommand
            {
                Connection = Conn
            };
            cmd.Parameters.AddWithValue("@descripcion", pArbol.Descripcion);
            cmd.Parameters.AddWithValue("@Especie", pArbol.Especie);
            cmd.Parameters.AddWithValue("@Lat", pArbol.Latitud);
            cmd.Parameters.AddWithValue("@Long", pArbol.Longitud);
            cmd.Parameters.AddWithValue("@imag", "");
            cmd.CommandText = "Insert into Referencias (Descripcion, Titulo, Latitud, Longitud, Imagen) values " +
                "(@descripcion,@Especie,@Lat, @Long,@imag)";
            Conn.Open();
            cmd.ExecuteNonQuery();
            Conn.Close();

        }
    }
}
