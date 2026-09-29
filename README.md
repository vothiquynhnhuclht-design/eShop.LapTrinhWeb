# 🛒 eShop - Ứng dụng Thương mại Điện tử (Blazor Clean Architecture)

> **Dự án Môn học:** Lập trình Web / Phát triển Ứng dụng Doanh nghiệp với Blazor (.NET 8)  
> **Kiến trúc:** Clean Architecture (Onion / Hexagonal Architecture) & Plugin Pattern  
> **Repository:** [https://github.com/vothiquynhnhuclht-design/eShop.LapTrinhWeb](https://github.com/vothiquynhnhuclht-design/eShop.LapTrinhWeb)

---

## 📖 1. Giới thiệu Dự án

**eShop** là một giải pháp thương mại điện tử hoàn chỉnh được xây dựng trên nền tảng **ASP.NET Core Blazor (.NET 8)**, áp dụng triệt để nguyên lý thiết kế **Clean Architecture** (Kiến trúc Sạch) và **Plugin Architecture**. 

Hệ thống được thiết kế nhằm đảm bảo tính độc lập giữa các tầng: Logic nghiệp vụ cốt lõi không bị phụ thuộc vào Framework giao diện hay Cơ sở dữ liệu cụ thể, giúp dễ dàng bảo trì, kiểm thử (Unit Test), và chuyển đổi linh hoạt giữa các nguồn dữ liệu khác nhau (In-Memory hoặc SQL Server).

---

## 🏛️ 2. Cấu trúc Kiến trúc (Clean Architecture)

Dự án được phân chia thành các Project chuyên biệt theo từng tầng kiến trúc:

```
eShop/
│
├── 📁 eShop.CoreBusiness/                   # [Core Layer] Chứa Domain Entities & Business Rules
│   ├── Models/
│   │   ├── Product.cs                       # Thực thể Sản phẩm
│   │   ├── Order.cs                         # Thực thể Đơn hàng & Logic nghiệp vụ tính tổng/validate
│   │   ├── OrderLineItem.cs                 # Chi tiết mặt hàng trong đơn
│   │   └── Customer.cs
│
├── 📁 eShop.UseCases/                        # [Application Layer] Chứa Use Cases & Interfaces
│   ├── SearchProductScreen/                 # Use Case tìm kiếm sản phẩm
│   ├── ViewProductScreen/                   # Use Case xem sản phẩm & thêm vào giỏ
│   ├── ShoppingCartScreen/                  # Use Case xem giỏ hàng, cập nhật số lượng, xóa sản phẩm
│   ├── OrderConfirmationScreen/             # Use Case đặt hàng (Place Order) & xem xác nhận
│   ├── AdminPortal/                         # Use Cases cho Admin (Xem đơn hàng chờ, chi tiết, xử lý đơn)
│   └── PluginInterfaces/                    # Khai báo Interfaces cho DataStore, UI, StateStore
│
├── 📁 Plugins/                              # [Infrastructure / Data Access Layer]
│   ├── eShop.DataStore.HardCode/            # Plugin lưu trữ dữ liệu trong bộ nhớ (In-Memory) & State Store
│   └── eShop.DataStore.SQL.Dapper/          # Plugin giao tiếp SQL Server qua Micro-ORM Dapper
│       ├── ISqlDataAccess.cs & SqlDataAccess.cs
│       ├── ProductRepository.cs
│       └── OrderRepository.cs
│
├── 📁 eShop.Web.Module/                     # [Presentation Components - Razor Class Libraries]
│   ├── eShop.Web.Common/                    # Reusable UI Controls (CartCountComponent, SearchBarComponent)
│   ├── eShop.Web.CustomerPortal/            # Các trang giao diện Khách hàng (Sản phẩm, Giỏ hàng, Đặt hàng)
│   └── eShop.Web.AdminPortal/               # Các trang giao diện Quản trị viên (Đơn hàng chờ, Chi tiết đơn)
│
└── 📁 eShop/                                # [Host Application] Blazor Server .NET 8 Host
    ├── Components/
    │   ├── Layout/ (MainLayout, NavMenu)
    │   ├── Pages/ (Home, Login, Error)
    │   ├── App.razor & Routes.razor
    ├── Program.cs                           # Cấu hình DI, Authentication Cookie Middleware & APIs
    └── appsettings.json                     # Cấu hình chuỗi kết nối CSDL
```

---

## ✨ 3. Các Tính năng Chính

### 🛍️ Phân hệ Khách hàng (Customer Portal)
1. **Tìm kiếm & Xem danh mục sản phẩm:**
   - Tìm kiếm trực quan theo từ khóa (hỗ trợ bấm phím `Enter` hoặc click icon tìm kiếm).
   - Hiển thị danh sách dạng Bootstrap Cards hiện đại, responsive trên mọi thiết bị.
   - Xem chi tiết sản phẩm với hình ảnh độ phân giải cao và mô tả đầy đủ.
2. **Quản lý Giỏ hàng (Shopping Cart & State Management):**
   - Thêm sản phẩm vào giỏ hàng với số lượng tùy chỉnh.
   - Badge hiển thị số lượng giỏ hàng trên thanh điều hướng tự động cập nhật thời gian thực thông qua cơ chế **State Store Event Subscriptions** (`IShoppingCartStateStore`).
   - Màn hình giỏ hàng (`/cart`): Tăng giảm số lượng sản phẩm, xóa sản phẩm khỏi giỏ, tự động tính tổng tiền.
3. **Thanh toán & Xác nhận Đơn hàng (Checkout & Confirmation):**
   - Form thông tin giao hàng có tích hợp kiểm tra ràng buộc dữ liệu (**Data Annotations Validation**).
   - Tự động sinh mã định danh duy nhất (**Unique GUID Reference**) cho mỗi đơn hàng sau khi đặt thành công.
   - Màn hình xác nhận đơn hàng (`/orderconfirm/{uniqueId}`).

### 🔐 Phân hệ Quản trị (Admin Portal)
1. **Xác thực & Phân quyền (Authentication & Authorization):**
   - Hệ thống xác thực bằng **ASP.NET Core Cookie Authentication**.
   - Bảo vệ các đường dẫn Admin (`/outstandingorders`, `/orderdetail/{id}`) bằng `@attribute [Authorize]` và thẻ `<AuthorizeRouteView>`.
   - Màn hình Đăng nhập (`/login`) với Antiforgery Token và thông báo lỗi trực quan.
   - Tài khoản Admin mặc định: `admin` / `admin123`.
   - Nút Đăng xuất (`/logout`) an toàn, xóa Session Cookie và chuyển hướng người dùng.
2. **Xử lý Đơn hàng (Order Processing):**
   - Xem danh sách toàn bộ các đơn hàng chưa xử lý (Outstanding Orders).
   - Xem chi tiết thông tin khách hàng, địa chỉ giao hàng và danh sách các mặt hàng đã mua.
   - Chức năng **Mark as Processed**: Cập nhật trạng thái đơn hàng đã xử lý, ghi nhận tên tài khoản Admin thực hiện và thời gian xử lý (`DateProcessed`).

---

## 🛠️ 4. Công nghệ Sử dụng

- **Ngôn ngữ & Nền tảng:** C# (.NET 8.0 & .NET Framework 4.7.2 tương thích Clean Architecture).
- **Frontend UI:** Blazor Server Component Model, Razor Class Libraries (RCL), Bootstrap 5, Bootstrap Icons.
- **Micro-ORM & Data Access:** Dapper, `System.Data.SqlClient`, ADO.NET.
- **Cơ sở dữ liệu:** Microsoft SQL Server 2019 / 2022 (kèm file script khởi tạo CSDL `eShop.SchemaAndData.sql`).
- **Xác thực:** ASP.NET Core Cookie Authentication & Claims-based Authorization.

---

## 🚀 5. Hướng dẫn Cài đặt và Khởi chạy

### Bước 1: Yêu cầu môi trường
- Cài đặt **.NET 8 SDK** ([Tải tại đây](https://dotnet.microsoft.com/download/dotnet/8.0)).
- **Visual Studio 2022** (phiên bản 17.8 trở lên, có chọn workload *ASP.NET and web development*) hoặc **VS Code**.
- **Microsoft SQL Server** & **SQL Server Management Studio (SSMS)** (tùy chọn nếu dùng Plugin CSDL SQL Server).

### Bước 2: Clone dự án về máy
```bash
git clone https://github.com/vothiquynhnhuclht-design/eShop.LapTrinhWeb.git
cd eShop.LapTrinhWeb
```

### Bước 3: Khởi tạo Cơ sở dữ liệu SQL Server (Tùy chọn)
1. Mở SSMS và kết nối đến SQL Server của bạn.
2. Mở file script `eShop.SchemaAndData.sql` trong thư mục gốc dự án.
3. Thực thi script để tạo Database `eShop`, các bảng và nạp sẵn 20 sản phẩm mẫu.
4. Kiểm tra chuỗi kết nối trong `eShop/eShop/appsettings.json`:
   ```json
   "ConnectionStrings": {
     "eShopConnection": "Server=localhost;Database=eShop;Trusted_Connection=True;TrustServerCertificate=True;"
   }
   ```

### Bước 4: Chạy dự án
- **Cách 1:** Mở file `eShop/eShop.sln` bằng Visual Studio 2022, chọn dự án khởi động là `eShop` và nhấn **F5** (hoặc `Ctrl + F5`).
- **Cách 2:** Dùng Terminal / dòng lệnh:
  ```bash
  cd eShop/eShop
  dotnet run
  ```
- Mở trình duyệt và truy cập: `https://localhost:7xxx` (hoặc cổng hiển thị trên console).

---

## 👥 Thông tin Tác giả
- **Người thực hiện:** Võ Thị Quỳnh Như
- **Email:** vothiquynhnhuclht@gmail.com
- **GitHub:** [vothiquynhnhuclht-design](https://github.com/vothiquynhnhuclht-design)

---
*Dự án được xây dựng phục vụ mục đích học tập và nghiên cứu kiến trúc phần mềm sạch trên nền tảng .NET.*
