/*---------------------------------------------------------------------------------------------------------------------------------
' Clase: ENTResponse
' Autor: Ruben.Cobos
' Fecha: 21-Octubre-2013
'
' Proposito:
'          Clase que controla las respuestas entre las diferentes capas de la aplicación
'
'----------------------------------------------------------------------------------------------------------------------------------*/

// Referencias
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Referencias manuales
using System.Data;

namespace Agenda.Entity.Object
{
    public class ENTResponse : ENTBase
    {

        private DataSet _DataSetResponse;
        private Boolean _GeneratesException;
        private String  _MessageError;
        private String  _MessageDB;


        // Constructor

        public ENTResponse()
        {
            _DataSetResponse = null;
            _GeneratesException = false;
            _MessageDB = "";
            _MessageError = "";
        }

        

        // Metodos publicos

        ///<remarks>
        ///   <name>ENTResponse.ExceptionRaised</name>
        ///   <create>21-Octubre-2013</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Indica la generación de una excepción en el método</summary>
        ///<param name="ErrorMessage">Descripción del mensaje de error</param>
        public void ExceptionRaised(String ErrorMessage)
        {
            _GeneratesException = true;
            _MessageError = ErrorMessage;
        }

        

        // Propiedades

        ///<remarks>
        ///   <name>ENTResponse.DataSetResponse</name>
        ///   <create>21-Octubre-2013</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna un dataset de respuesta</summary>
        public DataSet DataSetResponse
        {
            get { return _DataSetResponse; }
            set { _DataSetResponse = value; }
        }

        ///<remarks>
        ///   <name>ENTResponse.GeneratesException</name>
        ///   <create>21-Octubre-2013</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Indica si la llamada al método generó una excepción</summary>
        public Boolean GeneratesException
        {
            get { return _GeneratesException; }
        }

        ///<remarks>
        ///   <name>ENTResponse.MessageDB</name>
        ///   <create>21-Octubre-2013</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna un mensaje de respuesta de la base de datos</summary>
        public String MessageDB
        {
            get { return _MessageDB; }
            set { _MessageDB = value; }
        }

        ///<remarks>
        ///   <name>ENTResponse.MessageError</name>
        ///   <create>21-Octubre-2013</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Contiene el mensaje de error cuando se genera una excepción</summary>
        public String MessageError
        {
            get { return _MessageError; }
        }

    }
}
