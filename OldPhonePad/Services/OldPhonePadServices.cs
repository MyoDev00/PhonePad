using OldPhonePad.IServices;
using System.Text;
using System.Text.RegularExpressions;

namespace OldPhonePad.Services
{
    public class OldPhonePadServices: IOldPhonePadServices
    {
        public bool ValidateInput(string input)
        {
            Regex regex = new Regex(@"^[0-9\s\*]+#$");
            Match match = regex.Match(input);
            return match.Success;
        }
        public string ConvertKeyAsAlphabetic(string input)
        {
            #region initialize
            Dictionary<char, char[]> numToCharMap = new Dictionary<char, char[]>();
            numToCharMap.Add('0', new[] { ' ' });
            numToCharMap.Add('1', new[] { '&', '\'', '(' });
            numToCharMap.Add('2', new[] { 'A', 'B', 'C' });
            numToCharMap.Add('3', new[] { 'D', 'E', 'F' });
            numToCharMap.Add('4', new[] { 'G', 'H', 'I' });
            numToCharMap.Add('5', new[] { 'J', 'K', 'L' });
            numToCharMap.Add('6', new[] { 'M', 'N', 'O' });
            numToCharMap.Add('7', new[] { 'P', 'Q', 'R', 'S' });
            numToCharMap.Add('8', new[] { 'T', 'U', 'V' });
            numToCharMap.Add('9', new[] { 'W', 'X', 'Y', 'Z' });

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

                    var map = numToCharMap[queue.Value];
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

                    var map = numToCharMap[queue.Value];
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

                    var map = numToCharMap[queue.Value];
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
