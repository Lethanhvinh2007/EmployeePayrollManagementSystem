# Employee Payroll Management System

- **Môn học**: Lập trình Hướng đối tượng (OOP) - Đợt 1 HK1 2026-2027
- **Trường**: Đại học Sư phạm Kỹ thuật TP.HCM (HCMUTE)
- **Giảng viên hướng dẫn**: TS. Huỳnh Xuân Phụng
- **Sinh viên thực hiện**: Lê Thanh Vinh - MSSV: 25110075
- **Repository**: [https://github.com/Lethanhvinh2007/EmployeePayrollManagementSystem](https://github.com/Lethanhvinh2007/EmployeePayrollManagementSystem)

---

## 1. Giới thiệu dự án & Tiến độ Tuần 4

Dự án xây dựng phần mềm quản lý và tính lương nhân viên bằng C# (.NET Console).

Tiếp nối tiến độ các tuần trước, trong **Tuần 4 (Hoàn thiện đồ án & Kiểm chứng OCP)**, dự án đã hoàn thành:

1. **Mở rộng tính năng kiểm chứng nguyên lý Open/Closed Principle (OCP)**:
   - **Nhân viên mới (`FreelanceContractorEmployee`)**: Bổ sung hình thức Cộng tác viên tính lương theo số dự án hoàn thành (`CompletedProjects * RatePerProject`).
   - **Khoản khấu trừ mới (`FlatFeeDeduction`)**: Bổ sung khoản phí cố định (Đoàn phí công đoàn 100.000 VND).
   - **Kiểm chứng OCP**: Đưa đối tượng mới vào `PayrollRunner` tính lương và xuất phiếu `Payslip` chính xác mà **hoàn toàn không cần sửa một ký tự nào trong `PayrollRunner.cs` và `Employee.cs`**.

2. **Hệ thống khấu trừ lương (Deductions - Tuần 3)**:
   - Tạo interface `IDeduction` dùng chung cho các loại giảm trừ.
   - `TaxDeduction`: Tính thuế TNCN có trừ mức gia cảnh 11 triệu (chỉ phần lương vượt 11 triệu mới bị tính thuế).
   - `InsuranceDeduction`: Tính bảo hiểm bắt buộc 10.5% (BHXH, BHYT, BHTN), có áp dụng mức trần đóng bảo hiểm tối đa 36 triệu.
   - Gắn danh sách `List<IDeduction>` vào `Employee` theo quan hệ Composition.

3. **Phiếu báo lương (Payslip) & Lương thực lĩnh (NetPay)**:
   - Tạo lớp `Payslip` để in thông tin chi tiết: kỳ lương, lương gộp, từng khoản bị trừ và thực lĩnh.
   - Công thức: `NetPay = GrossPay - Deductions`, có xử lý chặn không để lương thực lĩnh bị âm nếu tiền phạt/khấu trừ lớn hơn lương gộp.

4. **Điều phối tính lương hàng loạt (PayrollRunner)**:
   - Xây dựng lớp `PayrollRunner` quản lý danh sách nhân viên và gọi tính lương tự động.
   - Áp dụng tính Đa hình: chỉ cần duyệt danh sách gọi hàm `GeneratePayslip()` mà không cần dùng câu lệnh `if/else` để kiểm tra loại nhân viên, giúp code dễ mở rộng sau này.
   - Xuất bảng tổng hợp tiền lương công ty ra console dạng bảng dễ nhìn.

5. **Kiểm thử tự động (Unit Tests)**:
   - Bộ 21 test case tự động kiểm tra đầy đủ các kịch bản: nhân viên mới, tính Overtime 40h x1.5, hoa hồng cận biên 0% và 100%, tính lương đa hình và bắt lỗi ngoại lệ dữ liệu.

---

## 2. Cấu trúc thư mục mã nguồn

- `Deductions/`: Chứa các lớp tính khấu trừ
  - [`IDeduction.cs`](https://github.com/Lethanhvinh2007/EmployeePayrollManagementSystem/blob/main/Deductions/IDeduction.cs): Interface tính giảm trừ
  - [`TaxDeduction.cs`](https://github.com/Lethanhvinh2007/EmployeePayrollManagementSystem/blob/main/Deductions/TaxDeduction.cs): Tính thuế thu nhập cá nhân
  - [`InsuranceDeduction.cs`](https://github.com/Lethanhvinh2007/EmployeePayrollManagementSystem/blob/main/Deductions/InsuranceDeduction.cs): Tính bảo hiểm bắt buộc
  - [`FlatFeeDeduction.cs`](https://github.com/Lethanhvinh2007/EmployeePayrollManagementSystem/blob/main/Deductions/FlatFeeDeduction.cs): Khấu trừ phí cố định (Đoàn phí - OCP)
- `Models/`: Các lớp đối tượng thực thể
  - [`Employee.cs`](https://github.com/Lethanhvinh2007/EmployeePayrollManagementSystem/blob/main/Models/Employee.cs): Lớp trừu tượng cha, chứa thông tin chung và danh sách khấu trừ
  - [`SalariedEmployee.cs`](https://github.com/Lethanhvinh2007/EmployeePayrollManagementSystem/blob/main/Models/SalariedEmployee.cs): Nhân viên biên chế hưởng lương cố định
  - [`HourlyEmployee.cs`](https://github.com/Lethanhvinh2007/EmployeePayrollManagementSystem/blob/main/Models/HourlyEmployee.cs): Nhân viên tính công theo giờ (có OT x1.5 trên 40h)
  - [`CommissionEmployee.cs`](https://github.com/Lethanhvinh2007/EmployeePayrollManagementSystem/blob/main/Models/CommissionEmployee.cs): Nhân viên hưởng lương cứng + hoa hồng doanh số
  - [`FreelanceContractorEmployee.cs`](https://github.com/Lethanhvinh2007/EmployeePayrollManagementSystem/blob/main/Models/FreelanceContractorEmployee.cs): Cộng tác viên tính lương theo dự án (OCP)
  - [`Department.cs`](https://github.com/Lethanhvinh2007/EmployeePayrollManagementSystem/blob/main/Models/Department.cs): Quản lý danh sách nhân viên theo phòng ban
  - [`Company.cs`](https://github.com/Lethanhvinh2007/EmployeePayrollManagementSystem/blob/main/Models/Company.cs): Quản lý toàn bộ phòng ban trong công ty
  - [`Payslip.cs`](https://github.com/Lethanhvinh2007/EmployeePayrollManagementSystem/blob/main/Models/Payslip.cs): Thông tin phiếu lương chi tiết
- `Services/`:
  - [`PayrollRunner.cs`](https://github.com/Lethanhvinh2007/EmployeePayrollManagementSystem/blob/main/Services/PayrollRunner.cs): Bộ điều khiển chạy tính lương cho toàn bộ công ty
- `Tests/`:
  - [`PayrollUnitTests.cs`](https://github.com/Lethanhvinh2007/EmployeePayrollManagementSystem/blob/main/Tests/PayrollUnitTests.cs): Bộ 21 bài test tự động
- [`Program.cs`](https://github.com/Lethanhvinh2007/EmployeePayrollManagementSystem/blob/main/Program.cs): Điểm khởi chạy demo chương trình và chạy bộ test

---

## 3. Kết quả chạy kiểm thử (Unit Tests)

Hệ thống tích hợp 21 bài kiểm thử tự động, kết quả đạt **21/21 Pass (100%)**:

- **Nhóm 1 - Nhân viên mới**: Test lương tháng cơ bản, làm 0h nhận 0đ, doanh số 0đ.
- **Nhóm 2 - Overtime**: Test dưới 40h, chạm đúng biên 40h, và làm vượt 40h tính x1.5.
- **Nhóm 3 - Hoa hồng**: Test cận biên 0% (chỉ nhận lương cứng), cận biên 100% và mức 5% bình thường.
- **Nhóm 4 - Composition & Polymorphism**: Test thêm nhân viên vào phòng ban, tính tổng lương công ty đa hình.
- **Nhóm 5 - Bắt lỗi ngoại lệ (Validation)**: Test chặn lương âm, giờ làm quá 168h/tuần, hoa hồng sai khoảng và chặn trùng mã nhân viên.

---

## 4. Cách tải và chạy chương trình

Yêu cầu máy đã cài .NET SDK (bản 8.0 trở lên).

```bash
# 1. Clone repo về máy (nếu chưa có)
git clone https://github.com/Lethanhvinh2007/EmployeePayrollManagementSystem.git
cd EmployeePayrollManagementSystem

# 2. Biên dịch
dotnet build

# 3. Chạy chương trình
dotnet run
```
