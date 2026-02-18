using Api.V1;
using Grpc.Core;

namespace Api.V1.Service;

/// <summary>
/// ApiService 实现：gRPC 与 ASP.NET Core 下同时支持 gRPC 与 gRPC-Web（HTTP）.
/// </summary>
public class ApiServiceImpl : ApiService.ApiServiceBase
{
    public override Task<HelloResponse> SayHello(HelloRequest request, ServerCallContext context)
    {
        var message = string.IsNullOrEmpty(request.Name)
            ? "Hello, World!"
            : $"Hello, {request.Name}!";
        return Task.FromResult(new HelloResponse { Message = message });
    }

    public override Task<HealthCheckResponse> Check(HealthCheckRequest request, ServerCallContext context)
    {
        return Task.FromResult(new HealthCheckResponse
        {
            Status = HealthCheckResponse.Types.Status.Serving
        });
    }
}
