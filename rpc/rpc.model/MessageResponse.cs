// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
namespace rpc.model
{
    using System;
    using System.Runtime.Remoting;

    [Serializable]
    public class MessageResponse
    {
        public string MessageId { get; set; }

        public string Error { get; set; }

        public object Result { get; set; }
    }
}