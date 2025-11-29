using EmployeesApi.Data;
using EmployeesApi.Models;
using EmployeesApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Employees API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter JWT token like: Bearer {your token}"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            new string[] {}
        }
    });
});

builder.Services.AddDbContext<EmployeeDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IEmployeeService, EmployeeService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactAppPolicy", policy =>
        policy.AllowAnyHeader().AllowAnyMethod().AllowCredentials());
});


var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"!]);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true
    };
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<EmployeeDbContext>();
    db.Database.Migrate();

    if (!db.Employees.Any())
    {
        db.Employees.AddRange(
            new Employee { FirstName = "Alice", LastName = "Smith", DateOfBirth = new DateTime(1990, 1, 1), EducationLevel = "Bachelor" },
            new Employee { FirstName = "Bob", LastName = "Johnson", DateOfBirth = new DateTime(1985, 3, 15), EducationLevel = "Master" },
            new Employee { FirstName = "Charlie", LastName = "Williams", DateOfBirth = new DateTime(1992, 7, 20), EducationLevel = "Bachelor" },
            new Employee { FirstName = "Diana", LastName = "Brown", DateOfBirth = new DateTime(1988, 11, 5), EducationLevel = "PhD" },
            new Employee { FirstName = "Ethan", LastName = "Davis", DateOfBirth = new DateTime(1995, 5, 12), EducationLevel = "High School" },
            new Employee { FirstName = "Leah", LastName = "James", DateOfBirth = new DateTime(1996, 8, 22), EducationLevel = "Master" },
            new Employee { FirstName = "Molly", LastName = "Brown", DateOfBirth = new DateTime(1978, 8, 7), EducationLevel = "PhD" },
            new Employee { FirstName = "Christopher", LastName = "Nolan", DateOfBirth = new DateTime(1999, 5, 29), EducationLevel = "Bachelor" },
            new Employee { FirstName = "John", LastName = "Walken", DateOfBirth = new DateTime(1968, 7, 14), EducationLevel = "PhD" },
            new Employee { FirstName = "Emma", LastName = "Stone", DateOfBirth = new DateTime(1982, 3, 17), EducationLevel = "Master" },
            new Employee { FirstName = "Diane", LastName = "Smith", DateOfBirth = new DateTime(1968, 8, 19), EducationLevel = "PhD" }
        );
        db.SaveChanges();
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("ReactAppPolicy");

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();