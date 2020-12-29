using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
	public class Predator : Animal<Herbivorous>
	{
		public Predator(Color color, int width, int height) : base(color, width, height)
		{
			Speed = 2;
			searchRadius = 500;
		}
	}
}
