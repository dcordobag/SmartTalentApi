namespace SmartTalent.Hotel.Api.Extensions
{
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using SmartTalent.Hotel.Api.Filters;
    using SmartTalent.Hotel.Api.Middleware;
    using SmartTalent.Hotel.BusinessLayer;
    using SmartTalent.Hotel.BusinessLayer.Interfaces;
    using SmartTalent.Hotel.DataAccess.Database;
    using SmartTalent.Hotel.DataAccess.Database.Dao;
    using SmartTalent.Hotel.DataAccess.Database.Dao.Interfaces;
    using SmartTalent.Hotel.DataAccess.Database.Interfaces;
    using SmartTalent.Hotel.DataAccessLayer.Database.Dao;
    using SmartTalent.Hotel.DataAccessLayer.Database.Dao.Interfaces;
    using System.Text;



    /// <summary>
    /// SetupConfExtension class
    /// This class contains setup and initialization of crosscutting services
    /// </summary>
    public static class SetupConfExtensions
    {
        /// <summary>
        /// Extension for initialize authentication handlers
        /// </summary>
        public static void AddAuthenticationConfiguration(this IServiceCollection service)
        {
            service.AddAuthentication("SmartBearer")
               .AddScheme<AuthenticationSchemeOptions, TokenAuthenticationMiddleware>("SmartBearer", options => { })
               .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
           {
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidateLifetime = true,
                   ValidateIssuerSigningKey = true,
                   ValidIssuer = Constants.Constants.Issuer,
                   ValidAudience = Constants.Constants.Audience,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Constants.Constants.SecretKey)),
               };
           });
        }

        /// <summary>
        /// Configuration of security headers
        /// </summary>
        public static void UseHttpSecurityHeaders(this WebApplication app)
        {
            app.Use(async (context, next) =>
            {
                context.Response.Headers.Append("X-Content-Type-Options", "nosniff");
                context.Response.Headers.Append("X-XSS-Protection", "1; mode=block");
                context.Response.Headers.Append("Strict-Transport-Security", "max-age=31536000; includeSubDomains");
                await next.Invoke();
            });
        }

        /// <summary>
        /// Services configuration for business rules
        /// </summary>
        public static void AddBusinessRulesServices(this IServiceCollection service)
        {

            service.AddScoped<IHotelBl, HotelBl>();
            service.AddScoped<IRoomBl, RoomBl>();
            service.AddScoped<IReservationBl, ReservationBl>();


            service.AddControllers(options =>
            {
                options.Filters.Add(typeof(ExceptionFilterAttribute));
            });

        }

        /// <summary>
        /// Services configuration for Data Access
        /// </summary>
        public static void AddDataAccessServices(this IServiceCollection service)
        {

            service.AddScoped<IHotelDao, HotelDao>();
            service.AddScoped<IRoomDao, RoomDao>();
            service.AddScoped<IReservationDao, ReservationDao>();
        }

        /// <summary>
        /// Services configuration for db context
        /// </summary>
        public static void AddDbContextServices(this IServiceCollection service, IConfiguration configuration)
        {

            MongoDbSettings databaseSettings = new()
            {
                ConnectionString = configuration["dbcn"],
                DatabaseName = configuration["dbname"]
            };

            IOptions<MongoDbSettings> options = Options.Create(databaseSettings);
            service.AddSingleton<IDatabaseContext>(new DatabaseContext(options));
        }
    }
}
