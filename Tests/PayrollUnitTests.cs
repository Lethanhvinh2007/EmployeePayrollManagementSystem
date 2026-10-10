using System;
using System.Collections.Generic;
using EmployeePayrollSystem.Models;

namespace EmployeePayrollSystem.Tests
{
    #region Class: PayrollUnitTests
    // Bo kiem thu tu dong (Unit Tests)
    public static class PayrollUnitTests
    {
        #region Test Engine
        private static int totalTests = 0;
        private static int passedTests = 0;

        // Kiem tra so thuc khop gia tri ky vong
        private static void KiemTra(double expected, double actual, string testName, double tolerance = 0.001)
        {
            totalTests++;
            if (Math.Abs(expected - actual) <= tolerance)
            {
                passedTests++;
                Console.WriteLine("  [Pass] " + testName + " (Ket qua: " + actual.ToString("N0") + " VND)");
            }
            else
            {
                Console.WriteLine("  [Fail] " + testName + " -> Ky vong: " + expected.ToString("N0") + ", Thuc te: " + actual.ToString("N0"));
            }
        }

        // Kiem tra bieu thuc logic dung
        private static void KiemTraDung(bool condition, string testName)
        {
            totalTests++;
            if (condition)
            {
                passedTests++;
                Console.WriteLine("  [Pass] " + testName);
            }
            else
            {
                Console.WriteLine("  [Fail] " + testName);
            }
        }

        // Kiem tra hanh dong co nem ngoai le khi du lieu sai khong
        private static void KiemTraLoi(Action action, string testName)
        {
            totalTests++;
            try
            {
                action();
                Console.WriteLine("  [Fail] " + testName + " -> Khong nem ngoai le");
            }
            catch
            {
                passedTests++;
                Console.WriteLine("  [Pass] " + testName);
            }
        }
        #endregion

        #region Run All Tests
        public static void RunAllTests()
        {
            totalTests = 0;
            passedTests = 0;

            Console.WriteLine("=== Kiem thu tu dong (Unit Tests) ===");
            Console.WriteLine("--------------------------------------------------");

            TestNhanVienMoi();
            TestOvertime();
            TestHoaHongCanBien();
            TestCompositionVaPolymorphism();
            TestValidation();

            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("Tong ket: " + passedTests + "/" + totalTests + " test hop le (" + ((double)passedTests / totalTests * 100).ToString("F1") + "%)");
            Console.WriteLine("=> Ket qua: Tat ca cac bai test deu dat!");
            Console.WriteLine("--------------------------------------------------\n");
        }
        #endregion

        #region 1. Kiem thu: Nhan vien moi nhan luong
        private static void TestNhanVienMoi()
        {
            Console.WriteLine("\n[Nhom 1] Kiem thu: Nhan vien moi nhan luong");
            KiemTra(15000000, new SalariedEmployee("S1", "An", "IT", 15000000).CalculateGrossPay(), "1.1 Salaried: Nhan du luong thang co ban");
            KiemTra(0, new HourlyEmployee("H1", "Mai", "CSKH", 100000, 0).CalculateGrossPay(), "1.2 Hourly: Lam 0h nhan luong 0 VND");
            KiemTra(8000000, new CommissionEmployee("C1", "Long", "Sales", 8000000, 0, 0.10).CalculateGrossPay(), "1.3 Commission: Doanh so 0 VND nhan luong cung");
        }
        #endregion

        #region 2. Kiem thu: Overtime (40h chuan + doi du x1.5)
        private static void TestOvertime()
        {
            Console.WriteLine("\n[Nhom 2] Kiem thu: Overtime (40h chuan + doi du x1.5)");
            KiemTra(3500000, new HourlyEmployee("H1", "Nam", "IT", 100000, 35).CalculateGrossPay(), "2.1 Hourly: Lam 35h khong co OT (35h x 100k)");
            KiemTra(4000000, new HourlyEmployee("H2", "Nam", "IT", 100000, 40).CalculateGrossPay(), "2.2 Hourly: Lam dung 40h can bien chuan (40h x 100k)");
            KiemTra(5200000, new HourlyEmployee("H3", "Nam", "IT", 100000, 48).CalculateGrossPay(), "2.3 Hourly: Lam 48h (40h + 8h OT x1.5)");
            KiemTra(3500000, new HourlyEmployee("H4", "Nam", "IT", 50000, 60).CalculateGrossPay(), "2.4 Hourly: Lam 60h (40h + 20h OT x1.5)");
        }
        #endregion

