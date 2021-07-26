using System;

namespace SystemModule.Sockets
{
    /// <summary>
    /// �첽Socket�����¼�������
    /// </summary>
    public class AsyncSocketErrorEventArgs : EventArgs
    {
        private AsyncSocketException _exception;

        /// <summary>
        /// ʹ��SocketException�������й���
        /// </summary>
        /// <param name="exception"></param>
        public AsyncSocketErrorEventArgs(AsyncSocketException exception)
        {
            this._exception = exception;
        }
    }
}