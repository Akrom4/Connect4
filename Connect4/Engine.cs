using System.Windows.Media;

namespace Connect4
{
    public class Engine : Player
    {
        public Engine(int id, Brush color) : base(id, color) { }

        public override void MakeMove(Game game)
        {
            // AI logic for making a move.
            // This could be as simple or as complex as you like.
        }
    }
}
