using Marten.Schema;

namespace Catalog.API.Data;

public class CatalogInitialData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        using var session = store.LightweightSession();

        if (await session.Query<Product>().AnyAsync())
            return;

        session.Store<Product>(GetPreConfiguredProducts());
        await session.SaveChangesAsync();
    }

    private static IEnumerable<Product> GetPreConfiguredProducts()
    {
        return new List<Product>
            {
                new Product()
                {
                    Id = new Guid("a37d162b-1aa9-4c93-a0ad-c36355e7393f"),
                    Name = "Canon EOS R5",
                    Description = "This mirrorless camera delivers stunning 45MP resolution with 8K video capabilities and advanced autofocus for professional photography.",
                    ImageFile = "camera-1.png",
                    Price = 3899.00M,
                    Category = new List<string> { "Camera" }
                },
                new Product()
                {
                    Id = new Guid("78eedb77-b19a-4dbc-8a7a-2adfa63d279c"),
                    Name = "Nikon Z6 II",
                    Description = "A versatile full-frame camera featuring dual processors for faster performance and improved low-light shooting.",
                    ImageFile = "camera-2.png",
                    Price = 1999.00M,
                    Category = new List<string> { "Camera" }
                },
                new Product()
                {
                    Id = new Guid("93eefca5-6aa1-4b7c-a55f-839e294f1233"),
                    Name = "Sony A7 IV",
                    Description = "Hybrid powerhouse with 33MP sensor, real-time tracking, and cinema-quality 4K video for creators.",
                    ImageFile = "camera-3.png",
                    Price = 2499.00M,
                    Category = new List<string> { "Camera" }
                },
                new Product()
                {
                    Id = new Guid("ad728659-1e6c-4917-b32b-88c79eb0154d"),
                    Name = "Fujifilm X-T5",
                    Description = "Compact APS-C camera with 40MP sensor and film simulations for retro-style shooting.",
                    ImageFile = "camera-4.png",
                    Price = 1699.00M,
                    Category = new List<string> { "Camera" }
                },
                new Product()
                {
                    Id = new Guid("4805ef06-f885-4cd8-903f-b87c6d7b3014"),
                    Name = "Leica Q3",
                    Description = "Luxury full-frame compact with 60MP sensor and versatile 28mm f/1.7 lens for premium imaging.",
                    ImageFile = "camera-5.png",
                    Price = 5995.00M,
                    Category = new List<string> { "Camera" }
                },
                new Product()
                {
                    Id = new Guid("5d143ccc-d230-4567-ba15-a64a1c49dc0a"),
                    Name = "DJI Osmo Pocket 3",
                    Description = "Portable gimbal camera with 1-inch sensor for stabilized 4K video on the go.",
                    ImageFile = "camera-6.png",
                    Price = 549.00M,
                    Category = new List<string> { "Camera" }
                },
                new Product()
                {
                    Id = new Guid("1031eef0-bd2e-4660-b8d9-598020a37cfc"),
                    Name = "GoPro Hero 12 Black",
                    Description = "Rugged action camera with HyperSmooth 6.0 stabilization and 5.3K video for extreme adventures.",
                    ImageFile = "camera-7.png",
                    Price = 399.00M,
                    Category = new List<string> { "Camera" }
                },
                new Product()
                {
                    Id = new Guid("efa507d5-97d1-489a-8fb9-29b622625bdc"),
                    Name = "Breville Smart Oven",
                    Description = "Versatile countertop oven with 11 cooking functions including air fry, roast, and dehydrate for perfect meals.",
                    ImageFile = "oven-1.png",
                    Price = 349.00M,
                    Category = new List<string> { "Home Kitchen" }
                },
                new Product()
                {
                    Id = new Guid("65dfe5c4-8372-4ad2-8d24-5d6ed92209fe"),
                    Name = "Dyson V15 Detect",
                    Description = "Cordless vacuum with laser dust detection, powerful suction, and up to 60 minutes runtime for deep cleaning.",
                    ImageFile = "vacuum-1.png",
                    Price = 749.00M,
                    Category = new List<string> { "Home Kitchen" }
                },
                new Product()
                {
                    Id = new Guid("6ab8dee7-6a26-4a6d-95fc-51304a116765"),
                    Name = "Samsung RS6 Refrigerator",
                    Description = "Smart French door fridge with AI energy mode, 23 cu ft capacity, and internal camera for inventory tracking.",
                    ImageFile = "fridge-1.png",
                    Price = 2199.00M,
                    Category = new List<string> { "White Appliances" }
                },
                new Product()
                {
                    Id = new Guid("3c5096a1-e935-42ec-a72e-60e4146d6f23"),
                    Name = "Bose QuietComfort 45",
                    Description = "Over-ear headphones with industry-leading noise cancellation and 24-hour battery life for immersive audio.",
                    ImageFile = "headphones-1.png",
                    Price = 329.00M,
                    Category = new List<string> { "Electronics" }
                },
                new Product()
                {
                    Id = new Guid("4bb93446-f17b-4c0a-b2de-b1d6e1e34ff0"),
                    Name = "Fitbit Charge 6",
                    Description = "Fitness tracker with GPS, heart rate monitoring, 7-day battery, and Google app integration for health insights.",
                    ImageFile = "fitness-1.png",
                    Price = 159.00M,
                    Category = new List<string> { "Wearables" }
                },
                new Product()
                {
                    Id = new Guid("3abb2637-e463-4bb8-b6c9-14c1d5b64278"),
                    Name = "IKEA Poäng Chair",
                    Description = "Iconic armchair with bentwood frame and cushioned seat for comfortable lounging in modern homes.",
                    ImageFile = "furniture-1.png",
                    Price = 199.00M,
                    Category = new List<string> { "Furniture" }
                },
                new Product()
                {
                    Id = new Guid("0160ee00-8428-4665-a04a-97ca0961a99d"),
                    Name = "YETI Rambler Tumbler",
                    Description = "Durable 20oz stainless steel tumbler keeps drinks ice-cold or piping hot for hours on any adventure.",
                    ImageFile = "tumbler-1.png",
                    Price = 35.00M,
                    Category = new List<string> { "Outdoor" }
                }
            };
    }
}