using System;

namespace LeoReview
{
    public class Controller : IController
    {
        private bool _gameContinue;

        private readonly IObservableModel _model;

        public Controller(IObservableModel model)
        {
            _model = model;
        }

        public void Initialize()
        {
            bool escPressed = false;

            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.LeftArrow:
                        _gameContinue = _model.MoveTiles(Direction.RightToLeft);
                        break;

                    case ConsoleKey.UpArrow:
                        _gameContinue = _model.MoveTiles(Direction.DownToUp);
                        break;

                    case ConsoleKey.DownArrow:
                        _gameContinue = _model.MoveTiles(Direction.UpToDown);
                        break;

                    case ConsoleKey.RightArrow:
                        _gameContinue = _model.MoveTiles(Direction.LeftToRight);
                        break;

                    case ConsoleKey.Escape:
                        escPressed = true;
                        break;

                    default:
                        break;
                }

                if (escPressed || !_gameContinue)
                    break;
            }
        }
    }
}
