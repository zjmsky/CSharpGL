﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSharpGL;

namespace c08d04_DrawModes
{
    partial class DrawModesNode : PickableNode, IRenderable
    {
        public DrawMode DrawMode
        {
            get
            {
                var method = this.RenderUnit.Methods[0];
                foreach (var vao in method.VertexArrayObjects)
                {
                    return vao.DrawCommand.Mode;
                }

                return CSharpGL.DrawMode.Patches;
            }
            set
            {
                var method = this.RenderUnit.Methods[0];
                foreach (var vao in method.VertexArrayObjects)
                {
                    vao.DrawCommand.Mode = value;
                }
            }
        }
        public static DrawModesNode Create(IBufferSource model, string position, string color, vec3 size)
        {
            RenderMethodBuilder builder;
            {
                var vs = new VertexShader(vertexCode);
                var fs = new FragmentShader(fragmentCode);
                var array = new ShaderArray(vs, fs);
                var map = new AttributeMap();
                map.Add("inPosition", position);
                map.Add("inColor", color);
                var pointSizeSwitch = new PointSizeSwitch(7);
                var lineWidthSwitch = new LineWidthSwitch(7);
                builder = new RenderMethodBuilder(array, map, pointSizeSwitch, lineWidthSwitch);
            }

            var node = new DrawModesNode(model, position, builder);
            node.Initialize();
            node.ModelSize = size;

            return node;
        }

        private DrawModesNode(IBufferSource model, string positionNameInIBufferSource, params RenderMethodBuilder[] builders)
            : base(model, positionNameInIBufferSource, builders)
        {
            this.EnableRendering = ThreeFlags.BeforeChildren | ThreeFlags.Children;
        }

        #region IRenderable 成员

        public ThreeFlags EnableRendering { get; set; }

        public void RenderBeforeChildren(RenderEventArgs arg)
        {
            ICamera camera = arg.Camera;
            mat4 projection = camera.GetProjectionMatrix();
            mat4 view = camera.GetViewMatrix();
            mat4 model = this.GetModelMatrix();

            var method = this.RenderUnit.Methods[0];
            ShaderProgram program = method.Program;
            program.SetUniform("mvpMatrix", projection * view * model);
            method.Render();
        }

        public void RenderAfterChildren(RenderEventArgs arg)
        {
        }

        #endregion
    }

}
