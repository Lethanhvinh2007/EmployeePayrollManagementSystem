using System;
using System.Collections.Generic;
using EmployeePayrollSystem.Models;

namespace EmployeePayrollSystem.Services
{
    // quan ly danh sach nhan vien va chay tinh luong hang loat
    public class PayrollRunner
    {
        #region Fields of PayrollRunner
        private List<Employee> employees = new List<Employee>();
        #endregion

        #region Properties of PayrollRunner
        public List<Employee> Employees
        {
            get { return employees; }
        }
        #endregion

        #region Methods of PayrollRunner
        // them nhan vien, kiem tra tranh trung ma NV
        public void AddEmployee(Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentException("Nhan vien khong duoc null!");
            }

            foreach (Employee e in employees)
            {
                if (e.Id == employee.Id)
                {
                    throw new InvalidOperationException("Nhan vien voi ma '" + employee.Id + "' da ton tai!");
                }
            }

            employees.Add(employee);
        }

        public void Clear()
        {
            employees.Clear();
        }

        // tinh luong cho ca danh sach nhan vien
        public List<Payslip> RunPayroll(string payPeriod)
        {
            if (employees.Count == 0)
            {
                throw new InvalidOperationException("Danh sach nhan vien dang rong!");
            }

            List<Payslip> payslips = new List<Payslip>();

            foreach (Employee emp in employees)
            {
                // goi ham da hinh tu lop con
                Payslip slip = emp.GeneratePayslip(payPeriod);
                payslips.Add(slip);
            }

            return payslips;
        }

        // in bang tong hop luong toan cong ty
        public void PrintSummaryReport(string payPeriod, List<Payslip> payslips)
        {
            double totalGross = 0;
            double totalDeductions = 0;
            double totalNet = 0;

            foreach (Payslip p in payslips)
            {
                totalGross += p.GrossPay;
                totalDeductions += p.TotalDeductions;
                totalNet += p.NetPay;
            }

            Console.WriteLine();
            Console.WriteLine($"=== Bang tong hop luong cong ty ({payPeriod}) ===");
            Console.WriteLine($" {"Ma NV",-8} | {"Ho va Ten",-12} | {"Luong gop",14} | {"Khau tru",12} | {"Thuc linh",14}");
            Console.WriteLine(new string('-', 72));

            foreach (Payslip slip in payslips)
            {
                Console.WriteLine($" {slip.EmployeeId,-8} | {slip.EmployeeName,-12} | {slip.GrossPay,10:N0} VND | {slip.TotalDeductions,8:N0} VND | {slip.NetPay,10:N0} VND");
            }

            Console.WriteLine(new string('-', 72));
            Console.WriteLine($" Tong cong ({payslips.Count} nhan vien) : | {totalGross,10:N0} VND | {totalDeductions,8:N0} VND | {totalNet,10:N0} VND");
        }
        #endregion
    }
}
