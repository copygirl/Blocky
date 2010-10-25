using System;
using System.Drawing;
using Imaging = System.Drawing.Imaging;
using OpenTK.Graphics.OpenGL;

namespace Blocky.Graphics
{
	public class Texture : IDisposable
	{
		public int ID { get; private set; }
		public Size Size { get; private set; }
		public Bitmap Bitmap { get; private set; }
		public Imaging.BitmapData BitmapData { get; private set; }
		
		public int Width { get { return Size.Width; } }
		public int Height { get { return Size.Height; } }
		
		public Texture(string filename) : this(new Bitmap(filename), false) {  }
		public Texture(string filename, bool keep) : this(new Bitmap(filename), keep) {  }
		public Texture(Bitmap bitmap) : this(bitmap, false) {  }
		public Texture(Bitmap bitmap, bool keep) : this()
		{
			Size = bitmap.Size;
			Imaging.BitmapData data = bitmap.LockBits(new Rectangle(0, 0, Width, Height),
			                                          Imaging.ImageLockMode.ReadOnly,
			                                          Imaging.PixelFormat.Format32bppArgb);
			GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
			GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba8, Width, Height, 0,
			              PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
			if (keep) {
				Bitmap = bitmap;
				BitmapData = data;
			} else {
				bitmap.UnlockBits(data);
				bitmap.Dispose();
			}
		}
		public Texture()
		{
			ID = GL.GenTexture();
			GL.BindTexture(TextureTarget.Texture2D, ID);
			GL.TexEnv(TextureEnvTarget.TextureEnv, TextureEnvParameter.TextureEnvMode, (int)TextureEnvMode.Modulate);
		}
		
		~Texture()
		{
			throw new Exception("Dispose() wasn't called on Texture.");
		}
		
		public void Dispose()
		{
			if (ID == 0) return;
			GL.DeleteTexture(ID);
			if (Bitmap != null) {
				Bitmap.UnlockBits(BitmapData);
				Bitmap.Dispose();
			}
			ID = 0;
			GC.SuppressFinalize(this);
		}
		
		public void SetMinFilter(TextureMinFilter minFilter)
		{
			GL.BindTexture(TextureTarget.Texture2D, ID);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)minFilter);
		}
		public void SetMagFilter(TextureMagFilter magFilter)
		{
			GL.BindTexture(TextureTarget.Texture2D, ID);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)magFilter);
		}
	}
}
