using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Xml;

namespace WcfService1
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
    // NOTE: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Service1.svc o Service1.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class Service1 : IService1
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }
        private string Cadena = "Data Source=192.168.1.60;Initial Catalog=prueba001;Persist Security Info=True;User ID=prueba;Password=2019";
        private string cadena2 = "Data Source=192.168.1.60;Initial Catalog=HistoriaClinica;Persist Security Info=True;User ID=usuhistoria;Password=2018";
        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
        public string server_Hosting()
        {
            string res = "";
            try
            {
                string server = "workstation id=prueba001.mssql.somee.com;packet size=4096;user id=joseph04_SQLLogin_1;pwd=26f8hpywqk;data source=prueba001.mssql.somee.com;persist security info=False;initial catalog=prueba001";
                SqlConnection cnn = new SqlConnection(server);
                cnn.Open();
                res = cnn.State.ToString();
            }
            catch (Exception ex)
            {
                res = ex.ToString();
            }

            return res;
        }

        public string server()
        {
            string res = "";
            try
            {
                String servidor = @"DESKTOP-PV6C33B\SQLEXPRESS";
                String usuario = "prueba";
                String password = "2019";
                String database = "HistoriaClinica";
                string server = String.Format("Data Source={0};Initial Catalog={1};User ID={2};Password={3}", servidor, database, usuario, password);

                SqlConnection cnn = new SqlConnection(server);
                cnn.Open();
                res = cnn.State.ToString();
            }
            catch (Exception ex)
            {
                res = ex.ToString();
            }

            return res;
        }

        //public static MySqlConnection probarConexion()
        //{
        //    MySqlConnection con;
        //    String servidor = "localhost";
        //    String puerto = "3306";
        //    String usuario = "root";
        //    String password = "2019";
        //    String database = "pruba";
        //    string Sesion;
        //    //Cadena de conexion
        //    Sesion = String.Format("server={0};port={1};user id={2}; password={3}; database={4}", servidor, puerto, usuario, password, database);
        //    con = new MySqlConnection(Sesion);
        //    con.Open();//se abre la conexion
        //    return con;
        //}

        public string obtenerRegistro()
        {
            DataTable tablaDatos = new DataTable();
            string server = "workstation id=prueba001.mssql.somee.com;packet size=4096;user id=joseph04_SQLLogin_1;pwd=26f8hpywqk;data source=prueba001.mssql.somee.com;persist security info=False;initial catalog=prueba001";
            string CadenaConexion = server;
                //"Data Source=192.168.0.110;Initial Catalog=HistoriaClinica;Persist Security Info=True;User ID=usuhistoria;Password=2018";
            SqlConnection conectar = new SqlConnection(CadenaConexion);
            conectar.Open();
            SqlDataAdapter cmd = new SqlDataAdapter("Select * from Cliente", conectar);
            DataSet data = new DataSet();
            cmd.Fill(data, "datos");
            tablaDatos = data.Tables[0];
            string registro = "";
            for (int iCampo = 0; iCampo < tablaDatos.Rows.Count; iCampo++)
            {
                Array a = tablaDatos.Rows[iCampo].ItemArray;
                for (int indice = 0; indice < a.Length; indice++)
                {
                    registro += a.GetValue(indice).ToString() + ",";
                }
                registro += "/";
            }
            return registro;
        }
        public DataTable TablaReistros()
        {
            DataTable tablaDatos = new DataTable();
            string CadenaConexion = Cadena;
            SqlConnection conectar = new SqlConnection(CadenaConexion);
            conectar.Open();
            string sql = "SELECT [Cli_Identificacion] " +
                        ", CONCAT ([Cli_Nombre1], + ' '+ isnull([Cli_Nombre2],''), + ' '+ [Cli_Apellido1] " +
                        ", isnull([Cli_Apellido2],'')) as NombreCompleto FROM[dbo].Cliente ";
            SqlDataAdapter cmd = new SqlDataAdapter(sql, conectar);
            cmd.Fill(tablaDatos);
            return tablaDatos;
        }
        public string GuardarXML(string Elemento1, string Elemento2, string Elemento3, string Elemento4)
        {
            string Respuesta = "";
            try
            {
                //RAIZ DEL DOCUMENTO
                XmlDocument document = new XmlDocument();
                XmlElement ElemenRaiz = document.CreateElement("Raiz");
                document.AppendChild(ElemenRaiz);

                XmlElement libro = document.CreateElement("Raiz_Sub");
                ElemenRaiz.AppendChild(libro);

                XmlElement titulo = document.CreateElement("Titulo");
                titulo.AppendChild(document.CreateTextNode("PONER TITULO"));
                libro.AppendChild(titulo);

                XmlElement Elm1 = document.CreateElement("Elemento1");
                Elm1.AppendChild(document.CreateTextNode(Elemento1));
                libro.AppendChild(Elm1);

                XmlElement Elm2 = document.CreateElement("Elemento2");
                Elm2.AppendChild(document.CreateTextNode(Elemento2));
                libro.AppendChild(Elm2);

                XmlElement Elm3 = document.CreateElement("Elemento3");
                Elm3.AppendChild(document.CreateTextNode(Elemento3));
                libro.AppendChild(Elm3);

                XmlElement Elm4 = document.CreateElement("Elemento3");
                Elm4.AppendChild(document.CreateTextNode(Elemento4));
                libro.AppendChild(Elm4);

                document.Save("c:\\xml\\Archivo.hml");
                Respuesta = "Archivo Guardado";
            }
            catch (Exception ex)
            {
                Respuesta = "Error " + ex.ToString();

            }
            return Respuesta;
        }
        public string IniciarSesion(string usuario, string contraseña)
        {
            string respuesta = "";
            try
            {                
                DataTable tablaDatos = new DataTable();
                string CadenaConexion = "Data Source=192.168.0.110;Initial Catalog=HistoriaClinica;Persist Security Info=True;User ID=usuhistoria;Password=2018";
                SqlConnection conectar = new SqlConnection(CadenaConexion);
                conectar.Open();
                SqlDataAdapter ad = new SqlDataAdapter("SELECT [Usu_NombreCompleto] FROM [dbo].[Usuario] WHERE Usu_Nombre = '" + usuario + "' And Usu_Contraseña ='" + contraseña + "'", conectar);
                DataSet data = new DataSet();
                ad.Fill(data, "datos");
                if (data.Tables[0].Rows.Count == 1)
                {
                    respuesta = data.Tables[0].Rows[0][0].ToString();
                }
            }
            catch (Exception ex)
            {
                respuesta = ex.ToString();
            }
          
            return respuesta;
        }
        public string ConsultarCliente( string Identificacion)
        {
            DataTable tablaDatos = new DataTable();
            string CadenaConexion = Cadena;
            SqlConnection conectar = new SqlConnection(CadenaConexion);
            conectar.Open();
            string sql = "SELECT [Cli_Identificacion] " +
                        ", CONCAT ([Cli_Nombre1], + ' '+ isnull([Cli_Nombre2],''), + ' '+ [Cli_Apellido1] " +
                        ", isnull([Cli_Apellido2],'')) as NombreCompleto FROM[dbo].Cliente " +
                        "WHERE Cli_Identificacion = '" + Identificacion + "'";
            SqlDataAdapter cmd = new SqlDataAdapter(sql, conectar);
            DataSet data = new DataSet();
            cmd.Fill(data, "datos");            
            string registro = "";
            if (data.Tables[0].Rows.Count>0)
            {
                registro = data.Tables[0].Rows[0]["NombreCompleto"].ToString();
            }
            return registro;
        }        
        public DataTable ConsultarProductos()
        {
            DataTable tablaDatos = new DataTable();
            string CadenaConexion = Cadena;
            SqlConnection conectar = new SqlConnection(CadenaConexion);
            conectar.Open();
            SqlDataAdapter cmd = new SqlDataAdapter("select* from producto", conectar);
            DataSet data = new DataSet();
            cmd.Fill(data, "datos");
            tablaDatos = data.Tables[0];
            return tablaDatos;
        }
        public DataTable ConsultarProductosListaPrecio(string codProducto,string Identificacion)

        {
            DataTable tablaDatos = new DataTable();
            string CadenaConexion = Cadena;
            SqlConnection conectar = new SqlConnection(CadenaConexion);
            conectar.Open();
            string sql = "SELECT  dbo.ListaPrecioDetalle.ListDet_CodProducto, "+
                            "dbo.ListaPrecioDetalle.ListDet_Precio,     "+
                            "dbo.Cliente.Cli_Identificacion, dbo.Producto.Prod_Existencia,  "+
                            "dbo.Producto.Prod_Descripcion                                  "+
                            "FROM    dbo.ListaPrecio INNER JOIN                             "+
                            "dbo.ListaPrecioDetalle ON                                      "+
                            "dbo.ListaPrecio.List_Codigo =                                  "+
                            "dbo.ListaPrecioDetalle.ListDet_CodLista                        "+
                            "INNER JOIN dbo.Producto ON                                     "+
                            "dbo.ListaPrecioDetalle.ListDet_CodProducto =                   "+
                            "dbo.Producto.Prod_Codigo                                       "+
                            "INNER JOIN dbo.Cliente ON                                      "+
                            "dbo.ListaPrecioDetalle.ListDet_CodLista =                      "+
                            "dbo.Cliente.Cli_CodLista                          " +
                "WHERE Cli_Identificacion='" + Identificacion + "' and ListDet_CodProducto" +
                "='" + codProducto + "'";
            SqlDataAdapter cmd = new SqlDataAdapter(sql, conectar);
            DataSet data = new DataSet();
            cmd.Fill(data, "datos");
            tablaDatos = data.Tables[0];
            return tablaDatos;
        }
        //await Task.Run(() => { respuesta = service.InsertarPedido(DateTime.Now.Date.Date,true,TxtIdentificacion.Text,Convert.ToDouble(TxtSubTotal.Text),true,"A", codproducto, cantidad, vardetalle); });

        public string InsertarPedido (string identificacion,double subtotal,List<string> codproducto, List<int> cantidad, List<int> vardetalle)
        {
            string respueesta = "";
            try
            {
                string CadenaConexion = Cadena;
                SqlConnection conectar = new SqlConnection(CadenaConexion);
                conectar.Open();

                string nuevo = "SELECT top 1 [Ped_Numero]  FROM [dbo].[Pedido] Order By [Ped_Numero] Desc";
                SqlCommand command = new SqlCommand(nuevo,conectar);
                int numeropedido = Convert.ToInt32(command.ExecuteScalar()) + 1;
                //if (numeropedido==0)
                //numeropedido=1

                string pedido = string.Format("INSERT INTO [dbo].[Pedido]  ([Ped_Numero] ,[Ped_IdCliente]  ,[Ped_Fecha]  ,[Ped_Valor])  " +
                                "VALUES ({0} ,'{1}' ,{2} ,{3})", numeropedido, identificacion, "GETDATE()", subtotal);
                command = new SqlCommand(pedido, conectar);
                command.ExecuteNonQuery();
                //conectar.Close();fecha.ToShortDateString().Substring(0,10).Trim()
                respueesta = pedido;

                for (int i = 0; i < codproducto.Count; i++)
                {
                    pedido = string.Format("INSERT INTO [dbo].[pedidoDetalle] " +
                                            "([PedDet_Numero]                 " +
                                            ",[PedDet_NunPedido]              " +
                                            ",[PedDet_CodProducto]            " +
                                            ",[PedDet_Precio]                 " +
                                            ",[PedDet_Cantidad])              " +
                                            "VALUES({0},{1},'{2}',{3},{4})",(i+1),numeropedido,codproducto[i],vardetalle[i],cantidad[i]);

                    command = new SqlCommand(pedido, conectar);
                    command.ExecuteNonQuery();
                    respueesta = respueesta + " - " + pedido;                     
                }
            }
            catch (Exception ex)
            {
                respueesta = ex.ToString();
            }
            return respueesta;
        }

        public DataTable ConsultarMultiple(string Criterio, string condicion)
        {

            string SQL = "";
            string valor = "";
            switch (condicion)
            {
                case "todo":
                    valor = ("WHERE Cli_Identificacion = '"+ Criterio +"'");
                    break;

                case "pendiente":
                    valor = ("WHERE Cli_Identificacion = '" + Criterio + "' AND Esta_Codigo<>'i'");
                    break;

                case "pedido":
                    valor = ("WHERE Ped_Numero = " + Criterio );
                    break;

                case "fecha":
                    valor = ("WHERE Ped_Fecha = '" + Criterio + "'");
                    break;

            }
            
            DataTable tablaDatos = new DataTable();
            string CadenaConexion = Cadena;
            SqlConnection conectar = new SqlConnection(CadenaConexion);
            conectar.Open();
            if (condicion=="pedido")
            {
                SQL = "SELECT  dbo.Pedido.Ped_Numero, dbo.Cliente.Cli_Identificacion,                                   " +
                            "dbo.Cliente.Cli_Nombre1, dbo.Cliente.Cli_Nombre2,                                          " +
                            "dbo.Cliente.Cli_Apellido1, dbo.Cliente.Cli_Apellido2,                                      " +
                            "dbo.Cliente.Cli_CodLista, dbo.ListaPrecio.List_Descripcion,                                " +
                            "dbo.Producto.Prod_Descripcion, dbo.Producto.Prod_Codigo,                                   " +
                            "dbo.Pedido.Ped_Fecha, dbo.Pedido.Ped_Valor,                                                " +
                            "dbo.Pedido.Ped_Estado, dbo.Estado.Esta_Codigo,                                             " +
                            "dbo.Estado.Esta_Descripcion, dbo.pedidoDetalle.PedDet_Precio,                              " +
                            "dbo.pedidoDetalle.PedDet_Cantidad                                                          " +
                            "FROM    dbo.Cliente INNER JOIN                                                             " +
                            "dbo.ListaPrecio ON dbo.Cliente.Cli_CodLista = dbo.ListaPrecio.List_Codigo                  " +
                            "INNER JOIN  dbo.ListaprecioDetalle ON dbo.ListaPrecio.List_Codigo =                        " +
                            "dbo.ListaprecioDetalle.ListDet_CodLista INNER JOIN                                         " +
                            "dbo.Pedido ON dbo.Cliente.Cli_Identificacion = dbo.Pedido.Ped_IdCliente INNER JOIN         " +
                            "dbo.Estado ON dbo.Pedido.Ped_Estado = dbo.Estado.Esta_Codigo INNER JOIN                    " +
                            "dbo.pedidoDetalle ON dbo.Pedido.Ped_Numero = dbo.pedidoDetalle.PedDet_NunPedido INNER JOIN " +
                            "dbo.Producto ON dbo.ListaprecioDetalle.ListDet_CodProducto = dbo.Producto.Prod_Codigo      " +
                            "AND dbo.pedidoDetalle.PedDet_CodProducto = dbo.Producto.Prod_Codigo                        " +
                            valor +
                            "ORDER BY  Ped_Numero desc";
            }
            else
            {
                SQL = "SELECT	dbo.Cliente.Cli_Identificacion, dbo.Cliente.Cli_Nombre1, " +
                    "dbo.Cliente.Cli_Nombre2, dbo.Cliente.Cli_Apellido1,             " +
                    "dbo.Cliente.Cli_Apellido2, dbo.Cliente.Cli_CodLista,            " +
                    "dbo.ListaPrecio.List_Descripcion,                               " +
                    "dbo.Pedido.Ped_Numero, dbo.Pedido.Ped_IdCliente,                " +
                    "dbo.Pedido.Ped_Fecha, dbo.Pedido.Ped_Valor,                     " +
                    "dbo.Pedido.Ped_Estado, dbo.Estado.Esta_Codigo,                  " +
                    "dbo.Estado.Esta_Descripcion                                     " +
                    "FROM    dbo.Cliente INNER JOIN                                  " +
                    "dbo.ListaPrecio ON dbo.Cliente.Cli_CodLista =                   " +
                    "dbo.ListaPrecio.List_Codigo INNER JOIN                          " +
                    "dbo.Pedido ON dbo.Cliente.Cli_Identificacion =                  " +
                    "dbo.Pedido.Ped_IdCliente INNER JOIN                             " +
                    "dbo.Estado ON dbo.Pedido.Ped_Estado = dbo.Estado.Esta_Codigo    " +
                    valor +
                    " ORDER BY  Ped_Numero desc";
            }

            SqlDataAdapter cmd = new SqlDataAdapter(SQL, conectar);
            DataSet data = new DataSet();
            cmd.Fill(data, "datos");
            tablaDatos = data.Tables[0];
            return tablaDatos;
        }

        public DataTable TodosPacientes()
        {
            DataTable tablaDatos = new DataTable();
            string CadenaConexion = Cadena;
            SqlConnection conectar = new SqlConnection(CadenaConexion);
            conectar.Open();
            string sql = "SELECT [Cli_Identificacion] " +
            ", CONCAT ([Cli_Nombre1], + ' '+ isnull([Cli_Nombre2],''), + ' '+ [Cli_Apellido1] " +
            ", isnull([Cli_Apellido2],'')) as NombreCompleto FROM[dbo].Cliente ";
            SqlDataAdapter cmd = new SqlDataAdapter(sql, conectar);
            DataSet data = new DataSet();
            cmd.Fill(data, "datos");
            tablaDatos = data.Tables[0];
            return tablaDatos;
        }

        public string ConsultarUsuario(string usuario,string contraseña)
        {
            string nombre="";
            DataTable tablaDatos = new DataTable();
            string CadenaConexion = Cadena;
            SqlConnection conectar = new SqlConnection(CadenaConexion);
            conectar.Open();
            string sql = string.Format("SELECT [Log_Usuario],[Log_Contraseña],[Log_Alias] FROM [dbo].[Usuario] WHERE Log_Usuario='{0}' AND Log_Contraseña='{1}'",usuario,contraseña);
            SqlDataAdapter cmd = new SqlDataAdapter(sql, conectar);
            DataSet data = new DataSet();
            cmd.Fill(data, "datos");
            tablaDatos = data.Tables[0];
            if (tablaDatos.Rows.Count>0)
            {
                nombre= tablaDatos.Rows[0]["Log_Alias"].ToString() + " " + sql;
            }
            return nombre;
        }
    }
}
