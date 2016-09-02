using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngineStage5
{
    class CONFIG
    {
        // Размер окна программы
        public static readonly int WIND_WIDTH = 800;
        public static readonly int WIND_HEIGHT = 600;

        // Координата начала игрового поля
        public static readonly int START_X = 20;
        public static readonly int START_Y = 8;

        // Размер тайла
        public static readonly int TILE_SIZE = 50;

        public static readonly float PHYS_GRAVITY = 1.1f; //5.0f; // Гравитация для физ. движка

        public static readonly float MAX_ENG_POWER = 5.0f;  // Максимальная мощность двигателя

//        public static readonly float MAX_LANDING_SPEED = 1.0f;  // Максимальная безопасная скорость касания платформы

//        public static readonly float MAX_LANDING_ANGLE = 15.0f; // Максимальный безопасный угол касания платформы


    }
}
