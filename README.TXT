

dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.Tools

dotnet ef dbcontext scaffold "Server=localhost,1433;Database=QL_GPLX;User Id=sa;Password=Thanhdat53140;Encrypt=TrueTrustServerCertificate=True;Server=localhost,1433;Database=QL_GPLX;User Id=sa;Password=Thanhdat53140;Encrypt=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models --context AppDBContext

clear
dotnet clean
dotnet build
dotnet run

MacBook's Dat
Server=localhost,1433;Database=QL_GPLX;User Id=sa;Password=Thanhdat53140;Encrypt=True;TrustServerCertificate=True;
PC's Dat
Server=DESKTOP-E9VB407\\MSSQLSERVER01;Database=QL_GPLX;Integrated Security=True;TrustServerCertificate=True;Encrypt=True;
---------------------
*Đăng kí khóa học
-Create
-Edit
---------------
*Lớp học
-Create
-Edit
-Details
--------------