var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var originsToAllowName = "myCors";
// cors
var myCorsOrigins = builder.Configuration
                    .GetSection("CorsSettings:AllowedOrigins")
                    .Get<string[]>() ?? Array.Empty<string>();

builder.Services.AddCors(
                    options =>
                    {
                        options.AddPolicy(name: originsToAllowName, policy =>
                        {
                            policy.WithOrigins(myCorsOrigins)
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                        });
                    });

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(originsToAllowName);
app.UseAuthorization();

app.MapControllers();

app.Run();

