# 高校社团管理系统

## 项目简介

高校社团管理系统是一个基于Vue 3 + .NET Web API + SQL Server的现代化社团管理平台，旨在提升高校社团管理效率，实现社团活动的信息化管理。

## 技术栈

### 前端
- **框架**: Vue 3
- **UI组件库**: Element Plus
- **路由**: Vue Router 4
- **状态管理**: Pinia
- **HTTP客户端**: Axios
- **构建工具**: Vite

### 后端
- **框架**: .NET Web API 9.0/10.0
- **ORM**: Entity Framework Core
- **数据库**: SQL Server 2022
- **认证**: JWT

## 项目结构

```
CollegeClubSystem/
├── frontend/                 # 前端Vue项目
│   ├── public/              # 静态资源
│   ├── src/
│   │   ├── api/             # API接口封装
│   │   ├── components/      # 公共组件
│   │   ├── router/          # 路由配置
│   │   ├── store/           # 状态管理
│   │   ├── utils/           # 工具函数
│   │   ├── views/           # 页面组件
│   │   ├── App.vue          # 根组件
│   │   └── main.js          # 入口文件
│   ├── .env                 # 环境变量
│   ├── package.json         # 依赖配置
│   └── vite.config.js       # Vite配置
├── backend/                 # 后端.NET项目
│   └── CollegeClubSystem/
│       ├── Controllers/     # API控制器
│       ├── Data/            # 数据库上下文
│       ├── DTOs/            # 数据传输对象
│       ├── Models/          # 实体模型
│       ├── Services/        # 业务逻辑层
│       ├── appsettings.json # 应用配置
│       └── Program.cs       # 启动类
├── database/                # 数据库脚本
│   └── init_database.sql   # 初始化脚本
└── README.md                # 项目说明
```

## 环境要求

### 前端
- Node.js >= 20.x
- npm >= 10.x

### 后端
- .NET SDK >= 9.0
- SQL Server 2022 (或SQL Server LocalDB)
- Visual Studio 2022 (推荐)

## 快速开始

### 1. 数据库配置

打开SQL Server Management Studio，执行数据库初始化脚本：

```sql
-- 运行 database/init_database.sql
```

### 2. 后端启动

```bash
cd backend/CollegeClubSystem
dotnet restore
dotnet run
```

后端服务将在 `http://localhost:5000` 启动

### 3. 前端启动

```bash
cd frontend
npm install
npm run dev
```

前端服务将在 `http://localhost:5173` 启动

## API接口

### 用户模块
- `POST /api/users/login` - 用户登录
- `POST /api/users/register` - 用户注册
- `GET /api/users/profile` - 获取个人信息
- `GET /api/users` - 获取用户列表 (管理员)

### 社团模块
- `GET /api/clubs` - 获取社团列表
- `GET /api/clubs/{id}` - 获取社团详情
- `POST /api/clubs` - 创建社团
- `POST /api/clubs/{id}/join` - 加入社团
- `POST /api/clubs/{id}/approve` - 审核社团 (管理员)

### 活动模块
- `GET /api/activities` - 获取活动列表
- `POST /api/activities` - 创建活动
- `POST /api/activities/{id}/apply` - 报名活动
- `POST /api/activities/{id}/approve` - 审核活动 (管理员)

### 财务模块
- `GET /api/finance` - 获取财务记录
- `POST /api/finance/apply` - 经费申请
- `POST /api/finance/reimburse` - 报销申请

## 角色权限

| 角色 | 权限 |
|------|------|
| SuperAdmin | 所有权限 |
| Admin | 用户管理、社团审核、活动审核、财务审核 |
| ClubManager | 社团管理、活动管理、财务申请 |
| Member | 查看社团、报名活动 |
| Student | 浏览社团、申请加入 |

## 测试账号

| 账号 | 密码 | 角色 |
|------|------|------|
| admin | admin123 | 超级管理员 |

## 开发说明

### 代码规范
- 前端：使用ESLint进行代码检查
- 后端：遵循C#编码规范

### 提交规范
- feat: 新功能
- fix: 修复bug
- docs: 文档更新
- style: 代码格式
- refactor: 代码重构

## 许可证

MIT License