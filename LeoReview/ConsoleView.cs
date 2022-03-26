using System;

namespace LeoReview
{
    public class ConsoleView : IObserver
    {
        public void Update(int[][] tiles)
        {
            Console.Clear();

            foreach (int[] row in tiles)
            {
                foreach (int cell in row)
                    Console.Write($"{cell}\t");
                
                Console.WriteLine();
            }
        }

        public ConsoleView()
        {
            Console.Clear();
        }
    }
}