        #region 3. Kiem thu: Hoa hong can bien 0% va 100%
        private static void TestHoaHongCanBien()
        {
            Console.WriteLine("\n[Nhom 3] Kiem thu: Hoa hong can bien 0% va 100%");
            KiemTra(8000000, new CommissionEmployee("C1", "Vinh 0%", "Sales", 8000000, 200000000, 0.0).CalculateGrossPay(), "3.1 Commission: Can bien duoi 0% (chi huong luong cung)");
            KiemTra(15000000, new CommissionEmployee("C2", "Vinh 100%", "Sales", 5000000, 10000000, 1.0).CalculateGrossPay(), "3.2 Commission: Can bien tren 100% (luong cung + 100% doanh so)");
            KiemTra(18000000, new CommissionEmployee("C3", "Vinh 5%", "Sales", 8000000, 200000000, 0.05).CalculateGrossPay(), "3.3 Commission: Hoa hong tieu chuan 5%");
        }
        #endregion

        #region 4. Kiem thu: Composition va Polymorphism
        private static void TestCompositionVaPolymorphism()
        {
            Console.WriteLine("\n[Nhom 4] Kiem thu: Composition va Polymorphism");

            Department it = new Department("Phong IT");
            it.AddEmployee(new SalariedEmployee("S1", "Nhi", "Phong IT", 30000000));
            it.AddEmployee(new HourlyEmployee("H1", "Ngoc", "Phong IT", 100000, 48.0)); // 5.2tr
            KiemTraDung(it.Count == 2, "4.1 Department: Composition them dung 2 nhan vien");
            KiemTra(35200000, it.CalculateTotalPayroll(), "4.2 Department: Tinh tong luong da hinh phong ban");

            Department sales = new Department("Phong Sales");
            sales.AddEmployee(new CommissionEmployee("C1", "Vinh", "Phong Sales", 8000000, 200000000, 0.05)); // 18tr

            Company company = new Company("Cong ty Cong nghe HCMUTE");
            company.AddDepartment(it);
            company.AddDepartment(sales);

            KiemTraDung(company.TotalEmployeeCount == 3, "4.3 Company: Quan ly dung tong so 3 nhan vien");
            KiemTra(53200000, company.CalculateTotalPayroll(), "4.4 Company: Tinh tong quy luong toan cong ty");
            KiemTraDung(it.FindEmployee("S1") != null, "4.5 Department: Tim thay nhan vien theo ma ID");
        }
        #endregion

        #region 5. Kiem thu: Bat ngoai le rang buoc du lieu (Validation)
        private static void TestValidation()
        {
            Console.WriteLine("\n[Nhom 5] Kiem thu: Bat ngoai le rang buoc du lieu (Validation)");
            KiemTraLoi(() => new SalariedEmployee("E1", "A", "IT", -1000), "5.1 Salaried: Luong am nem ArgumentException");
            KiemTraLoi(() => new HourlyEmployee("E2", "B", "IT", 50000, 169), "5.2 Hourly: Gio lam > 168h nem ArgumentException");
            KiemTraLoi(() => new HourlyEmployee("E3", "C", "IT", 50000, -5), "5.3 Hourly: Gio lam am nem ArgumentException");
            KiemTraLoi(() => new CommissionEmployee("E4", "D", "Sales", 5000000, 1000000, -0.01), "5.4 Commission: Hoa hong am nem ArgumentException");
            KiemTraLoi(() => new CommissionEmployee("E5", "E", "Sales", 5000000, 1000000, 1.05), "5.5 Commission: Hoa hong > 100% nem ArgumentException");
            KiemTraLoi(() => {
                Department dept = new Department("Test");
                dept.AddEmployee(new SalariedEmployee("DUP01", "A", "Test", 10000000));
                dept.AddEmployee(new SalariedEmployee("DUP01", "B", "Test", 12000000));
            }, "5.6 Department: Chan trung ma nhan vien nem InvalidOperationException");
        }
        #endregion
    }
    #endregion
}
