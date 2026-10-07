using System;

namespace EmployeePayrollSystem.Models
{
    #region Subclass: CommissionEmployee
    // Nhan vien hoa hong doanh so: luong cung + % hoa hong
    public class CommissionEmployee : Employee
    {
        #region Fields of CommissionEmployee
        private double baseSalary;
        private double grossSales;
        private double commissionRate;
        #endregion

        #region Properties of CommissionEmployee
        public double BaseSalary
        {
            get { return baseSalary; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Luong co ban khong duoc am!");
                }
                baseSalary = value;
            }
        }

        public double GrossSales
        {
            get { return grossSales; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Doanh so ban hang khong duoc am!");
                }
                grossSales = value;
            }
        }

        public double CommissionRate
        {
            get { return commissionRate; }
            set
            {
                if (value < 0 || value > 1.0)
                {
                    throw new ArgumentException("Ty le hoa hong phai tu 0.0 den 1.0!");
                }
                commissionRate = value;
            }
        }

        // tien hoa hong nhan duoc tu doanh so
        public double CommissionEarned
        {
            get { return grossSales * commissionRate; }
        }
        #endregion

        #region Constructor of CommissionEmployee
        public CommissionEmployee(
            string id, 
            string name, 
            string department, 
            double baseSalary, 
            double grossSales, 
            double commissionRate)
            : base(id, name, department)
        {
            this.BaseSalary = baseSalary;
            this.GrossSales = grossSales;
            this.CommissionRate = commissionRate;
        }
        #endregion

        #region Methods of CommissionEmployee
        // tinh luong gop: luong cung + tien hoa hong
        public override double CalculateGrossPay()
        {
            return BaseSalary + CommissionEarned;
        }

        public override string GetEmployeeType()
        {
            return $"Nhan vien hoa hong (Doanh so: {GrossSales:N0}, Hoa hong: {CommissionRate * 100:F1}%)";
        }
        #endregion
    }
    #endregion
}
