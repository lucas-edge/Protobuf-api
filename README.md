# protobuf-api

基于 Protocol Buffers 的 API 定义与代码生成，支持 **C#** 的 **gRPC** 与 **HTTP**（gRPC-Web）接口。

## 项目结构

```
proto/                 # 协议定义
  api/v1/
    hello.proto        # 消息定义
    service.proto      # 服务定义
csharp/                # C# 实现（Grpc.Tools 生成 + ASP.NET Core 服务端）
  Api.V1.sln
  Api.V1/
```

## 从 Proto 生成 C# 代码

| 语言 | 命令 | 说明 |
|------|------|------|
| **C#** | `dotnet build csharp/Api.V1/Api.V1.csproj` | Grpc.Tools 根据 `<Protobuf>` 从 proto 生成 C# 到 `obj/` 并参与编译 |

一键生成（需先安装 .NET 8 SDK）：

```bash
./scripts/generate.sh
```

## 前置要求

- **C#**: .NET 8 SDK

## 协议说明

- **gRPC**：标准 gRPC 调用（HTTP/2）。
- **HTTP**：通过 gRPC-Web 在 HTTP/1.1 或 HTTP/2 上暴露同一套 RPC，便于浏览器或只支持 HTTP 的客户端使用。

## C#

### 生成与运行

```bash
cd csharp
dotnet restore
dotnet build   # 自动从 proto 生成 C# 代码并编译
dotnet run --project Api.V1
```

- **gRPC** 端口：50052（HTTP/2）
- **gRPC-Web / HTTP** 端口：50053（HTTP/1.1 与 HTTP/2）

浏览器或 curl 可通过 50053 使用 gRPC-Web 调用同一服务。

### 依赖

- `Grpc.Tools`: 编译期从 `.proto` 生成 C# 客户端/服务端
- `Grpc.AspNetCore`: gRPC 服务端
- `Grpc.AspNetCore.Web`: gRPC-Web（HTTP）支持

## 修改协议后

1. 编辑 `proto/api/v1/*.proto`。
2. **C#**：在 `csharp/` 下执行 `dotnet build`，会重新根据 proto 生成并编译。

## 示例接口

| 方法        | 说明         |
|-------------|--------------|
| `SayHello`  | 简单问候     |
| `Check`     | 健康检查     |

可按需在 `proto/api/v1/` 中新增消息与服务，再在 C# 中实现对应逻辑。
