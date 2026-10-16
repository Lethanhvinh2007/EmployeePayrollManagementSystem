using System;
using System.Collections.Generic;
using EmployeePayrollSystem.Deductions;

namespace EmployeePayrollSystem.Models
{
    #region Abstract base class: Employee
    //lop co so chua thong tin chung nhan vien va danh sach khau tru
    public abstract class Employee
    {
        #region Fields of Employee
        private string id = "";
        private string name = "";
        private string department = "";
        private List<IDeduction> deductions = new List<IDeduction>();
        #endregion

        #region Properties of Employee
        public string Id
        {
            get { return id; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Ma nhan vien khong duoc de trong");
                }
                id = value;
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Ten nhan vien khong duoc de trong");
                }
                name = value;
            }
        }

        public string Department
        {
            get { return department; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Phong ban khong duoc de trong");
                }
                department = value;
            }
        }

        public List<IDeduction> Deductions
        {
            get { return deductions; }
        }
        #endregion

        #region Constructor of Employee
        protected Employee(string id, string name, string department)
        {
            this.Id = id;
            this.Name = name;
            this.Department = department;
        }
        #endregion

        #region Methods of Employee
        //them khoan tru luong nhu thue, bao hiem
        public void AddDeduction(IDeduction deduction)
        {
            if (deduction == null)
            {
                throw new ArgumentException("Khoan khau tru khong duoc null");
            }

            //kiem tra tranh add trung
            foreach (IDeduction d in deductions)
            {
                if (d.Name == deduction.Name)
                {
                    throw new InvalidOperationException("Khoan khau tru '" + deduction.Name + "' da ton tai!");
                }
            }

            deductions.Add(deduction);
        }

        // Tinh luong gop, lop con tu override theo cach tinh rieng
        public abstract double CalculateGrossPay();

        //tra ve ten hinh thuc lam viec
        public abstract string GetEmployeeType();

        // Tinh tong tien khau tru
        public double CalculateTotalDeductions()
        {
            double gross = CalculateGrossPay();
            double total = 0;
            foreach (IDeduction d in deductions)
            {
                total += d.Calculate(gross);
            }
            return total;
        }

        // Thuc linh = gross - khau tru, chan khong de am
        public double CalculateNetPay()
        {
            double gross = CalculateGrossPay();
            double totalDeductions = CalculateTotalDeductions();
            return Math.Max(0, gross - totalDeductions);
        }

        //tao phieu luong chi tiet
        public Payslip GeneratePayslip(string payPeriod)
        {
            return new Payslip(Id, Name, GetEmployeeType(), payPeriod, CalculateGrossPay(), deductions);
        }

        public override string ToString()
        {
            return "[" + Id + "] " + Name + " (" + GetEmployeeType() + ") - Phong: " + Department + " | Luong gop: " + CalculateGrossPay().ToString("N0") + " VND | Thuc linh: " + CalculateNetPay().ToString("N0") + " VND";
        }
        #endregion
    }
    #endregion
}
