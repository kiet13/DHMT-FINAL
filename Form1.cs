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
            public int Index { get; set; }
            public bool IsSelected { get; set; }
            public List<Quad> Quads {get; set;}
            public Color Color { get; set; }
            public Vertex Center { get; set; }

            public Object3D()
            {
                IsSelected = false;
                Quads = new List<Quad>();
            }

            public void Draw(OpenGL gl)
            {
                
                gl.Color((float)Color.R / 255,
                    (float)Color.G / 255,
                    (float)Color.B / 255);

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
                {
                    gl.LineWidth(2);
                    gl.Color(1.0f, 0.5f, 0);
                }
                else
                {
                    gl.LineWidth(1);
                    gl.Color(0.0f, 0.0f, 0.0f, 0.5f);
                }
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

            public void Translate(OpenGL gl, 
                float newX, float newY, float newZ)
            {
                Vertex moveDirection = new Vertex
                {
                    X = newX - Center.X,
                    Y = newY - Center.Y,
                    Z = newZ - Center.Z
                };

                Center.X = newX;
                Center.Y = newY;
                Center.Z = newZ;

                for (int i = 0; i < Quads.Count; i++)
                {
                    for (int j = 0; j < Quads[i].Vertices.Count; j++)
                    {
                        Quads[i].Vertices[j].X += moveDirection.X;
                        Quads[i].Vertices[j].Y += moveDirection.Y;
                        Quads[i].Vertices[j].Z += moveDirection.Z;
                    }
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
                Vertex vertex1 = new Vertex { X = -half_a, Y = -half_a, Z = -half_a };
                Vertex vertex2 = new Vertex { X = half_a, Y = -half_a, Z = -half_a };
                Vertex vertex3 = new Vertex { X = half_a, Y = -half_a, Z = half_a };
                Vertex vertex4 = new Vertex { X = -half_a, Y = -half_a, Z = half_a };
                List<Vertex> vertices = new List<Vertex> {
                    vertex1, vertex2, vertex3, vertex4 };

                quads[0].Vertices = vertices;
                Quads.Add(quads[0]);

                // Left
                vertex1 = new Vertex { X = -half_a, Y = -half_a, Z = -half_a };
                vertex2 = new Vertex { X = -half_a, Y = half_a, Z = -half_a };
                vertex3 = new Vertex { X = half_a, Y = half_a, Z = -half_a };
                vertex4 = new Vertex { X = half_a, Y = -half_a, Z = -half_a };
                vertices = new List<Vertex> {
                    vertex1, vertex2, vertex3, vertex4 };

                quads[1].Vertices = vertices;
                Quads.Add(quads[1]);

                // Back
                vertex1 = new Vertex { X = -half_a, Y = -half_a, Z = -half_a };
                vertex2 = new Vertex { X = -half_a, Y = half_a, Z = -half_a };
                vertex3 = new Vertex { X = -half_a, Y = half_a, Z = half_a};
                vertex4 = new Vertex { X = -half_a, Y = -half_a, Z = half_a};
                vertices = new List<Vertex> {
                    vertex1, vertex2, vertex3, vertex4 };

                quads[2].Vertices = vertices;
                Quads.Add(quads[2]);

                // Right
                vertex1 = new Vertex { X = -half_a, Y = half_a, Z = half_a };
                vertex2 = new Vertex { X = half_a, Y = half_a, Z = half_a };
                vertex3 = new Vertex { X = half_a, Y = -half_a, Z = half_a };
                vertex4 = new Vertex { X = -half_a, Y = -half_a, Z = half_a };
                vertices = new List<Vertex> {
                    vertex1, vertex2, vertex3, vertex4 };

                quads[3].Vertices = vertices;
                Quads.Add(quads[3]);

                // Front
                vertex1 = new Vertex { X = half_a, Y = half_a, Z = half_a };
                vertex2 = new Vertex { X = half_a, Y = half_a, Z = -half_a };
                vertex3 = new Vertex { X = half_a, Y = -half_a, Z = -half_a };
                vertex4 = new Vertex { X = half_a, Y = -half_a, Z = half_a };
                vertices = new List<Vertex> {
                    vertex1, vertex2, vertex3, vertex4 };

                quads[4].Vertices = vertices;
                Quads.Add(quads[4]);

                // Top
                vertex1 = new Vertex { X = -half_a, Y = half_a, Z = -half_a };
                vertex2 = new Vertex { X = half_a, Y = half_a, Z = -half_a };
                vertex3 = new Vertex { X = half_a, Y = half_a, Z = half_a };
                vertex4 = new Vertex { X = -half_a, Y = half_a, Z = half_a };
                vertices = new List<Vertex> {
                    vertex1, vertex2, vertex3, vertex4 };

                quads[5].Vertices = vertices;
                Quads.Add(quads[5]);

            }

            public override string ToString()
            {
                return "Cube " + Index;
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
                float half_height = height / 2;

                Vertex vertex1, vertex2, vertex3, vertex4;
                List<Vertex> vertices;

                // Left
                vertex1 = new Vertex { X = 0.0f, Y = half_height, Z = 0.0f };
                vertex2 = new Vertex { X = -half_a, Y = -half_height, Z = -half_a };
                vertex3 = new Vertex { X = half_a, Y = -half_height, Z = -half_a };

                vertices = new List<Vertex> { vertex1, vertex2, vertex3 };

                quads[0].Vertices = vertices;
                Quads.Add(quads[0]);

                // Back
                vertex1 = new Vertex { X = 0.0f, Y = half_height, Z = 0.0f };
                vertex2 = new Vertex { X = -half_a, Y = -half_height, Z = half_a };
                vertex3 = new Vertex { X = -half_a, Y = -half_height, Z = -half_a };

                vertices = new List<Vertex> { vertex1, vertex2, vertex3 };

                quads[1].Vertices = vertices;
                Quads.Add(quads[1]);

                // Right
                vertex1 = new Vertex { X = 0.0f, Y = half_height, Z = 0.0f };
                vertex2 = new Vertex { X = half_a, Y = -half_height, Z = half_a };
                vertex3 = new Vertex { X = -half_a, Y = -half_height, Z = half_a };

                vertices = new List<Vertex> { vertex1, vertex2, vertex3 };

                quads[2].Vertices = vertices;
                Quads.Add(quads[2]);

                // Front
                vertex1 = new Vertex { X = 0.0f, Y = half_height, Z = 0.0f };
                vertex2 = new Vertex { X = half_a, Y = -half_height, Z = -half_a };
                vertex3 = new Vertex { X = half_a, Y = -half_height, Z = half_a };

                vertices = new List<Vertex> { vertex1, vertex2, vertex3 };

                quads[3].Vertices = vertices;
                Quads.Add(quads[3]);

                // Bottom
                vertex1 = new Vertex { X = -half_a, Y = -half_height, Z = -half_a };
                vertex2 = new Vertex { X = half_a, Y = -half_height, Z = -half_a };
                vertex3 = new Vertex { X = half_a, Y = -half_height, Z = half_a };
                vertex4 = new Vertex { X = -half_a, Y = -half_height, Z = half_a };
                vertices = new List<Vertex> {
                    vertex1, vertex2, vertex3, vertex4 };

                quads[4].Vertices = vertices;
                Quads.Add(quads[4]);

            }

            public override string ToString()
            {
                return "Pyramid " + Index;
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
                float half_height = height / 2;


                Vertex vertex1, vertex2, vertex3, vertex4;
                List<Vertex> vertices;

               

                // Front left

                vertex1 = new Vertex { X = b, Y = -half_height, Z = c };
                vertex2 = new Vertex { X = a, Y = -half_height, Z = a };
                vertex3 = new Vertex { X = a, Y = half_height, Z = a };
                vertex4 = new Vertex { X = b, Y = half_height, Z = c };

                vertices = new List<Vertex> { vertex1, vertex2, vertex3, vertex4 };

                quads[0].Vertices = vertices;
                Quads.Add(quads[0]);

                // Front right
                vertex1 = new Vertex { X = a, Y = -half_height, Z = a };
                vertex2 = new Vertex { X = c, Y = -half_height, Z = b };
                vertex3 = new Vertex { X = c, Y = half_height, Z = b };
                vertex4 = new Vertex { X = a, Y = half_height, Z = a };

                vertices = new List<Vertex> { vertex1, vertex2, vertex3, vertex4 };

                quads[1].Vertices = vertices;
                Quads.Add(quads[1]);

                // Back
                vertex1 = new Vertex { X = b, Y = -half_height, Z = c };
                vertex2 = new Vertex { X = c, Y = -half_height, Z = b };
                vertex3 = new Vertex { X = c, Y = half_height, Z = b };
                vertex4 = new Vertex { X = b, Y = half_height, Z = c };

                vertices = new List<Vertex> { vertex1, vertex2, vertex3, vertex4 };

                quads[2].Vertices = vertices;
                Quads.Add(quads[2]);

                // Bottom
                vertex1 = new Vertex { X = b, Y = -half_height, Z = c };
                vertex2 = new Vertex { X = a, Y = -half_height, Z = a };
                vertex3 = new Vertex { X = c, Y = -half_height, Z = b };

                vertices = new List<Vertex> { vertex1, vertex2, vertex3 };

                quads[3].Vertices = vertices;
                Quads.Add(quads[3]);

                // Top
                vertex1 = new Vertex { X = b, Y = half_height, Z = c };
                vertex2 = new Vertex { X = a, Y = half_height, Z = a };
                vertex3 = new Vertex { X = c, Y = half_height, Z = b };

                vertices = new List<Vertex> { vertex1, vertex2, vertex3 };

                quads[4].Vertices = vertices;
                Quads.Add(quads[4]);
            }

            public override string ToString()
            {
                return "Prism " + Index;

            }
        }

        /* Global variable */
        bool isDraw = false;
        float sizeEdge = 0.0f;
        float height = 0.0f;
        Color userColor;
        int numCube = 0, numPyramid = 0, numPrism = 0;
        List<Object3D> objects = new List<Object3D>();
        int selectedIndex = -1;

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
                    Cube cube = new Cube(sizeEdge)
                    {
                        Index = numCube,
                        Color = userColor,
                        Center = new Vertex { X = 0.0f, Y = 0.0f, Z = 0.0f }
                    };

                    lstObject.Items.Add(cube);
                    objects.Add(cube);
                    numCube++;
                }
                // Pyramid
                else if (cboObject.SelectedIndex == 1)
                {
                    Pyramid pyramid = new Pyramid(sizeEdge, height)
                    {
                        Index = numPyramid,
                        Color = userColor,
                        Center = new Vertex { X = 0.0f, Y = 0.0f, Z = 0.0f}
                    };

                    lstObject.Items.Add(pyramid);
                    objects.Add(pyramid);
                    numPyramid++;
                }
                // Triagular prism
                else if (cboObject.SelectedIndex == 2)
                {
                    Prism prism = new Prism(sizeEdge, height)
                    {
                        Index = numPrism,
                        Color = userColor,
                        Center = new Vertex { X = 0.0f, Y = 0.0f, Z = 0.0f }
                    };
                    lstObject.Items.Add(prism);
                    objects.Add(prism);
                    numPrism++;
                }
                
                isDraw = false;
            }
        }

        void RedrawScreen(OpenGL gl)
        {
            foreach (Object3D object3D in lstObject.Items)
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

            txtSizeEdge.Text = txtHeight.Text = "";
        }

        private void cboObject_SelectedIndexChanged(object sender, EventArgs e)
        {
            

        }

        private void lstObject_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selectedIndex != -1)
                objects[selectedIndex].IsSelected = false; // Old select object

            int idx = lstObject.SelectedIndex;
            objects[idx].IsSelected = true;
            selectedIndex = idx;
            txtPositionX.Text = objects[idx].Center.X.ToString();
            txtPositionY.Text = objects[idx].Center.Y.ToString();
            txtPositionZ.Text = objects[idx].Center.Z.ToString();
        }

        private void txtPositionX_TextChanged(object sender, EventArgs e)
        {
            OpenGL gl = openGLControl.OpenGL;
            if (selectedIndex != -1)
            {
                bool success = float.TryParse(txtPositionX.Text, out float newX);
                if (success == true)
                {
                    int idx = lstObject.SelectedIndex;
                    float newY = objects[idx].Center.Y;
                    float newZ = objects[idx].Center.Z;
                    objects[idx].Translate(gl, newX, newY, newZ);
                }
            }
        }

        private void txtPositionY_TextChanged(object sender, EventArgs e)
        {
            OpenGL gl = openGLControl.OpenGL;
            if (selectedIndex != -1)
            {
                bool success = float.TryParse(txtPositionY.Text, out float newY);
                if (success == true)
                {
                    int idx = lstObject.SelectedIndex;
                    float newX = objects[idx].Center.X;
                    float newZ = objects[idx].Center.Z;
                    objects[idx].Translate(gl, newX, newY, newZ);
                }
            }
        }

        private void txtPositionZ_TextChanged(object sender, EventArgs e)
        {
            OpenGL gl = openGLControl.OpenGL;
            if (selectedIndex != -1)
            {
                bool success = float.TryParse(txtPositionZ.Text, out float newZ);
                if (success == true)
                {
                    int idx = lstObject.SelectedIndex;
                    float newX = objects[idx].Center.X;
                    float newY = objects[idx].Center.Y;
                    objects[idx].Translate(gl, newX, newY, newZ);
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            numCube = numPyramid = numPrism = 0;
            lstObject.Items.Clear();
            objects.Clear();

            txtSizeEdge.Text = txtHeight.Text = "";
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
