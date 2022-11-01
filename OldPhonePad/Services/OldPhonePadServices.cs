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
            //one or more than character 0-9 or * and must end with #
            Regex regex = new Regex(@"^[0-9\s\*]+#$");
            Match match = regex.Match(input);
            return match.Success;
        }
        public string ConvertKeyAsAlphabetic(string input)
        {
            #region initialize
            
            StringBuilder alphabeticBuilder = new StringBuilder();

            //temporary store one character form input
            char? queue = null;
            //queue character count
            int queueCount = 0;
            #endregion

            foreach (char item in input)
            {
                //if item is #, reach end of string,
                //if no key in queue, return result
                //other wise, convert queue key to alphabetic and return result
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

                //if item is *, it will do backspace action
                //if no key in queue and there has previous converted charactes, remove last character from builder
                //if there has key in queue ,no need to convert to alphabetic, just remove key from queue and reset count
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

                //if item is white space,it is assume as waiting second and convert queue key to alphabetic
                //if no key in queue, no action needed and move to next key
                //if key in queue, convert it then reset queue and queueCount
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

                //if item is between 0 to 9, add to queue or convert to alphabetic
                //if queue is empty, add item to queue and set queueCount 1
                //if item is same as queue, increase queue Count 
                //otherwise, convert queue key to alphabetic and set item in queue
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
        /// <summary>
        /// Converts the character count to get mapping index.
        /// </summary>
        /// <example>
        /// map=["A","B","C"];
        /// key="1111";
        /// Length of map is 3 key count is 4,so count to index will be 0;
        /// </remark>
        /// <param name="length">The length of mapping char array.</param>
        /// <param name="count">The count of the key.</param>
        /// <returns>System.Int32.</returns>
        private int ConvertCountToIndex(int length, int count)
        {
            int mod = count % length;
            return (mod == 0 ? length : mod) - 1;
        }

    }
}
