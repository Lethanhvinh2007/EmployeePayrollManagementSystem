using System;

namespace EmployeePayrollSystem.Models
{
    #region Subclass: SalariedEmployee
    //nhan vien bien che: luong thang co dinh
    public class SalariedEmployee : Employee
    {
        #region Properties of SalariedEmployee
        private double monthlySalary;

        public double MonthlySalary
        {
            get { return monthlySalary; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Muc luong thang khong duoc am");
                }
                monthlySalary = value;
            }
        }
        #endregion

        #region Constructor of SalariedEmployee
        public SalariedEmployee(string id, string name, string department, double monthlySalary)
            : base(id, name, department)
        {
            this.MonthlySalary = monthlySalary;
        }
        #endregion

        #region Methods of SalariedEmployee
        // ghi de tinh luong gop
        public override double CalculateGrossPay()
        {
            return MonthlySalary;
        }

        public override string GetEmployeeType()
        {
            return "Nhan vien bien che (Salaried)";
        }
        #endregion
    }
    #endregion
}
