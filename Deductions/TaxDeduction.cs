using System;

namespace EmployeePayrollSystem.Deductions
{
    #region Class TaxDeduction
    // thue thu nhap ca nhan (PIT) co giam tru gia canh 11tr
    public class TaxDeduction : IDeduction
    {
        #region Properties of TaxDeduction
        public string Name { get; set; }
        public string Description { get; set; }
        public double TaxRate { get; set; }
        public double PersonalRelief { get; set; }
        #endregion

        #region Constructor of TaxDeduction
        // Constructor mac dinh: thue 10%, muc giam tru 11 trieu
        public TaxDeduction() : this(0.10, 11000000)
        {
        }

        // Constructor day du tham so (Overloaded Constructor)
        public TaxDeduction(double taxRate, double personalRelief)
        {
            if (taxRate < 0 || taxRate > 1)
            {
                throw new ArgumentException("Thue suat phai nam trong khoang tu 0 den 1 (0% - 100%)");
            }
            if (personalRelief < 0)
            {
                throw new ArgumentException("Muc giam tru gia canh khong duoc la so am");
            }

            this.Name = "Thue thu nhap ca nhan (PIT)";
            this.TaxRate = taxRate;
            this.PersonalRelief = personalRelief;
            this.Description = "Thue " + (taxRate * 100) + "% tren phan thu nhap vuot " + personalRelief + " VND";
        }
        #endregion

        #region Methods of TaxDeduction
        public double Calculate(double grossPay)
        {
            if (grossPay <= 0)
            {
                return 0;
            }

            // chi tinh thue neu luong vuot muc giam tru 11tr
            double taxableIncome = grossPay - PersonalRelief;
            if (taxableIncome <= 0)
            {
                return 0;
            }

            return taxableIncome * TaxRate;
        }
        #endregion
    }
    #endregion
}
