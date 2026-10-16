# Employee Payroll Management System

Đồ án môn học: Lập trình Hướng đối tượng (OOP)  
Trường: Đại học Sư phạm Kỹ thuật TP.HCM (HCMUTE)  
Giảng viên hướng dẫn: TS. Huỳnh Xuân Phụng  
Sinh viên thực hiện: Lê Thanh Vinh - MSSV: 25110075  
Repository: [https://github.com/Lethanhvinh2007/EmployeePayrollManagementSystem](https://github.com/Lethanhvinh2007/EmployeePayrollManagementSystem)  

---

## 1. Giới thiệu đề tài (Tiến độ: Tuần 3)

Chương trình quản lý và tính lương nhân viên xây dựng trên nền tảng C# .NET Console.

Dựa trên đề cương đồ án môn học và nhận xét của GVHD ở Tuần 2, giai đoạn Tuần 3 tập trung hiện thực hóa trọn vẹn các yêu cầu thiết kế hệ thống tính lương:

1. **Hệ thống khấu trừ (Interface IDeduction - Encapsulation & Composition):**
   - Định nghĩa giao diện trừu tượng `IDeduction` chuẩn hóa hành vi tính giảm trừ lương.
   - `TaxDeduction`: Tính thuế thu nhập cá nhân (PIT) có xét mức giảm trừ gia cảnh 11.000.000 VND (chỉ tính thuế đối với phần thu nhập chịu thuế vượt ngưỡng 11 triệu).
   - `InsuranceDeduction`: Tính bảo hiểm bắt buộc tỷ lệ 10.5% (BHXH 8%, BHYT 1.5%, BHTN 1%), áp dụng quy tắc chặn mức trần đóng bảo hiểm 36.000.000 VND (thu nhập vượt trần chỉ đóng tối đa trên 36 triệu).
   - Lớp `Employee` quản lý danh sách `List<IDeduction>` thông qua quan hệ **Composition**, đảm bảo logic khấu trừ không bị trùng lặp ở các lớp con (**Encapsulation**).
2. **Phiếu báo lương chi tiết (`Payslip`) & Tính thực lĩnh (`NetPay`):**
   - Sinh phiếu lương chi tiết `Payslip` cho từng nhân viên theo kỳ trả lương.
   - Tính lương thực lĩnh: `NetPay = GrossPay - TotalDeductions`.
   - Cơ chế bảo vệ cận biên: Đảm bảo thực lĩnh không bao giờ nhận giá trị âm (`Math.Max(0, netPay)`).
3. **Bộ điều khiển tính lương hàng loạt (`PayrollRunner` - Polymorphism):**
   - Áp dụng nguyên lý đa hình (Polymorphism) để tính toán lương, khấu trừ và sinh phiếu lương hàng loạt `RunPayroll()` cho toàn bộ nhân viên mà hoàn toàn không dùng câu lệnh rẽ nhánh `if/else` theo loại nhân viên.
   - Tuân thủ nguyên lý đóng mở (**Open/Closed Principle - OCP**): khi mở rộng thêm loại nhân viên mới, mã nguồn của `PayrollRunner` được giữ nguyên vẹn.
   - Xuất bảng tổng hợp tiền lương công ty `PrintSummaryReport()` có định dạng trực quan.
4. **Bộ kiểm thử tự động (21 Unit Tests):**
   - Kiểm tra đầy đủ kịch bản thực thi hoàn chỉnh (Happy Path end-to-end).
   - Kiểm tra kịch bản tính lương đa hình (Polymorphism/Inheritance).
   - Bắt các ngoại lệ dữ liệu sai và thao tác không hợp lệ (Validation/Error handling).

---

## 2. Cấu trúc mã nguồn (Tuần 3)

```text
Employee Payroll Management System/
│
├── Deductions/                      # [Mới Tuần 3] Module khấu trừ lương
│   ├── IDeduction.cs                # Giao diện chung cho các khoản giảm trừ
│   ├── TaxDeduction.cs              # Khấu trừ thuế TNCN (giảm trừ gia cảnh 11tr)
│   └── InsuranceDeduction.cs        # Khấu trừ bảo hiểm bắt buộc 10.5% (trần 36tr)
│
├── Models/                          # Các lớp thực thể đối tượng
│   ├── Employee.cs                  # Lớp cơ sở trừu tượng (Cập nhật Deductions & NetPay)
│   ├── SalariedEmployee.cs          # Nhân viên biên chế hưởng lương tháng
│   ├── HourlyEmployee.cs            # Nhân viên theo giờ & Overtime x1.5 (> 40h)
│   ├── CommissionEmployee.cs        # Nhân viên hoa hồng doanh số
│   ├── Department.cs                # Quản lý danh sách nhân viên phòng ban (Composition)
│   ├── Company.cs                   # Quản lý toàn bộ phòng ban công ty (Composition)
│   └── Payslip.cs                   # [Mới Tuần 3] Phiếu báo lương chi tiết
│
├── Services/                        # [Mới Tuần 3] Module dịch vụ & điều phối
│   └── PayrollRunner.cs             # Bộ điều phối tính lương hàng loạt đa hình
│
├── Tests/                           # Module kiểm thử tự động
│   └── PayrollUnitTests.cs          # Bộ 21 bài kiểm thử tự động (Happy path, Validation, Polymorphism)
│
├── Program.cs                       # Entry point chạy demo hệ thống & chạy 21 tests
├── EmployeePayrollSystem.csproj     # Cấu hình dự án .NET
└── README.md                        # Tài liệu tiến độ dự án
```

---

## 3. Danh mục 5 nhóm kiểm thử tự động (21 Unit Tests)

| Nhóm | Nội dung kiểm thử | Số ca test | Kết quả |
| :--- | :--- | :---: | :---: |
| **Nhóm 1** | Nhân viên mới nhận lương (Lương tháng cơ bản, 0h làm, 0đ doanh số) | 3 | Pass (100%) |
| **Nhóm 2** | Overtime (35h không OT, cận biên 40h chuẩn, 48h và 60h hưởng OT x1.5) | 4 | Pass (100%) |
| **Nhóm 3** | Hoa hồng cận biên (Cận biên dưới 0%, cận biên trên 100%, tiêu chuẩn 5%) | 3 | Pass (100%) |
| **Nhóm 4** | Composition và Polymorphism (Phòng ban, Công ty tính tổng lương đa hình) | 5 | Pass (100%) |
| **Nhóm 5** | Ràng buộc hợp lệ dữ liệu cơ bản (Lương âm, giờ > 168h, hoa hồng < 0 hoặc > 1, trùng mã NV) | 6 | Pass (100%) |
| **Tổng** | **Toàn bộ hệ thống kiểm thử** | **21** | **21/21 Pass (100%)** |

---

## 4. Hướng dẫn biên dịch và chạy chương trình

Yêu cầu môi trường: Cài đặt .NET SDK (.NET 8.0 trở lên).

```bash
# 1. Di chuyển vào thư mục dự án
cd "Employee Payroll Management System"

# 2. Biên dịch dự án
dotnet build

# 3. Chạy chương trình và kích hoạt 21 Unit Tests tự động
dotnet run
```
