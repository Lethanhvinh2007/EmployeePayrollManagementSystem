using System;
using System.Collections.Generic;
using EmployeePayrollSystem.Models;

namespace EmployeePayrollSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== DO AN LAP TRINH HUONG DOI TUONG (OOP) - HCMUTE ===");
            Console.WriteLine("De tai: Employee Payroll Management System (Tuan 1)");
            Console.WriteLine("Sinh vien: Le Thanh Vinh - MSSV: 25110075\n");

            TestKhoiTaoVaTinhLuongGop();
            TestKiemTraRangBuocDuLieu();

            Console.WriteLine("\n[Hoan tat kiem thu Tuan 1]");
        }

        #region 1. Kiem thu khoi tao va tinh luong gop
        static void TestKhoiTaoVaTinhLuongGop()
        {
            Console.WriteLine("=== 1. Kiem thu khoi tao 3 loai nhan vien va tinh luong gop ===");

            // 1. Nhan vien bien che (Salaried)
            SalariedEmployee emp1 = new SalariedEmployee("SAL01", "Nhi", "IT", 30000000);

            // 2. Nhan vien theo gio (Hourly) - lam 48h (tieu chuan 40h + 8h OT x1.5)
            HourlyEmployee emp2 = new HourlyEmployee("HOU02", "Ngoc", "CSKH", 100000, 48.0);

            // 3. Nhan vien hoa hong (Commission) - luong cung 8tr + doanh so 200tr hoa hong 5%
            CommissionEmployee emp3 = new CommissionEmployee("COM03", "Vinh", "Sales", 8000000, 200000000, 0.05);

            // Gom vao danh sach lop co so Employee de kiem tra tinh da hinh
            List<Employee> danhSach = new List<Employee>();
            danhSach.Add(emp1);
            danhSach.Add(emp2);
            danhSach.Add(emp3);

            Console.WriteLine("\nDanh sach nhan vien va luong gop (Gross Pay):");
            foreach (Employee emp in danhSach)
            {
                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine(emp.ToString());
                Console.WriteLine(" -> Loai hinh : " + emp.GetEmployeeType());
                Console.WriteLine(" -> Luong gop : " + emp.CalculateGrossPay().ToString("N0") + " VND");
            }
            Console.WriteLine("--------------------------------------------------");
        }
        #endregion

        #region 2. Kiem thu bat ngoai le du lieu sai (Validation)
        static void TestKiemTraRangBuocDuLieu()
        {
            Console.WriteLine("\n=== 2. Kiem thu bat ngoai le rang buoc du lieu (Validation) ===");

            // Test 1: Luong am
            try
            {
                Console.Write("Test luong am: ");
                SalariedEmployee loi1 = new SalariedEmployee("ERR01", "Quang", "Nhan su", -5000000);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("[Bat loi thanh cong] " + ex.Message);
            }

            // Test 2: Gio lam viec vuot 168h/tuan
            try
            {
                Console.Write("Test gio lam > 168h: ");
                HourlyEmployee loi2 = new HourlyEmployee("ERR02", "Huy", "Bao ve", 50000, 180);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("[Bat loi thanh cong] " + ex.Message);
            }

            // Test 3: Ty le hoa hong > 1.0 (100%)
            try
            {
                Console.Write("Test hoa hong > 100%: ");
                CommissionEmployee loi3 = new CommissionEmployee("ERR03", "Nam", "Sales", 5000000, 100000000, 1.5);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("[Bat loi thanh cong] " + ex.Message);
            }

            // Test 4: Ma nhan vien de trong
            try
            {
                Console.Write("Test ma de trong: ");
                SalariedEmployee loi4 = new SalariedEmployee("", "An", "Ke toan", 10000000);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("[Bat loi thanh cong] " + ex.Message);
            }
        }
        #endregion
    }
}
