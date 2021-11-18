namespace MemoryGame.Cards
{
    public struct Location
    {

        #region Constructor

        public Location(int x, int y) : this()
        {
            X = x;
            Y = y;
        }

        #endregion

        #region Accessers

        public int X { get; set; }
        public int Y { get; set; }

        #endregion
        
    }
}