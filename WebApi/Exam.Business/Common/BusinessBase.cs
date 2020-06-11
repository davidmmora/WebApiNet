using Exam.Entities.Common;
using System;
using System.Text;

namespace Exam.Business.Common
{
    public abstract class BusinessBase
    {
        /// <summary>
        /// Mensaje de error construido
        /// </summary>
        protected StringBuilder Message { get; set; }

        protected string GetMessage()
        {
            return Message?.ToString();
        }


        /// <summary>
        /// Bandera que indica si la validación fue satisfactoria
        /// </summary>
        protected bool IsValid { get; set; }

        /// <summary>
        /// Bandera que indica si los mensajes de salida deben construirse con formato web
        /// </summary>
        protected bool ForWeb { get; set; }

        /// <summary>
        /// Prepara los elementos comunes de validación
        /// </summary>
        /// <param name="initMessage">Mensaje inicial de error de salida</param>
        protected void InitalizeMessage(string initMessage = null, bool forWeb = true)
        {
            IsValid = true;
            ForWeb = forWeb;
            Message = new StringBuilder();
            if (!string.IsNullOrEmpty(initMessage))
            {
                Message.Append(initMessage);
            }
            else
            {
                if (ForWeb)
                {
                    Message.Append("<p>There are some errors on your request: <ul>");
                }
                else
                {
                    Message.Append("There are some errors on your request:" + Environment.NewLine);
                }
            }
        }

        /// <summary>
        /// Termina la cadena de error con el mensaje introducido
        /// </summary>
        /// <param name="endMessage">Cadena para terminar el mensaje de error</param>
        protected void EndMessage(string endMessage = null)
        {
            if (!string.IsNullOrEmpty(endMessage))
            {
                if (ForWeb)
                {
                    Message.Append("</ul></p>");
                }
                else
                {
                    Message.Append(endMessage);
                }
            }
        }

        /// <summary>
        /// Agrega una línea con el mensaje de error indicado y desactiva la bandera de validez
        /// </summary>
        /// <param name="message">Mensaje de error</param>
        protected void AppendMessage(string message)
        {
            IsValid = false;
            if (ForWeb)
            {
                Message.Append(string.Format("<li>   {0}</li>", message));
            }
            else
            {
                Message.Append(string.Format("   {0}{1}", message, Environment.NewLine));
            }
        }

        /// <summary>
        /// Agrega una línea con el mensaje de error indicado y desactiva la bandera de validez
        /// si la bandera de error es verdadera
        /// </summary>
        /// <param name="errorCondition">Bandera de error</param>
        /// <param name="message">Mensaje de error</param>
        protected void AppendMessage(bool errorCondition, string message)
        {
            if (errorCondition)
            {
                IsValid = false;
                AppendMessage(message);
            }
        }
    }
}
