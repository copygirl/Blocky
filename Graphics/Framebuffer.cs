using System;
using System.Collections.Generic;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Blocky.Graphics
{
	public class Framebuffer : IDisposable
	{
		int _id;
		
		public int ID { get; private set; }
		
		public int Width { get; private set; }
		public int Height { get; private set; }

		public Framebuffer(int width, int height)
		{
			Width = width;
			Height = height;

			ID = GL.GenTexture();
			GL.BindTexture(TextureTarget.Texture2D, ID);
			GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba8, Width, Height, 0,
			              PixelFormat.Rgba, PixelType.UnsignedByte, IntPtr.Zero);

			GL.Ext.GenFramebuffers(1, out _id);
			GL.Ext.BindFramebuffer(FramebufferTarget.FramebufferExt, _id);
			GL.Ext.FramebufferTexture2D(FramebufferTarget.FramebufferExt,
			                            FramebufferAttachment.ColorAttachment0Ext, TextureTarget.Texture2D, ID, 0);

			GL.PushAttrib(AttribMask.ViewportBit);
			GL.Viewport(0, 0, Width, Height);
			GL.Clear(ClearBufferMask.ColorBufferBit);
			GL.PopAttrib();
			GL.Ext.BindFramebuffer(FramebufferTarget.FramebufferExt, 0);
		}
		
		~Framebuffer()
		{
			throw new Exception("Dispose() wasn't called on Framebuffer.");
		}

		public void Dispose()
		{
			if (ID == 0) return;
			GL.DeleteTexture(ID);
			GL.Ext.DeleteFramebuffers(1, ref _id);
			ID = 0;
			Width = 0; Height = 0;
			GC.SuppressFinalize(this);
		}

		public void Begin()
		{
			GL.Ext.BindFramebuffer(FramebufferTarget.FramebufferExt, _id);
			GL.PushAttrib(AttribMask.ViewportBit);
			GL.Viewport(0, 0, Width, Height);
		}
		public void End()
		{
			GL.PopAttrib();
			GL.Ext.BindFramebuffer(FramebufferTarget.FramebufferExt, 0);
		}
	}
}
