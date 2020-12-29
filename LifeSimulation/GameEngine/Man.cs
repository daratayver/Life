using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;

namespace GameEngine
{
	public class Man : Human
	{
		public Man(Color color, int width, int height) : base(color, width, height)
		{
			
		}

		public void BuildHouse(ref ICollection<MapObject> mapObjects)
		{
			Home = new House(Color.Brown, 64,64);
			Home.SetLocation(X, Y);
			mapObjects.Add(Home);
		}
	}
}
