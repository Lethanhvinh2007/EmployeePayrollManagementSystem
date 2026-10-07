using System;

namespace EmployeePayrollSystem.Models
{
    #region Abstract base class: Employee
    //lop co so truu tuong chua thong tin chung cua nhan vien
    public abstract class Employee
    {
        #region Fields of Employee
        private string id = "";
        private string name = "";
        private string department = "";
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
        // Tinh luong gop, cac lop con se override theo cach tinh rieng
        public abstract double CalculateGrossPay();

        // Tra ve ten loai hinh nhan vien
        public abstract string GetEmployeeType();

        public override string ToString()
        {
            return "[" + Id + "] " + Name + " (" + GetEmployeeType() + ") - Phong: " + Department + " | Luong gop: " + CalculateGrossPay().ToString("N0") + " VND";
        }
        #endregion
    }
    #endregion
}
