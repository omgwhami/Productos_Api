using ProductosApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Agregamos los controladores y Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Cada vez que alguien pida un IProductoRepository, se le da un ProductoRepository
builder.Services.AddScoped<IProductoRepository, ProductoRepository>();

var app = builder.Build();

// Swagger queda disponible siempre para poder probar la API
app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
