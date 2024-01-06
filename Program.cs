using InjectionMachineModule.Application.Helpers;
using InjectionMachineModule.Mapping;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder
            .WithOrigins("localhost", "http://localhost:5173", "https://web-thaiduong-mes.vercel.app")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
        });
});

builder.Services.AddAutoMapper(typeof(ModelToViewModelProfile));
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining<ModelToViewModelProfile>();
});

builder.Services.AddSingleton<RestClient>();
builder.Services.AddSingleton<MesApiUrlHelper>();
builder.Services.AddSingleton(new HttpClient());

var app = builder.Build();

app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
