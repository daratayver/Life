using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
	public abstract class Sprite : Layer
	{
		readonly Color Color;
		int Width;
		int Hight;

	

		public Sprite(Color color, int width, int height)
		{
			Color = color;
			Width = width;
			Height = height;
			
		}

		

		public override void Draw(Graphics g)
		{
			if (!Visible) return;

			Rectangle rect = new Rectangle(X, Y, Width, Height);
			SolidBrush brush = new SolidBrush(Color);
			Pen pen = new Pen(Color.Black, 3);

			g.FillRectangle(brush, rect);
			g.DrawRectangle(pen, rect);
		}
	}
}
