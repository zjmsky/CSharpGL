﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSharpGL;

namespace c08d04_DrawModes
{
    class DrawModesModel : IBufferSource
    {
        private readonly vec3[] positions;
        private readonly vec3[] colors;

        private vec3 size;
        public vec3 GetSize()
        {
            return this.size;
        }

        public DrawModesModel()
        {
            const int count = 14;
            {
                var positions = new vec3[count];
                for (int w = 0; w < count; w++)
                {
                    positions[w] = new vec3(
                        (float)(Math.Cos((float)w / (count + 1) * Math.PI * 2)),
                        (float)(Math.Sin((float)w / (count + 1) * Math.PI * 2)),
                        0) * 3;
                }
                BoundingBox box = positions.Move2Center();
                this.size = box.MaxPosition - box.MinPosition;
                this.positions = positions;
            }
            {
                var random = new Random();
                var colors = new vec3[positions.Length];
                for (int i = 0; i < colors.Length; i++)
                {
                    colors[i] = new vec3((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble());
                }
                this.colors = colors;
            }
        }

        public const string strPosition = "position";
        private VertexBuffer positionBuffer;
        public const string strColor = "color";
        private VertexBuffer colorBuffer;

        private IDrawCommand drawCmd;

        #region IBufferSource 成员

        public IEnumerable<VertexBuffer> GetVertexAttribute(string bufferName)
        {
            if (strPosition == bufferName)
            {
                if (this.positionBuffer == null)
                {
                    this.positionBuffer = this.positions.GenVertexBuffer(VBOConfig.Vec3, BufferUsage.StaticDraw);
                }

                yield return this.positionBuffer;
            }
            else if (strColor == bufferName)
            {
                if (this.colorBuffer == null)
                {
                    this.colorBuffer = this.colors.GenVertexBuffer(VBOConfig.Vec3, BufferUsage.StaticDraw);
                }

                yield return this.colorBuffer;
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public IEnumerable<IDrawCommand> GetDrawCommand()
        {
            if (this.drawCmd == null)
            {
                //this.drawCmd = new DrawArraysCmd(DrawMode.Triangles, this.positions.Length);
                var indexes = new uint[positions.Length];
                for (uint i = 0; i < indexes.Length; i++)
                {
                    indexes[i] = i;
                }
                var indexBuffer = indexes.GenIndexBuffer(BufferUsage.StaticDraw);
                this.drawCmd = new DrawElementsCmd(indexBuffer, DrawMode.Triangles);
            }

            yield return this.drawCmd;
        }

        #endregion

    }
}
