using System;

namespace EmployeePayrollSystem.Deductions
{
    #region Class FlatFeeDeduction
    // khoan khau tru co dinh (nhu doan phi, quy cong doan...)
    public class FlatFeeDeduction : IDeduction
    {
        #region Properties of FlatFeeDeduction
        public string Name { get; set; }
        public string Description { get; set; }
        public double FlatAmount { get; set; }
        #endregion

        #region Constructor of FlatFeeDeduction
        public FlatFeeDeduction(string name, double flatAmount)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Ten khoan phi khong duoc de trong");
            }
            if (flatAmount < 0)
            {
                throw new ArgumentException("So tien phi khong duoc la so am");
            }

            this.Name = name;
            this.FlatAmount = flatAmount;
            this.Description = "Khau tru co dinh: " + flatAmount.ToString("N0") + " VND";
        }
        #endregion

        #region Methods of FlatFeeDeduction
        public double Calculate(double grossPay)
        {
            if (grossPay <= 0)
            {
                return 0;
            }

            // khong tru qua so tien luong gop
            if (grossPay < FlatAmount)
            {
                return grossPay;
            }

            return FlatAmount;
        }
        #endregion
    }
    #endregion
}
