// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
namespace rpc.core
{
    using System;
    using System.Net;
    using System.Security.Cryptography.X509Certificates;
    using System.Threading.Tasks;
    using DotNetty.Codecs;
    using DotNetty.Handlers.Tls;
    using DotNetty.Transport.Bootstrapping;
    using DotNetty.Transport.Channels;
    using DotNetty.Transport.Channels.Sockets;
    using rpc.serialize;

    public class MessageSendConnector
    {
        private IEventLoopGroup eventLoopGroup;
        private ISerializer protocol;
        IPEndPoint ipEndPoint;

        public MessageSendConnector(IEventLoopGroup eventLoopGroup, IPEndPoint ipEndPoint, ISerializer protocol)
        {
            this.eventLoopGroup = eventLoopGroup;
            this.ipEndPoint = ipEndPoint;
            this.protocol = protocol;
        }

        public async Task Call()
        {
            var bootstrap = new Bootstrap();
            bootstrap
                .Group(this.eventLoopGroup)
                .Channel<TcpSocketChannel>()
                .Option(ChannelOption.SoKeepalive, true)
                .Handler(new MessageSendChannelInitializer<ISocketChannel>(channel =>
                {
                    //IChannelPipeline pipeline = channel.Pipeline;

                    //pipeline.AddLast(new LengthFieldPrepender(2));
                    //pipeline.AddLast(new LengthFieldBasedFrameDecoder(ushort.MaxValue, 0, 2, 0, 2));

                    //pipeline.AddLast(new MessageSendHandler());
                    //RpcServerLoader.Instance.SetMessageSendHandler(pipeline.Get<MessageSendHandler>());
                }).BuildSerializeProtocol(new DefaultSerializer()));

            IChannel bootstrapChannel = await bootstrap.ConnectAsync(this.ipEndPoint);

            Console.ReadLine();

            await bootstrapChannel.CloseAsync();
        }
    }
}