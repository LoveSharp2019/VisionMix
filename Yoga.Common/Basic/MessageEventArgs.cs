using System;

namespace Yoga.Common.Basic
{
    public enum Level
    {
        Normal,
        Err
    }
    public class MessageEventArgs : EventArgs
    {
        public readonly string Message;
        public readonly Level level;
        public MessageEventArgs(Level level, string message)
        {
            this.Message = message;
            this.level = level;
        }
        public MessageEventArgs( string message)
        {
            this.Message = message;
            this.level = Level.Normal;
        }
    }
}
