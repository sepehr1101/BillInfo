using System;

namespace BillInfo
{
    public interface ICheckDigit
    {
        /// <summary>
        /// تولید کد دیجیت و سپس الحاق به رشته عددی اصلی
        /// </summary>
        /// <param name="originNumber">رشته عددی اصلی</param>
        /// <param name="insertLocation">محل قرار گیری کد دیجیت </param>
        /// <returns></returns>
        string GetAppendedCheckDigitString(string originNumber, InsertLocation? insertLocation);
    }

    public class CheckDigit : ICheckDigit
    {
        private string GenerateCheckDigit(string originNumber)
        {
            const int ZORE = 0;
            const int ONE = 1;
            const int TWO = 2;
            const int SEVEN = 7;
            const int ELEVEN = 11;
            int i = 1, j = 1, temp = 0, digitCode;
            do
            {
                j = j + ONE;
                if (j > SEVEN)
                {
                    j = TWO;
                }
                temp = temp + Convert.ToInt32(originNumber.Substring(originNumber.Length - i, ONE)) * j;
                i++;
            } while (originNumber.Length >= i);

            digitCode = temp % ELEVEN;
            digitCode = digitCode < TWO ? ZORE : ELEVEN - digitCode;           
            return digitCode.ToString().Trim();
        }
        //
        public string GetAppendedCheckDigitString(string originNumber, InsertLocation? insertLocation = InsertLocation.Post)
        {
            var checkDigit = GenerateCheckDigit(originNumber);
            switch (insertLocation)
            {
                case InsertLocation.Pre:
                    return String.Concat(checkDigit, checkDigit);
                case InsertLocation.Post:
                    return String.Concat(originNumber,checkDigit);
                default:
                    throw new NotImplementedException();
            }
        }
    }
    public enum InsertLocation
    {
        Pre,
        Post
    }
}
