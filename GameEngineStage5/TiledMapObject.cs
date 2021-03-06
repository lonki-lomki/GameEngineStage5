﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GameEngineStage5
{
    /// <summary>
    /// Класс, описывиющий тайловую карту, импортированную из редактора Tiled
    /// </summary>
    public class TiledMapObject : Entity
    {
        public Map map;

        public TiledMapObject() : base()
        {
        }

        public TiledMapObject(String id) : base(id)
        {
        }

        public TiledMapObject(String id, GameData gd) : base(id, gd)
        {
        }

        public TiledMapObject(String id, GameData gd, Map map) : base(id, gd)
        {
            this.map = map;
        }

        public override void render(Graphics g)
        {
            base.render(g);

            // Отображение тайловой карты в виде матрицы
            //map.Layers.Count - количество уровней тайлов
            //map.Layers[i] - описание уровня
            //map.Layers[i].Tiles - массив чисел, описывающих тайлы (матрица развернута в одну строку)
            //map.Width - ширина карты в тайлах
            //map.Height - высота карты в тайлах
            //map.Layers[i].Width - ширина уровня в тайлах
            //map.Layers[i].Height - высота уровня в тайлах

            // Имя файла с тайлами
            int ccc = map.Tilesets.Count;

            // Цикл по строкам
            for (int j  = 0; j < map.Layers["Layer 1"].Height; j++)
            {
                for (int i = 0; i < map.Layers["Layer 1"].Width; i++)
                {
                    int tileCode = map.Layers["Layer 1"].Tiles[i + j * map.Layers["Layer 1"].Width];
                    if (tileCode > 0)
                    {
                        //g.DrawString("" + tileCode, new Font("Arial", 12), Brushes.Black, position.X + i * 14, position.Y + j * 14);
                        //g.DrawImage(gd.ss.getSprite(tileCode-1, 0), position.X + i * gd.ss.getTileWidth(), position.Y + j * gd.ss.getTileHeight(), gd.ss.getTileWidth(), gd.ss.getTileHeight());
                        g.DrawImage(gd.rm.getImage("tileset-" + tileCode), position.X + i * gd.ss.getTileWidth(), position.Y + j * gd.ss.getTileHeight(), gd.ss.getTileWidth(), gd.ss.getTileHeight());
                    }
                }
            }
            //g.DrawString(s.Substring(i, 1), new Font("Arial", 12), Brushes.Black, position.X + i * 12, position.Y + j * 12);
        }
    }
}
