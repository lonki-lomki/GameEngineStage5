using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngineStage5
{
    /// <summary>
    /// Базовый класс для описания игровой сцены
    /// </summary>
    public class Scene
    {

        public GameData.GameState ID;

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

        virtual public void KeyDown()
        {

        }

        virtual public void KeyUp()
        {

        }

        virtual public void MouseDown()
        {

        }

    }
}
