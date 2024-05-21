
using Cinema.Backend.Infrastructure;
using Cinema.BLL.HelperService;
using Cinema.BLL.Services;
using Cinema.DAL.Context;
using Cinema.DAL.Implemantations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Cinema.BLL.EmailSender;
using PdfSharp.Charting;
using Cinema.BLL.TicketPdfGenerator;
using PdfSharp.Fonts;

namespace Cinema.Backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
            builder.Services.AddProblemDetails();
            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresDbConnection"));
            });


            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JWT:Key").Value)),
                    ValidateLifetime = true,
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies["token"];

                        return Task.CompletedTask;
                    }

                };

            });




            builder.Services.AddScoped(provider => new UnitOfWork(provider.GetRequiredService<ApplicationDbContext>()));
            builder.Services.AddScoped<JWTService>();
            builder.Services.AddScoped<PasswordHash>();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddScoped<ApplicationUserServices>();
            builder.Services.AddScoped<MailjetEmailSender>();

            builder.Services.AddScoped<TicketPdfGenerator>();
            GlobalFontSettings.FontResolver = new CustomFontResolver();


            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseExceptionHandler();

            app.UseHttpsRedirection();
            app.UseStaticFiles();


            app.UseAuthentication();
            app.UseAuthorization();



            app.MapControllers();


            app.Run();

        }
    }
}
