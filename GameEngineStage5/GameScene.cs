using System.Collections.Generic;
using System.Drawing;
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

            // TODO: у нас несколько тайлсетов. Служебные тайлсеты желательно пометить особым образом и обрабатывать отдельно (пометить точкой, @, # или что-то в этом духе)
            // Есть такие наборы тайлов: Tiles - тайлы для отображения карты, Monsters - тайлы и параметры монстров, Towers - тайлы и параметры башен

            // Загрузить отдельные спрайты в менеджер ресурсов как самостоятельные изображения (для ускорения отображения)
            // TODO: загружать только Tiles!!!
            //////foreach (Tileset ts in gd.map.Tilesets.Values)
            //////{

            Tileset ts = gd.map.Tilesets["Tiles"];

            int tileCount = ts.FirstTileID;
            // Загрузить базовое изображение
            gd.rm.addElementAsImage(ts.Image, @"Resources\Sprites\" + ts.Image);

            // Создать объект - карту спрайтов
            gd.ss = new SpriteSheet(gd.rm.getImage(ts.Image), ts.TileWidth, ts.TileHeight, ts.Spacing, ts.Margin);

            // Цикл по спрайтам внутри матрицы спрайтов
            for (int j = 0; j < ts.ImageHeight / ts.TileHeight; j++)
            {
                for (int i = 0; i < ts.ImageWidth / ts.TileWidth; i++)
                {
                    // Добавить этот спрайт в хранилище и наименованием tileset-<порядковый номер спрайта>
                    // TODO: как быть, если наборов тайлов несколько? Как вести нумерацию?
                    gd.rm.addElementAsImage("tileset-" + tileCount, gd.ss.getSprite(i, j));
                    tileCount++;
                }
            }

            // Заполнить массив проходимости тайлов
            foreach (int key in ts.TileProperties.Keys)
            {
                Tileset.TilePropertyList tpl = ts.TileProperties[key];
                //gd.log.write("key:" + (ts.FirstTileID + key) + " passability:" + tpl["Passability"]);
                if ("1".Equals(tpl["Passability"]))
                {
                    gd.canMove.Add(ts.FirstTileID + key);
                }
            }

            //////}

            // Загрузить параметры монстров
            // TODO: если тайлсет собран из нескольких изображений, в поле Image записывается имя последнего изображения
            ts = gd.map.Tilesets["Monsters"];
            
            foreach (int key in ts.TileProperties.Keys)
            {
                Tileset.TilePropertyList tpl = ts.TileProperties[key];
                // Доступны следующие свойства монстра: Name, Damage, Exp, HP, Speed
                // Создать структуру
                MobStruct ms = new MobStruct();
                ms.damage = float.Parse(tpl["Damage"]);
                ms.exp    = float.Parse(tpl["Exp"]);
                ms.hp     = float.Parse(tpl["HP"]);
                ms.speed  = float.Parse(tpl["Speed"]);
                ms.name   = tpl["Name"];
                // Занести в хранилище
                gd.mobtypes.Add(ms.name, ms);
            }


            // TODO: проверка сериализации списка монстров
            List<MobStruct> mmm =  new List<MobStruct>(gd.mobtypes.Values);
            string sss = UtilsParse.ToStringList(mmm);
            gd.log.write(sss);


            // Загрузить параметры башен
            ts = gd.map.Tilesets["Towers"];

            foreach (int key in ts.TileProperties.Keys)
            {
                Tileset.TilePropertyList tpl = ts.TileProperties[key];
                // Доступны следующие свойства башни: BulletSpeed, Cost, DamFreq, DamRadius, Damage, Desc, Elem, Name, Next
                // Создать структуру
                TowerStruct tws = new TowerStruct();
                tws.bulletSpeed = float.Parse(tpl["BulletSpeed"]);
                tws.cost = float.Parse(tpl["Cost"]);
                tws.damage = float.Parse(tpl["Damage"]);
                tws.damage_freq = float.Parse(tpl["DamFreq"]);
                tws.damage_radius = float.Parse(tpl["DamRadius"]);
                tws.description = tpl["Desc"];
                tws.element = tpl["Elem"];
                tws.name = tpl["Name"];
                tws.nextTowerType = tpl["Next"];

                // Занести в хранилище
                gd.towertypes.Add(tws.name, tws);
            }

            // TODO: проверка сериализации списка башен
            List<TowerStruct> ttt = new List<TowerStruct>(gd.towertypes.Values);
            sss = UtilsParse.ToStringList(ttt);
            gd.log.write(sss);



            /*
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
                gd.path.Add(new PointF(float.Parse(arr[0]) + path_x + CONFIG.START_X, float.Parse(arr[1]) + path_y + CONFIG.START_Y));
            }
            */

            // Создать объект для отображения карты
            TiledMapObject tmo = new TiledMapObject("TiledMapObject", gd, gd.map);
            // Координаты игрового поля на экране
			tmo.setPosition(CONFIG.START_X, CONFIG.START_Y);
			// Другие параметры
			tmo.setLayer(1);

			// Добавить объект на сцену
			objects.Add(tmo);

            // Получить путь движения монстра с помощью алгоритма поиска пути
            gd.astar = new Astar();
            gd.aStarPath = gd.astar.pathFinderAstar(gd.map, new Point(2, 35), new Point(226, 384), CONFIG.TILE_SIZE, gd.canMove, new List<int>());

            // Добавить монстра
            Monster m = new Monster(0.2f, 1.0f, 1.0f, 1.0f, true);
            m.setLayer(2);
            m.setImage(gd.rm.getImage("*"));
            objects.Add(m);

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
