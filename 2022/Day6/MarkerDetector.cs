using System.Linq;

namespace Day6
{
    internal class MarkerDetector
    {
        private readonly int _markerLength;
        public MarkerDetector(int markerLength)
        {
            _markerLength = markerLength;
        }
        internal int GetFirstMarkerPosition(string buffer)
        {
            var span = new Span<char>(buffer.ToArray());
            int currentChar = 0;
            while ((currentChar + _markerLength) < buffer.Length)
            {
                var slice = span.Slice(currentChar, _markerLength);
                if (slice.ToArray().Distinct().Count() == _markerLength)
                {
                    return currentChar + _markerLength;
                }
                currentChar++;
            }
            return buffer.Length;
        }
    }
}