-- Tạo cơ sở dữ liệu FoodOrderSystem
CREATE DATABASE FoodOrderSystem;
GO

-- Sử dụng cơ sở dữ liệu vừa tạo
USE [FoodOrderSystem]
GO

-- Tạo bảng Account
CREATE TABLE [dbo].[Account](
	[account_id] [int] IDENTITY(1,1) NOT NULL,
	[email] [nvarchar](50) NULL,
	[password] [nvarchar](50) NULL,
	[fullname] [nvarchar](50) NULL,
	[phone] [nvarchar](50) NULL,
	[status_acc] [int] NULL,
	[address] [nvarchar](50) NULL,
	[dob] [date] NULL,
	[gender] [int] NULL,
	[token] [nvarchar](max) NULL,
	[role] [int] NULL,
	[update_time] [time](7) NULL,
	[create_time] [time](7) NULL,
	[update_by] [nvarchar](max) NULL,
	[create_by] [nvarchar](max) NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[account_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

-- Tạo bảng Category (danh mục món ăn)
CREATE TABLE [dbo].[Category](
	[category_id] [int] IDENTITY(1,1) NOT NULL,
	[category_name] [nvarchar](50) NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[category_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

-- Tạo bảng Customer
CREATE TABLE [dbo].[Customer](
	[customer_id] [int] IDENTITY(1,1) NOT NULL,
	[account_id] [int] NULL,
	[point] [int] NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[customer_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

-- Tạo bảng Order
CREATE TABLE [dbo].[Order](
	[order_id] [int] NOT NULL,
	[order_date] [date] NULL,
	[note] [nvarchar](50) NULL,
	[totalPrice] [int] NULL,
	[status_order] [int] NULL,
	[status_payment] [int] NULL,
	[customer_id] [int] NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[order_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

-- Tạo bảng Order_detail
CREATE TABLE [dbo].[Order_detail](
	[detail_id] [int] IDENTITY(1,1) NOT NULL,
	[product_id] [int] NULL,
	[order_id] [int] NULL,
	[quantity] [int] NULL,
 CONSTRAINT [PK_Order_detail] PRIMARY KEY CLUSTERED 
(
	[detail_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

-- Tạo bảng Product (món ăn)
CREATE TABLE [dbo].[Product](
	[product_id] [int] IDENTITY(1,1) NOT NULL,
	[price] [int] NULL,
	[img] [nvarchar](50) NULL,
	[description] [nvarchar](max) NULL,
	[product_name] [nvarchar](50) NULL,
	[release_date] [date] NULL,
	[author] [nvarchar](50) NULL, -- Có thể đổi thành chef hoặc bỏ
	[quantity] [int] NULL,
	[status_pro] [int] NULL,
	[create_by] [nvarchar](max) NULL,
	[update_by] [nvarchar](max) NULL,
	[update_time] [time](7) NULL,
	[create_time] [time](7) NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[product_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

-- Tạo bảng Product_Category (liên kết món ăn với danh mục)
CREATE TABLE [dbo].[Product_Category](
	[product_id] [int] NOT NULL,
	[category_id] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[product_id] ASC,
	[category_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

-- Tạo bảng Product_Feedback (đánh giá món ăn)
CREATE TABLE [dbo].[Product_Feedback](
	[feedback_id] [int] IDENTITY(1,1) NOT NULL,
	[product_id] [int] NULL,
	[star] [int] NULL,
	[comment] [text] NULL,
	[customer_id] [int] NULL,
 CONSTRAINT [PK_Product_Feedback] PRIMARY KEY CLUSTERED 
(
	[feedback_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

-- Tạo bảng Shipping
CREATE TABLE [dbo].[Shipping](
	[ship_id] [int] NOT NULL,
	[address] [nvarchar](50) NULL,
	[status_ship] [int] NULL,
	[status_payment] [int] NULL,
	[staff_id] [int] NULL,
 CONSTRAINT [PK_Shipping] PRIMARY KEY CLUSTERED 
(
	[ship_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

-- Tạo bảng Staff
CREATE TABLE [dbo].[Staff](
	[staff_id] [int] IDENTITY(1,1) NOT NULL,
	[account_id] [int] NULL,
	[role] [int] NULL,
 CONSTRAINT [PK_Staff] PRIMARY KEY CLUSTERED 
(
	[staff_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

-- Chèn dữ liệu mẫu vào bảng Account
SET IDENTITY_INSERT [dbo].[Account] ON 
INSERT [dbo].[Account] ([account_id], [email], [password], [fullname], [phone], [status_acc], [address], [dob], [gender], [token], [role], [update_time], [create_time], [update_by], [create_by]) VALUES 
(1, N'customer1@gmail.com', N'5f4dcc3b5aa765d61d8327deb882cf99', N'Nguyen Van A', N'0912345678', 1, N'123 Tran Phu, Hanoi', CAST(N'1995-05-10' AS Date), 1, NULL, 0, NULL, NULL, NULL, NULL),
(2, N'seller@gmail.com', N'5f4dcc3b5aa765d61d8327deb882cf99', N'Tran Thi B', N'0987654321', 1, N'45 Le Loi, Hanoi', CAST(N'1990-03-15' AS Date), 1, NULL, 1, NULL, NULL, NULL, NULL),
(3, N'manager@gmail.com', N'5f4dcc3b5aa765d61d8327deb882cf99', N'Le Van C', N'0933456789', 1, N'67 Nguyen Trai, Hanoi', CAST(N'1985-07-20' AS Date), 0, NULL, 1, NULL, NULL, NULL, NULL),
(4, N'shipper@gmail.com', N'5f4dcc3b5aa765d61d8327deb882cf99', N'Pham Thi D', N'0978563412', 1, N'89 Hoang Hoa Tham, Hanoi', CAST(N'1992-11-25' AS Date), 1, NULL, 1, NULL, NULL, NULL, NULL),
(5, N'customer2@gmail.com', N'5f4dcc3b5aa765d61d8327deb882cf99', N'Hoang Van E', N'0945678901', 1, N'12 Ly Thuong Kiet, Hanoi', CAST(N'1998-09-30' AS Date), 0, NULL, 0, NULL, NULL, NULL, NULL),
(6, N'admin@gmail.com', N'5f4dcc3b5aa765d61d8327deb882cf99', N'A', N'0', 1, N'FPT', CAST(N'1969-06-09' AS Date), 1, NULL, 1, NULL, NULL, NULL, NULL);
SET IDENTITY_INSERT [dbo].[Account] OFF
GO

-- Chèn dữ liệu mẫu vào bảng Category
SET IDENTITY_INSERT [dbo].[Category] ON 
INSERT [dbo].[Category] ([category_id], [category_name]) VALUES 
(1, N'Món chính'),
(2, N'Món khai vị'),
(3, N'Tráng miệng'),
(4, N'Đồ uống'),
(5, N'Món chay');
SET IDENTITY_INSERT [dbo].[Category] OFF
GO

-- Chèn dữ liệu mẫu vào bảng Customer
SET IDENTITY_INSERT [dbo].[Customer] ON 
INSERT [dbo].[Customer] ([customer_id], [account_id], [point]) VALUES 
(1, 1, 50),
(2, 5, 20);
SET IDENTITY_INSERT [dbo].[Customer] OFF
GO

-- Chèn dữ liệu mẫu vào bảng Product (món ăn)
SET IDENTITY_INSERT [dbo].[Product] ON 
INSERT [dbo].[Product] ([product_id], [price], [img], [description], [product_name], [release_date], [author], [quantity], [status_pro], [create_by], [update_by], [update_time], [create_time]) VALUES 
(1, 50000, N'./img/pho.jpg', N'Phở bò truyền thống với nước dùng thơm ngon.', N'Phở Bò', CAST(N'2023-01-01' AS Date), N'Chef Nguyen', 50, 1, NULL, NULL, NULL, NULL),
(2, 30000, N'./img/spring_roll.jpg', N'Gỏi cuốn tươi ngon với tôm và rau củ.', N'Gỏi Cuốn', CAST(N'2023-02-01' AS Date), N'Chef Tran', 100, 1, NULL, NULL, NULL, NULL),
(3, 20000, N'./img/che.jpg', N'Chè đậu xanh ngọt mát.', N'Chè Đậu Xanh', CAST(N'2023-03-01' AS Date), N'Chef Le', 30, 1, NULL, NULL, NULL, NULL),
(4, 15000, N'./img/tra_da.jpg', N'Trà đá mát lạnh giải nhiệt.', N'Trà Đá', CAST(N'2023-04-01' AS Date), N'Chef Pham', 200, 1, NULL, NULL, NULL, NULL),
(5, 45000, N'./img/vegetarian.jpg', N'Cơm chay với rau củ tươi ngon.', N'Cơm Chay', CAST(N'2023-05-01' AS Date), N'Chef Hoang', 20, 1, NULL, NULL, NULL, NULL);
SET IDENTITY_INSERT [dbo].[Product] OFF
GO

-- Chèn dữ liệu mẫu vào bảng Product_Category
INSERT [dbo].[Product_Category] ([product_id], [category_id]) VALUES 
(1, 1), -- Phở Bò thuộc Món chính
(2, 2), -- Gỏi Cuốn thuộc Món khai vị
(3, 3), -- Chè Đậu Xanh thuộc Tráng miệng
(4, 4), -- Trà Đá thuộc Đồ uống
(5, 5); -- Cơm Chay thuộc Món chay
GO

-- Chèn dữ liệu mẫu vào bảng Staff
SET IDENTITY_INSERT [dbo].[Staff] ON 
INSERT [dbo].[Staff] ([staff_id], [account_id], [role]) VALUES 
(1, 2, 3),
(2, 3, 1),
(3, 4, 2),
(4, 6, 0);
SET IDENTITY_INSERT [dbo].[Staff] OFF
GO

-- Chèn dữ liệu mẫu vào bảng Order
INSERT [dbo].[Order] ([order_id], [order_date], [note], [totalPrice], [status_order], [status_payment], [customer_id]) VALUES 
(1, CAST(N'2025-03-05' AS Date), N'Giao nhanh', 80000, 1, 1, 1),
(2, CAST(N'2025-03-05' AS Date), N'Không hành', 45000, 0, 0, 2);
GO

-- Chèn dữ liệu mẫu vào bảng Order_detail
SET IDENTITY_INSERT [dbo].[Order_detail] ON 
INSERT [dbo].[Order_detail] ([detail_id], [product_id], [order_id], [quantity]) VALUES 
(1, 1, 1, 1), -- 1 Phở Bò
(2, 2, 1, 1), -- 1 Gỏi Cuốn
(3, 5, 2, 1); -- 1 Cơm Chay
SET IDENTITY_INSERT [dbo].[Order_detail] OFF
GO

-- Chèn dữ liệu mẫu vào bảng Product_Feedback
SET IDENTITY_INSERT [dbo].[Product_Feedback] ON 
INSERT [dbo].[Product_Feedback] ([feedback_id], [product_id], [star], [comment], [customer_id]) VALUES 
(1, 1, 5, N'Phở rất ngon, nước dùng đậm đà!', 1),
(2, 5, 4, N'Cơm chay ổn, nhưng hơi ít rau.', 2);
SET IDENTITY_INSERT [dbo].[Product_Feedback] OFF
GO

-- Chèn dữ liệu mẫu vào bảng Shipping
INSERT [dbo].[Shipping] ([ship_id], [address], [status_ship], [status_payment], [staff_id]) VALUES 
(1, N'123 Tran Phu, Hanoi', 1, 1, 3),
(2, N'12 Ly Thuong Kiet, Hanoi', 0, 0, 3);
GO

-- Thiết lập các ràng buộc khóa ngoại
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_Account] FOREIGN KEY([account_id])
REFERENCES [dbo].[Account] ([account_id])
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_Account]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Customer] FOREIGN KEY([customer_id])
REFERENCES [dbo].[Customer] ([customer_id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Customer]
GO
ALTER TABLE [dbo].[Order_detail]  WITH CHECK ADD  CONSTRAINT [FK_Order_detail_Order] FOREIGN KEY([order_id])
REFERENCES [dbo].[Order] ([order_id])
GO
ALTER TABLE [dbo].[Order_detail] CHECK CONSTRAINT [FK_Order_detail_Order]
GO
ALTER TABLE [dbo].[Order_detail]  WITH CHECK ADD  CONSTRAINT [FK_Order_detail_Product] FOREIGN KEY([product_id])
REFERENCES [dbo].[Product] ([product_id])
GO
ALTER TABLE [dbo].[Order_detail] CHECK CONSTRAINT [FK_Order_detail_Product]
GO
ALTER TABLE [dbo].[Product_Category]  WITH CHECK ADD  CONSTRAINT [FK__Product_C__categ__4AB81AF0] FOREIGN KEY([category_id])
REFERENCES [dbo].[Category] ([category_id])
GO
ALTER TABLE [dbo].[Product_Category] CHECK CONSTRAINT [FK__Product_C__categ__4AB81AF0]
GO
ALTER TABLE [dbo].[Product_Category]  WITH CHECK ADD  CONSTRAINT [FK__Product_C__produ__49C3F6B7] FOREIGN KEY([product_id])
REFERENCES [dbo].[Product] ([product_id])
GO
ALTER TABLE [dbo].[Product_Category] CHECK CONSTRAINT [FK__Product_C__produ__49C3F6B7]
GO
ALTER TABLE [dbo].[Product_Feedback]  WITH CHECK ADD  CONSTRAINT [FK_Product_Feedback_Customer] FOREIGN KEY([customer_id])
REFERENCES [dbo].[Customer] ([customer_id])
GO
ALTER TABLE [dbo].[Product_Feedback] CHECK CONSTRAINT [FK_Product_Feedback_Customer]
GO
ALTER TABLE [dbo].[Product_Feedback]  WITH CHECK ADD  CONSTRAINT [FK_Product_Feedback_Product] FOREIGN KEY([product_id])
REFERENCES [dbo].[Product] ([product_id])
GO
ALTER TABLE [dbo].[Product_Feedback] CHECK CONSTRAINT [FK_Product_Feedback_Product]
GO
ALTER TABLE [dbo].[Shipping]  WITH CHECK ADD  CONSTRAINT [FK_Shipping_Order] FOREIGN KEY([ship_id])
REFERENCES [dbo].[Order] ([order_id])
GO
ALTER TABLE [dbo].[Shipping] CHECK CONSTRAINT [FK_Shipping_Order]
GO
ALTER TABLE [dbo].[Shipping]  WITH CHECK ADD  CONSTRAINT [FK_Shipping_Staff] FOREIGN KEY([staff_id])
REFERENCES [dbo].[Staff] ([staff_id])
GO
ALTER TABLE [dbo].[Shipping] CHECK CONSTRAINT [FK_Shipping_Staff]
GO
ALTER TABLE [dbo].[Staff]  WITH CHECK ADD  CONSTRAINT [FK_Staff_Account] FOREIGN KEY([account_id])
REFERENCES [dbo].[Account] ([account_id])
GO
ALTER TABLE [dbo].[Staff] CHECK CONSTRAINT [FK_Staff_Account]
GO