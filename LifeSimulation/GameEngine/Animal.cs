using System;
using System.Collections.Generic;
using System.Drawing;

namespace GameEngine
{
	public abstract class Animal<T> : Creature
	{

		protected int dx = 0, dy = 0;
		static protected Random random;

		protected Creature Goal;
		protected int Speed { get; set; }
		protected int searchRadius;
		protected bool foodFound;

		public double Satiety { get; protected set; }
		public Animal(Color color, int width, int height) : base(color, width, height)
		{
			random = new Random(Environment.TickCount);

			Speed = 1;

			MoveRandom();
			switch (random.Next(3))
			{
				case 0:
					MoveUp();
					break;
				case 1:
					MoveRight();
					break;
				case 2:
					MoveLeft();
					break;
				case 3:
					MoveDown();
					break;
			}

			Satiety = 1;
			searchRadius = 300;

			for (int i = 0; i < 1000; i++);
		}

		public override void Update(ref ICollection<Creature> creatures, ref ICollection<MapObject> mapObjects)
		{
			Satiety -= 0.0005;
			if (Satiety <= 0.9)
			{
				if (Satiety <= 0) Visible = false;
				{
					SearchFood(ref creatures);
					if (Goal != null) MoveToFood(ref creatures);
					else MoveRandom();
				}
			
			}
			else
			{
				MoveRandom();
			}

			Move();
		}

		protected void Move()
		{
			X += dx;
			Y += dy;
		}

		protected void MoveRandom()
		{
			if (random.Next(100) == 0)
			{
				switch (random.Next(4))
				{
					case 0:
						MoveUp();
						break;
					case 1:
						MoveRight();
						break;
					case 2:
						MoveLeft();
						break;
					case 3:
						MoveDown();
						break;
					case 4:
						dx = 0;
						dy = 0;
						break;
				}
			}

			if (X <= 0)
			{
				switch (random.Next(3))
				{
					case 0:
						MoveRight();
						break;
					case 1:
						MoveDown();
						break;
					case 2:
						MoveUp();
						break;
				}

			}
			else if (X >= 2048 - Width)
			{
				switch (random.Next(3))
				{
					case 0:
						MoveLeft();
						break;
					case 1:
						MoveDown();
						break;
					case 2:
						MoveUp();
						break;
				}
			}

			if (Y <= 0 )
			{
				switch (random.Next(3))
				{
					case 0:
						MoveDown();
						break;
					case 1:
						MoveRight();
						break;
					case 2:
						MoveLeft();
						break;
				}

			}
			else if (Y >= 2048 - Height)
			{
				switch (random.Next(3))
				{
					case 0:
						MoveUp();
						break;
					case 1:
						MoveRight();
						break;
					case 2:
						MoveLeft();
						break;
				}
			}

	
		}

		protected void MoveRight()
		{
			dx = Speed;
			dy = 0;
		}
		protected void MoveLeft()
		{
			dx = -Speed;
			dy = 0;
		}
		protected void MoveUp()
		{
			dx = 0;
			dy = -Speed;
		}
		protected void MoveDown()
		{
			dx = 0;
			dy = Speed;
		}

		public virtual void SearchFood(ref ICollection<Creature> creatures)
		{
			long min;
			if (Goal != null && creatures.Contains(Goal))
				min = (GetCenterX() - Goal.GetCenterX()) * (GetCenterX() - Goal.GetCenterX())
					+ (GetCenterY() - Goal.GetCenterY()) * (GetCenterY() - Goal.GetCenterY());
			else
			{
				min = 1000000000;
				Goal = null;
			}
			foreach (Creature gameObject in creatures)
			{
				//var obj = gameObject as Plant;
				if (gameObject.GetType() == typeof(T))
				{
					long dist = (GetCenterX() - gameObject.GetCenterX()) * (GetCenterX() - gameObject.GetCenterX())
						+ (GetCenterY() - gameObject.GetCenterY()) * (GetCenterY() - gameObject.GetCenterY());
					if (dist <= searchRadius * searchRadius
						&& dist < min)
					{
						Goal = gameObject;
					}
				}
			}
		}
		public void MoveToFood(ref ICollection<Creature> creatures)
		{
			if ((GetCenterX() - Goal.GetCenterX()) * (GetCenterX() - Goal.GetCenterX())
					+ (GetCenterY() - Goal.GetCenterY()) * (GetCenterY() - Goal.GetCenterY()) > 10)
			{
				if ((GetCenterX() - Goal.GetCenterX()) * (GetCenterX() - Goal.GetCenterX())
					> (GetCenterY() - Goal.GetCenterY()) * (GetCenterY() - Goal.GetCenterY()))
				{
					if (GetCenterX() - Goal.GetCenterX() < 0)
					{
						dx = Speed;
						dy = 0;
					}
					else if (GetCenterX() - Goal.GetCenterX() > 0)
					{
						dx = -Speed;
						dy = 0;
					}
				}
				else
				{
					if (GetCenterY() - Goal.GetCenterY() < 0)
					{
						dx = 0;
						dy = Speed;

					}
					else if (GetCenterY() - Goal.GetCenterY() > 0)
					{
						dx = 0;
						dy = -Speed;
					}
				}

			}
			else
			{
				Satiety = 1;
				foreach (Creature gameObj in creatures)
				{
					//var obj = gameObj as Plant;
					if (Goal == gameObj)
					{
						gameObj.Visible = false;
						break;
					}
				}
				Goal = null;
			}
		}

		

		public override void Draw(Graphics g)
		{
			var fontFamily = new FontFamily("Times New Roman");
			var font = new Font(fontFamily, 16, FontStyle.Regular, GraphicsUnit.Pixel);
			StringFormat sf = new StringFormat
			{
				LineAlignment = StringAlignment.Center,
				Alignment = StringAlignment.Center
			};
			g.DrawString("Сытость: " + Math.Ceiling(Satiety * 100) + "%", font, Brushes.White, GetCenterX(), Y - 5, sf);

			base.Draw(g);
		}
	}
}
