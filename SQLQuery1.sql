USE QLSinhVien;
GO
CREATE TABLE Users (
    Username NVARCHAR(50) PRIMARY KEY,
    Password NVARCHAR(50)
);


INSERT INTO Users (Username, Password) VALUES ('admin', '123');
CREATE TABLE LopHoc (
    MaLop NVARCHAR(10) PRIMARY KEY,    
    TenLop NVARCHAR(50) NOT NULL
);
CREATE TABLE SinhVien (
    MaSV NVARCHAR(10) PRIMARY KEY,
    TenSV NVARCHAR(50),
    GioiTinh NVARCHAR(10),
    NgaySinh DATE,
    TenLop NVARCHAR(50),
    );