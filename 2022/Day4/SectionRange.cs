namespace Day4
{
    internal class SectionRange
    {
        private SectionRange(int idFrom, int idTo)
        {
            IdFrom= idFrom;
            IdTo= idTo;
        }

        public int IdFrom { get; }
        public int IdTo { get; }

        public static SectionRange FromText(string range1Text)
        {
            var parts = range1Text.Split('-');
            return new SectionRange(int.Parse(parts[0]), int.Parse(parts[1]));
        }

        public bool FullyContains(SectionRange rangeOther)
        {
            return IdFrom <= rangeOther.IdFrom && IdTo >= rangeOther.IdTo;
        }

        internal bool OverlapsWith(SectionRange rangeOther)
        {
            return (IdFrom <= rangeOther.IdFrom && IdTo >= rangeOther.IdFrom) ||
                    (IdFrom <= rangeOther.IdTo && IdTo >= rangeOther.IdTo) ||
                    FullyContains(rangeOther) ||
                    rangeOther.FullyContains(this);
        }
    }
}