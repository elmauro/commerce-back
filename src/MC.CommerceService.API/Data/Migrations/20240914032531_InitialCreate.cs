using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MC.CommerceService.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    LastUpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    LastUpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    LastUpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    LastUpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategory",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    LastUpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategory", x => new { x.ProductId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_ProductCategory_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductCategory_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderProducts",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    LastUpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProducts", x => new { x.OrderId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_OrderProducts_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName", "CreatedAt", "CreatedBy", "LastUpdatedAt", "LastUpdatedBy" },
                values: new object[,]
                {
                    { new Guid("22222222-2222-2222-2222-222222222221"), "Electronics", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 8, DateTimeKind.Unspecified).AddTicks(9650), new TimeSpan(0, 0, 0, 0, 0)), "", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 8, DateTimeKind.Unspecified).AddTicks(9652), new TimeSpan(0, 0, 0, 0, 0)), "" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "Books", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 8, DateTimeKind.Unspecified).AddTicks(9654), new TimeSpan(0, 0, 0, 0, 0)), "", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 8, DateTimeKind.Unspecified).AddTicks(9655), new TimeSpan(0, 0, 0, 0, 0)), "" },
                    { new Guid("22222222-2222-2222-2222-222222222223"), "Clothing", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 8, DateTimeKind.Unspecified).AddTicks(9657), new TimeSpan(0, 0, 0, 0, 0)), "", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 8, DateTimeKind.Unspecified).AddTicks(9658), new TimeSpan(0, 0, 0, 0, 0)), "" },
                    { new Guid("22222222-2222-2222-2222-222222222224"), "Home & Kitchen", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 8, DateTimeKind.Unspecified).AddTicks(9660), new TimeSpan(0, 0, 0, 0, 0)), "", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 8, DateTimeKind.Unspecified).AddTicks(9661), new TimeSpan(0, 0, 0, 0, 0)), "" },
                    { new Guid("22222222-2222-2222-2222-222222222225"), "Sports", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 8, DateTimeKind.Unspecified).AddTicks(9663), new TimeSpan(0, 0, 0, 0, 0)), "", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 8, DateTimeKind.Unspecified).AddTicks(9663), new TimeSpan(0, 0, 0, 0, 0)), "" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "CreatedAt", "CreatedBy", "Email", "FirstName", "LastName", "LastUpdatedAt", "LastUpdatedBy", "Phone" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 9, DateTimeKind.Unspecified).AddTicks(4833), new TimeSpan(0, 0, 0, 0, 0)), "", "john.doe@example.com", "John", "Doe", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 9, DateTimeKind.Unspecified).AddTicks(4834), new TimeSpan(0, 0, 0, 0, 0)), "", "123-456-7890" },
                    { new Guid("00000000-0000-0000-0000-000000000002"), new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 9, DateTimeKind.Unspecified).AddTicks(4838), new TimeSpan(0, 0, 0, 0, 0)), "", "jane.smith@example.com", "Jane", "Smith", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 9, DateTimeKind.Unspecified).AddTicks(4838), new TimeSpan(0, 0, 0, 0, 0)), "", "234-567-8901" },
                    { new Guid("00000000-0000-0000-0000-000000000003"), new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 9, DateTimeKind.Unspecified).AddTicks(4847), new TimeSpan(0, 0, 0, 0, 0)), "", "michael.johnson@example.com", "Michael", "Johnson", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 9, DateTimeKind.Unspecified).AddTicks(4848), new TimeSpan(0, 0, 0, 0, 0)), "", "345-678-9012" },
                    { new Guid("00000000-0000-0000-0000-000000000004"), new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 9, DateTimeKind.Unspecified).AddTicks(4851), new TimeSpan(0, 0, 0, 0, 0)), "", "emily.williams@example.com", "Emily", "Williams", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 9, DateTimeKind.Unspecified).AddTicks(4851), new TimeSpan(0, 0, 0, 0, 0)), "", "456-789-0123" },
                    { new Guid("00000000-0000-0000-0000-000000000005"), new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 9, DateTimeKind.Unspecified).AddTicks(4853), new TimeSpan(0, 0, 0, 0, 0)), "", "chris.brown@example.com", "Chris", "Brown", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 9, DateTimeKind.Unspecified).AddTicks(4854), new TimeSpan(0, 0, 0, 0, 0)), "", "567-890-1234" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Code", "CreatedAt", "CreatedBy", "Description", "LastUpdatedAt", "LastUpdatedBy", "Price", "Stock", "Title" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "P001", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 12, DateTimeKind.Unspecified).AddTicks(4029), new TimeSpan(0, 0, 0, 0, 0)), "", "A powerful laptop", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 12, DateTimeKind.Unspecified).AddTicks(4030), new TimeSpan(0, 0, 0, 0, 0)), "", 1000m, 50, "Laptop" },
                    { new Guid("11111111-1111-1111-1111-111111111112"), "P002", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 12, DateTimeKind.Unspecified).AddTicks(4034), new TimeSpan(0, 0, 0, 0, 0)), "", "A modern smartphone", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 12, DateTimeKind.Unspecified).AddTicks(4035), new TimeSpan(0, 0, 0, 0, 0)), "", 800m, 30, "Smartphone" },
                    { new Guid("11111111-1111-1111-1111-111111111113"), "P003", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 12, DateTimeKind.Unspecified).AddTicks(4038), new TimeSpan(0, 0, 0, 0, 0)), "", "A versatile tablet", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 12, DateTimeKind.Unspecified).AddTicks(4039), new TimeSpan(0, 0, 0, 0, 0)), "", 500m, 20, "Tablet" },
                    { new Guid("11111111-1111-1111-1111-111111111114"), "P004", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 12, DateTimeKind.Unspecified).AddTicks(4041), new TimeSpan(0, 0, 0, 0, 0)), "", "Noise-canceling headphones", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 12, DateTimeKind.Unspecified).AddTicks(4042), new TimeSpan(0, 0, 0, 0, 0)), "", 200m, 100, "Headphones" },
                    { new Guid("11111111-1111-1111-1111-111111111115"), "P005", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 12, DateTimeKind.Unspecified).AddTicks(4045), new TimeSpan(0, 0, 0, 0, 0)), "", "A stylish smartwatch", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 12, DateTimeKind.Unspecified).AddTicks(4045), new TimeSpan(0, 0, 0, 0, 0)), "", 250m, 60, "Smartwatch" },
                    { new Guid("11111111-1111-1111-1111-111111111116"), "P006", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 12, DateTimeKind.Unspecified).AddTicks(4048), new TimeSpan(0, 0, 0, 0, 0)), "", "A 24-inch monitor", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 12, DateTimeKind.Unspecified).AddTicks(4049), new TimeSpan(0, 0, 0, 0, 0)), "", 300m, 40, "Monitor" },
                    { new Guid("11111111-1111-1111-1111-111111111117"), "P007", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 12, DateTimeKind.Unspecified).AddTicks(4052), new TimeSpan(0, 0, 0, 0, 0)), "", "Mechanical keyboard", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 12, DateTimeKind.Unspecified).AddTicks(4052), new TimeSpan(0, 0, 0, 0, 0)), "", 100m, 70, "Keyboard" },
                    { new Guid("11111111-1111-1111-1111-111111111118"), "P008", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 12, DateTimeKind.Unspecified).AddTicks(4055), new TimeSpan(0, 0, 0, 0, 0)), "", "Wireless mouse", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 12, DateTimeKind.Unspecified).AddTicks(4055), new TimeSpan(0, 0, 0, 0, 0)), "", 50m, 80, "Mouse" },
                    { new Guid("11111111-1111-1111-1111-111111111119"), "P009", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 12, DateTimeKind.Unspecified).AddTicks(4061), new TimeSpan(0, 0, 0, 0, 0)), "", "Ergonomic office chair", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 12, DateTimeKind.Unspecified).AddTicks(4062), new TimeSpan(0, 0, 0, 0, 0)), "", 150m, 25, "Chair" },
                    { new Guid("11111111-1111-1111-1111-111111111120"), "P010", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 12, DateTimeKind.Unspecified).AddTicks(4065), new TimeSpan(0, 0, 0, 0, 0)), "", "Adjustable standing desk", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 12, DateTimeKind.Unspecified).AddTicks(4065), new TimeSpan(0, 0, 0, 0, 0)), "", 500m, 20, "Desk" },
                    { new Guid("11111111-1111-1111-1111-111111111121"), "P011", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 12, DateTimeKind.Unspecified).AddTicks(4068), new TimeSpan(0, 0, 0, 0, 0)), "", "Wireless printer", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 12, DateTimeKind.Unspecified).AddTicks(4069), new TimeSpan(0, 0, 0, 0, 0)), "", 200m, 15, "Printer" },
                    { new Guid("11111111-1111-1111-1111-111111111122"), "P012", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 12, DateTimeKind.Unspecified).AddTicks(4071), new TimeSpan(0, 0, 0, 0, 0)), "", "Document scanner", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 12, DateTimeKind.Unspecified).AddTicks(4072), new TimeSpan(0, 0, 0, 0, 0)), "", 150m, 30, "Scanner" },
                    { new Guid("11111111-1111-1111-1111-111111111123"), "P013", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 12, DateTimeKind.Unspecified).AddTicks(4074), new TimeSpan(0, 0, 0, 0, 0)), "", "HD webcam", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 12, DateTimeKind.Unspecified).AddTicks(4075), new TimeSpan(0, 0, 0, 0, 0)), "", 80m, 50, "Webcam" },
                    { new Guid("11111111-1111-1111-1111-111111111124"), "P014", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 12, DateTimeKind.Unspecified).AddTicks(4077), new TimeSpan(0, 0, 0, 0, 0)), "", "Wireless router", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 12, DateTimeKind.Unspecified).AddTicks(4078), new TimeSpan(0, 0, 0, 0, 0)), "", 120m, 45, "Router" },
                    { new Guid("11111111-1111-1111-1111-111111111125"), "P015", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 12, DateTimeKind.Unspecified).AddTicks(4080), new TimeSpan(0, 0, 0, 0, 0)), "", "1TB external hard drive", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 12, DateTimeKind.Unspecified).AddTicks(4081), new TimeSpan(0, 0, 0, 0, 0)), "", 90m, 100, "External Hard Drive" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "CreatedAt", "CreatedBy", "CustomerId", "LastUpdatedAt", "LastUpdatedBy", "OrderDate", "TotalAmount" },
                values: new object[,]
                {
                    { new Guid("33333333-3333-3333-3333-333333333331"), new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 10, DateTimeKind.Unspecified).AddTicks(786), new TimeSpan(0, 0, 0, 0, 0)), "", new Guid("00000000-0000-0000-0000-000000000001"), new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 10, DateTimeKind.Unspecified).AddTicks(787), new TimeSpan(0, 0, 0, 0, 0)), "", new DateTime(2024, 9, 14, 3, 25, 31, 10, DateTimeKind.Utc).AddTicks(782), 500m },
                    { new Guid("33333333-3333-3333-3333-333333333332"), new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 10, DateTimeKind.Unspecified).AddTicks(790), new TimeSpan(0, 0, 0, 0, 0)), "", new Guid("00000000-0000-0000-0000-000000000002"), new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 10, DateTimeKind.Unspecified).AddTicks(791), new TimeSpan(0, 0, 0, 0, 0)), "", new DateTime(2024, 9, 14, 3, 25, 31, 10, DateTimeKind.Utc).AddTicks(790), 1000m },
                    { new Guid("33333333-3333-3333-3333-333333333333"), new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 10, DateTimeKind.Unspecified).AddTicks(794), new TimeSpan(0, 0, 0, 0, 0)), "", new Guid("00000000-0000-0000-0000-000000000003"), new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 10, DateTimeKind.Unspecified).AddTicks(794), new TimeSpan(0, 0, 0, 0, 0)), "", new DateTime(2024, 9, 14, 3, 25, 31, 10, DateTimeKind.Utc).AddTicks(793), 750m },
                    { new Guid("33333333-3333-3333-3333-333333333334"), new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 10, DateTimeKind.Unspecified).AddTicks(797), new TimeSpan(0, 0, 0, 0, 0)), "", new Guid("00000000-0000-0000-0000-000000000004"), new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 10, DateTimeKind.Unspecified).AddTicks(797), new TimeSpan(0, 0, 0, 0, 0)), "", new DateTime(2024, 9, 14, 3, 25, 31, 10, DateTimeKind.Utc).AddTicks(796), 900m },
                    { new Guid("33333333-3333-3333-3333-333333333335"), new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 10, DateTimeKind.Unspecified).AddTicks(800), new TimeSpan(0, 0, 0, 0, 0)), "", new Guid("00000000-0000-0000-0000-000000000005"), new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 10, DateTimeKind.Unspecified).AddTicks(801), new TimeSpan(0, 0, 0, 0, 0)), "", new DateTime(2024, 9, 14, 3, 25, 31, 10, DateTimeKind.Utc).AddTicks(800), 1200m }
                });

            migrationBuilder.InsertData(
                table: "ProductCategory",
                columns: new[] { "CategoryId", "ProductId", "CreatedAt", "CreatedBy", "LastUpdatedAt", "LastUpdatedBy" },
                values: new object[,]
                {
                    { new Guid("22222222-2222-2222-2222-222222222221"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 11, DateTimeKind.Unspecified).AddTicks(9970), new TimeSpan(0, 0, 0, 0, 0)), "", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 11, DateTimeKind.Unspecified).AddTicks(9971), new TimeSpan(0, 0, 0, 0, 0)), "" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 11, DateTimeKind.Unspecified).AddTicks(9972), new TimeSpan(0, 0, 0, 0, 0)), "", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 11, DateTimeKind.Unspecified).AddTicks(9973), new TimeSpan(0, 0, 0, 0, 0)), "" },
                    { new Guid("22222222-2222-2222-2222-222222222223"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 12, DateTimeKind.Unspecified).AddTicks(16), new TimeSpan(0, 0, 0, 0, 0)), "", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 12, DateTimeKind.Unspecified).AddTicks(17), new TimeSpan(0, 0, 0, 0, 0)), "" },
                    { new Guid("22222222-2222-2222-2222-222222222221"), new Guid("11111111-1111-1111-1111-111111111112"), new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 11, DateTimeKind.Unspecified).AddTicks(9974), new TimeSpan(0, 0, 0, 0, 0)), "", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 11, DateTimeKind.Unspecified).AddTicks(9974), new TimeSpan(0, 0, 0, 0, 0)), "" },
                    { new Guid("22222222-2222-2222-2222-222222222223"), new Guid("11111111-1111-1111-1111-111111111112"), new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 11, DateTimeKind.Unspecified).AddTicks(9975), new TimeSpan(0, 0, 0, 0, 0)), "", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 11, DateTimeKind.Unspecified).AddTicks(9976), new TimeSpan(0, 0, 0, 0, 0)), "" },
                    { new Guid("22222222-2222-2222-2222-222222222224"), new Guid("11111111-1111-1111-1111-111111111112"), new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 12, DateTimeKind.Unspecified).AddTicks(18), new TimeSpan(0, 0, 0, 0, 0)), "", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 12, DateTimeKind.Unspecified).AddTicks(18), new TimeSpan(0, 0, 0, 0, 0)), "" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new Guid("11111111-1111-1111-1111-111111111113"), new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 11, DateTimeKind.Unspecified).AddTicks(9977), new TimeSpan(0, 0, 0, 0, 0)), "", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 11, DateTimeKind.Unspecified).AddTicks(9978), new TimeSpan(0, 0, 0, 0, 0)), "" },
                    { new Guid("22222222-2222-2222-2222-222222222224"), new Guid("11111111-1111-1111-1111-111111111113"), new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 11, DateTimeKind.Unspecified).AddTicks(9979), new TimeSpan(0, 0, 0, 0, 0)), "", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 11, DateTimeKind.Unspecified).AddTicks(9979), new TimeSpan(0, 0, 0, 0, 0)), "" },
                    { new Guid("22222222-2222-2222-2222-222222222225"), new Guid("11111111-1111-1111-1111-111111111113"), new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 12, DateTimeKind.Unspecified).AddTicks(19), new TimeSpan(0, 0, 0, 0, 0)), "", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 12, DateTimeKind.Unspecified).AddTicks(20), new TimeSpan(0, 0, 0, 0, 0)), "" },
                    { new Guid("22222222-2222-2222-2222-222222222221"), new Guid("11111111-1111-1111-1111-111111111114"), new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 12, DateTimeKind.Unspecified).AddTicks(21), new TimeSpan(0, 0, 0, 0, 0)), "", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 12, DateTimeKind.Unspecified).AddTicks(21), new TimeSpan(0, 0, 0, 0, 0)), "" },
                    { new Guid("22222222-2222-2222-2222-222222222223"), new Guid("11111111-1111-1111-1111-111111111114"), new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 11, DateTimeKind.Unspecified).AddTicks(9980), new TimeSpan(0, 0, 0, 0, 0)), "", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 11, DateTimeKind.Unspecified).AddTicks(9981), new TimeSpan(0, 0, 0, 0, 0)), "" },
                    { new Guid("22222222-2222-2222-2222-222222222225"), new Guid("11111111-1111-1111-1111-111111111114"), new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 11, DateTimeKind.Unspecified).AddTicks(9982), new TimeSpan(0, 0, 0, 0, 0)), "", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 11, DateTimeKind.Unspecified).AddTicks(9982), new TimeSpan(0, 0, 0, 0, 0)), "" },
                    { new Guid("22222222-2222-2222-2222-222222222221"), new Guid("11111111-1111-1111-1111-111111111115"), new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 11, DateTimeKind.Unspecified).AddTicks(9983), new TimeSpan(0, 0, 0, 0, 0)), "", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 11, DateTimeKind.Unspecified).AddTicks(9983), new TimeSpan(0, 0, 0, 0, 0)), "" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new Guid("11111111-1111-1111-1111-111111111115"), new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 12, DateTimeKind.Unspecified).AddTicks(22), new TimeSpan(0, 0, 0, 0, 0)), "", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 12, DateTimeKind.Unspecified).AddTicks(22), new TimeSpan(0, 0, 0, 0, 0)), "" },
                    { new Guid("22222222-2222-2222-2222-222222222224"), new Guid("11111111-1111-1111-1111-111111111115"), new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 11, DateTimeKind.Unspecified).AddTicks(9985), new TimeSpan(0, 0, 0, 0, 0)), "", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 11, DateTimeKind.Unspecified).AddTicks(9985), new TimeSpan(0, 0, 0, 0, 0)), "" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new Guid("11111111-1111-1111-1111-111111111116"), new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 11, DateTimeKind.Unspecified).AddTicks(9986), new TimeSpan(0, 0, 0, 0, 0)), "", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 11, DateTimeKind.Unspecified).AddTicks(9987), new TimeSpan(0, 0, 0, 0, 0)), "" },
                    { new Guid("22222222-2222-2222-2222-222222222225"), new Guid("11111111-1111-1111-1111-111111111116"), new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 11, DateTimeKind.Unspecified).AddTicks(9987), new TimeSpan(0, 0, 0, 0, 0)), "", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 11, DateTimeKind.Unspecified).AddTicks(9988), new TimeSpan(0, 0, 0, 0, 0)), "" },
                    { new Guid("22222222-2222-2222-2222-222222222221"), new Guid("11111111-1111-1111-1111-111111111117"), new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 11, DateTimeKind.Unspecified).AddTicks(9991), new TimeSpan(0, 0, 0, 0, 0)), "", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 11, DateTimeKind.Unspecified).AddTicks(9991), new TimeSpan(0, 0, 0, 0, 0)), "" },
                    { new Guid("22222222-2222-2222-2222-222222222223"), new Guid("11111111-1111-1111-1111-111111111117"), new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 11, DateTimeKind.Unspecified).AddTicks(9989), new TimeSpan(0, 0, 0, 0, 0)), "", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 11, DateTimeKind.Unspecified).AddTicks(9989), new TimeSpan(0, 0, 0, 0, 0)), "" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new Guid("11111111-1111-1111-1111-111111111118"), new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 11, DateTimeKind.Unspecified).AddTicks(9993), new TimeSpan(0, 0, 0, 0, 0)), "", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 11, DateTimeKind.Unspecified).AddTicks(9994), new TimeSpan(0, 0, 0, 0, 0)), "" },
                    { new Guid("22222222-2222-2222-2222-222222222225"), new Guid("11111111-1111-1111-1111-111111111118"), new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 11, DateTimeKind.Unspecified).AddTicks(9992), new TimeSpan(0, 0, 0, 0, 0)), "", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 11, DateTimeKind.Unspecified).AddTicks(9993), new TimeSpan(0, 0, 0, 0, 0)), "" },
                    { new Guid("22222222-2222-2222-2222-222222222221"), new Guid("11111111-1111-1111-1111-111111111119"), new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 11, DateTimeKind.Unspecified).AddTicks(9996), new TimeSpan(0, 0, 0, 0, 0)), "", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 11, DateTimeKind.Unspecified).AddTicks(9996), new TimeSpan(0, 0, 0, 0, 0)), "" },
                    { new Guid("22222222-2222-2222-2222-222222222224"), new Guid("11111111-1111-1111-1111-111111111119"), new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 11, DateTimeKind.Unspecified).AddTicks(9995), new TimeSpan(0, 0, 0, 0, 0)), "", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 11, DateTimeKind.Unspecified).AddTicks(9995), new TimeSpan(0, 0, 0, 0, 0)), "" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new Guid("11111111-1111-1111-1111-111111111120"), new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 11, DateTimeKind.Unspecified).AddTicks(9997), new TimeSpan(0, 0, 0, 0, 0)), "", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 11, DateTimeKind.Unspecified).AddTicks(9998), new TimeSpan(0, 0, 0, 0, 0)), "" },
                    { new Guid("22222222-2222-2222-2222-222222222223"), new Guid("11111111-1111-1111-1111-111111111120"), new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 11, DateTimeKind.Unspecified).AddTicks(9999), new TimeSpan(0, 0, 0, 0, 0)), "", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 11, DateTimeKind.Unspecified).AddTicks(9999), new TimeSpan(0, 0, 0, 0, 0)), "" },
                    { new Guid("22222222-2222-2222-2222-222222222224"), new Guid("11111111-1111-1111-1111-111111111121"), new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 12, DateTimeKind.Unspecified).AddTicks(1), new TimeSpan(0, 0, 0, 0, 0)), "", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 12, DateTimeKind.Unspecified).AddTicks(2), new TimeSpan(0, 0, 0, 0, 0)), "" },
                    { new Guid("22222222-2222-2222-2222-222222222225"), new Guid("11111111-1111-1111-1111-111111111121"), new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 12, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 12, DateTimeKind.Unspecified).AddTicks(1), new TimeSpan(0, 0, 0, 0, 0)), "" },
                    { new Guid("22222222-2222-2222-2222-222222222221"), new Guid("11111111-1111-1111-1111-111111111122"), new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 12, DateTimeKind.Unspecified).AddTicks(3), new TimeSpan(0, 0, 0, 0, 0)), "", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 12, DateTimeKind.Unspecified).AddTicks(4), new TimeSpan(0, 0, 0, 0, 0)), "" },
                    { new Guid("22222222-2222-2222-2222-222222222225"), new Guid("11111111-1111-1111-1111-111111111122"), new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 12, DateTimeKind.Unspecified).AddTicks(5), new TimeSpan(0, 0, 0, 0, 0)), "", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 12, DateTimeKind.Unspecified).AddTicks(5), new TimeSpan(0, 0, 0, 0, 0)), "" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new Guid("11111111-1111-1111-1111-111111111123"), new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 12, DateTimeKind.Unspecified).AddTicks(6), new TimeSpan(0, 0, 0, 0, 0)), "", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 12, DateTimeKind.Unspecified).AddTicks(6), new TimeSpan(0, 0, 0, 0, 0)), "" },
                    { new Guid("22222222-2222-2222-2222-222222222224"), new Guid("11111111-1111-1111-1111-111111111123"), new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 12, DateTimeKind.Unspecified).AddTicks(7), new TimeSpan(0, 0, 0, 0, 0)), "", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 12, DateTimeKind.Unspecified).AddTicks(8), new TimeSpan(0, 0, 0, 0, 0)), "" },
                    { new Guid("22222222-2222-2222-2222-222222222223"), new Guid("11111111-1111-1111-1111-111111111124"), new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 12, DateTimeKind.Unspecified).AddTicks(9), new TimeSpan(0, 0, 0, 0, 0)), "", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 12, DateTimeKind.Unspecified).AddTicks(9), new TimeSpan(0, 0, 0, 0, 0)), "" },
                    { new Guid("22222222-2222-2222-2222-222222222225"), new Guid("11111111-1111-1111-1111-111111111124"), new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 12, DateTimeKind.Unspecified).AddTicks(12), new TimeSpan(0, 0, 0, 0, 0)), "", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 12, DateTimeKind.Unspecified).AddTicks(13), new TimeSpan(0, 0, 0, 0, 0)), "" },
                    { new Guid("22222222-2222-2222-2222-222222222221"), new Guid("11111111-1111-1111-1111-111111111125"), new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 12, DateTimeKind.Unspecified).AddTicks(14), new TimeSpan(0, 0, 0, 0, 0)), "", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 12, DateTimeKind.Unspecified).AddTicks(14), new TimeSpan(0, 0, 0, 0, 0)), "" },
                    { new Guid("22222222-2222-2222-2222-222222222223"), new Guid("11111111-1111-1111-1111-111111111125"), new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 12, DateTimeKind.Unspecified).AddTicks(15), new TimeSpan(0, 0, 0, 0, 0)), "", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 12, DateTimeKind.Unspecified).AddTicks(16), new TimeSpan(0, 0, 0, 0, 0)), "" }
                });

            migrationBuilder.InsertData(
                table: "OrderProducts",
                columns: new[] { "OrderId", "ProductId", "CreatedAt", "CreatedBy", "LastUpdatedAt", "LastUpdatedBy", "Quantity" },
                values: new object[,]
                {
                    { new Guid("33333333-3333-3333-3333-333333333331"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 11, DateTimeKind.Unspecified).AddTicks(278), new TimeSpan(0, 0, 0, 0, 0)), "", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 11, DateTimeKind.Unspecified).AddTicks(279), new TimeSpan(0, 0, 0, 0, 0)), "", 2 },
                    { new Guid("33333333-3333-3333-3333-333333333331"), new Guid("11111111-1111-1111-1111-111111111112"), new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 11, DateTimeKind.Unspecified).AddTicks(336), new TimeSpan(0, 0, 0, 0, 0)), "", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 11, DateTimeKind.Unspecified).AddTicks(337), new TimeSpan(0, 0, 0, 0, 0)), "", 1 },
                    { new Guid("33333333-3333-3333-3333-333333333331"), new Guid("11111111-1111-1111-1111-111111111113"), new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 11, DateTimeKind.Unspecified).AddTicks(338), new TimeSpan(0, 0, 0, 0, 0)), "", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 11, DateTimeKind.Unspecified).AddTicks(339), new TimeSpan(0, 0, 0, 0, 0)), "", 3 },
                    { new Guid("33333333-3333-3333-3333-333333333332"), new Guid("11111111-1111-1111-1111-111111111114"), new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 11, DateTimeKind.Unspecified).AddTicks(340), new TimeSpan(0, 0, 0, 0, 0)), "", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 11, DateTimeKind.Unspecified).AddTicks(340), new TimeSpan(0, 0, 0, 0, 0)), "", 1 },
                    { new Guid("33333333-3333-3333-3333-333333333332"), new Guid("11111111-1111-1111-1111-111111111115"), new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 11, DateTimeKind.Unspecified).AddTicks(341), new TimeSpan(0, 0, 0, 0, 0)), "", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 11, DateTimeKind.Unspecified).AddTicks(342), new TimeSpan(0, 0, 0, 0, 0)), "", 2 },
                    { new Guid("33333333-3333-3333-3333-333333333332"), new Guid("11111111-1111-1111-1111-111111111116"), new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 11, DateTimeKind.Unspecified).AddTicks(343), new TimeSpan(0, 0, 0, 0, 0)), "", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 11, DateTimeKind.Unspecified).AddTicks(343), new TimeSpan(0, 0, 0, 0, 0)), "", 3 },
                    { new Guid("33333333-3333-3333-3333-333333333333"), new Guid("11111111-1111-1111-1111-111111111117"), new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 11, DateTimeKind.Unspecified).AddTicks(344), new TimeSpan(0, 0, 0, 0, 0)), "", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 11, DateTimeKind.Unspecified).AddTicks(345), new TimeSpan(0, 0, 0, 0, 0)), "", 1 },
                    { new Guid("33333333-3333-3333-3333-333333333333"), new Guid("11111111-1111-1111-1111-111111111118"), new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 11, DateTimeKind.Unspecified).AddTicks(346), new TimeSpan(0, 0, 0, 0, 0)), "", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 11, DateTimeKind.Unspecified).AddTicks(346), new TimeSpan(0, 0, 0, 0, 0)), "", 2 },
                    { new Guid("33333333-3333-3333-3333-333333333334"), new Guid("11111111-1111-1111-1111-111111111119"), new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 11, DateTimeKind.Unspecified).AddTicks(347), new TimeSpan(0, 0, 0, 0, 0)), "", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 11, DateTimeKind.Unspecified).AddTicks(348), new TimeSpan(0, 0, 0, 0, 0)), "", 3 },
                    { new Guid("33333333-3333-3333-3333-333333333334"), new Guid("11111111-1111-1111-1111-111111111120"), new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 11, DateTimeKind.Unspecified).AddTicks(349), new TimeSpan(0, 0, 0, 0, 0)), "", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 11, DateTimeKind.Unspecified).AddTicks(349), new TimeSpan(0, 0, 0, 0, 0)), "", 1 },
                    { new Guid("33333333-3333-3333-3333-333333333334"), new Guid("11111111-1111-1111-1111-111111111121"), new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 11, DateTimeKind.Unspecified).AddTicks(350), new TimeSpan(0, 0, 0, 0, 0)), "", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 11, DateTimeKind.Unspecified).AddTicks(350), new TimeSpan(0, 0, 0, 0, 0)), "", 2 },
                    { new Guid("33333333-3333-3333-3333-333333333335"), new Guid("11111111-1111-1111-1111-111111111122"), new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 11, DateTimeKind.Unspecified).AddTicks(352), new TimeSpan(0, 0, 0, 0, 0)), "", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 11, DateTimeKind.Unspecified).AddTicks(352), new TimeSpan(0, 0, 0, 0, 0)), "", 3 },
                    { new Guid("33333333-3333-3333-3333-333333333335"), new Guid("11111111-1111-1111-1111-111111111123"), new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 11, DateTimeKind.Unspecified).AddTicks(353), new TimeSpan(0, 0, 0, 0, 0)), "", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 11, DateTimeKind.Unspecified).AddTicks(354), new TimeSpan(0, 0, 0, 0, 0)), "", 1 },
                    { new Guid("33333333-3333-3333-3333-333333333335"), new Guid("11111111-1111-1111-1111-111111111124"), new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 11, DateTimeKind.Unspecified).AddTicks(355), new TimeSpan(0, 0, 0, 0, 0)), "", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 11, DateTimeKind.Unspecified).AddTicks(355), new TimeSpan(0, 0, 0, 0, 0)), "", 2 },
                    { new Guid("33333333-3333-3333-3333-333333333335"), new Guid("11111111-1111-1111-1111-111111111125"), new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 11, DateTimeKind.Unspecified).AddTicks(356), new TimeSpan(0, 0, 0, 0, 0)), "", new DateTimeOffset(new DateTime(2024, 9, 14, 3, 25, 31, 11, DateTimeKind.Unspecified).AddTicks(357), new TimeSpan(0, 0, 0, 0, 0)), "", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CreatedAt",
                table: "Categories",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CreatedAt",
                table: "Customers",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Email",
                table: "Customers",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_CreatedAt",
                table: "OrderProducts",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_ProductId",
                table: "OrderProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CreatedAt",
                table: "Orders",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategory_CategoryId",
                table: "ProductCategory",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategory_CreatedAt",
                table: "ProductCategory",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CreatedAt",
                table: "Products",
                column: "CreatedAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderProducts");

            migrationBuilder.DropTable(
                name: "ProductCategory");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
