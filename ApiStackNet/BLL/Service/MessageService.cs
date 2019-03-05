using ApiStackNet.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiStackNet.BLL.Service
{
    public class MessageService
    {

        public MessageService()
        {
            UiMessages = new List<UiMessage>();
        }
        
        public  bool HasMessages { get { return (UiMessages!=null && UiMessages.Count > 0); }  }
        public  List<UiMessage> UiMessages { get; set; } 
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
            var uiMessage = new UiMessage();
            uiMessage.Message = message;
            uiMessage.Type = UiMessageType.INFO;
            uiMessage.Target = target ?? UiMessageTarget.CONSOLE;

            uiMessage.Title = title;
            uiMessage.Code = code ?? "INFO_GENERIC";
            uiMessage.FieldId = fieldId ?? string.Empty;

            UiMessages.Add(uiMessage);
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
            var uiMessage = new UiMessage();
            uiMessage.Message = message;
            uiMessage.Type = UiMessageType.SUCCESS;
            uiMessage.Target = target ?? UiMessageTarget.TOAST;

            uiMessage.Title = title;
            uiMessage.Code = code ?? "SUCC_GENERIC";
            uiMessage.FieldId = fieldId ?? string.Empty;

            UiMessages.Add(uiMessage);
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
            var uiMessage = new UiMessage();
            uiMessage.Message = message;
            uiMessage.Type = UiMessageType.ERROR;
            uiMessage.Target = target ?? UiMessageTarget.TOAST;

            uiMessage.Title = title;
            uiMessage.Code = code ?? "ERR_GENERIC";
            uiMessage.FieldId = fieldId ?? string.Empty;

            UiMessages.Add(uiMessage);
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
            var uiMessage = new UiMessage();
            uiMessage.Message = message;
            uiMessage.Type = UiMessageType.WARNING;
            uiMessage.Target = target ?? UiMessageTarget.TOAST;

            uiMessage.Title = title;
            uiMessage.Code = code ?? "WARN_GENERIC";
            uiMessage.FieldId = fieldId ?? string.Empty;

            UiMessages.Add(uiMessage);
        }

    }
}
