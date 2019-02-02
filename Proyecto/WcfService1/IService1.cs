using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfService1
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IService1" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        [OperationContract]
        string obtenerRegistro();
        // TODO: agregue aquí sus operaciones de servicio
        [OperationContract]
        string GuardarXML(string Elemento1, string Elemento2, string Elemento3, string Elemento4);
        // TODO: agregue aquí sus operaciones de servicio
        [OperationContract]
         DataTable TablaReistros();

        [OperationContract]
        string IniciarSesion(string usuario, string contraseña);

        [OperationContract]
        string ConsultarCliente(string Identificacion);

        [OperationContract]
        DataTable ConsultarProductos();

        [OperationContract]
        DataTable ConsultarProductosListaPrecio(string codProducto, string Identificacion);
        [OperationContract]
        string server_Hosting();
        [OperationContract]
        string server();
        [OperationContract]
        string InsertarPedido(string identificacion, double subtotal, List<string> codproducto, List<int> cantidad, List<int> vardetalle);

        [OperationContract]
        DataTable ConsultarMultiple(string Criterio, string condicion);

        [OperationContract]
        DataTable TodosPacientes();

        [OperationContract]
        string ConsultarUsuario(string usuario, string contraseña);
    }
    // Utilice un contrato de datos, como se ilustra en el ejemplo siguiente, para agregar tipos compuestos a las operaciones de servicio.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
