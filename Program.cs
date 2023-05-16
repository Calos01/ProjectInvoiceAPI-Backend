using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectInvoiceAPI_Backend.Container;
using ProjectInvoiceAPI_Backend.Models;
using ProjectInvoiceAPI_Backend.Profiles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Conexion
builder.Services.AddDbContext<InvoiceDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("Conexion")));

//AutoMapper
var automapper = new MapperConfiguration(item => item.AddProfile(new InvoiceProfile()));
IMapper mapper = automapper.CreateMapper();
builder.Services.AddSingleton(mapper);
//builder.Services.AddAutoMapper(typeof(Program));

//Repository
builder.Services.AddTransient<IRepositoryCustomer, RepositoryCustomer>();
builder.Services.AddTransient<IRepositoryInvoice, RepositoryInvoice>();
builder.Services.AddTransient<IRepositoryProduct, RepositoryProduct>();

//CORS
builder.Services.AddCors(options => options.AddPolicy("AllowWebapp",
                                    builder => builder.AllowAnyOrigin()
                                                    .AllowAnyHeader()
                                                    .AllowAnyMethod()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//CORS 
app.UseCors("AllowWebapp");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
