﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using GameEngine;
using System.Drawing.Imaging;

namespace BadLife
{
	public class Game: GameView
	{
		
		Tile[] Tiles;
		TileMap TileMap;

		Bitmap PlantBitmap;
		//ICollection<Plant> Plants;

		Bitmap BunnyBitmap;
		//ICollection<Herbivorous> Bunnies;
		Bitmap ReindearBitmap;
		//ICollection<Herbivorous> Reindears;
		Bitmap SheepBitmap;
		//ICollection<Herbivorous> Sheeps;

		Bitmap FoxBitmap;
		//ICollection<Predator> Foxes;
		Bitmap WolfBitmap;
		//ICollection<Predator> Wolfs;
		Bitmap BearBitmap;
		//ICollection<Predator> Bears;

		Bitmap OmBitmap;
		//ICollection<Omnivores> Oms;
		Bitmap OmnBitmap;
		//ICollection<Omnivores> Omns;
		Bitmap OmniBitmap;
		//ICollection<Omnivores> Omnis;

		Bitmap WomanBitmap;
		Bitmap ManBitmap;
		//ICollection<Human> People;

		ICollection<Creature> Creatures;

		ICollection<Creature> Borns;

		ICollection<Creature> Deads;

		ICollection<MapObject> MapObjects;

		static Random random;

		public Game()
		{
			FormBorderStyle = FormBorderStyle.FixedDialog;
			MaximizeBox = false;
		}
		public override void OnDraw(Graphics g, Bitmap backBuffer)
		{
			g.Clear(Color.Gray);
			MoveMap();

			TileMap.Draw(g);


			foreach (MapObject o in MapObjects)
			{
				o.Draw(g);
			}

			foreach (Creature d in Creatures)
			{
				d.Draw(g);
			}

			CreateGraphics().DrawImage(backBuffer, GameMap);
		}

		public override void OnUpdate()
		{
			foreach (Creature d in Creatures)
			{
				d.Update(ref Creatures, ref MapObjects);

				if (d.GetType() == typeof(Woman) && ((Woman)d).IsPregnant)
				{
					Borns.Add(((Woman)d).GiveBirth());
				}

				if (!d.Visible) Deads.Add(d);
			}

			foreach (Creature d in Borns)
			{
					Creatures.Add(d);
			}

			foreach (Creature d in Deads)
			{
				if (Creatures.Contains(d))
					Creatures.Remove(d);
			}

			Deads.Clear();
			Borns.Clear();

			if (random.Next(10) == 0)
			{
				Plant p = new Plant(Color.LightGreen, 32, 32);

				int x = random.Next(16, 2048);
				int y = random.Next(16, 2048);
				p.SetLocation(x, y);

				int frame = random.Next(7);
				Creatures.Add(p);
			}
			if (random.Next(20) == 0)
			{
				Herbivorous b = new Herbivorous(Color.White, 32, 32);

				int x = random.Next(16, 2048);
				int y = random.Next(16, 2048);
				b.SetLocation(x, y);

				int ind = random.Next(4);
				

				Creatures.Add(b);
			}
			if (random.Next(20) == 0)
			{
				Predator p = new Predator(Color.Orange, 32, 32);

				int x = random.Next(16, 2048);
				int y = random.Next(16, 2048);
				p.SetLocation(x, y);

				int ind = random.Next(4);
				

				Creatures.Add(p);
			}
		}

