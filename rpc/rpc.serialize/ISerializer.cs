// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
namespace rpc.serialize
{
    public interface ISerializer
    {
        /// <summary>
        /// serialize a object to byte array ,for netty client send message
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        byte[] Serialize(object target);

        /// <summary>
        /// deserialize byte array received by servier side
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        object Deserialize(byte[] buffer);
    }
}