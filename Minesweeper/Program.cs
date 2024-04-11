using Microsoft.Extensions.DependencyInjection;
using Minesweeper.Controllers;
using Minesweeper.Interfaces;
using Minesweeper.Managers;

namespace Minesweeper
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IBoardManager, BoardManager>()
                .AddSingleton<IGameController, GameController>()
                .BuildServiceProvider();

            var gameController = serviceProvider.GetService<IGameController>();
            gameController.StartGame();
        }
    }
}
