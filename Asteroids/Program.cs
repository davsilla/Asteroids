﻿using System;

namespace Asteroids
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new Asteroids())
                game.Run();
        }
    }
}
