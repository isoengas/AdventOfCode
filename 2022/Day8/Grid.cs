namespace Day8
{
    internal class Grid
    {
        private readonly List<List<int>> _rows = new List<List<int>>();

        public Grid(int numCols)
        {
            NumCols = numCols;
        }

        public int NumCols { get; }
        public int NumRows => _rows.Count;

        internal void AddRow(IEnumerable<int> heights)
        {
            var row = heights.ToList();
            if (row.Count != NumCols)
                throw new InvalidOperationException($"Invalid number of elements in the row, expected {NumCols} but got {row.Count}");

            _rows.Add(heights.ToList());
        }

        public IEnumerable<(int x, int y, int height)> EnumerateItems()
        {
            for (int x = 0; x < NumCols; x++)
                for (int y = 0; y < NumRows; y++)
                    yield return (x, y, _rows[y][x]);
        }

        public bool IsItemVisible(int x, int y)
        {
            if (x < 0 || x >= NumCols) throw new InvalidOperationException();
            if (y < 0 || y >= NumRows) throw new InvalidOperationException();

            // If the item is on the edge, it's visible
            if (x == 0 || y == 0 || x == NumCols - 1 || y == NumRows - 1)
                return true;
            return IsItemVisibleFromTheTop(x, y) || IsItemVisibleFromTheLeft(x, y) || IsItemVisibleFromTheRight(x, y) || IsItemVisibleFromTheBottom(x, y);
        }

        private int[] Col(int col)
        {
            return _rows.Select(r => r[col]).ToArray();
        }

        private int[] Row(int row)
        {
            return _rows[row].ToArray();
        }

        private bool IsItemVisibleFromTheTop(int x, int y)
        {
            var col = Col(x);
            for (int i = 0; i < y; i++)
            {
                if (col[i] >= col[y]) return false;
            }
            return true;
        }

        private bool IsItemVisibleFromTheBottom(int x, int y)
        {
            var col = Col(x);
            for (int i = NumRows - 1; i > y; i--)
            {
                if (col[i] >= col[y]) return false;
            }
            return true;
        }

        private bool IsItemVisibleFromTheLeft(int x, int y)
        {
            var row = Row(y);
            for (int i = 0; i < x; i++)
            {
                if (row[i] >= row[x]) return false;
            }
            return true;
        }

        private bool IsItemVisibleFromTheRight(int x, int y)
        {
            var row = Row(y);
            for (int i = NumCols - 1; i > x; i--)
            {
                if (row[i] >= row[x]) return false;
            }
            return true;
        }

        internal int ScenicScore(int x, int y)
        {
            return VisibleTreesLookingTop(x, y) * VisibleTreesLookingBottom(x, y) * VisibleTreesLookingLeft(x, y) * VisibleTreesLookingRight(x, y);
        }

        private int VisibleTreesLookingTop(int x, int y)
        {
            if (y == 0) return 0;
            var col = Col(x);
            int count = 0;
            for (int i = y - 1; i >= 0; i--)
            {
                count++;
                if (col[i] >= col[y]) break;
            }
            return count;
        }

        private int VisibleTreesLookingBottom(int x, int y)
        {
            if (y == NumRows - 1) return 0;
            var col = Col(x);
            int count = 0;
            for (int i = y + 1; i < NumRows; i++)
            {
                count++;
                if (col[i] >= col[y]) break;
            }
            return count;
        }

        private int VisibleTreesLookingLeft(int x, int y)
        {
            if (x == 0) return 0;
            var row = Row(y);
            int count = 0;
            for (int i = x - 1; i >= 0; i--)
            {
                count++;
                if (row[i] >= row[x]) break;
            }
            return count;
        }

        private int VisibleTreesLookingRight(int x, int y)
        {
            if (x == NumCols - 1) return 0;
            var row = Row(y);
            int count = 0;
            for (int i = x + 1; i < NumCols; i++)
            {
                count++;
                if (row[i] >= row[x]) break;
            }
            return count;
        }
    }
}