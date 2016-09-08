using System;
using System.Collections.Generic;
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
			gd.map = Map.Load(@"Resources\Levels\MapTest.tmx");

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
