using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using BulletSharp;
using Blocky.Graphics;

namespace Blocky.Props
{
	public class Playground : Prop
	{
		static CollisionShape CreateShape(float size)
		{
			CompoundShape shape = new CompoundShape();
			shape.AddChildShape(Matrix4.CreateTranslation(-size, 0.0f, 0.0f), new StaticPlaneShape(new Vector3(1.0f, 0.0f, 0.0f), 0.0f));
			shape.AddChildShape(Matrix4.CreateTranslation(size, 0.0f, 0.0f), new StaticPlaneShape(new Vector3(-1.0f, 0.0f, 0.0f), 0.0f));
			shape.AddChildShape(Matrix4.CreateTranslation(0.0f, -size, 0.0f), new StaticPlaneShape(new Vector3(0.0f, 1.0f, 0.0f), 0.0f));
			shape.AddChildShape(Matrix4.CreateTranslation(0.0f, size, 0.0f), new StaticPlaneShape(new Vector3(0.0f, -1.0f, 0.0f), 0.0f));
			shape.AddChildShape(Matrix4.CreateTranslation(0.0f, 0.0f, -size), new StaticPlaneShape(new Vector3(0.0f, 0.0f, 1.0f), 0.0f));
			shape.AddChildShape(Matrix4.CreateTranslation(0.0f, 0.0f, size), new StaticPlaneShape(new Vector3(0.0f, 0.0f, -1.0f), 0.0f));
			return shape;
		}
		
		public float Size { get; private set; }
		
		public Playground(float size, Matrix4 start)
			: base(CreateShape(size), 0.0f, start)
		{
			Size = size;
			Body.Friction = 1.0f;
		}
		
		public override void Render() {  }
	}
}
