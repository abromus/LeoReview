using System;

namespace LeoReview
{
    class Program
    {
        private static void Main()
        {
            Console.WriteLine("Lets Get this party started! Write field size!");

            int fieldSize;
            int oneTile = 1;
            int twoTiles = 2;

            do
            {
                if(int.TryParse(Console.ReadLine(), out fieldSize) == false)
                {
                    Console.WriteLine("INTEGER MOTHERFUCKER! DO YOU WRITE IT?");

                    continue;
                }

                if (fieldSize == oneTile)
                    Console.WriteLine("SAY 1 AGAIN! SAY 1 AGAIN, I DARE YOU, I DOUBLE DARE YOU, MOTHERFICKER, SAY 1 ONE MORE GOD DAMN TIME!");

                if (fieldSize < oneTile)
                    Console.WriteLine($"{fieldSize} AIN'T NO POSITIVE NUMBER I EVER HEARD OF!");
            } while (fieldSize < twoTiles);

            IObservableModel model = new Model(fieldSize);
            IController controller = new Controller(model);
            IObserver view = new ConsoleView();

            model.AddObserver(view);
            controller.Initialize();

            Console.WriteLine("Game Over!");
            Console.ReadKey();
        }
    }
}
