﻿using System;
using System.Collections.Generic;

namespace CSharpGL
{
    internal class QuadsRecognizer : PrimitiveRecognizer
    {
        protected override void RecognizeUInt(uint lastVertexId, DrawElementsCmd cmd, List<RecognizedPrimitiveInfo> primitiveInfoList)
        {
            IndexBuffer indexBuffer = cmd.IndexBufferObject;
            int length = indexBuffer.Length;
            IntPtr pointer = indexBuffer.MapBuffer(MapBufferAccess.ReadOnly);
            unsafe
            {
                var array = (uint*)pointer.ToPointer();
                uint i = 0;
                for (i = i + 3; i < length; i += 4)
                {
                    var value = array[i];
                    if (value == lastVertexId)
                    {
                        var item = new RecognizedPrimitiveInfo(i, array[i - 3], array[i - 2], array[i - 1], lastVertexId);
                        primitiveInfoList.Add(item);
                    }
                }
            }
            indexBuffer.UnmapBuffer();
        }

        protected override void RecognizeUShort(uint lastVertexId, DrawElementsCmd cmd, List<RecognizedPrimitiveInfo> primitiveInfoList)
        {
            IndexBuffer indexBuffer = cmd.IndexBufferObject;
            int length = indexBuffer.Length;
            IntPtr pointer = indexBuffer.MapBuffer(MapBufferAccess.ReadOnly);
            unsafe
            {
                var array = (ushort*)pointer.ToPointer();
                uint i = 0;
                for (i = i + 3; i < length; i += 4)
                {
                    var value = array[i];
                    if (value == lastVertexId)
                    {
                        var item = new RecognizedPrimitiveInfo(i, array[i - 3], array[i - 2], array[i - 1], lastVertexId);
                        primitiveInfoList.Add(item);
                    }
                }
            }
            indexBuffer.UnmapBuffer();
        }

        protected override void RecognizeByte(uint lastVertexId, DrawElementsCmd cmd, List<RecognizedPrimitiveInfo> primitiveInfoList)
        {
            IndexBuffer indexBuffer = cmd.IndexBufferObject;
            int length = indexBuffer.Length;
            IntPtr pointer = indexBuffer.MapBuffer(MapBufferAccess.ReadOnly);
            unsafe
            {
                var array = (byte*)pointer.ToPointer();
                uint i = 0;
                for (i = i + 3; i < length; i += 4)
                {
                    var value = array[i];
                    if (value == lastVertexId)
                    {
                        var item = new RecognizedPrimitiveInfo(i, array[i - 3], array[i - 2], array[i - 1], lastVertexId);
                        primitiveInfoList.Add(item);
                    }
                }
            }
            indexBuffer.UnmapBuffer();
        }

        protected override void RecognizeUInt(uint lastVertexId, DrawElementsCmd cmd, List<RecognizedPrimitiveInfo> primitiveInfoList, uint primitiveRestartIndex)
        {
            IndexBuffer indexBuffer = cmd.IndexBufferObject;
            int length = indexBuffer.Length;
            IntPtr pointer = indexBuffer.MapBuffer(MapBufferAccess.ReadOnly);
            unsafe
            {
                var array = (uint*)pointer.ToPointer();
                long nearestRestartIndex = -1;
                uint i = 0;
                while (i < length && array[i] == primitiveRestartIndex)
                { nearestRestartIndex = i; i++; }
                for (i = i + 3; i < length; i++)
                {
                    var value = array[i];
                    if (value == primitiveRestartIndex)
                    {
                        nearestRestartIndex = i;
                    }
                    else if (((i - 3 - nearestRestartIndex) % 4 == 0)
                        && (value == lastVertexId)
                        && (array[i - 1] != primitiveRestartIndex)
                        && (array[i - 2] != primitiveRestartIndex)
                        && (array[i - 3] != primitiveRestartIndex))
                    {
                        var item = new RecognizedPrimitiveInfo(i, array[i - 3], array[i - 2], array[i - 1], lastVertexId);
                        primitiveInfoList.Add(item);
                    }
                }
            }
            indexBuffer.UnmapBuffer();
        }

        protected override void RecognizeUShort(uint lastVertexId, DrawElementsCmd cmd, List<RecognizedPrimitiveInfo> primitiveInfoList, uint primitiveRestartIndex)
        {
            IndexBuffer indexBuffer = cmd.IndexBufferObject;
            int length = indexBuffer.Length;
            IntPtr pointer = indexBuffer.MapBuffer(MapBufferAccess.ReadOnly);
            unsafe
            {
                var array = (ushort*)pointer.ToPointer();
                long nearestRestartIndex = -1;
                uint i = 0;
                while (i < length && array[i] == primitiveRestartIndex)
                { nearestRestartIndex = i; i++; }
                for (i = i + 3; i < length; i++)
                {
                    var value = array[i];
                    if (value == primitiveRestartIndex)
                    {
                        nearestRestartIndex = i;
                    }
                    else if (((i - 3 - nearestRestartIndex) % 4 == 0)
                        && (value == lastVertexId)
                        && (array[i - 1] != primitiveRestartIndex)
                        && (array[i - 2] != primitiveRestartIndex)
                        && (array[i - 3] != primitiveRestartIndex))
                    {
                        var item = new RecognizedPrimitiveInfo(i, array[i - 3], array[i - 2], array[i - 1], lastVertexId);
                        primitiveInfoList.Add(item);
                    }
                }
            }
            indexBuffer.UnmapBuffer();
        }

        protected override void RecognizeByte(uint lastVertexId, DrawElementsCmd cmd, List<RecognizedPrimitiveInfo> primitiveInfoList, uint primitiveRestartIndex)
        {
            IndexBuffer indexBuffer = cmd.IndexBufferObject;
            int length = indexBuffer.Length;
            IntPtr pointer = indexBuffer.MapBuffer(MapBufferAccess.ReadOnly);
            unsafe
            {
                var array = (byte*)pointer.ToPointer();
                long nearestRestartIndex = -1;
                uint i = 0;
                while (i < length && array[i] == primitiveRestartIndex)
                { nearestRestartIndex = i; i++; }
                for (i = i + 3; i < length; i++)
                {
                    var value = array[i];
                    if (value == primitiveRestartIndex)
                    {
                        nearestRestartIndex = i;
                    }
                    else if (((i - 3 - nearestRestartIndex) % 4 == 0)
                        && (value == lastVertexId)
                        && (array[i - 1] != primitiveRestartIndex)
                        && (array[i - 2] != primitiveRestartIndex)
                        && (array[i - 3] != primitiveRestartIndex))
                    {
                        var item = new RecognizedPrimitiveInfo(i, array[i - 3], array[i - 2], array[i - 1], lastVertexId);
                        primitiveInfoList.Add(item);
                    }
                }
            }
            indexBuffer.UnmapBuffer();
        }
    }
}
