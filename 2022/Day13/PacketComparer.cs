using System.Text.Json.Nodes;

namespace Day13
{
    public class PacketComparer : IComparer<JsonArray>
    {
        public int Compare(JsonArray? array1, JsonArray? array2)
        {
            if (array1 == null) throw new InvalidOperationException();
            if (array2 == null) throw new InvalidOperationException();

            for (int i = 0; i < array1.Count; i++)
            {
                if (i >= array2.Count) return 1; // Right side is smaller, Not in order
                var node1 = array1[i];
                var node2 = array2[i];
                if (node1 is JsonArray subArray1 && node2 is JsonArray subArray2)
                {
                    var subIsOrdered = Compare(subArray1, subArray2);
                    if (subIsOrdered != 0) return subIsOrdered;
                }
                if (node1 is JsonValue subValue1 && node2 is JsonValue subValue2)
                {
                    if ((int)subValue1 > (int)subValue2) return 1;
                    if ((int)subValue1 < (int)subValue2) return -1;
                }
                if (node1 is JsonArray subArray11 && node2 is JsonValue subValue21)
                {
                    JsonArray subArray21 = new JsonArray(JsonValue.Create<int>((int)subValue21));
                    var subIsOrdered = Compare(subArray11, subArray21);
                    if (subIsOrdered != 0) return subIsOrdered;
                }
                if (node1 is JsonValue subValue13 && node2 is JsonArray subArray23)
                {
                    JsonArray subArray13 = new JsonArray(JsonValue.Create<int>((int)subValue13));
                    var subIsOrdered = Compare(subArray13, subArray23);
                    if (subIsOrdered != 0) return subIsOrdered;
                }
            }
            if (array1.Count == array2.Count) return 0;
            return -1;
        }
    }
}
