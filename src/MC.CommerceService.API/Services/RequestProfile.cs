using AutoMapper;
using MC.CommerceService.API.ClientModels;
using MC.CommerceService.API.Data.Models;

namespace MC.CommerceService.API.Services
{
    /// <summary>
    /// Configures mappings for the product data transfer object to the product entity model.
    /// This mapping configuration is used by AutoMapper to convert between <see cref="ProductRequest"/> DTOs
    /// and <see cref="Product"/> entities, ensuring that data is transferred correctly and efficiently
    /// between different layers of the application.
    /// </summary>
    public class ProductRequestProfile : Profile
    {
        public ProductRequestProfile()
        {
            CreateMap<ProductRequest, Product>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.Stock, opt => opt.MapFrom(src => src.Stock));
        }
    }

    /// <summary>
    /// Defines an AutoMapper profile for mapping between different instances of the <see cref="Product"/> class.
    /// This configuration is typically used for cloning objects or applying updates to existing objects without
    /// affecting certain system-managed properties like timestamps and record statuses.
    /// </summary>
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, Product>()
                .ForMember(dest => dest.ProductId, opt => opt.Ignore())
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.Stock, opt => opt.MapFrom(src => src.Stock))
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.LastUpdatedAt, opt => opt.Ignore());
        }
    }

    /// <summary>
    /// Configures mappings for the category data transfer object to the category entity model.
    /// This mapping configuration is used by AutoMapper to convert between <see cref="CategoryRequest"/> DTOs
    /// and <see cref="Category"/> entities, ensuring that data is transferred correctly and efficiently
    /// between different layers of the application.
    /// </summary>
    public class CategoryRequestProfile : Profile
    {
        public CategoryRequestProfile()
        {
            CreateMap<CategoryRequest, Category>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.CategoryName));
        }
    }

    /// <summary>
    /// Defines an AutoMapper profile for mapping between different instances of the <see cref="Category"/> class.
    /// This configuration is typically used for cloning objects or applying updates to existing objects without
    /// affecting certain system-managed properties like timestamps and record statuses.
    /// </summary>
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, Category>()
                .ForMember(dest => dest.CategoryId, opt => opt.Ignore())
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.CategoryName))
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.LastUpdatedAt, opt => opt.Ignore());
        }
    }

    /// <summary>
    /// Configures mappings for the order data transfer object to the order entity model.
    /// This mapping configuration is used by AutoMapper to convert between <see cref="OrderRequest"/> DTOs
    /// and <see cref="Order"/> entities, ensuring that data is transferred correctly and efficiently
    /// between different layers of the application.
    /// </summary>
    public class OrderRequestProfile : Profile
    {
        public OrderRequestProfile()
        {
            CreateMap<OrderRequest, Order>()
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount))
                .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.OrderDate));
        }
    }

    /// <summary>
    /// Defines an AutoMapper profile for mapping between different instances of the <see cref="Order"/> class.
    /// This configuration is typically used for cloning objects or applying updates to existing objects without
    /// affecting certain system-managed properties like timestamps and record statuses.
    /// </summary>
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, Order>()
                .ForMember(dest => dest.OrderId, opt => opt.Ignore())
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount))
                .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.OrderDate))
                .ForMember(dest => dest.OrderProducts, opt => opt.Ignore()) // Ignore the OrderProducts collection
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.LastUpdatedAt, opt => opt.Ignore());
        }
    }

    /// <summary>
    /// Configures mappings for the customer data transfer object to the customer entity model.
    /// This mapping configuration is used by AutoMapper to convert between <see cref="CustomerRequest"/> DTOs
    /// and <see cref="Customer"/> entities, ensuring that data is transferred correctly and efficiently
    /// between different layers of the application.
    /// </summary>
    public class CustomerRequestProfile : Profile
    {
        public CustomerRequestProfile()
        {
            CreateMap<CustomerRequest, Customer>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone));
        }
    }

    /// <summary>
    /// Defines an AutoMapper profile for mapping between different instances of the <see cref="Customer"/> class.
    /// This configuration is typically used for cloning objects or applying updates to existing objects without
    /// affecting certain system-managed properties like timestamps and record statuses.
    /// </summary>
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, Customer>()
                .ForMember(dest => dest.CustomerId, opt => opt.Ignore())
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.LastUpdatedAt, opt => opt.Ignore());
        }
    }

    /// <summary>
    /// Configures mappings for the order product data transfer object to the order product entity model.
    /// This mapping configuration is used by AutoMapper to convert between <see cref="OrderProductRequest"/> DTOs
    /// and <see cref="OrderProduct"/> entities, ensuring that data is transferred correctly and efficiently
    /// between different layers of the application.
    /// </summary>
    public class OrderProductRequestProfile : Profile
    {
        public OrderProductRequestProfile()
        {
            CreateMap<OrderProductRequest, OrderProduct>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));
        }
    }

    /// <summary>
    /// Defines an AutoMapper profile for mapping between different instances of the <see cref="OrderProduct"/> class.
    /// This configuration is typically used for cloning objects or applying updates to existing objects without
    /// affecting certain system-managed properties like timestamps and record statuses.
    /// </summary>
    public class OrderProductProfile : Profile
    {
        public OrderProductProfile()
        {
            CreateMap<OrderProduct, OrderProduct>()
                .ForMember(dest => dest.OrderId, opt => opt.Ignore()) // Ignore the OrderId during updates
                .ForMember(dest => dest.ProductId, opt => opt.Ignore()) // Ignore the ProductId during updates
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore()) // Ignore system-managed fields
                .ForMember(dest => dest.LastUpdatedAt, opt => opt.Ignore());
        }
    }

    /// <summary>
    /// Configures mappings for the product-category data transfer object to the product-category entity model.
    /// This mapping configuration is used by AutoMapper to convert between <see cref="ProductCategoryRequest"/> DTOs
    /// and <see cref="ProductCategory"/> entities, ensuring that data is transferred correctly and efficiently
    /// between different layers of the application.
    /// </summary>
    public class ProductCategoryRequestProfile : Profile
    {
        public ProductCategoryRequestProfile()
        {
            CreateMap<ProductCategoryRequest, ProductCategory>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId));
        }
    }

    /// <summary>
    /// Defines an AutoMapper profile for mapping between different instances of the <see cref="ProductCategory"/> class.
    /// This configuration is typically used for cloning objects or applying updates to existing objects without
    /// affecting certain system-managed properties like timestamps and record statuses.
    /// </summary>
    public class ProductCategoryProfile : Profile
    {
        public ProductCategoryProfile()
        {
            CreateMap<ProductCategory, ProductCategory>()
                .ForMember(dest => dest.ProductId, opt => opt.Ignore()) // Ignore ProductId during updates
                .ForMember(dest => dest.CategoryId, opt => opt.Ignore()) // Ignore CategoryId during updates
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore()) // Ignore system-managed fields
                .ForMember(dest => dest.LastUpdatedAt, opt => opt.Ignore());
        }
    }
}
