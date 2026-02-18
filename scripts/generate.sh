#!/usr/bin/env bash
# 从 proto 生成 C# 代码
set -e
cd "$(dirname "$0")/.."

echo "==> 生成 C# 代码 (Grpc.Tools)"
dotnet build csharp/Api.V1/Api.V1.csproj

echo ""
echo "完成。C# 代码由 Grpc.Tools 在 obj/ 生成并参与编译。"
