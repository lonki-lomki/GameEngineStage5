using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace GameEngineStage5
{
    /// <summary>
    /// Класс, описывающий сцену, на которой будет проходить основная игра
    /// </summary>
    public class GameScene : Scene
    {
        public GameScene(GameData.GameState ID, GameData gd) : base(ID, gd)
        {

        }

        /// <summary>
        /// Инициализация сцены
        /// </summary>
        public override void Init()
        {
            base.Init();

			// Загрузить ресурсы, необходимые для данной сцены
			gd.rm.clear();
			gd.rm.addElementAsImage ("#", @"Resources\Sprites\tile_wall.png");
			gd.rm.addElementAsImage (".", @"Resources\Sprites\tile_space.png");
			gd.rm.addElementAsImage ("*", @"Resources\Sprites\tile_path.png");
			gd.rm.addElementAsImage ("+", @"Resources\Sprites\tile_busy.png");

            // Создать объект - тайловую карту и загрузить данные из файла
            gd.map = Map.Load(@"Resources\Levels\Level_001.tmx");

            // Загрузить отдельные спрайты в менеджер ресурсов как самостоятельные изображения (для ускорения отображения)
            foreach (Tileset ts in gd.map.Tilesets.Values)
            {
                int tileCount = ts.FirstTileID;
                // Загрузить базовое изображение
                gd.rm.addElementAsImage(ts.Image, @"Resources\Sprites\" + ts.Image);

                // Создать объект - карту спрайтов
                gd.ss = new SpriteSheet(gd.rm.getImage(ts.Image), ts.TileWidth, ts.TileHeight, ts.Spacing, ts.Margin);

                // Цикл по спрайтам внутри матрицы спрайтов
                for (int j = 0; j < ts.ImageHeight/ts.TileHeight; j++)
                {
                    for (int i = 0; i < ts.ImageWidth/ts.TileWidth; i++)
                    {
                        // Добавить этот спрайт в хранилище и наименованием tileset-<порядковый номер спрайта>
                        // TODO: как быть, если наборов тайлов несколько? Как вести нумерацию?
                        gd.rm.addElementAsImage("tileset-" + tileCount, gd.ss.getSprite(i, j));
                        tileCount++;
                    }
                }
            }

            // Загрузить путь, по которому движутся враги
            // Пример строки: "0,0 96,0 97,63 129,64 130,191 319,192 321,100 447,100 447,253 225,257 226,353"
            // набор пиксельных координат Х и У относительно координаты левого верхнего угла объекта.
            string path = gd.map.ObjectGroups["Path"].Objects["Path"].Points;
            int path_x = gd.map.ObjectGroups["Path"].Objects["Path"].X;
            int path_y = gd.map.ObjectGroups["Path"].Objects["Path"].Y;
            // Массив координат точек ломаной линии
            string[] points = path.Split(' ');
            gd.path = new List<PointF>();
            // Занести данные в массив точек
            for (int i = 0; i < points.Length; i++)
            {
                string[] arr = points[i].Split(',');
                gd.path.Add(new PointF(float.Parse(arr[0]), float.Parse(arr[1])));
            }


            /*
            // Открыть файл с описанием этапа
            FileStream fileStrem = new FileStream(@"Resources\Levels\level_001.txt", FileMode.Open, FileAccess.Read);
            // Чтение тайловой карты (результат записывается в переменные map и legend данного экземпляра)
            gd.map.LoadMap(fileStrem);
            */
            // Создать объект для отображения карты
            TiledMapObject tmo = new TiledMapObject("TiledMapObject", gd, gd.map);
            // Координаты игрового поля на экране
			tmo.setPosition(20.0f, 50.0f);
			// Другие параметры
			tmo.setLayer(2);

			// Добавить объект на сцену
			objects.Add(tmo);


        }

		public override void KeyDown(object sender, KeyEventArgs e)
        {
			base.KeyDown(sender, e);

			if (e.KeyCode == Keys.Escape) {
				Application.Exit ();
			}

        }

        public override void Render()
        {
            base.Render();
        }

        public override void Update(int delta)
        {
            base.Update(delta);
        }

    }
}
