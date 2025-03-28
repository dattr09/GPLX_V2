

dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.Tools

dotnet ef dbcontext scaffold "Server=localhost,1433;Database=QL_GPLX;User Id=sa;Password=Thanhdat53140;Encrypt=TrueTrustServerCertificate=True;Server=localhost,1433;Database=QL_GPLX;User Id=sa;Password=Thanhdat53140;Encrypt=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models --context AppDBContext

clear
dotnet clean
dotnet build
dotnet run

Server=localhost,1433;Database=QL_GPLX;User Id=sa;Password=Thanhdat53140;Encrypt=True;TrustServerCertificate=True;
sửa sau khi có kết quả thi thì xoá ở đăng kí thi