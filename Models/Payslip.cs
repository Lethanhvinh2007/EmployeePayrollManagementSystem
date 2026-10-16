using System;
using System.Collections.Generic;
using EmployeePayrollSystem.Deductions;

namespace EmployeePayrollSystem.Models
{
    #region Class Payslip
    // thong tin in tren phieu luong nhan vien
    public class Payslip
    {
        #region Properties of Payslip
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeType { get; set; }
        public string PayPeriod { get; set; }
        public string GeneratedAt { get; set; }

        public double GrossPay { get; set; }
        public List<IDeduction> Deductions { get; set; }
        public double TotalDeductions { get; set; }
        public double NetPay { get; set; }
        #endregion

        #region Constructor of Payslip
        public Payslip(string employeeId, string employeeName, string employeeType, string payPeriod, double grossPay, List<IDeduction> deductions)
        {
            this.EmployeeId = employeeId;
            this.EmployeeName = employeeName;
            this.EmployeeType = employeeType;
            this.PayPeriod = payPeriod;
            this.GrossPay = grossPay;
            this.Deductions = deductions != null ? deductions : new List<IDeduction>();
            this.GeneratedAt = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            // cong tong cac khoan khau tru lai
            double total = 0;
            foreach (IDeduction d in this.Deductions)
            {
                total += d.Calculate(grossPay);
            }
            this.TotalDeductions = total;
            // chan thuc linh khong bao gio bi am
            this.NetPay = Math.Max(0, grossPay - total);
        }
        #endregion

        #region Methods of Payslip
        //in phieu luong ra dang chuoi
        public override string ToString()
        {
            string output = "--- Phieu bao luong nhan vien ---\n";
            output += "Ky luong     : " + PayPeriod + "\n";
            output += "Ma nhan vien : " + EmployeeId + "\n";
            output += "Ho va ten    : " + EmployeeName + "\n";
            output += "Hinh thuc    : " + EmployeeType + "\n";
            output += "Luong gop    : " + GrossPay.ToString("N0") + " VND\n";
            output += "Cac khoan khau tru:\n";

            if (Deductions.Count == 0)
            {
                output += "  (Khong co)\n";
            }
            else
            {
                foreach (IDeduction d in Deductions)
                {
                    double amount = d.Calculate(GrossPay);
                    output += "  - " + d.Name + ": -" + amount.ToString("N0") + " VND\n";
                }
            }

            output += "Tong khau tru: -" + TotalDeductions.ToString("N0") + " VND\n";
            output += "Thuc linh    : " + NetPay.ToString("N0") + " VND\n";
            output += "---------------------------------";
            return output;
        }
        #endregion
    }
    #endregion
}
