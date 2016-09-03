using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

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

            // Открыть файл с описанием этапа
            FileStream fileStrem = new FileStream(@"Resources\Levels\level_001.txt", FileMode.Open, FileAccess.Read);
            // Чтение тайловой карты (результат записывается в переменные map и legend данного экземпляра)
            gd.map.LoadMap(fileStrem);
            // Создать объект для отображения карты
            TileMapObject tmp = new TileMapObject("TileMapObject", gd, gd.map);
            // Координаты игрового поля на экране
            // ...

        }

        public override void KeyDown()
        {
            base.KeyDown();
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
