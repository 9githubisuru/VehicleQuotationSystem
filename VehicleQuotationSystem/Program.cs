using QuotationAPI.Data;
using QuotationAPI.Repositories;
using QuotationAPI.Data;
using QuotationAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ✅ Dependency Injection
builder.Services.AddSingleton<ExcelService>();
builder.Services.AddScoped<OracleDbContext>();
builder.Services.AddScoped<QuotationRepository>();

builder.Services.AddCors(options =>             
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy
                .SetIsOriginAllowed(origin => true)
                .AllowAnyHeader()
                .AllowAnyMethod();
        }); 
});

builder.Services.AddScoped<OracleDbContext>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
