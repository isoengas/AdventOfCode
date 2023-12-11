
namespace Day4
{
    internal class Card
    {
        private string _cardText;
        private readonly HashSet<int> _winningNumbers;
        private readonly HashSet<int> _myNumbers;

        public Card(string cardText)
        {
            var winningNumbersText = cardText.Split(':')[1].Split('|')[0];
            var myNumbersText = cardText.Split(':')[1].Split('|')[1];
            _winningNumbers = new HashSet<int>(winningNumbersText.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse));
            _myNumbers = new HashSet<int>(myNumbersText.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse));
            NumInstances = 1;
        }

        internal int NumInstances { get; set; }

        internal int GetMatchingNumbersCount()
        {
            return _myNumbers.Intersect(_winningNumbers).Count();
        }

        internal int GetPoints()
        {
            return GetMatchingNumbersCount() switch
            {
                0 => 0,
                (int n) => (int)Math.Pow(2, n-1)
            };
        }
    }
}