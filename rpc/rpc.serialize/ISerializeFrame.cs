// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
namespace rpc.serialize
{
    using DotNetty.Transport.Channels;

    public interface ISerializeFrame
    {
         void Select(ISerializer protocol, IChannelPipeline pipeline);  
    }
}