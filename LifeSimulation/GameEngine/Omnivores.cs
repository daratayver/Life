using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
	public class Omnivores : Animal<IPH>
	{
		public Omnivores(Color color, int width, int height) : base(color, width, height)
		{
		}


	}
}
