using System;
using System.Collections.Generic;

namespace EmployeePayrollSystem.Models
{
    #region Class: Department
    // Lop dai dien cho mot phong ban quan ly danh sach nhan vien (Composition)
    public class Department
    {
        #region Fields of Department
        private string departmentName = "";
        private List<Employee> employees = new List<Employee>();
        #endregion

        #region Properties of Department
        public string DepartmentName
        {
            get { return departmentName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Ten phong ban khong duoc de trong");
                }
                departmentName = value;
            }
        }

        // Tra ve danh sach nhan vien trong phong ban
        public List<Employee> Employees
        {
            get { return employees; }
        }

        public int Count
        {
            get { return employees.Count; }
        }
        #endregion

        #region Constructor of Department
        public Department(string departmentName)
        {
            this.DepartmentName = departmentName;
        }
        #endregion

        #region Methods of Department
        // Them nhan vien vao phong ban (kiem tra tranh trung ma)
        public void AddEmployee(Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentException("Nhan vien khong duoc null");
            }

            foreach (Employee emp in employees)
            {
                if (emp.Id == employee.Id)
                {
                    throw new InvalidOperationException("Nhan vien voi ma '" + employee.Id + "' da ton tai trong phong ban " + departmentName);
                }
            }

            // Cap nhat phong ban cua nhan vien khop voi phong ban nay
            employee.Department = this.DepartmentName;
            employees.Add(employee);
        }

        // Xoa nhan vien khoi phong ban theo ma ID
        public bool RemoveEmployee(string employeeId)
        {
            Employee target = FindEmployee(employeeId);
            if (target != null)
            {
                return employees.Remove(target);
            }
            return false;
        }

        // Tim kiem nhan vien theo ma ID
        public Employee FindEmployee(string employeeId)
        {
            foreach (Employee emp in employees)
            {
                if (emp.Id == employeeId)
                {
                    return emp;
                }
            }
            return null;
        }

        // Tinh tong luong ca phong ban bang Da hinh (Polymorphism)
        // Duyet qua danh sach va goi CalculateGrossPay(), khong can kiem tra kieu con
        public double CalculateTotalPayroll()
        {
            double total = 0;
            foreach (Employee emp in employees)
            {
                total += emp.CalculateGrossPay();
            }
            return total;
        }

        public override string ToString()
        {
            return "Phong ban: " + DepartmentName + " (" + employees.Count + " nhan vien) - Tong luong: " + CalculateTotalPayroll().ToString("N0") + " VND";
        }
        #endregion
    }
    #endregion
}
