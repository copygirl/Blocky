using System;
using System.Drawing;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace Blocky
{
	public class Camera
	{
		bool _lastMouseEnabled = false;
		
		public Vector3d Location { get; set; }
		public Vector2 Rotation { get; set; }
		
		public float MoveSpeed { get; set; }
		public float MouseSpeed { get; set; }
		public bool MoveEnabled { get; set; }
		public bool MouseEnabled { get; set; }
		public Point MousePosition { get; set; }
		
		public Camera()
		{
			MoveSpeed = 15;
			MouseSpeed = 5;
			
			Game game = Game.Instance;
			MousePosition = new Point(game.Width/2, game.Height/2);
		}
		
		public void Update(double time)
		{
			Game game = Game.Instance;
			Point center = new Point(game.ClientSize.Width/2, game.ClientSize.Height/2);
			
			if (MouseEnabled && _lastMouseEnabled && game.Focused) {
				MouseDevice mouse = game.Mouse;
				float yaw = Rotation.X;
				float pitch = Rotation.Y;
				yaw += (mouse.X - center.X) * MouseSpeed * (float)time;
				pitch += (mouse.Y - center.Y) * MouseSpeed * (float)time;
				yaw = ((yaw % 360) + 360) % 360;
				pitch = Math.Max(-90, Math.Min(90, pitch));
				Cursor.Position = game.PointToScreen(center);
				Rotation = new Vector2(yaw, pitch);
			} else if (MouseEnabled && !_lastMouseEnabled && game.Focused) {
				Cursor.Hide();
				MousePosition = game.PointToClient(Cursor.Position);
				Cursor.Position = game.PointToScreen(center);
				_lastMouseEnabled = true;
			} else if (_lastMouseEnabled) {
				Cursor.Show();
				Cursor.Position = game.PointToScreen(MousePosition);
				_lastMouseEnabled = false;
			}
			
			if (MoveEnabled && game.Focused) {
				KeyboardDevice keyboard = game.Keyboard;
				double x = Location.X;
				double y = Location.Y;
				double z = Location.Z;
				float yaw = (float)Math.PI * Rotation.X / 180;
				float pitch = (float)Math.PI * Rotation.Y / 180;
				double speed = MoveSpeed * time;
				if ((keyboard[Key.A] ^ keyboard[Key.D]) &&
				    (keyboard[Key.W] ^ keyboard[Key.S]))
					speed /= Math.Sqrt(2);
				if (keyboard[Key.ShiftLeft] && !keyboard[Key.ControlLeft]) speed *= 5;
				else if (keyboard[Key.ControlLeft] && !keyboard[Key.ShiftLeft]) speed /= 3;
				if (keyboard[Key.W] && !keyboard[Key.S]) {
					x += Math.Sin(yaw) * Math.Cos(pitch) * speed;
					y -= Math.Sin(pitch) * speed;
					z -= Math.Cos(yaw) * Math.Cos(pitch) * speed;
				} else if (keyboard[Key.S] && !keyboard[Key.W]) {
					x -= Math.Sin(yaw) * Math.Cos(pitch) * speed;
					y += Math.Sin(pitch) * speed;
					z += Math.Cos(yaw) * Math.Cos(pitch) * speed;
				}
				if (keyboard[Key.A] && !keyboard[Key.D]) {
					x -= Math.Cos(yaw) * speed;
					z -= Math.Sin(yaw) * speed;
				} else if (keyboard[Key.D] && !keyboard[Key.A]) {
					x += Math.Cos(yaw) * speed;
					z += Math.Sin(yaw) * speed;
				}
				Location = new Vector3d(x, y, z);
			}
		}
		
		public void Render()
		{
			GL.Rotate(Rotation.Y, 1.0, 0.0, 0.0);
			GL.Rotate(Rotation.X, 0.0, 1.0, 0.0);
			GL.Translate(-Location);
		}
	}
}