		public override void OnLoadContent()
		{
			int tileSize = 64;
	
			Tiles = new Tile[3];
			Tiles[0] = new Tile(Color.Green);
			Tiles[1] = new Tile(Color.GreenYellow);
			Tiles[2] = new Tile(Color.DarkGreen);

			int row = 2048 / tileSize;
			int col = 2048 / tileSize;
			TileMap = new TileMap(Tiles, row, col, tileSize);

			random = new Random();
			for (int i = 0; i < row; i++)
				for (int j = 0; j < col; j++)
					if (random.Next(3) == 0)
					{
						if (random.Next(2) == 0)
							TileMap.SetTile(i, j, 1);
						else TileMap.SetTile(i, j, 2);
					}

			Creatures = new List<Creature>();

			PlantBitmap = new Bitmap(32, 32, PixelFormat.Format24bppRgb);
			for (int i = 0; i < 300; i++)
			{
				Plant p = new Plant(Color.LightGreen, 32, 32);

				int x = random.Next(16, 2048);
				int y = random.Next(16, 2048);
				p.SetLocation(x, y);

				
				
				Creatures.Add(p);
			}

			BunnyBitmap = new Bitmap(32, 32, PixelFormat.Format24bppRgb);
			ReindearBitmap = new Bitmap(32, 32, PixelFormat.Format24bppRgb);
			SheepBitmap = new Bitmap(32, 32, PixelFormat.Format24bppRgb);
			for (int i = 0; i < 50; i++)
			{
				Herbivorous b;
				int ran = random.Next(3);
				if (ran == 0)
					b = new Herbivorous(Color.LightGray, 32, 32);
				else if (ran == 1) 
					b = new Herbivorous(Color.SandyBrown, 32, 32);
				else 
					b = new Herbivorous(Color.White, 32, 32);

				int x = random.Next(16, 2048) + 16;
				int y = random.Next(16, 2048) + 16;
				b.SetLocation(x, y);

				

				Creatures.Add(b);
			}

		

			FoxBitmap = new Bitmap(48, 48, PixelFormat.Format24bppRgb);
			WolfBitmap = new Bitmap(48, 48, PixelFormat.Format24bppRgb);
			BearBitmap = new Bitmap(48, 48, PixelFormat.Format24bppRgb);
			for (int i = 0; i < 35; i++)
			{
				Predator f;
				int ran = random.Next(3);
				if (ran == 0)
					f = new Predator(Color.Orange, 48, 48);
				else if (ran == 1) 
					f = new Predator(Color.DarkGray, 48, 48);
				else 
					f = new Predator(Color.SaddleBrown, 48, 48);

				int x = random.Next(16, 2048) + 16;
				int y = random.Next(16, 2048) + 16;
				f.SetLocation(x, y);

				Creatures.Add(f);
			}

			
		
			OmBitmap = new Bitmap(48, 48, PixelFormat.Format24bppRgb);
			OmnBitmap = new Bitmap(48, 48, PixelFormat.Format24bppRgb);
			OmniBitmap = new Bitmap(48, 48, PixelFormat.Format24bppRgb);
			for (int i = 0; i < 20; i++)
			{
				Omnivores f;
				int ran = random.Next(3);
				if (ran == 0)
					f = new Omnivores(Color.DarkRed, 48, 48);
				else if (ran == 1)
					f = new Omnivores(Color.Red, 48, 48);
				else
					f = new Omnivores(Color.IndianRed, 48, 48);

				int x = random.Next(16, 2048) + 16;
				int y = random.Next(16, 2048) + 16;
				f.SetLocation(x, y);

				Creatures.Add(f);
			}


			ManBitmap = new Bitmap(32, 32, PixelFormat.Format24bppRgb);
			WomanBitmap = new Bitmap(32, 32, PixelFormat.Format24bppRgb);
			for (int i = 0; i < 15; i++)
			{
				Human h;
				if (random.Next(2) == 0)
					h = new Man(Color.Blue, 32, 32);
				else h = new Woman(Color.HotPink, 32, 32);

				int x = random.Next(16, 2048) + 16;
				int y = random.Next(16, 2048) + 16;
				h.SetLocation(x, y);

				
				Creatures.Add(h);
			}
		

			MapObjects = new List<MapObject>();

			Borns = new List<Creature>();
			Deads = new List<Creature>();
		}

		public override void OnUnloadContent()
		{
		

			PlantBitmap.Dispose();

			BunnyBitmap.Dispose();

			FoxBitmap.Dispose();
		}

	}
}
