using System;

namespace EmployeePayrollSystem.Models
{
    #region Subclass: FreelanceContractorEmployee
    // lop mo rong them de chung minh OCP (khong can sua code cu)
    public class FreelanceContractorEmployee : Employee
    {
        #region Fields of FreelanceContractorEmployee
        private int completedProjects;
        private double ratePerProject;
        #endregion

        #region Properties of FreelanceContractorEmployee
        public int CompletedProjects
        {
            get { return completedProjects; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("So luong du an khong duoc am!");
                }
                completedProjects = value;
            }
        }

        public double RatePerProject
        {
            get { return ratePerProject; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Don gia moi du an phai lon hon 0!");
                }
                ratePerProject = value;
            }
        }
        #endregion

        #region Constructor of FreelanceContractorEmployee
        public FreelanceContractorEmployee(
            string id, 
            string name, 
            string department, 
            int completedProjects, 
            double ratePerProject)
            : base(id, name, department)
        {
            this.CompletedProjects = completedProjects;
            this.RatePerProject = ratePerProject;
        }
        #endregion

        #region Methods of FreelanceContractorEmployee
        // luong gop = so du an * gia moi project
        public override double CalculateGrossPay()
        {
            return CompletedProjects * RatePerProject;
        }

        public override string GetEmployeeType()
        {
            return $"Cong tac vien Freelance ({CompletedProjects} du an x {RatePerProject:N0} VND)";
        }
        #endregion
    }
    #endregion
}
