using System;

namespace Game
{
    public static class Program : Object
    {
        public static void Main(String[] args)
        {
            var gameWindow = new ArtilleryGameWindow(1600, 800);

            gameWindow.Run();
        }
    }
}
