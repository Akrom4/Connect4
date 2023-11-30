using System.Windows.Media;

namespace Connect4
{
    public class Human : Player
    {
        public Human(int id, Brush color) : base(id, color) { }

        public override void MakeMove(Game game)
        {
            // Logic for human player making a move.
            // This could involve interacting with the UI.
        }
    }
}
