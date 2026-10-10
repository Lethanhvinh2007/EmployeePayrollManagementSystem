# Employee Payroll Management System

Đồ án môn học: Lập trình Hướng đối tượng (OOP)  
Trường: Đại học Công Nghệ Kỹ thuật TP.HCM (HCMUTE)  
Giảng viên hướng dẫn: TS. Huỳnh Xuân Phụng  
Sinh viên thực hiện: Lê Thanh Vinh - MSSV: 25110075  

---

## 1. Giới thiệu đề tài

Chương trình quản lý và tính lương nhân viên được xây dựng trên nền tảng C# .NET Console.

Trong giai đoạn đầu (Tuần 1), dự án tập trung thiết kế lớp cơ sở trừu tượng `Employee` và 3 lớp con kế thừa tương ứng với các hình thức trả lương phổ biến:
- **SalariedEmployee**: Nhân viên biên chế hưởng lương tháng cố định.
- **HourlyEmployee**: Nhân viên làm việc theo giờ, có tính tiền làm thêm ngoài giờ (Overtime hệ số 1.5x) khi vượt quá định mức 40 giờ/tuần.
- **CommissionEmployee**: Nhân viên kinh doanh hưởng lương cứng cơ bản kết hợp phần trăm hoa hồng theo doanh số bán hàng.

---

## 2. Cấu trúc mã nguồn

Các lớp đối tượng được tổ chức tách riêng từng file trong thư mục `Models/`:

- `Models/Employee.cs`: Lớp cơ sở trừu tượng, định nghĩa các thuộc tính chung (Id, Name, Department) và phương thức trừu tượng `CalculateGrossPay()`.
- `Models/SalariedEmployee.cs`: Lớp con kế thừa Employee, tính lương cố định hàng tháng.
- `Models/HourlyEmployee.cs`: Lớp con kế thừa Employee, tính lương theo giờ và giờ làm thêm OT.
- `Models/CommissionEmployee.cs`: Lớp con kế thừa Employee, tính lương cứng kết hợp hoa hồng doanh số.
- `Program.cs`: Khởi tạo đối tượng, kiểm thử công thức tính lương gộp và kiểm tra các ràng buộc dữ liệu đầu vào (Validation).
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

# Chạy chương trình kiểm thử Tuần 1
dotnet run
```
