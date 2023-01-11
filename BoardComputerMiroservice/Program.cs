using BoardComputerMiroservice.AsyncDataServices;
using BoardComputerMiroservice.Data;
using BoardComputerMiroservice.SyncDataServices.Grpc;
using BoardComputerMiroservice.SyncDataServices.Http;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDdContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQL")));

builder.Services.AddScoped<IBoardComputerRepository, BoardComputerRepository>();

builder.Services.AddSingleton<IMessageBusClient, MessageBusClient>();

builder.Services.AddGrpc();

builder.Services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGrpcService<GrpcBoardComputerService>();

app.Run();
