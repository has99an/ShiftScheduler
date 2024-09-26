using ShiftSchedulerAPI.BusinessLogicLayer;
using ShiftSchedulerAPI.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registrer dine dataadgangs tjenester her
builder.Services.AddScoped<IShiftAccess, ShiftAccess>();
builder.Services.AddScoped<IEmployeeAccess, EmployeeAccess>();
builder.Services.AddScoped<IEmployeeLogic, EmployeeLogic>();
builder.Services.AddScoped<IShiftLogic, ShiftLogic>();
var app = builder.Build();

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
