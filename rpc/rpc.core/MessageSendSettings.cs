// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
namespace rpc.core
{
    using System.Configuration;
    using System.Net;

    public  static class MessageSendSettings
    {
        public static IPAddress Host
        {
            get { return IPAddress.Parse(ConfigurationManager.AppSettings["host"]); }
        }

        public static int Port
        {
            get { return int.Parse(ConfigurationManager.AppSettings["port"]); }
        }

        public static int Size
        {
            get { return int.Parse(ConfigurationManager.AppSettings["size"]); }
        }
    }
}