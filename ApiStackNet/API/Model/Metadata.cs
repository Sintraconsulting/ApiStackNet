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
            var uiMessage = new UIMessage();
            uiMessage.Message = message;
            uiMessage.Type = UiMessageType.INFO;
            uiMessage.Target = target ?? UiMessageTarget.CONSOLE;

            uiMessage.Title = title;
            uiMessage.Code = code ?? "INFO_GENERIC";
            uiMessage.FieldId = fieldId ?? string.Empty;

            this.UiMessages.Add(uiMessage);
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
            var uiMessage = new UIMessage();
            uiMessage.Message =  message;
            uiMessage.Type = UiMessageType.SUCCESS;
            uiMessage.Target = target ?? UiMessageTarget.TOAST;

            uiMessage.Title = title;
            uiMessage.Code = code ?? "SUCC_GENERIC";
            uiMessage.FieldId = fieldId ?? string.Empty;

            this.UiMessages.Add(uiMessage);
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
            var uiMessage = new UIMessage();
            uiMessage.Message = message;
            uiMessage.Type = UiMessageType.ERROR;
            uiMessage.Target = target ?? UiMessageTarget.TOAST;

            uiMessage.Title = title; 
            uiMessage.Code = code ?? "ERR_GENERIC";
            uiMessage.FieldId = fieldId ?? string.Empty;

            this.UiMessages.Add(uiMessage);
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
            var uiMessage = new UIMessage();
            uiMessage.Message = message;
            uiMessage.Type = UiMessageType.WARNING;
            uiMessage.Target = target ?? UiMessageTarget.TOAST;

            uiMessage.Title = title;
            uiMessage.Code = code ?? "WARN_GENERIC";
            uiMessage.FieldId = fieldId ?? string.Empty;

            this.UiMessages.Add(uiMessage);
        }
    }
}
