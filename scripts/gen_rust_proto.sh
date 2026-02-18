#!/usr/bin/env bash
# 使用 protoc + protoc-gen-prost + protoc-gen-tonic 生成 Rust 代码
# 依赖: protoc, cargo install protoc-gen-prost protoc-gen-tonic
set -e
ROOT="$(cd "$(dirname "$0")/.." && pwd)"
cd "$ROOT"
PROTO_ROOT="$ROOT/proto"
OUT_DIR="$ROOT/src/generated"

mkdir -p "$OUT_DIR"

if ! command -v protoc >/dev/null 2>&1; then
  echo "错误: 未找到 protoc。请安装 Protocol Buffers 编译器。" >&2
  exit 1
fi
if ! command -v protoc-gen-prost >/dev/null 2>&1; then
  echo "错误: 未找到 protoc-gen-prost。请运行: cargo install protoc-gen-prost" >&2
  exit 1
fi
if ! command -v protoc-gen-tonic >/dev/null 2>&1; then
  echo "错误: 未找到 protoc-gen-tonic。请运行: cargo install protoc-gen-tonic" >&2
  exit 1
fi

protoc -I "$PROTO_ROOT" \
  "$PROTO_ROOT/api/v1/hello.proto" \
  "$PROTO_ROOT/api/v1/service.proto" \
  --prost_out="$OUT_DIR" \
  --tonic_out="$OUT_DIR"

echo "已生成 Rust 代码到 $OUT_DIR"
ls -la "$OUT_DIR"
