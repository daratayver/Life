using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
	public class Plant : Creature, IPH
	{
		public Plant(Color color, int width, int height) : base(color, width, height)
		{
		}

		public override void Update(ICollection<Creature> creatures, ICollection<MapObject> mapObjects)
		{
		}
	}
}
