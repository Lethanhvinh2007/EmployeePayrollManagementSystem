# Employee Payroll Management System

Đồ án môn học: Lập trình Hướng đối tượng (OOP)  
Trường: Đại học Công Nghệ Kỹ thuật TP.HCM (HCMUTE)  
Giảng viên hướng dẫn: TS. Huỳnh Xuân Phụng  
Sinh viên thực hiện: Lê Thanh Vinh - MSSV: 25110075  

---

## 1. Giới thiệu đề tài (Tiến độ: Tuần 2)

Chương trình quản lý và tính lương nhân viên xây dựng trên nền tảng C# .NET Console.

Trong giai đoạn Tuần 2, dự án phát triển theo đúng định hướng và nhận xét của GVHD:
1. **Quan hệ thành phần (Composition):** Xây dựng lớp `Department` (quản lý danh sách `List<Employee>`) và lớp `Company` (quản lý danh sách `List<Department>`).
2. **Tính đa hình (Polymorphism):** Tính tổng lương toàn công ty và từng phòng ban bằng cách duyệt danh sách và kích hoạt `CalculateGrossPay()`, hoàn toàn không sử dụng câu lệnh rẽ nhánh `if/else` theo kiểu nhân viên.
3. **Bộ kiểm thử tự động (Automated Unit Tests):** Thay thế kịch bản kiểm thử thủ công bằng hệ thống tự động kiểm tra đối soát, bao phủ toàn bộ các kịch bản cận biên (nhân viên mới nhận lương, công thức Overtime 40h + phần dôi dư x1.5, hoa hồng cận biên 0% và 100%, bắt lỗi ngoại lệ).

---

## 2. Cấu trúc mã nguồn (Tuần 2)

Dự án được tổ chức rõ ràng theo từng gói chức năng:

- `Models/Employee.cs`: Lớp cơ sở trừu tượng chứa các thuộc tính chung (Id, Name, Department) và phương thức trừu tượng `CalculateGrossPay()`.
- `Models/SalariedEmployee.cs`: Lớp con kế thừa Employee, tính lương cố định tháng.
- `Models/HourlyEmployee.cs`: Lớp con kế thừa Employee, tính lương theo giờ và OT (40h chuẩn + dôi dư x1.5).
- `Models/CommissionEmployee.cs`: Lớp con kế thừa Employee, tính lương cứng kết hợp hoa hồng doanh số.
- `Models/Department.cs`: Quản lý danh sách nhân viên của phòng ban bằng Composition, tính tổng lương phòng ban đa hình.
- `Models/Company.cs`: Quản lý danh sách phòng ban toàn công ty bằng Composition, tính tổng quỹ lương công ty.
- `Tests/PayrollUnitTests.cs`: Bộ kiểm thử tự động bao gồm 21 ca test kiểm tra độ chính xác và bắt lỗi ngoại lệ.
- `Program.cs`: Trình diễn mô hình quản lý công ty và kích hoạt chạy toàn bộ các bài Unit Test tự động.
- `EmployeePayrollSystem.csproj`: File cấu hình dự án .NET.

---

## 3. Hướng dẫn biên dịch và chạy chương trình

Yêu cầu môi trường: Cài đặt .NET SDK (.NET 8.0 trở lên).

Thực hiện các lệnh sau trong terminal:

```bash
# Di chuyển vào thư mục dự án
cd "Employee Payroll Management System"

# Biên dịch dự án
dotnet build

# Chạy chương trình và bộ Unit Test tự động Tuần 2
dotnet run
```
