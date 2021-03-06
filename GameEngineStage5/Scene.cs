﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace GameEngineStage5
{
    /// <summary>
    /// Базовый класс для описания игровой сцены
    /// </summary>
    public class Scene
    {

        /// <summary>
        /// Уникальный идентификатор сцены
        /// </summary>
		public GameData.GameState ID;

		/// <summary>
		/// Список объектов на сцене
		/// </summary>
		public List<Entity> objects = new List<Entity> ();

        public GameData gd;

        public Scene(GameData.GameState ID, GameData gd)
        {
            this.ID = ID;
            this.gd = gd;
        }

        virtual public void Init()
        {

        }

        virtual public void Render()
        {

        }

        virtual public void Update(int delta)
        {

        }

		virtual public void KeyDown(object sender, KeyEventArgs e)
        {

        }

		virtual public void KeyUp(object sender, KeyEventArgs e)
        {

        }

        virtual public void MouseDown()
        {

        }

    }
}
