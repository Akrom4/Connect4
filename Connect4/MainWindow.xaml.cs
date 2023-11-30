using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Connect4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new Connect4ViewModel();
            InitializeBoardGrid();
            
        }

        private void InitializeBoardGrid()
        {
            int columns = 7; // Number of columns
            int rows = 6;    // Number of rows

            for (int col = 0; col < columns; col++)
            {
                // Safely cast to UniformGrid
                if (BoardGrid.Children[col] is UniformGrid uniformGrid)
                {
                    int currentColumn = col; // Local variable to capture the current column index
                    uniformGrid.MouseEnter += UniformGrid_MouseEnter;
                    uniformGrid.MouseLeave += UniformGrid_MouseLeave;

                    uniformGrid.MouseLeftButtonDown += (sender, e) =>
                    {
                        if (DataContext is Connect4ViewModel viewModel)
                        {
                            viewModel.MakeMoveCommand.Execute(currentColumn);
                        }
                    };

                    for (int row = 0; row < rows; row++)
                    {
                        Button cellButton = new()
                        {
                            Style = (Style)Resources["CellButtonStyle"],
                            CommandParameter = col
                        };

                        cellButton.SetBinding(Button.CommandProperty, new Binding("MakeMoveCommand")
                        {
                            Source = this.DataContext
                        });

                        uniformGrid.Children.Add(cellButton);
                    }
                }
            }
        }


        private void UniformGrid_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is UniformGrid uniformGrid && int.TryParse(uniformGrid.Tag.ToString(), out int column))
            {
                HighlightColumn(column, true); // Pass true to highlight
            }
        }

        private void UniformGrid_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is UniformGrid uniformGrid && int.TryParse(uniformGrid.Tag.ToString(), out int column))
            {
                HighlightColumn(column, false); // Pass false to remove highlight
            }
        }


        public void HighlightColumn(int column, bool highlight)
        {
            var viewModel = DataContext as Connect4ViewModel;
            if (viewModel != null)
            {
                int row = viewModel.GetNextAvailableRow(column);
                if (row != -1)
                {
                    Button? targetButton = FindButtonInColumn(column, row);
                    if (targetButton != null)
                    {
                        var ellipse = FindChild<Ellipse>(targetButton);
                        if (ellipse != null)
                        {
                            Brush colorToUse;
                            if (highlight && viewModel.CurrentPlayer != null)
                            {
                                colorToUse = viewModel.CurrentPlayer.PlayerColor ?? new SolidColorBrush(Colors.LightGray);
                            }
                            else
                            {
                                colorToUse = new SolidColorBrush(Colors.LightGray);
                            }

                            ellipse.Fill = colorToUse;
                        }
                    }
                }
            }
        }





        private Button? FindButtonInColumn(int column, int row)
        {
            UniformGrid? uniformGrid = FindUniformGridForColumn(column);
            if (uniformGrid != null)
            {
                // The buttons are added in a top-down manner, so the index matches the row
                if (row < uniformGrid.Children.Count)
                {
                    // Safely cast to Button and check for null
                    return uniformGrid.Children[row] as Button;
                }
            }
            return null;
        }



        private UniformGrid? FindUniformGridForColumn(int column)
        {
            // Assuming BoardGrid is the Grid containing UniformGrids for each column
            if (column < BoardGrid.Children.Count)
            {
                return (UniformGrid)BoardGrid.Children[column];
            }
            return null;
        }
        private T? FindChild<T>(DependencyObject parent) where T : DependencyObject
        {
            if (parent == null) return null;

            T? foundChild = null;

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is T)
                {
                    foundChild = (T)child;
                    break;
                }
                else
                {
                    foundChild = FindChild<T>(child);

                    if (foundChild != null) break;
                }
            }

            return foundChild;
        }


        public void RedrawBoard()
        {
            var viewModel = DataContext as Connect4ViewModel;
            if (viewModel == null) return;

            int[,] board = viewModel.Board;
            var player1 = viewModel.Game.Player1; 
            var player2 = viewModel.Game.Player2;

            for (int col = 0; col < board.GetLength(0); col++)
            {
                for (int row = 0; row < board.GetLength(1); row++)
                {
                    Button? cellButton = FindButtonInColumn(col, row);
                    if (cellButton != null)
                    {
                        var ellipse = FindChild<Ellipse>(cellButton);
                        if (ellipse != null)
                        {
                            Brush color = new SolidColorBrush(Colors.LightGray);
                            if (board[col, row] == player1.Id)
                            {
                                color = player1.PlayerColor;
                            }
                            else if (board[col, row] == player2.Id)
                            {
                                color = player2.PlayerColor;
                            }
                            ellipse.Fill = color;
                        }
                    }
                }
            }
        }


    }
}