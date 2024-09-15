using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace MC.CommerceService.API.Data.ResourceConfiguration
{
    /// <summary>
    /// This is used to keep track of who created or changed a record and when it happened.
    /// </summary>
    public interface IResource
    {
        /// <summary>
        /// Who made this record? Could be a user's name or an app's name.
        /// </summary>
        string CreatedBy { get; set; }

        /// <summary>
        /// Who last changed this record?
        /// </summary>
        string LastUpdatedBy { get; set; }

        /// <summary>
        /// When was this record first made?
        /// </summary>
        DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// When was this record last changed?
        /// </summary>
        DateTimeOffset LastUpdatedAt { get; set; }
    }

    /// <summary>
    /// This sets up the basic rules for saving records in the database that track creation and changes.
    /// </summary>
    /// <typeparam name="T">The type of the record. It needs to be a class that can be created without any arguments.</typeparam>
    public class CommonResourceConfiguration<T> : IEntityTypeConfiguration<T> where T : class, IResource, new()
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            // Makes sure there's a quick way to look up when records were created.
            builder.HasIndex((T resource) => resource.CreatedAt);
        }
    }

    /// <summary>
    /// Sets up specific rules for mssql to manage when records were created or changed.
    /// </summary>
    /// <typeparam name="T">The type of the record.</typeparam>
    public class DatabaseResourceConfiguration<T> : CommonResourceConfiguration<T> where T : class, IResource, new()
    {
        // Define specific GUIDs for consistent references
        public const string customer1Id = "00000000-0000-0000-0000-000000000001";
        public const string customer2Id = "00000000-0000-0000-0000-000000000002";
        public const string customer3Id = "00000000-0000-0000-0000-000000000003";
        public const string customer4Id = "00000000-0000-0000-0000-000000000004";
        public const string customer5Id = "00000000-0000-0000-0000-000000000005";

        public const string product1Id = "11111111-1111-1111-1111-111111111111";
        public const string product2Id = "11111111-1111-1111-1111-111111111112";
        public const string product3Id = "11111111-1111-1111-1111-111111111113";
        public const string product4Id = "11111111-1111-1111-1111-111111111114";
        public const string product5Id = "11111111-1111-1111-1111-111111111115";
        public const string product6Id = "11111111-1111-1111-1111-111111111116";
        public const string product7Id = "11111111-1111-1111-1111-111111111117";
        public const string product8Id = "11111111-1111-1111-1111-111111111118";
        public const string product9Id = "11111111-1111-1111-1111-111111111119";
        public const string product10Id = "11111111-1111-1111-1111-111111111120";
        public const string product11Id = "11111111-1111-1111-1111-111111111121";
        public const string product12Id = "11111111-1111-1111-1111-111111111122";
        public const string product13Id = "11111111-1111-1111-1111-111111111123";
        public const string product14Id = "11111111-1111-1111-1111-111111111124";
        public const string product15Id = "11111111-1111-1111-1111-111111111125";

        public const string category1Id = "22222222-2222-2222-2222-222222222221";
        public const string category2Id = "22222222-2222-2222-2222-222222222222";
        public const string category3Id = "22222222-2222-2222-2222-222222222223";
        public const string category4Id = "22222222-2222-2222-2222-222222222224";
        public const string category5Id = "22222222-2222-2222-2222-222222222225";

        public const string order1Id = "33333333-3333-3333-3333-333333333331";
        public const string order2Id = "33333333-3333-3333-3333-333333333332";
        public const string order3Id = "33333333-3333-3333-3333-333333333333";
        public const string order4Id = "33333333-3333-3333-3333-333333333334";
        public const string order5Id = "33333333-3333-3333-3333-333333333335";

        public override void Configure(EntityTypeBuilder<T> builder)
        {
            base.Configure(builder);
            // Sets up the date and time types for mssql and makes sure new records get the current time.
            builder.Property((T e) => e.CreatedAt).HasColumnType("datetimeoffset").HasDefaultValueSql("SYSDATETIMEOFFSET()");
            builder.Property((T e) => e.LastUpdatedAt).HasColumnType("datetimeoffset").HasDefaultValueSql("SYSDATETIMEOFFSET()");
        }
    }
}
