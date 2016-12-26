using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillInfo
{
    public interface IBillIdManager
    {
        /// <summary>
        /// تولید شناسه قبض مشترک
        /// </summary>
        /// <param name="customerNumber">شماره پرونده یا ردیف</param>
        /// <param name="_3digitZoneId">کد شهر سه رقمی</param>
        /// <param name="govermenralServiceProvider">شرکت ارائه دهنده خدمت</param>
        /// <returns></returns>
        string GenerateBillId(string customerNumber, string _3digitZoneId,
            GovermenralServiceProvider govermenralServiceProvider);
    }

    public class BillIdManager : IBillIdManager
    {
        private readonly ICheckDigit _checkDigit;

        public BillIdManager(ICheckDigit checkDigit)
        {
            _checkDigit = checkDigit;
        }
        public string GenerateBillId(string customerNumber, string _3digitZoneId,
            GovermenralServiceProvider govermenralServiceProvider = GovermenralServiceProvider.Abfa)
        {
            var modifiedCustomerNumber = customerNumber.Substring(0, customerNumber.Length - 1);//حذف رقم آخر شماره پرونده
            var zoneIdAndServiceProvider = _3digitZoneId + Convert.ToInt32(govermenralServiceProvider);
            var billId = _checkDigit.GetAppendedCheckDigitString(modifiedCustomerNumber + zoneIdAndServiceProvider, InsertLocation.Post);
            return billId;
        }
    }

    public enum GovermenralServiceProvider
    {
        Abfa = 1
    }
}
