using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using BulletSharp;
using Blocky.Graphics;

namespace Blocky.Props
{
	public class Cube : Prop
	{
		public float Size { get; private set; }
		public Texture Texture { get; set; }
		
		public RectangleD Top { get; set; }
		public RectangleD Bottom { get; set; }
		public RectangleD Right { get; set; }
		public RectangleD Left { get; set; }
		public RectangleD Front { get; set; }
		public RectangleD Back { get; set; }
		
		public Cube(Matrix4 start)
			: this(1.0f, start) {  }
		public Cube(float size, Matrix4 start)
			: this(size, (float)Math.Pow(size, 3), start) {  }
		public Cube(float size, float mass, Matrix4 start)
			: base(new BoxShape(size), mass, start)
		{
			Size = size;
		}
		
		public override void Render()
		{
			GL.PushMatrix();
			Matrix4 matrix = State.WorldTransform;
			GL.MultMatrix(ref matrix);
			
			Draw3D.TexturedCube(Size, Top, Bottom, Right, Left, Front, Back);
			GL.PopMatrix();
		}
	}
}
