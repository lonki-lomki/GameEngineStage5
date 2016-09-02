using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace GameEngineStage5
{
    public partial class Form1 : Form
    {
        private Timer timer = new Timer();

        // Счётчик количества тиков
        private long tickCount = 0;
        // Для определения длины интервала времени в тиках
        private long saveTickCount = 0;

        /// <summary>
        /// Игровые данные
        /// </summary>
        private GameData gd;

        private Image backgroundImage;

        private Logger log;

        public Form1()
        {
            InitializeComponent();

            //log = new Logger("Log.txt");

            gd = GameData.Instance;
            gd.log = log;

            // Получить доступ к ресурсам, встроенным в проект
            gd.myAssembly = Assembly.GetExecutingAssembly();

            // Размер окна программы
            this.Width = CONFIG.WIND_WIDTH;
            this.Height = CONFIG.WIND_HEIGHT;

            // Настройки окна программы
            KeyPreview = true;
            DoubleBuffered = true;

            // Начальные параметры для обработки интервалов по таймеру
            tickCount = Environment.TickCount; //GetTickCount();
            saveTickCount = tickCount;

            // Настройки таймера
            timer.Enabled = true;
            timer.Tick += new EventHandler(OnTimer);
            timer.Interval = 20;
            timer.Start();

            // Создать физический мир
            gd.world = new PhysWorld(log);

            //------------------------------------------------------




        }

        private void OnTimer(object obj, EventArgs ea)
        {
            int delta;

            // Новое значение времени
            tickCount = Environment.TickCount;

            delta = (int)(tickCount - saveTickCount);

            gd.world.update(delta);

            // Проверить актуальность объектов (убрать со сцены уничтоженные объекты)
            for (int i = gd.world.objects.Count - 1; i >= 0; i--)
            {
                if (gd.world.objects[i].isDestroyed())
                {
                    // Удалить из "мира"
                    gd.world.objects.RemoveAt(i);
                }
            }

            // Проверить коллизию между платформой и кораблем
            /*
            PointF pos = gd.player.getPosition();
            Vector v1 = gd.pc1.rotate(gd.player.getAngle() - 90.0f);
            Vector v2 = gd.pc2.rotate(gd.player.getAngle() - 90.0f);
            if (((RectangleCollider)gd.platform.getCollider()).getRect().Contains(pos.X + 21 + v1.X, pos.Y + 21 + v1.Y) == true ||
                ((RectangleCollider)gd.platform.getCollider()).getRect().Contains(pos.X + 21 + v2.X, pos.Y + 21 + v2.Y) == true)
            {
                // Проверить скорость, на которой было касание платформы
                if (Math.Abs(gd.player.getVelocity().Y) > CONFIG.MAX_LANDING_SPEED)
                {
                    // Сели на платформу с допустимой скоростью - ПОБЕДА
                    gd.currentGameState = GameData.GameState.GameOver;
                }
                else
                {
                    // Разбились об платформу
                    gd.currentGameState = GameData.GameState.GameWin;
                    gd.player.setDestroyed(true);
                }

            }
            */

            saveTickCount = tickCount;

            Invalidate(false);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;


            // Вывести фоновое изображение, если оно есть
            if (backgroundImage != null)
            {
                g.DrawImage(backgroundImage, 0.0f, 0.0f);
            }


            if (gd.currentGameState == GameData.GameState.GameWin)
            {
                g.DrawString("WIN!", new Font("Arial", 30), Brushes.Green, 350.0f, 250.0f);
                //g.DrawString("Скорость по Y: " + gd.player.getVelocity().Y, new Font("Arial", 12), Brushes.Green, 300.0f, 300.0f);
                return;
            }

            if (gd.currentGameState == GameData.GameState.GameOver)
            {
                g.DrawString("GAME OVER!", new Font("Arial", 30), Brushes.Red, 270.0f, 250.0f);
                return;
            }


            // Цикл отображения всех объектов на всех уровнях
            // TODO: перенести в отдельный метод класса World
            // Цикл по уровням (пока 3 уровня)
            for (int i = 0; i < 3; i++)
            {
                foreach (Entity ent in gd.world.objects)
                {
                    if (ent.getLayer() == i)
                    {
                        ent.render(g);
                    }
                }
            }

            // Вывод текстовой информации
            //g.DrawString("Тяга: " + gd.player.getEngPower(), new Font("Arial", 12), Brushes.Black, 20.0f, 10.0f);
            //g.DrawString("Скорость по Х: " + gd.player.getVelocity().X, new Font("Arial", 12), Brushes.Black, 20.0f, 30.0f);
            //g.DrawString("Скорость по Y: " + gd.player.getVelocity().Y, new Font("Arial", 12), Brushes.Black, 20.0f, 50.0f);

            // Для отладки - вывести два точечных коллайдера
            /*
            PointF pos = gd.player.getPosition();
            Vector v = gd.pc1.rotate(gd.player.getAngle()-90.0f);
            g.DrawEllipse(Pens.Green, pos.X + 20 + v.X, pos.Y + 20 + v.Y, 2, 2);
            v = gd.pc2.rotate(gd.player.getAngle()-90.0f);
            g.DrawEllipse(Pens.Green, pos.X + 20 + v.X, pos.Y + 20 + v.Y, 2, 2);
            */

        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {

        }
    }
}
