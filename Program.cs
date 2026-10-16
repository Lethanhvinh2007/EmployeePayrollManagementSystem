using System;
using System.Collections.Generic;
using EmployeePayrollSystem.Deductions;
using EmployeePayrollSystem.Models;
using EmployeePayrollSystem.Services;
using EmployeePayrollSystem.Tests;

namespace EmployeePayrollSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("==================================================================");
            Console.WriteLine("=== DO AN LAP TRINH HUONG DOI TUONG (OOP) - KHOA CNTT - HCMUTE ===");
            Console.WriteLine("=== De tai: Employee Payroll Management System (Tien do Tuan 3) ===");
            Console.WriteLine("=== Sinh vien: Le Thanh Vinh - MSSV: 25110075                  ===");
            Console.WriteLine("==================================================================\n");

            // 1. Chay mo hinh quan ly Phong ban & Cong ty bang Composition (Tuan 2)
            DemoCongTyTuan2();

            // 2. Chay nghiep vu Tuan 3: Khau tru (IDeduction), Phieu luong (Payslip) & PayrollRunner
            DemoPayrollRunnerTuan3();

            // 3. Chay toan bo bo kiem thu tu dong 33 Unit Tests bao phu tat ca Edge Cases
            PayrollUnitTests.RunAllTests();

            Console.WriteLine("[Hoan tat toan bo chuong trinh Tuan 3]");
        }

        #region 1. Trinh dien mo hinh Cong ty & Phong ban (Tuan 2)
        static void DemoCongTyTuan2()
        {
            Console.WriteLine("=== 1. DEMO MO HINH CONG TY & PHONG BAN (COMPOSITION - TUAN 2) ===");

            Company company = new Company("Cong ty Cong nghe HCMUTE");

            Department itDept = new Department("Phong Ky thuat IT");
            itDept.AddEmployee(new SalariedEmployee("SAL01", "Nhi", "Phong Ky thuat IT", 30000000));
            itDept.AddEmployee(new HourlyEmployee("HOU02", "Ngoc", "Phong Ky thuat IT", 100000, 48.0));

            Department salesDept = new Department("Phong Kinh doanh Sales");
            salesDept.AddEmployee(new CommissionEmployee("COM03", "Vinh", "Phong Kinh doanh Sales", 8000000, 200000000, 0.05));

            company.AddDepartment(itDept);
            company.AddDepartment(salesDept);

            company.PrintPayrollReport();
            Console.WriteLine();
        }
        #endregion

        #region 2. Trinh dien PayrollRunner & Khau tru IDeduction (Tuan 3)
        static void DemoPayrollRunnerTuan3()
        {
            Console.WriteLine("=== 2. DEMO HE THONG KHAU TRU & DIEU HANH TINH LUONG (TUAN 3) ===");
            Console.WriteLine("Mo hinh: IDeduction (TaxDeduction giam tru 11tr, InsuranceDeduction tran 36tr)");

            PayrollRunner runner = new PayrollRunner();

            // Khoi tao nhan vien va gan cac chinh sach khau tru
            SalariedEmployee emp1 = new SalariedEmployee("E001", "Tran Minh", "IT", 35000000);
            emp1.AddDeduction(new InsuranceDeduction(0.105, 36000000));
            emp1.AddDeduction(new TaxDeduction(0.10, 11000000));

            HourlyEmployee emp2 = new HourlyEmployee("E002", "Le Ngoc", "CSKH", 120000, 45.0); // 40h*120k + 5h*180k = 5.7tr
            emp2.AddDeduction(new InsuranceDeduction(0.105, 36000000));

            CommissionEmployee emp3 = new CommissionEmployee("E003", "Pham Vinh", "Sales", 10000000, 300000000, 0.05); // 10tr + 15tr = 25tr
            emp3.AddDeduction(new InsuranceDeduction(0.105, 36000000));
            emp3.AddDeduction(new TaxDeduction(0.10, 11000000));

            runner.AddEmployee(emp1);
            runner.AddEmployee(emp2);
            runner.AddEmployee(emp3);

            // Chay tinh luong ky thang 10/2026 bang Da hinh (Polymorphism)
            List<Payslip> payslips = runner.RunPayroll("Thang 10/2026");

            // In thu phieu luong chi tiet cho 1 nhan vien (Payslip.ToString())
            Console.WriteLine("\n[Mau phieu bao luong chi tiet - Payslip]:");
            Console.WriteLine(payslips[0]);

            // In bang tong hop luong toan bo nhan vien
            runner.PrintSummaryReport("Thang 10/2026", payslips);
            Console.WriteLine();
        }
        #endregion
    }
}
