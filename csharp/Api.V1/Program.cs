using Api.V1.Service;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);

// 添加 gRPC 服务（同时支持 gRPC 与 gRPC-Web/HTTP）
builder.Services.AddGrpc();
builder.Services.AddGrpcWeb(o => o.GrpcWebEnabled = true);

// Kestrel 允许 HTTP/2（gRPC）与 HTTP/1.1（gRPC-Web）
builder.WebHost.ConfigureKestrel(o =>
{
    o.ListenAnyIP(50052, listen =>
    {
        listen.Protocols = HttpProtocols.Http2;  // gRPC
    });
    o.ListenAnyIP(50053, listen =>
    {
        listen.Protocols = HttpProtocols.Http1AndHttp2; // gRPC-Web / HTTP
    });
});

var app = builder.Build();

app.UseGrpcWeb();
app.MapGrpcService<ApiServiceImpl>().EnableGrpcWeb();

app.MapGet("/", () => "ApiService gRPC/gRPC-Web 运行中。gRPC: 50052, gRPC-Web: 50053");

app.Run();
