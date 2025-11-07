using Candle;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace GameJam
{
    public class App
    {
        public static void Main()
        {
            Engine.Get.Run(MyGame.Get);
        }
    }
}
