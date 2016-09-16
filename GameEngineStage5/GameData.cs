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

        public Map map;

		public Scene curScene = null;

		public bool sceneChange = false;

        //public Player player;

        //public Landscape landscape;

        //public Platform platform;

        //public PointCollider pc1;

        //public PointCollider pc2;

        public SpriteSheet ss;

        public List<PointF> path;   // Набор координат точек, из которых состоит путь


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
    }

}
