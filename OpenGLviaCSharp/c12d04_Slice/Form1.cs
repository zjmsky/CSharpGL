﻿using CSharpGL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace c12d04_Slice
{
    public partial class Form1 : Form
    {
        private Scene scene;
        private ActionList actionList;
        private SceneNodeBase rootNode;

        public Form1()
        {
            InitializeComponent();

            // init resources.
            this.Load += FormMain_Load;
            // render event.
            this.winGLCanvas1.OpenGLDraw += winGLCanvas1_OpenGLDraw;
            // resize event.
            this.winGLCanvas1.Resize += winGLCanvas1_Resize;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            var position = new vec3(5, 3, 4);
            var center = new vec3(0, 0, 0);
            var up = new vec3(0, 1, 0);
            var camera = new Camera(position, center, up, CameraType.Ortho, this.winGLCanvas1.Width, this.winGLCanvas1.Height);
            var scene = new Scene(camera);
            var rootNode = GetRootNode();
            scene.RootNode = rootNode;
            this.scene = scene;
            this.rootNode = rootNode;

            var list = new ActionList();
            var transformAction = new TransformAction(scene);
            list.Add(transformAction);
            var renderAction = new RenderAction(scene);
            list.Add(renderAction);
            this.actionList = list;

            // Enable manipualter of camera!
            var manipulater = new FirstPerspectiveManipulater();
            manipulater.BindingMouseButtons = GLMouseButtons.Right;
            manipulater.Bind(camera, this.winGLCanvas1);
        }

        private SceneNodeBase GetRootNode()
        {
            var rootNode = new GroupNode();
            {
                var axisNode = AxisNode.Create();
                axisNode.Scale = new vec3(1, 1, 1) * 2f;
                rootNode.Children.Add(axisNode);
            }
            {
                var cameraNode = CameraNode.Create();
                cameraNode.Scale = new vec3(1, 1, 1) * 1f;
                rootNode.Children.Add(cameraNode);
            }
            {
                var perspectiveNode = PerspectiveNode.Create();
                rootNode.Children.Add(perspectiveNode);
            }
            //{
            //    var cameraOutlineNode = CameraOutlineNode.Create();
            //    rootNode.Children.Add(cameraOutlineNode);
            //}
            //{
            //    var groundNode = GroundNode.Create();
            //    rootNode.Children.Add(groundNode);
            //}
            {
                var sliceNode = SliceNode.Create();
                sliceNode.WorldPosition = new vec3(0, 0, -7);
                sliceNode.Scale = new vec3(1, 1, 1) * 7;
                rootNode.Children.Add(sliceNode);
            }
            {
                var cubeNode = CubeNode.Create();
                cubeNode.WorldPosition = new vec3(0, 0, -7);
                cubeNode.RotationAngle = 35;
                cubeNode.RotationAxis = new vec3(1, 1, 1);
                rootNode.Children.Add(cubeNode);
            }
            {
                var groundNode = GroundNode.Create();
                groundNode.Scale = new vec3(1, 1, 1) * 0.5f;
                groundNode.WorldPosition = new vec3(0, 0, -7);
                groundNode.RotationAngle = 90;
                groundNode.RotationAxis = new vec3(1, 0, 0);
                rootNode.Children.Add(groundNode);
            }

            return rootNode;
        }

        private void winGLCanvas1_OpenGLDraw(object sender, PaintEventArgs e)
        {
            ActionList list = this.actionList;
            if (list != null)
            {
                vec4 clearColor = this.scene.ClearColor;
                GL.Instance.ClearColor(clearColor.x, clearColor.y, clearColor.z, clearColor.w);
                GL.Instance.Clear(GL.GL_COLOR_BUFFER_BIT | GL.GL_DEPTH_BUFFER_BIT | GL.GL_STENCIL_BUFFER_BIT);

                Viewport viewport = Viewport.GetCurrent();
                list.Act(new ActionParams(viewport));
            }
        }

        void winGLCanvas1_Resize(object sender, EventArgs e)
        {
            this.scene.Camera.AspectRatio = ((float)this.winGLCanvas1.Width) / ((float)this.winGLCanvas1.Height);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.rootNode.RotationAxis = new vec3(0, 1, 0);
            this.rootNode.RotationAngle += 1f;
        }
    }
}