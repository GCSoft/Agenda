/*---------------------------------------------------------------------------------------------------------------------------------
' Nombre:	KeepAliveSession
' Autor:	Ruben.Cobos
' Fecha:	07-Jul-2015
'----------------------------------------------------------------------------------------------------------------------------------*/

// Referencias
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// Referencias manuales
using System.Web.SessionState;

namespace Agenda.Web.Include.Handler
{
    
    public class KeepAliveSession : IHttpHandler, IRequiresSessionState

    {

        // Propiedades

		public bool IsReusable
		{
			get { return true; }
		}


        // Eventos

        public void ProcessRequest(HttpContext context)
        {

            // Quitar cache
            context.Response.Cache.SetNoStore();

            // Se regresa un comentario en blanco en JavaScript
            // Debido a que el manejador hereda de la interfase IRequiresSessionState, al momento de invocarse se regenera la sesión en automático
            context.Response.ContentType = "application/x-javascript";
            context.Response.Write("//");
        }

        

    }
}