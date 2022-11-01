﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OldPhonePad.IServices
{
    public interface IOldPhonePadServices
    {
        Dictionary<char, char[]> GetNumToCharMap();
        bool ValidateInput(string keyInput);
        string ConvertKeyAsAlphabetic(string keyInput);
    }
}
