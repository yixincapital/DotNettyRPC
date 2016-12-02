// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
namespace rpc.core
{
    using System;
    using System.Threading;
    using rpc.model;

    public class MessageSendCallBack
    {
        private MessageRequest request;
        private MessageResponse response;
        readonly object _lock = new object();
        SpinLock spinlock = new SpinLock();

        public MessageSendCallBack(MessageRequest request)
        {
            this.request = request;
        }

        public object Start()
        {
            bool lockTaken = false;
            try
            {
                lock (this._lock)
                {                    
                    this.spinlock.TryEnter(1000 * 10, ref lockTaken); // set locker timeout
                    return this.response;
                }
            }
            finally
            {
                 //TODO  maybe some callback for caller
                //Console.WriteLine("locked response ");
            }
        }

        public void Over(MessageResponse response)
        {
            lock (this._lock)
            {
                this.spinlock.Exit(); // release the lock 
                this.response = response;
            }
        }
    }
}