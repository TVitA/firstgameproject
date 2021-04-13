using System;

namespace SomeGarbageLibrary
{
    public class Airplane : GameObject
    {
        private Single speed;

        public Airplane()
        {
        }

        public Bonus DropBonus()
        {
            Random random = new Random();

            BonusFactory factory = null;

            switch (random.Next(0, 3))
            {
                case 0:
                    factory = new AmmoBonusFactory();
                    break;

                case 1:
                    factory = new ArmorBonusFactory();
                    break;

                case 2:
                    factory = new ReloadSpeedBonusFactory();
                    break;

                default:
                    break;
            }

            return factory.GetBonus();
        }
    }
}