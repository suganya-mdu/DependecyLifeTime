using DependencyLifetime;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IIDSingleton>(new ID());
builder.Services.AddScoped<IIDScoped,ID>();
builder.Services.AddTransient<IIDTransient,ID>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};


app.MapGet("/GetDependencyLifeTime", (IIDSingleton idSingleton,
                    IIDScoped idScoped1, IIDScoped idScoped2,
                    IIDTransient idTransient1, IIDTransient idTransient2) =>
{
    
    
    return $"Singleton Instance-Only One Instance for any number of Requests\r\n\r\n" +
    $"Singleton Instance: {idSingleton.Value}\r\n\r\n" +
        $"Scoped Instance-One Instance created and used per Http Request \r\n\r\n" +
         $"Scoped Instance 1: {idScoped1.Value}\r\n\r Scoped Instance 2: {idScoped2.Value}\r\n\r\n" +
        $"Transient instance- New Instance for every Http Request\r\n\r\n" +
        $"Transient New Instance 1: {idTransient1.Value}\r\n\r Transient New Instance 2: {idTransient2.Value}\r\n\r\n" +
        $"\t\t!!!Execute Again to see the new Results!!!" ;
})
.WithName("GetDependencyLifeTime")
.WithOpenApi();

app.Run();

 
