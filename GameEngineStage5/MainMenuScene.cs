﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace GameEngineStage5
{
    /// <summary>
    /// Класс, в котором реализуется работа со сценой главного меню
    /// </summary>
    public class MainMenuScene : Scene
    {
        public MainMenuScene(GameData.GameState ID, GameData gd) : base(ID, gd)
        {

        }

        /// <summary>
        /// Инициализация сцены
        /// </summary>
        public override void Init()
        {
            base.Init();

        }

        /// <summary>
        /// Обработка нажатых клавиш
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void KeyDown(object sender, KeyEventArgs e)
        {
            base.KeyDown(sender, e);

            if (e.KeyCode == Keys.Escape)
            {
                Application.Exit();
            }
        }

        public override void MouseDown()
        {
            base.MouseDown();
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
