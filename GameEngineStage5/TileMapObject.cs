using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GameEngineStage5
{
    /// <summary>
    /// Класс, описывающий игровое поле как отдельный объект
    /// </summary>
    public class TileMapObject : Entity
    {

        public TileMap map;

		public TileMapObject() : base()
        {
        }

        public TileMapObject(String id) : base(id)
        {
        }

        public TileMapObject(String id, GameData gd) : base(id, gd)
        {
        }

        public TileMapObject(String id, GameData gd, TileMap map) : base(id, gd)
        {
            this.map = map;
        }


        public override void render(Graphics g)
        {
            base.render(g);

            // Отображение тайловой карты в виде матрицы
            // Цикл по строкам
			int j = 0;
            foreach (string s in map.map)
            {
                // Цикл по столбцам
                for (int i = 0; i < s.Length; i++)
                {
					//g.DrawString (s.Substring (i, 1), new Font ("Arial", 12), Brushes.Black, position.X + i * 10, position.Y + j * 10);
					//Image img = Image.FromFile(@"Resources\Sprites\"+map.legend[s.Substring (i, 1)]);
					g.DrawImage(Image.FromFile(@"Resources\Sprites\"+map.legend[s.Substring (i, 1)]), position.X + i * CONFIG.TILE_SIZE, position.Y + j * CONFIG.TILE_SIZE, CONFIG.TILE_SIZE, CONFIG.TILE_SIZE);
                }

				j++;
            }
        }

    }
}
