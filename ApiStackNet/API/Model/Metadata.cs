using ApiStackNet.BLL.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiStackNet.API.Model
{
    public class Metadata
    {
        public IList<UiMessage> UiMessages { get; set; } = new List<UiMessage>();

        /// <summary>
        /// Add INFO message for the client
        /// </summary>
        /// <param name="message">content</param>
        /// <param name="title">content title</param>
        /// <param name="code">message code</param>
        /// <param name="fieldId">ui field target's id</param>
        /// <param name="target">message destination type</param>
        public void AddInfo(string message, UiMessageTarget? target = null, string title = null, string code = null, string fieldId = null)
        {
            var UiMessage = new UiMessage();
            UiMessage.Message = message;
            UiMessage.Type = UiMessageType.INFO;
            UiMessage.Target = target ?? UiMessageTarget.CONSOLE;

            UiMessage.Title = title;
            UiMessage.Code = code ?? "INFO_GENERIC";
            UiMessage.FieldId = fieldId ?? string.Empty;

            this.UiMessages.Add(UiMessage);
        }

        /// <summary>
        /// Add SUCCESS message for the client
        /// </summary>
        /// <param name="message">content</param>
        /// <param name="title">content title</param>
        /// <param name="code">message code</param>
        /// <param name="fieldId">ui field target's id</param>
        /// <param name="target">message destination type</param>
        public void AddSuccess(string message, UiMessageTarget? target = null, string title = null, string code = null, string fieldId = null)
        {
            var UiMessage = new UiMessage();
            UiMessage.Message =  message;
            UiMessage.Type = UiMessageType.SUCCESS;
            UiMessage.Target = target ?? UiMessageTarget.TOAST;

            UiMessage.Title = title;
            UiMessage.Code = code ?? "SUCC_GENERIC";
            UiMessage.FieldId = fieldId ?? string.Empty;

            this.UiMessages.Add(UiMessage);
        }

        /// <summary>
        /// Add ERROR message for the client
        /// </summary>
        /// <param name="message">content</param>
        /// <param name="title">content title</param>
        /// <param name="code">message code</param>
        /// <param name="fieldId">ui field target's id</param>
        /// <param name="target">message destination type</param>
        public void AddError(string message, UiMessageTarget? target = null, string title = null, string code = null, string fieldId = null)
        {
            var UiMessage = new UiMessage();
            UiMessage.Message = message;
            UiMessage.Type = UiMessageType.ERROR;
            UiMessage.Target = target ?? UiMessageTarget.TOAST;

            UiMessage.Title = title; 
            UiMessage.Code = code ?? "ERR_GENERIC";
            UiMessage.FieldId = fieldId ?? string.Empty;

            this.UiMessages.Add(UiMessage);
        }

        /// <summary>
        /// Add WARNING message for the client
        /// </summary>
        /// <param name="message">content</param>
        /// <param name="title">content title</param>
        /// <param name="code">message code</param>
        /// <param name="fieldId">ui field target's id</param>
        /// <param name="target">message destination type</param>
        public void AddWarning(string message, UiMessageTarget? target = null, string title = null, string code = null, string fieldId = null)
        {
            var UiMessage = new UiMessage();
            UiMessage.Message = message;
            UiMessage.Type = UiMessageType.WARNING;
            UiMessage.Target = target ?? UiMessageTarget.TOAST;

            UiMessage.Title = title;
            UiMessage.Code = code ?? "WARN_GENERIC";
            UiMessage.FieldId = fieldId ?? string.Empty;

            this.UiMessages.Add(UiMessage);
        }
    }
}
