using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Media;

namespace Connect4
{
    internal class Connect4ViewModel : INotifyPropertyChanged
    {
        private Game game;
        public Game Game { get { return game; } }

        public event PropertyChangedEventHandler? PropertyChanged;

        public Connect4ViewModel()
        {
            Player player1 = new Human(1, new SolidColorBrush(Colors.Yellow));
            Player player2 = new Human(2, new SolidColorBrush(Colors.Red));
            game = new Game(player1, player2);
            MakeMoveCommand = new RelayCommand(MakeMove);
        }

        // Expose game board and current player to UI
        public int[,] Board => game.Board;
        public Player CurrentPlayer => game.CurrentPlayer;
    

        // Command to make a move
        public ICommand MakeMoveCommand { get; private set; }


        private void MakeMove(object? parameter)
        {
            if (parameter is int column)
            {
                game.PlayMove(column);
                OnPropertyChanged(nameof(Board));
                OnPropertyChanged(nameof(CurrentPlayer)); // Notify that current player may have changed
                App.Current.Dispatcher.Invoke(() =>
                {
                    var mainWindow = App.Current.MainWindow as MainWindow;
                    mainWindow?.RedrawBoard();
                    mainWindow?.HighlightColumn(column, true);
                });
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public int GetNextAvailableRow(int column)
        {
            return game.GetAvailableRow(column);
        }
    }
}
