﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
	public abstract class Creature : Sprite
	{
		public Creature(Color color, int width, int height) : base(color, width, height)
		{
		}

		public abstract void Update(ref ICollection<Creature> creatures, ref ICollection<MapObject> mapObjects);
	
	}
}
