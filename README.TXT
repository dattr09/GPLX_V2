

dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.Tools


dotnet ef dbcontext scaffold "Server=localhost,1433;Database=QL_GPLX;User Id=sa;Password=Thanhdat53140;Encrypt=TrueTrustServerCertificate=True;Server=localhost,1433;Database=QL_GPLX;User Id=sa;Password=Thanhdat53140;Encrypt=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models --context AppDBContext

Server=DESKTOP-E9VB407\\DAT;Database=QL_GPLX;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;

Server=localhost,1433;Database=QL_GPLX;User Id=sa;Password=Thanhdat53140;Encrypt=True;TrustServerCertificate=True;
---------------NOTE---------------
---------------DEADLINE 16H30 17/03/2025---------------
CRUD GPLX
chưa xem thêm xoá sửa đc

CRUD KẾT QUẢ THI

Vi Phạm GPLX CHƯA THÊM VỚI SỬA ĐC

NÊN SẮP XẾP CÁC NÚT LẠI CHO PHÙ HỢP 

VD NẾU CÁC CHẮC NĂNG NÀO XEM THÌ ĐỂ CHUNG CÒN CHỨC NĂNG NÀO CẦN CRUD THÌ ĐỂ CHUNG 
clear
dotnet clean
dotnet build
dotnet run