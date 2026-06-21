@echo off
echo 正在启动高校社团管理系统...

echo 1. 启动后端服务...
start "后端服务" cmd /k "cd backend\CollegeClubSystem && dotnet run"

echo 等待后端启动...
timeout /t 5 /nobreak > nul

echo 2. 启动前端服务...
start "前端服务" cmd /k "cd frontend && npm run dev"

echo 系统启动完成！
echo 后端地址: http://localhost:5000
echo 前端地址: http://localhost:5173
echo Swagger文档: http://localhost:5000/swagger