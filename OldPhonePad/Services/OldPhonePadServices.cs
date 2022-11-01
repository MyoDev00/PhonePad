using OldPhonePad.IServices;
using System.Text;
using System.Text.RegularExpressions;

namespace OldPhonePad.Services
{
    public class OldPhonePadServices: IOldPhonePadServices
    {
        private Dictionary<char, char[]> NumToCharMap;
        public OldPhonePadServices()
        {
            NumToCharMap = new Dictionary<char, char[]>();
            NumToCharMap.Add('0', new[] { ' ' });
            NumToCharMap.Add('1', new[] { '&', '\'', '(' });
            NumToCharMap.Add('2', new[] { 'A', 'B', 'C' });
            NumToCharMap.Add('3', new[] { 'D', 'E', 'F' });
            NumToCharMap.Add('4', new[] { 'G', 'H', 'I' });
            NumToCharMap.Add('5', new[] { 'J', 'K', 'L' });
            NumToCharMap.Add('6', new[] { 'M', 'N', 'O' });
            NumToCharMap.Add('7', new[] { 'P', 'Q', 'R', 'S' });
            NumToCharMap.Add('8', new[] { 'T', 'U', 'V' });
            NumToCharMap.Add('9', new[] { 'W', 'X', 'Y', 'Z' });
        }

        public Dictionary<char, char[]> GetNumToCharMap()
        {
            return NumToCharMap;
        }

        public bool ValidateInput(string input)
        {
            Regex regex = new Regex(@"^[0-9\s\*]+#$");
            Match match = regex.Match(input);
            return match.Success;
        }
        public string ConvertKeyAsAlphabetic(string input)
        {
            #region initialize
            
            StringBuilder alphabeticBuilder = new StringBuilder();

            char? queue = null;
            int queueCount = 0;
            #endregion

            foreach (char item in input)
            {
                #region input => #
                if (item == '#')
                {
                    if (queueCount == 0)
                        return alphabeticBuilder.ToString();

                    var map = NumToCharMap[queue.Value];
                    int actualIndex = ConvertCountToIndex(map.Length, queueCount);

                    alphabeticBuilder.Append(map[actualIndex]);
                    return alphabeticBuilder.ToString();
                }
                #endregion

                #region input => *
                if (item == '*')
                {
                    if (queueCount == 0 && alphabeticBuilder.Length > 0)
                    {
                        alphabeticBuilder.Remove(alphabeticBuilder.Length - 1, 1);
                        continue;
                    }

                    queue = null;
                    queueCount = 0;
                    continue;
                }
                #endregion

                #region input => white space
                if (item == ' ')
                {
                    if (queueCount == 0)
                        continue;

                    var map = NumToCharMap[queue.Value];
                    int actualIndex = ConvertCountToIndex(map.Length, queueCount);

                    alphabeticBuilder.Append(map[actualIndex]);
                    queue = null;
                    queueCount = 0;
                    continue;
                }
                #endregion

                #region input => 0 to 9
                if (queue.HasValue)
                {
                    if (queue == item)
                    {
                        queueCount++;
                        continue;
                    }

                    var map = NumToCharMap[queue.Value];
                    int actualIndex = ConvertCountToIndex(map.Length, queueCount);

                    alphabeticBuilder.Append(map[actualIndex]);
                    queue = item;
                    queueCount = 1;
                    continue;
                }
                else
                {
                    queue = item;
                    queueCount = 1;
                }
                #endregion
            }

            return alphabeticBuilder.ToString();
        }
        private int ConvertCountToIndex(int length, int count)
        {
            int mod = count % length;
            return (mod == 0 ? length : mod) - 1;
        }

    }
}
