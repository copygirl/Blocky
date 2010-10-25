using System;
using System.Drawing;
using OpenTK.Graphics.OpenGL;

namespace Blocky.Graphics
{
	public static class Draw
	{
		public static void Rectangle(Rectangle rect)
		{
			Rectangle(rect.X, rect.Y, rect.Width, rect.Height);
		}
		public static void Rectangle(int x, int y, int width, int height)
		{
			GL.Begin(BeginMode.Quads);
			GL.Vertex3(x + width, y, 0.0f);
			GL.Vertex3(x, y, 0.0f);
			GL.Vertex3(x, y + height, 0.0f);
			GL.Vertex3(x + width, y + height, 0.0f);
			GL.End();
		}
		
		public static void Texture(int x, int y, Texture tex)
		{
			Texture(x, y, tex.Width, tex.Height, tex);
		}
		
		public static void Texture(int x, int y, int width, int height, Texture tex)
		{
			GL.BindTexture(TextureTarget.Texture2D, tex.ID);
			GL.Begin(BeginMode.Quads);
			GL.TexCoord2(1.0, 0.0); GL.Vertex2(x + width, y);
			GL.TexCoord2(1.0, 1.0); GL.Vertex2(x + width, y + height);
			GL.TexCoord2(0.0, 1.0); GL.Vertex2(x, y + height);
			GL.TexCoord2(0.0, 0.0); GL.Vertex2(x, y);
			GL.End();
		}
	}
}
