using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Blocky.Graphics
{
	public static class Draw3D
	{
		public static void Cube(float size)
		{
			GL.Begin(BeginMode.Quads);
			
			GL.Normal3(0.0f, 1.0f, 0.0f);
			GL.Vertex3( size, size, -size);
			GL.Vertex3(-size, size, -size);
			GL.Vertex3(-size, size,  size);
			GL.Vertex3( size, size,  size);
			
			GL.Normal3(0.0f, -1.0f, 0.0f);
			GL.Vertex3(-size, -size,  size);
			GL.Vertex3(-size, -size, -size);
			GL.Vertex3( size, -size, -size);
			GL.Vertex3( size, -size,  size);
			
			GL.Normal3(1.0f, 0.0f, 0.0f);
			GL.Vertex3(size,  size,  size);
			GL.Vertex3(size,  size, -size);
			GL.Vertex3(size, -size, -size);
			GL.Vertex3(size, -size,  size);
			
			GL.Normal3(-1.0f, 0.0f, 0.0f);
			GL.Vertex3(-size,  size, -size);
			GL.Vertex3(-size,  size,  size);
			GL.Vertex3(-size, -size,  size);
			GL.Vertex3(-size, -size, -size);
			
			GL.Normal3(0.0f, 0.0f, 1.0f);
			GL.Vertex3(-size,  size, size);
			GL.Vertex3( size,  size, size);
			GL.Vertex3( size, -size, size);
			GL.Vertex3(-size, -size, size);
			
			GL.Normal3(0.0f, 0.0f, -1.0f);
			GL.Vertex3( size,  size, -size);
			GL.Vertex3(-size,  size, -size);
			GL.Vertex3(-size, -size, -size);
			GL.Vertex3( size, -size, -size);
			
			GL.End();
		}
		public static void InvertedCube(float size)
		{
			GL.Begin(BeginMode.Quads);
			
			GL.Normal3(0.0f, 1.0f, 0.0f);
			GL.Vertex3(size, -size, size);
			GL.Vertex3(size, -size, -size);
			GL.Vertex3(-size, -size, -size);
			GL.Vertex3(-size, -size, size);
			
			GL.Normal3(0.0f, -1.0f, 0.0f);
			GL.Vertex3(size, size, -size);
			GL.Vertex3(size, size, size);
			GL.Vertex3(-size, size, size);
			GL.Vertex3(-size, size, -size);
			
			GL.Normal3(1.0f, 0.0f, 0.0f);
			GL.Vertex3(-size, -size, size);
			GL.Vertex3(-size, -size, -size);
			GL.Vertex3(-size, size, -size);
			GL.Vertex3(-size, size, size);
			
			GL.Normal3(-1.0f, 0.0f, 0.0f);
			GL.Vertex3(size, -size, size);
			GL.Vertex3(size, size, size);
			GL.Vertex3(size, size, -size);
			GL.Vertex3(size, -size, -size);
			
			GL.Normal3(0.0f, 0.0f, 1.0f);
			GL.Vertex3(-size, -size, -size);
			GL.Vertex3(size, -size, -size);
			GL.Vertex3(size, size, -size);
			GL.Vertex3(-size, size, -size);
			
			GL.Normal3(0.0f, 0.0f, -1.0f);
			GL.Vertex3(size, -size, size);
			GL.Vertex3(-size, -size, size);
			GL.Vertex3(-size, size, size);
			GL.Vertex3(size, size, size);
			
			GL.End();
		}
		
		public static void TexturedCube(float size)
		{
			TexturedCube(size, new RectangleD(0.0, 0.0, 1.0, 1.0));
		}
		public static void TexturedCube(float size, RectangleD sides)
		{
			TexturedCube(size, sides, sides, sides, sides, sides, sides);
		}
		public static void TexturedCube(float size,
		                                RectangleD top, RectangleD bottom,
		                                RectangleD right, RectangleD left,
		                                RectangleD front, RectangleD back)
		{
			GL.Begin(BeginMode.Quads);
			
			GL.Normal3(0.0f, 1.0f, 0.0f);
			GL.TexCoord2(top.Right, top.Top);    GL.Vertex3( size, size, -size);
			GL.TexCoord2(top.Left,  top.Top);    GL.Vertex3(-size, size, -size);
			GL.TexCoord2(top.Left,  top.Bottom); GL.Vertex3(-size, size,  size);
			GL.TexCoord2(top.Right, top.Bottom); GL.Vertex3( size, size,  size);
			
			GL.Normal3(0.0f, -1.0f, 0.0f);
			GL.TexCoord2(bottom.Right, bottom.Top);    GL.Vertex3(-size, -size,  size);
			GL.TexCoord2(bottom.Left,  bottom.Top);    GL.Vertex3(-size, -size, -size);
			GL.TexCoord2(bottom.Left,  bottom.Bottom); GL.Vertex3( size, -size, -size);
			GL.TexCoord2(bottom.Right, bottom.Bottom); GL.Vertex3( size, -size,  size);
			
			GL.Normal3(1.0f, 0.0f, 0.0f);
			GL.TexCoord2(right.Right, right.Top);    GL.Vertex3(size,  size, -size);
			GL.TexCoord2(right.Left,  right.Top);    GL.Vertex3(size,  size,  size);
			GL.TexCoord2(right.Left,  right.Bottom); GL.Vertex3(size, -size,  size);
			GL.TexCoord2(right.Right, right.Bottom); GL.Vertex3(size, -size, -size);
			
			GL.Normal3(-1.0f, 0.0f, 0.0f);
			GL.TexCoord2(left.Right, left.Top);    GL.Vertex3(-size,  size,  size);
			GL.TexCoord2(left.Left,  left.Top);    GL.Vertex3(-size,  size, -size);
			GL.TexCoord2(left.Left,  left.Bottom); GL.Vertex3(-size, -size, -size);
			GL.TexCoord2(left.Right, left.Bottom); GL.Vertex3(-size, -size,  size);
			
			GL.Normal3(0.0f, 0.0f, 1.0f);
			GL.TexCoord2(front.Right, front.Top);    GL.Vertex3( size,  size, size);
			GL.TexCoord2(front.Left,  front.Top);    GL.Vertex3(-size,  size, size);
			GL.TexCoord2(front.Left,  front.Bottom); GL.Vertex3(-size, -size, size);
			GL.TexCoord2(front.Right, front.Bottom); GL.Vertex3( size, -size, size);
			
			GL.Normal3(0.0f, 0.0f, -1.0f);
			GL.TexCoord2(back.Right, back.Top);    GL.Vertex3(-size,  size, -size);
			GL.TexCoord2(back.Left,  back.Top);    GL.Vertex3( size,  size, -size);
			GL.TexCoord2(back.Left,  back.Bottom); GL.Vertex3( size, -size, -size);
			GL.TexCoord2(back.Right, back.Bottom); GL.Vertex3(-size, -size, -size);
			
			GL.End();
		}
	}
}
