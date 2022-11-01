using OldPhonePad.IServices;
using OldPhonePad.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OldPhonePad.UnitTest
{
    public class OldPhonePadServices_Test
    {
        IOldPhonePadServices oldPhonePadServices;

        public OldPhonePadServices_Test()
        {
            oldPhonePadServices = new OldPhonePadServices();
        }

        [Theory]
        [InlineData("0#")]
        [InlineData("*11#")]
        [InlineData("0*1#")]
        [InlineData("0***1**#")]
        [InlineData("1234#")]
        [InlineData("000123456789#")]
        public async Task ValidateInput_Should_Success(string input)
        {
            var result = oldPhonePadServices.ValidateInput(input);
            Assert.True(result);
        }
        
        [Theory]
        [InlineData("")]
        [InlineData("#")]
        [InlineData("1234")]
        [InlineData("1#2#")]
        [InlineData("***")]
        public async Task ValidateInput_Should_Failed(string input)
        {
            var result = oldPhonePadServices.ValidateInput(input);
            Assert.False(result);
        }


    }
}
