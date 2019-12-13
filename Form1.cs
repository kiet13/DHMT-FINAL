using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpGL;
using SharpGL.WinForms;

namespace DHMT_CK
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        class Vertex
        {
            public float X { get; set; }
            public float Y { get; set; }
            public float Z { get; set; }
        }

        class Quad
        {
            public List<Vertex> Vertices { get; set; }
        }

        class Object3D
        {
            public bool IsSelected { get; set; }
            public List<Quad> Quads {get; set;}

            public Object3D()
            {
                IsSelected = false;
                Quads = new List<Quad>();
            }

            public void Draw(OpenGL gl)
            {
                gl.Color(0.8f, 0.9f, 1.0f, 0.5f);

                gl.Begin(OpenGL.GL_QUADS);
                for (int i = 0; i < Quads.Count; i++)
                {
                    for (int j = 0; j < Quads[i].Vertices.Count; j++)
                    {
                        float X = Quads[i].Vertices[j].X;
                        float Y = Quads[i].Vertices[j].Y;
                        float Z = Quads[i].Vertices[j].Z;
                        gl.Vertex(X, Y, Z);
                    }
                }
                gl.End();
                gl.Flush();

                // Draw boundary;
                if (this.IsSelected == true)
                    gl.Color(1.0f, 0.5f, 0);
                else
                    gl.Color(0.25f, 0.25f, 0.25f);

                for (int i = 0; i < Quads.Count; i++)
                {
                    gl.Begin(OpenGL.GL_LINE_LOOP);
                    for (int j = 0; j < Quads[i].Vertices.Count; j++)
                    {
                        float X = Quads[i].Vertices[j].X;
                        float Y = Quads[i].Vertices[j].Y;
                        float Z = Quads[i].Vertices[j].Z;
                        gl.Vertex(X, Y, Z);
                    }
                    gl.End();
                    gl.Flush();
                }
            }
        
        }

        class Cube : Object3D
        {             
            public Cube(float a)
            {
                Quad[] quads = new Quad[6];
                for (int i = 0; i < 6; i++)
                    quads[i] = new Quad();
                float half_a = a / 2;

                // Bottom
                Vertex vertex1 = new Vertex { X = 0.0f, Y = 0.0f, Z = 0.0f };
                Vertex vertex2 = new Vertex { X = a, Y = 0.0f, Z = 0.0f };
                Vertex vertex3 = new Vertex { X = a, Y = 0.0f, Z = a };
                Vertex vertex4 = new Vertex { X = 0.0f, Y = 0.0f, Z = a };
                List<Vertex> vertices = new List<Vertex> {
                    vertex1, vertex2, vertex3, vertex4 };

                quads[0].Vertices = vertices;
                Quads.Add(quads[0]);

                // Left
                vertex1 = new Vertex { X = 0.0f, Y = 0.0f, Z = 0.0f };
                vertex2 = new Vertex { X = 0.0f, Y = a, Z = 0.0f };
                vertex3 = new Vertex { X = a, Y = a, Z = 0.0f };
                vertex4 = new Vertex { X = a, Y = 0.0f, Z = 0.0f };
                vertices = new List<Vertex> {
                    vertex1, vertex2, vertex3, vertex4 };

                quads[1].Vertices = vertices;
                Quads.Add(quads[1]);

                // Back
                vertex1 = new Vertex { X = 0.0f, Y = 0.0f, Z = 0.0f };
                vertex2 = new Vertex { X = 0.0f, Y = a, Z = 0.0f };
                vertex3 = new Vertex { X = 0.0f, Y = a, Z = a};
                vertex4 = new Vertex { X = 0.0f, Y = 0.0f, Z = a};
                vertices = new List<Vertex> {
                    vertex1, vertex2, vertex3, vertex4 };

                quads[2].Vertices = vertices;
                Quads.Add(quads[2]);

                // Right
                vertex1 = new Vertex { X = 0.0f, Y = a, Z = a };
                vertex2 = new Vertex { X = a, Y = a, Z = a };
                vertex3 = new Vertex { X = a, Y = 0.0f, Z = a };
                vertex4 = new Vertex { X = 0.0f, Y = 0.0f, Z = a };
                vertices = new List<Vertex> {
                    vertex1, vertex2, vertex3, vertex4 };

                quads[3].Vertices = vertices;
                Quads.Add(quads[3]);

                // Front
                vertex1 = new Vertex { X = a, Y = a, Z = a };
                vertex2 = new Vertex { X = a, Y = a, Z = 0.0f };
                vertex3 = new Vertex { X = a, Y = 0.0f, Z = 0.0f };
                vertex4 = new Vertex { X = a, Y = 0.0f, Z = a };
                vertices = new List<Vertex> {
                    vertex1, vertex2, vertex3, vertex4 };

                quads[4].Vertices = vertices;
                Quads.Add(quads[4]);

                // Top
                vertex1 = new Vertex { X = 0.0f, Y = a, Z = 0.0f };
                vertex2 = new Vertex { X = a, Y = a, Z = 0.0f };
                vertex3 = new Vertex { X = a, Y = a, Z = a };
                vertex4 = new Vertex { X = 0.0f, Y = a, Z = a };
                vertices = new List<Vertex> {
                    vertex1, vertex2, vertex3, vertex4 };

                quads[5].Vertices = vertices;
                Quads.Add(quads[5]);

            }               
        }

        class Pyramid : Object3D
        {
            public Pyramid(float a, float height)
            {
                Quad[] quads = new Quad[5];
                for (int i = 0; i < 5; i++)
                    quads[i] = new Quad();
                float half_a = a / 2;

                Vertex vertex1, vertex2, vertex3, vertex4;
                List<Vertex> vertices;

                // Left
                vertex1 = new Vertex { X = 0.0f, Y = height, Z = 0.0f };
                vertex2 = new Vertex { X = -half_a, Y = 0.0f, Z = -half_a };
                vertex3 = new Vertex { X = half_a, Y = 0.0f, Z = -half_a };

                vertices = new List<Vertex> { vertex1, vertex2, vertex3 };

                quads[0].Vertices = vertices;
                Quads.Add(quads[0]);

                // Back
                vertex1 = new Vertex { X = 0.0f, Y = height, Z = 0.0f };
                vertex2 = new Vertex { X = -half_a, Y = 0.0f, Z = half_a };
                vertex3 = new Vertex { X = -half_a, Y = 0.0f, Z = -half_a };

                vertices = new List<Vertex> { vertex1, vertex2, vertex3 };

                quads[1].Vertices = vertices;
                Quads.Add(quads[1]);

                // Right
                vertex1 = new Vertex { X = 0.0f, Y = height, Z = 0.0f };
                vertex2 = new Vertex { X = half_a, Y = 0.0f, Z = half_a };
                vertex3 = new Vertex { X = -half_a, Y = 0.0f, Z = half_a };

                vertices = new List<Vertex> { vertex1, vertex2, vertex3 };

                quads[2].Vertices = vertices;
                Quads.Add(quads[2]);

                // Front
                vertex1 = new Vertex { X = 0.0f, Y = height, Z = 0.0f };
                vertex2 = new Vertex { X = half_a, Y = 0.0f, Z = -half_a };
                vertex3 = new Vertex { X = half_a, Y = 0.0f, Z = half_a };

                vertices = new List<Vertex> { vertex1, vertex2, vertex3 };

                quads[3].Vertices = vertices;
                Quads.Add(quads[3]);

                // Bottom
                vertex1 = new Vertex { X = -half_a, Y = 0.0f, Z = -half_a };
                vertex2 = new Vertex { X = half_a, Y = 0.0f, Z = -half_a };
                vertex3 = new Vertex { X = half_a, Y = 0.0f, Z = half_a };
                vertex4 = new Vertex { X = -half_a, Y = 0.0f, Z = half_a };
                vertices = new List<Vertex> {
                    vertex1, vertex2, vertex3, vertex4 };

                quads[4].Vertices = vertices;
                Quads.Add(quads[4]);

            }
        }


        class Prism : Object3D
        {
            public Prism(float size, float height)
            {
                Quad[] quads = new Quad[5];
                for (int i = 0; i < 5; i++)
                    quads[i] = new Quad();

                float a, b, c;
                a  = size * (float)Math.Sqrt(6) / 6;
                b = size * (float)(-Math.Sqrt(6) + 3 * Math.Sqrt(2)) / 12;
                c = size * (float)(-Math.Sqrt(6) - 3 * Math.Sqrt(2)) / 12;

                Vertex vertex1, vertex2, vertex3, vertex4;
                List<Vertex> vertices;

               

                // Front left

                vertex1 = new Vertex { X = b, Y = 0.0f, Z = c };
                vertex2 = new Vertex { X = a, Y = 0.0f, Z = a };
                vertex3 = new Vertex { X = a, Y = height, Z = a };
                vertex4 = new Vertex { X = b, Y = height, Z = c };

                vertices = new List<Vertex> { vertex1, vertex2, vertex3, vertex4 };

                quads[0].Vertices = vertices;
                Quads.Add(quads[0]);

                // Front right
                vertex1 = new Vertex { X = a, Y = 0.0f, Z = a };
                vertex2 = new Vertex { X = c, Y = 0.0f, Z = b };
                vertex3 = new Vertex { X = c, Y = height, Z = b };
                vertex4 = new Vertex { X = a, Y = height, Z = a };

                vertices = new List<Vertex> { vertex1, vertex2, vertex3, vertex4 };

                quads[1].Vertices = vertices;
                Quads.Add(quads[1]);

                // Back
                vertex1 = new Vertex { X = b, Y = 0.0f, Z = c };
                vertex2 = new Vertex { X = c, Y = 0.0f, Z = b };
                vertex3 = new Vertex { X = c, Y = height, Z = b };
                vertex4 = new Vertex { X = b, Y = height, Z = c };

                vertices = new List<Vertex> { vertex1, vertex2, vertex3, vertex4 };

                quads[2].Vertices = vertices;
                Quads.Add(quads[2]);

                // Bottom
                vertex1 = new Vertex { X = b, Y = 0.0f, Z = c };
                vertex2 = new Vertex { X = a, Y = 0.0f, Z = a };
                vertex3 = new Vertex { X = c, Y = 0.0f, Z = b };

                vertices = new List<Vertex> { vertex1, vertex2, vertex3 };

                quads[3].Vertices = vertices;
                Quads.Add(quads[3]);

                // Top
                vertex1 = new Vertex { X = b, Y = height, Z = c };
                vertex2 = new Vertex { X = a, Y = height, Z = a };
                vertex3 = new Vertex { X = c, Y = height, Z = b };

                vertices = new List<Vertex> { vertex1, vertex2, vertex3 };

                quads[4].Vertices = vertices;
                Quads.Add(quads[4]);
            }
        }

        /* Global variable */
        List<Object3D> objects = new List<Object3D>();
        bool isDraw = false;
        float sizeEdge = 0.0f;
        float height = 0.0f;
        Color userColor;

        private void openGLControl_OpenGLInitialized(object sender, EventArgs e)
        {
            OpenGL gl = openGLControl.OpenGL;
            gl.Enable(OpenGL.GL_BLEND);
            gl.BlendFunc(OpenGL.GL_SRC_ALPHA, 
                OpenGL.GL_ONE_MINUS_SRC_ALPHA);
            gl.ClearColor(0, 0, 0, 0);
        }

        private void openGLControl_Resized(object sender, EventArgs e)
        {
            OpenGL gl = openGLControl.OpenGL;

            //set ma tran viewport
            gl.Viewport(0, 0, openGLControl.Width, openGLControl.Height);

            //set ma tran phep chieu
            gl.MatrixMode(OpenGL.GL_PROJECTION);

            gl.Perspective(60,
           openGLControl.Width / openGLControl.Height,
               1.0, 20.0);

            //set ma tran model view
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            gl.LookAt(
                9, 8, 12,
                0, 0, 0,
                0, 1, 0);
        }

        private void openGLControl_OpenGLDraw(object sender, RenderEventArgs args)
        {
            OpenGL gl = openGLControl.OpenGL;
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            RedrawScreen(gl);
          
            if (isDraw == true)
            {
                // Cube
                if (cboObject.SelectedIndex == 0)
                {
                    
                    Cube cube = new Cube(sizeEdge);
                    objects.Add(cube);
                }
                // Pyramid
                else if (cboObject.SelectedIndex == 1)
                {
                    Pyramid pyramid = new Pyramid(sizeEdge, height);
                    objects.Add(pyramid);
                }
                // Triagular prism
                else if (cboObject.SelectedIndex == 2)
                {
                    Prism prism = new Prism(sizeEdge, height);
                    objects.Add(prism);
                }
                
                isDraw = false;
            }
        }

        void RedrawScreen(OpenGL gl)
        {
            foreach (Object3D object3D in objects)
                object3D.Draw(gl);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] objectNames = { "Cube", "Pyramid", "Triagular prism" };
            cboObject.Items.AddRange(objectNames);
            cboObject.SelectedIndex = 0;

            userColor = Color.White;
            btnColor.BackColor = userColor;
        }

        private void btnDraw_Click(object sender, EventArgs e)
        {
            isDraw = true;
            bool success;
            success = float.TryParse(txtSizeEdge.Text, out sizeEdge);

            if (success == false)
                MessageBox.Show("Invalid size!!!", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            if (cboObject.SelectedIndex != 0)
            {
                success = float.TryParse(txtHeight.Text, out height);

                if (success == false)
                    MessageBox.Show("Invalid height!!!", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void cboObject_SelectedIndexChanged(object sender, EventArgs e)
        {
            

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            objects.Clear();
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                userColor = colorDialog1.Color;
                btnColor.BackColor = userColor;
            }
        }
    }
}
