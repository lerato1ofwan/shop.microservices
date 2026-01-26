using Discount.Grpc;
using Grpc.Core;

namespace Discount.Grpc.Services;

public class DiscountService
{
    private readonly ILogger<DiscountService> _logger;
    public DiscountService(ILogger<DiscountService> logger)
    {
        _logger = logger;
    }
}