using System;

namespace EmployeePayrollSystem.Models
{
    #region Subclass: HourlyEmployee
    // Nhan vien lam theo gio co tinh overtime
    public class HourlyEmployee : Employee
    {
        #region Properties of HourlyEmployee
        private double hourlyRate;
        private double hoursWorked;

        public double HourlyRate
        {
            get { return hourlyRate; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Don gia luong theo gio phai lon hon 0");
                }
                hourlyRate = value;
            }
        }

        public double HoursWorked
        {
            get { return hoursWorked; }
            set
            {
                if (value < 0 || value > 168)
                {
                    throw new ArgumentException("So gio lam viec trong tuan phai tu 0 den 168 gio");
                }
                hoursWorked = value;
            }
        }
        #endregion

        #region Constructor of HourlyEmployee
        public HourlyEmployee(string id, string name, string department, double hourlyRate, double hoursWorked)
            : base(id, name, department)
        {
            this.HourlyRate = hourlyRate;
            this.HoursWorked = hoursWorked;
        }
        #endregion

        #region Methods of HourlyEmployee
        // tinh luong: 40h dau tinh gia goc, gio vuot tinh x1.5
        public override double CalculateGrossPay()
        {
            double standardHours = Math.Min(HoursWorked, 40);
            double overtimeHours = Math.Max(0, HoursWorked - 40);

            return (standardHours * HourlyRate) + (overtimeHours * HourlyRate * 1.5);
        }

        //hien thi kem so gio OT neu co
        public override string GetEmployeeType()
        {
            double overtimeHours = Math.Max(0, HoursWorked - 40);
            if (overtimeHours > 0)
            {
                return "Nhan vien theo gio (" + HoursWorked + "h - co " + overtimeHours + "h OT)";
            }
            return "Nhan vien theo gio (" + HoursWorked + "h)";
        }
        #endregion
    }
    #endregion
}
