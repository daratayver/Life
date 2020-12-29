using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;

namespace GameEngine
{
	public class Woman : Human
	{
		public bool IsPregnant { get; private set; }
		public Woman(Color color, int width, int height) : base(color, width, height)
		{
			IsPregnant = false;
		}

		public void Childbearing()
		{
			IsPregnant = true;
		}

		public Human GiveBirth()
		{
			Human child;
			if (random.Next(2) == 0)
				child = new Man(Color.Blue, 32, 32);
			else child = new Woman(Color.Pink, 32, 32);

			int x = X;
			int y = Y;
			child.SetLocation(x, y);

			

			IsPregnant = false;

			return child;
		}
	}
}
