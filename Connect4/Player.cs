using System.Windows.Media;

namespace Connect4
{
    public abstract class Player
    {
        public int Id { get; set; }
        public Brush PlayerColor { get; set; }

        // Constructor
        protected Player(int id, Brush color)
        {
            Id = id;
            PlayerColor = color;
        }

        // Abstract method to be implemented by derived classes
        public abstract void MakeMove(Game game);
    }

}
