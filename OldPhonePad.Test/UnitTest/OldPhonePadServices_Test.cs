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
        public void ValidateInput_Should_Success(string input)
        {
            //#region Arrange
            //#endregion

            #region Act
            var result = oldPhonePadServices.ValidateInput(input);
            #endregion

            #region Assert
            Assert.True(result);
            #endregion

        }
        
        [Theory]
        [InlineData("")]
        [InlineData("#")]
        [InlineData("1234")]
        [InlineData("1#2#")]
        [InlineData("***")]
        public void ValidateInput_Should_Failed(string input)
        {
            //#region Arrange
            //#endregion

            #region Act
            var result = oldPhonePadServices.ValidateInput(input);
            #endregion

            #region Assert
            Assert.False(result);
            #endregion

        }

        [Fact]
        public void ConvertKeyAsAlphabetic_EachKeyMapping_Test()
        {
            //#region Arrange
            //#endregion

            #region Act
            foreach (var map in oldPhonePadServices.GetNumToCharMap())
            {
                for(int i = 1; i <= map.Value.Length; i++)
                {
                    var input = new string(map.Key, i)+"#";
                    var exceptedResult = map.Value[i-1].ToString();

                    var result = oldPhonePadServices.ConvertKeyAsAlphabetic(input);

                    #region Assert
                    Assert.Equal(exceptedResult,result);
                    #endregion
                }
            }
            
            #endregion
        }

        [Theory]
        [InlineData("4433555 555666#", "HELLO")]
        [InlineData("8 88777444666*664#", "TURING")]
        [InlineData("2222#", "A")]
        [InlineData("222222#", "C")]
        [InlineData("222 222#", "CC")]
        [InlineData("3333 3333#", "DD")]
        [InlineData("****#", "")]
        [InlineData("4*4*5*#", "")]
        [InlineData("333*#", "")]
        [InlineData("3330444*4 44*#", "F G")]
        [InlineData("0#", " ")]
        [InlineData("20220222#", "A B C")]
        public void ConvertKeyAsAlphabetic_RandomString_Test(string input,string exceptedResult)
        {
            //#region Arrange
            //#endregion

            #region Act
            var result = oldPhonePadServices.ConvertKeyAsAlphabetic(input);
            #endregion

            #region Assert
            Assert.Equal(exceptedResult, result);
            #endregion

        }
    }
}
