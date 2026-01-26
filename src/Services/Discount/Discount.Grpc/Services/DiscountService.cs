using Mapster;
using Grpc.Core;
using Discount.Grpc.Data;
using Microsoft.EntityFrameworkCore;
using Discount.Grpc.Models;

namespace Discount.Grpc.Services;

public class DiscountService
    (DiscountDbContext dbContext, ILogger<DiscountService> logger)
    : DiscountProtoService.DiscountProtoServiceBase
{

    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbContext.Coupons 
            .FirstOrDefaultAsync(c => c.ProductName == request.ProductName);

        if (coupon is null)
            coupon = new Coupon
            {
                ProductName = "No Discount",
                Description = "No Discount Desc",
                Amount = 0
            };

        logger.LogInformation("Discount is retrieved for ProductName : {ProductName}, Amount : {Amount}",
            coupon.ProductName, coupon.Amount);
            
        var couponModel = coupon.Adapt<CouponModel>();    
        return couponModel;
    }

    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>();

        if(coupon is null)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object"));

        dbContext.Add(coupon);
        await dbContext.SaveChangesAsync();

        logger.LogInformation("Discount is successfully created. ProductName: {ProductName}", coupon.ProductName);

        return coupon.Adapt<CouponModel>();
    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>();
        if (coupon is null)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object"));

        dbContext.Update(coupon);
        await dbContext.SaveChangesAsync();

        logger.LogInformation("Discount is successfully updated. ProductName: {ProductName}", coupon.ProductName);

        return coupon.Adapt<CouponModel>();
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbContext.Coupons
            .FirstOrDefaultAsync(x => x.ProductName == request.ProductName);
    
        if(coupon is null)
            throw new RpcException(new Status(StatusCode.NotFound, $"Discount with productName={request.ProductName} not found"));

        dbContext.Coupons.Remove(coupon);
        await dbContext.SaveChangesAsync();

        logger.LogInformation("Discount is successfully deleted. ProductName: {ProductName}", coupon.ProductName);

        return new DeleteDiscountResponse { Success = true };
    }
}