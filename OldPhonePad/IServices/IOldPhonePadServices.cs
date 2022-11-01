using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OldPhonePad.IServices
{
    public interface IOldPhonePadServices
    {
        /// <summary>
        /// Gets the number to character map.
        /// </summary>
        /// <returns>Dictionary&lt;System.Char, System.Char[]&gt;.</returns>
        Dictionary<char, char[]> GetNumToCharMap();

        /// <summary>
        /// Validates the input.Input should include only 0-9 or * and must end with #;
        /// </summary>
        /// <param name="keyInput">The key input.</param>
        /// <returns><c>true</c> if input is valid, <c>false</c> otherwise.</returns>
        bool ValidateInput(string keyInput);

        /// <summary>
        /// Converts the input key into alphabetical letters using a mapping.
        /// </summary>
        /// <param name="keyInput">The key input.</param>
        /// <returns>System.String.</returns>
        string ConvertKeyAsAlphabetic(string keyInput);
    }
}
