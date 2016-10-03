using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;

namespace GameEngineStage5
{
    public class GameData
    {
        // Набор значений для определения текущего состояния игры
        public enum GameState
        {
            NotStarted,
            MainMenu,
            Level,
            LevelWin,
            GameWin,
            GameOver
        }

        public PhysWorld world;

        public Assembly myAssembly;

        public GameState currentGameState = GameState.Level;

        public int score = 0;

        public Random rnd = new Random();

        public Logger log;

        public ResourceManager rm;

        /////////////////////////////////////////////////////////

        /// <summary>
        /// Множитель, влияющий на игровую скорость (скорость изменения времени, чем больше значение, тем быстрее идёт время)
        /// </summary>
        public int gameSpeed = 1;

        public Map map;

		public Scene curScene = null;

		public bool sceneChange = false;

        public int curMonsterNumber = 1;    // Номер текущего монстра (для уникальной нумерации монстров)

        public List<int> canMove = new List<int>(); // Массив проходимости тайлов - заносятся индексы проходимых тайлов

        //public Player player;

        //public Landscape landscape;

        //public Platform platform;

        //public PointCollider pc1;

        //public PointCollider pc2;

        public SpriteSheet ss;

        public List<PointF> path;   // Набор координат точек, из которых состоит путь (объект - polyline)

        public List<PointF> aStarPath;
        public Astar astar;

        public List<Wave> waves;



        private static GameData instance;

        private GameData()
        {
        }

        public static GameData Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameData();
                }
                return instance;
            }
        }

        /// <summary>
        /// Загрузка уровня и инициализация всех переменных для выбранного этапа
        /// </summary>
        public void Init()
        {
        }

        /// <summary>
        /// Получить расстояние между двумя точками
        /// </summary>
        /// <param name="p1">первая точка</param>
        /// <param name="p2">вторая точка</param>
        /// <returns>расстояние</returns>
        public float distance(PointF p1, PointF p2)
        {
            float dX = p2.X - p1.X;
            float dY = p2.Y - p1.Y;
            return (float)Math.Sqrt(dX * dX + dY * dY);
        }

        /// <summary>
        /// Определение угла поворота отрезка между данными точками
        /// </summary>
        /// <param name="p1">первая точка</param>
        /// <param name="p2">вторая точка</param>
        /// <returns>угол поворота в градусах</returns>
        public float getAngle(PointF p1, PointF p2)
        {
            // TODO: ошибка при вычислении угла 180 градусов (в минус по оси Х)
            // 447,255 224,255 

            float dX = p2.X - p1.X;
            float dY = p2.Y - p1.Y;

            // Для корректировки результата необходимо разделить обработку по направлению по оси Х
            if (dX >= 0)
            {
                // Углы от -90 до 90. Обрабатываем без корректировки
                return (float)(Math.Atan(dY / dX) * 180 / Math.PI);
            }
            else
            {
                // Углы от -90 до -180 и от 90 до 180. Требуется корректировка.
                if (dY >= 0)
                {
                    // Углы от 90 до 180
                    return 180.0f - (float)(Math.Atan(dY / (0 - dX)) * 180 / Math.PI);
                }
                else
                {
                    return (float)(Math.Atan(dY / (0 - dX)) * 180 / Math.PI) + 180.0f;
                }
            }
        }

        /// <summary>
        /// Плавное движение по отрезку между двумя точками
        /// </summary>
        /// <param name="p1">стартовая точка</param>
        /// <param name="p2">финишная точка</param>
        /// <param name="percent">процент</param>
        /// <returns>позиция на отрезке, соответствующая проценту</returns>
        public PointF lerp(PointF p1, PointF p2, float percent)
        {
            float dX = p2.X - p1.X;
            float dY = p2.Y - p1.Y;
            return new PointF(p1.X + dX * percent, p1.Y + dY * percent);
        }

    }

}
