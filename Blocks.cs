using System;
using System.Drawing;

namespace Blocky
{
	public static class Blocks
	{
		static Point[][] data = new Point[][]{
			Rock(1, 0),
			Grass(0, 0, 2, 0, 3, 0),
			Rock(2, 0),
			Rock(0, 1),
			Rock(4, 0),
			Rock(1, 1),
			Rock(2, 1),
			Rock(3, 1),
			Rock(0, 2),
			Rock(1, 2),
			Rock(2, 2),
			Tree(5, 1, 4, 1),
			Rock(4, 3),
			Rock(1, 3),
			Rock(0, 4),
			Grass(7, 1, 7, 3, 7, 2),
			Grass(6, 1, 6, 3, 6, 2),
			Tree(6, 0, 5, 0),
			Rock(7, 0),
			Grass(9, 0, 10, 0, 8, 0),
			Tree(4, 0, 3, 2),
			Rock(4, 2),
			Rock(5, 2),
			Rock(1, 4),
			Chest(9, 1, 11, 1, 10, 1),
			Rock(2, 3),
			Grass(8, 1, 8, 3, 8, 2),
			Workbench(11, 2, 4, 0, 11, 3, 12, 3),
			Chest(1, 0, 12, 2, 13, 2),
			Rock(3, 3),
			Rock(2, 4),
			Rock(8, 4),
			Jukebox(11, 4, 10, 4)
		};
		
		public static Point[][] Data { get { return data; } }
		
		static Point[] Rock(byte x, byte y)
		{
			Point side = new Point(x, y);
			return new Point[]{ side, side, side, side, side, side };
		}
		static Point[] Tree(byte topBottomX, byte topBottomY, byte sideX, byte sideY)
		{
			Point topBottom = new Point(topBottomX, topBottomY);
			Point side = new Point(sideX, sideY);
			return new Point[]{ topBottom, topBottom, side, side, side, side };
		}
		static Point[] Grass(byte topX, byte topY, byte bottomX, byte bottomY, byte sideX, byte sideY)
		{
			Point top = new Point(topX, topY);
			Point side = new Point(sideX, sideY);
			Point bottom = new Point(bottomX, bottomY);
			return new Point[]{ top, bottom, side, side, side, side };
		}
		static Point[] Chest(byte topBottomX, byte topBottomY,
		                          byte frontX, byte frontY, byte sideX, byte sideY)
		{
			Point topBottom = new Point(topBottomX, topBottomY);
			Point front = new Point(frontX, frontY);
			Point side = new Point(sideX, sideY);
			return new Point[]{ topBottom, topBottom, side, side, front, side };
		}
		/*static Point[] Furnace(byte topX, byte topY, byte bottomX, byte bottomY,
		                            byte frontX, byte frontY, byte sideX, byte sideY)
		{
			Point top = new Point(topX, topY);
			Point bottom = new Point(bottomX, bottomY);
			Point front = new Point(frontX, frontY);
			Point side = new Point(sideX, sideY);
			return new Point[]{ top, bottom, front, side, side, side };
		}*/
		static Point[] Workbench(byte topX, byte topY, byte bottomX, byte bottomY,
		                          byte frontBackX, byte frontBackY, byte rightLeftX, byte rightLeftY)
		{
			Point top = new Point(topX, topY);
			Point frontBack = new Point(frontBackX, frontBackY);
			Point rightLeft = new Point(rightLeftX, rightLeftY);
			Point bottom = new Point(bottomX, bottomY);
			return new Point[]{ top, bottom, rightLeft, rightLeft, frontBack, frontBack };
		}
		static Point[] Jukebox(byte topX, byte topY, byte sideX, byte sideY)
		{
			Point top = new Point(topX, topY);
			Point side = new Point(sideX, sideY);
			return new Point[]{ top, side, side, side, side, side };
		}
	}
}
