using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using System.ComponentModel;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using BulletSharp;
using Blocky.Graphics;

namespace Blocky
{
	public class Game : GameWindow
	{
		public static Game Instance { get; private set; }
		
		static void Main()
		{
			try {
				Assembly.Load("OpenTK");
				Assembly.Load("BulletSharp");
			} catch { return; }
			new Game().Run(60.0);
		}
		
		Random _rnd = new Random();
		
		public Camera Camera { get; private set; }
		public Physics Physics { get; private set; }
		
		public Texture Texture { get; private set; }
		
		Game() : base(1280, 1024, new GraphicsMode(32, 24, 0, 0), "Blocky", GameWindowFlags.Fullscreen)
		{
			Instance = this;
			Camera = new Camera(){
				Location = new Vector3d(0, -2, 36),
				Rotation = new Vector2(0, 20) };
			Physics = new Physics();
			
			Texture = new Texture("terrain.png");
			Texture.SetMinFilter(TextureMinFilter.Nearest);
			Texture.SetMagFilter(TextureMagFilter.Nearest);
			
			new Props.Playground(32, Matrix4.CreateTranslation(0.0f, 0.0f, 0.0f));
			for (int x = 1; x <= 7; x++)
				for (int y = 1; y <= 7; y++) 
					for (int z = 1; z <= 7; z++)
						SpawnCube(1.0f, 1.0f, Matrix4.CreateTranslation(x*2-8.0f, y*2-1.0f, z*2-8.0f));
		}
		
		protected override void OnLoad(EventArgs e)
		{
			
		}
		
		protected override void OnClosing(CancelEventArgs e)
		{
			Texture.Dispose();
		}
		
		protected override void OnResize(EventArgs e)
		{
			GL.Viewport(ClientSize);
		}
		
		protected override void OnUpdateFrame(FrameEventArgs e)
		{
			Camera.MouseEnabled = Mouse[MouseButton.Right];
			Camera.MoveEnabled = Mouse[MouseButton.Right];
			
			if (Keyboard[Key.Space]) SpawnCube();
			
			Camera.Update(e.Time);
			Physics.Update(e.Time/3.5f);
			Camera.Rotation = Camera.Rotation + new Vector2(0.0f, (float)e.Time*0.95f);
			
			if (Keyboard[Key.Escape]) Exit();
		}
		
		public void SpawnCube()
		{
			SpawnCube(1.0f, 1.0f, Matrix4.Identity);
		}
		public void SpawnCube(float size, float mass, Matrix4 start)
		{
			Point[] block = Blocks.Data[_rnd.Next(0, Blocks.Data.Length)];
			RectangleD top    = new RectangleD(block[0].X / 16.0, block[0].Y / 16.0, 0.0625, 0.0625);
			RectangleD bottom = new RectangleD(block[1].X / 16.0, block[1].Y / 16.0, 0.0625, 0.0625);
			RectangleD right  = new RectangleD(block[2].X / 16.0, block[2].Y / 16.0, 0.0625, 0.0625);
			RectangleD left   = new RectangleD(block[3].X / 16.0, block[3].Y / 16.0, 0.0625, 0.0625);
			RectangleD front  = new RectangleD(block[4].X / 16.0, block[4].Y / 16.0, 0.0625, 0.0625);
			RectangleD back   = new RectangleD(block[5].X / 16.0, block[5].Y / 16.0, 0.0625, 0.0625);
			new Props.Cube(size, mass, start){
				Texture = Texture,
				Top = top, Bottom = bottom,
				Right = right, Left = left,
				Front = front, Back = back
			};
		}
		
		protected override void OnRenderFrame(FrameEventArgs e)
		{
			RenderWorld();
			RenderInterface();
			
			SwapBuffers();
		}
		
		public void RenderWorld()
		{
			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadIdentity();
			Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(
				(float)Math.PI/4, (float)Width/Height, 0.1f, 512f);
			GL.LoadMatrix(ref projection);
			GL.MatrixMode(MatrixMode.Modelview);
			GL.LoadIdentity();
			
			GL.ClearColor(Color4.Black);
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
			GL.Enable(EnableCap.DepthTest);
			GL.Enable(EnableCap.CullFace);
			GL.Enable(EnableCap.Normalize);
			GL.Enable(EnableCap.Lighting);
			GL.Enable(EnableCap.Light0);
			GL.Light(LightName.Light0, LightParameter.Ambient, new Color4(0.3f, 0.3f, 0.3f, 0.0f));
			GL.Light(LightName.Light0, LightParameter.Diffuse, new Color4(0.3f, 0.3f, 0.3f, 0.0f));
			GL.Enable(EnableCap.ColorMaterial);
			GL.ColorMaterial(MaterialFace.Front, ColorMaterialParameter.AmbientAndDiffuse);
			GL.Enable(EnableCap.Texture2D);
			GL.Enable(EnableCap.AlphaTest);
			GL.AlphaFunc(AlphaFunction.Greater, 0.5f);
			
			Camera.Render();
			GL.Light(LightName.Light0, LightParameter.Position, new Vector4(0.8f, 1.0f, 0.6f, 0.0f));
			foreach (Prop prop in Physics.Props)
				prop.Render();
		}
		
		public void RenderInterface()
		{
			
		}
	}
}
