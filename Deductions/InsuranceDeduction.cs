using System;

namespace EmployeePayrollSystem.Deductions
{
    #region Class InsuranceDeduction
    // khau tru bao hiem bat buoc 10.5% (BHXH, BHYT, BHTN)
    public class InsuranceDeduction : IDeduction
    {
        #region Properties of InsuranceDeduction
        public string Name { get; set; }
        public string Description { get; set; }
        public double InsuranceRate { get; set; }
        public double MaxInsurableSalary { get; set; }
        #endregion

        #region Constructor of InsuranceDeduction
        // Constructor mac dinh: BHXH 10.5%, tran dong 36 trieu
        public InsuranceDeduction() : this(0.105, 36000000)
        {
        }

        // Constructor day du tham so (Overloaded Constructor)
        public InsuranceDeduction(double insuranceRate, double maxInsurableSalary)
        {
            if (insuranceRate < 0 || insuranceRate > 1)
            {
                throw new ArgumentException("Ty le dong bao hiem phai tu 0 den 1");
            }
            if (maxInsurableSalary <= 0)
            {
                throw new ArgumentException("Muc tran luong dong bao hiem phai lon hon 0");
            }

            this.Name = "Bao hiem bat buoc (10.5%)";
            this.InsuranceRate = insuranceRate;
            this.MaxInsurableSalary = maxInsurableSalary;
            this.Description = "Bao hiem " + (insuranceRate * 100) + "% (Tran: " + maxInsurableSalary + " VND)";
        }
        #endregion

        #region Methods of InsuranceDeduction
        public double Calculate(double grossPay)
        {
            if (grossPay <= 0)
            {
                return 0;
            }

            // muc dong bao hiem khong duoc vuot tran 36tr
            double insurableBase = Math.Min(grossPay, MaxInsurableSalary);
            return insurableBase * InsuranceRate;
        }
        #endregion
    }
    #endregion
}
