namespace EmployeePayrollSystem.Deductions
{
    #region Interface IDeduction
    //interface chung cho cac khoan giam tru nhu thue, bao hiem...
    public interface IDeduction
    {
        string Name { get; }
        string Description { get; }

        // tinh so tien khau tru theo luong gop
        double Calculate(double grossPay);
    }
    #endregion
}
