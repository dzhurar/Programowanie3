using System.Configuration;
using System.Data;
using System.Windows;

namespace Programowanie3
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e); // required

            // Common class with data for both players
            GameSession session = new GameSession();

            // window for player 1 (Board1 as own, Board2 as enemy)
            Battleship window1 = new Battleship("Player 1", true, session);
            window1.Left = 100; // shift to the right
            window1.Top = 200;

            // window for player 2 (Board2 as own, Board1 as enemy)
            Battleship window2 = new Battleship("Player 2", false, session);
            window2.Left = 1000; // shift to the left
            window2.Top = 200;

            // show both windows
            window1.Show();
            window2.Show();
        }
    }

}
