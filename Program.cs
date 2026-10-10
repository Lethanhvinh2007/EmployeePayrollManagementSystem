using System;
using EmployeePayrollSystem.Models;
using EmployeePayrollSystem.Tests;

namespace EmployeePayrollSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Do an Lap trinh huong doi tuong (OOP) - HCMUTE ===");
            Console.WriteLine("De tai: Employee Payroll Management System (Tuan 2)");
            Console.WriteLine("Sinh vien: Le Thanh Vinh - MSSV: 25110075\n");

            // 1. Chay thu nghiem mo hinh phong ban va cong ty (Composition & Polymorphism)
            DemoCongTy();

            // 2. Chay bo kiem thu tu dong (Unit Tests)
            PayrollUnitTests.RunAllTests();

            Console.WriteLine("[Hoan tat chuong trinh]");
        }

        #region Trinh dien mo hinh Cong ty & Phong ban (Composition)
        static void DemoCongTy()
        {
            Console.WriteLine("=== Demo quan ly cong ty va phong ban (Composition) ===");

            // 1. Khoi tao cong ty
            Company company = new Company("Cong ty Cong nghe HCMUTE");

            // 2. Khoi tao phong ban IT va them cac nhan vien (Composition)
            Department itDept = new Department("Phong Ky thuat IT");
            SalariedEmployee salEmp = new SalariedEmployee("SAL01", "Nhi", "Phong Ky thuat IT", 30000000);
            HourlyEmployee houEmp = new HourlyEmployee("HOU02", "Ngoc", "Phong Ky thuat IT", 100000, 48.0); // 40h + 8h OT x1.5
            itDept.AddEmployee(salEmp);
            itDept.AddEmployee(houEmp);

            // 3. Khoi tao phong ban Sales va them nhan vien hoa hong
            Department salesDept = new Department("Phong Kinh doanh Sales");
            CommissionEmployee comEmp = new CommissionEmployee("COM03", "Vinh", "Phong Kinh doanh Sales", 8000000, 200000000, 0.05);
            salesDept.AddEmployee(comEmp);

            // 4. Them cac phong ban vao cong ty (Company so huu cac Department)
            company.AddDepartment(itDept);
            company.AddDepartment(salesDept);

            // 5. In bao cao quy luong toan cong ty bang Da hinh (Polymorphism)
            company.PrintPayrollReport();
        }
        #endregion
    }
}
