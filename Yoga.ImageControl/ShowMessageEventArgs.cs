using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yoga.ImageControl
{
    public enum MessageType
    {
        MouseMessage,
        ShowOk,
        ShowNg
    }
    public class ShowMessageEventArgs
    {
        public readonly MessageType MessageType;
        public readonly string message;
        public ShowMessageEventArgs(MessageType messageType)
        {
            this.MessageType = messageType;
        }
        public ShowMessageEventArgs(string message)
        {
            this.MessageType = MessageType.MouseMessage;
            this.message = message;
        }
    }
}
