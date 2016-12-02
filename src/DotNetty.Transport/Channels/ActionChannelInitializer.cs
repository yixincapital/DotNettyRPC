// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace DotNetty.Transport.Channels
{
    using System;
    using System.Diagnostics.Contracts;

    public sealed class ActionSendChannelInitializer<T> : ChannelInitializer<T>
        where T : IChannel
    {
        readonly Action<T> initializationAction;
       
        public ActionSendChannelInitializer(Action<T> initializationAction)
        {
            Contract.Requires(initializationAction != null);

            this.initializationAction = initializationAction;
        }

        protected override void InitChannel(T channel)
        {
            this.initializationAction(channel);
        }
    }
}