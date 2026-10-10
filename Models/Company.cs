using System;
using System.Collections.Generic;

namespace EmployeePayrollSystem.Models
{
    #region Class: Company
    // Lop dai dien cho toan bo cong ty quan ly cac phong ban (Composition)
    public class Company
    {
        #region Fields of Company
        private string companyName = "";
        private List<Department> departments = new List<Department>();
        #endregion

        #region Properties of Company
        public string CompanyName
        {
            get { return companyName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Ten cong ty khong duoc de trong");
                }
                companyName = value;
            }
        }

        public List<Department> Departments
        {
            get { return departments; }
        }

        // Tong so luong nhan vien tren toan bo cac phong ban
        public int TotalEmployeeCount
        {
            get
            {
                int count = 0;
                foreach (Department dept in departments)
                {
                    count += dept.Count;
                }
                return count;
            }
        }
        #endregion

        #region Constructor of Company
        public Company(string companyName)
        {
            this.CompanyName = companyName;
        }
        #endregion

        #region Methods of Company
        // Them mot phong ban moi vao cong ty
        public void AddDepartment(Department dept)
        {
            if (dept == null)
            {
                throw new ArgumentException("Phong ban khong duoc null");
            }

            foreach (Department d in departments)
            {
                if (d.DepartmentName.ToLower() == dept.DepartmentName.ToLower())
                {
                    throw new InvalidOperationException("Phong ban '" + dept.DepartmentName + "' da ton tai trong cong ty!");
                }
            }

            departments.Add(dept);
        }

        // Tim phong ban theo ten
        public Department FindDepartment(string deptName)
        {
            foreach (Department d in departments)
            {
                if (d.DepartmentName.ToLower() == deptName.ToLower())
                {
                    return d;
                }
            }
            return null;
        }

        // Them truc tiep mot nhan vien vao phong ban chi dinh
        public void AddEmployeeToDepartment(Employee employee, string departmentName)
        {
            Department dept = FindDepartment(departmentName);
            if (dept == null)
            {
                dept = new Department(departmentName);
                departments.Add(dept);
            }
            dept.AddEmployee(employee);
        }

        // Tinh tong quy luong toan cong ty (Polymorphism qua cac phong ban va nhan vien)
        public double CalculateTotalPayroll()
        {
            double total = 0;
            foreach (Department dept in departments)
            {
                total += dept.CalculateTotalPayroll();
            }
            return total;
        }

        // In bao cao tong hop luong toan cong ty
        public void PrintPayrollReport()
        {
            Console.WriteLine("\n=== Bao cao bang luong toan cong ty ===");
            Console.WriteLine("Cong ty: " + CompanyName + " | Tong so nhan vien: " + TotalEmployeeCount);
            Console.WriteLine("--------------------------------------------------");

            foreach (Department dept in departments)
            {
                Console.WriteLine("\n- " + dept.DepartmentName + " (" + dept.Count + " nhan vien):");
                Console.WriteLine("--------------------------------------------------");
                foreach (Employee emp in dept.Employees)
                {
                    Console.WriteLine("   " + emp.ToString());
                }
                Console.WriteLine("   -> Tong luong phong ban: " + dept.CalculateTotalPayroll().ToString("N0") + " VND");
            }

            Console.WriteLine("\n--------------------------------------------------");
            Console.WriteLine("=> Tong quy luong toan cong ty: " + CalculateTotalPayroll().ToString("N0") + " VND");
            Console.WriteLine("--------------------------------------------------\n");
        }
        #endregion
    }
    #endregion
}
