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
            foreach (string s in map.map)
            {
                // Цикл по столбцам
                for (int i = 0; i < s.Length; i++)
                {
                    //...
                }
            }
        }

    }
}
