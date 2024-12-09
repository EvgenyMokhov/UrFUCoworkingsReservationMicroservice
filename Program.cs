using UrFUCoworkingsReservationMicroservice.Data.Implementations;
using UrFUCoworkingsReservationMicroservice.Data.Interfaces;
using UrFUCoworkingsReservationMicroservice.Data;
using Microsoft.EntityFrameworkCore;
using MassTransit;
using UrFUCoworkingsReservationMicroservice.Rabbit.Services.Reservations;
using UrFUCoworkingsReservationMicroservice.Rabbit.Services.Times;
using RabbitMQ.Client;
using UrFUCoworkingsReservationMicroservice.Business_Logic.Services;
using UrFUCoworkingsModels.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IPlaces, Places>();
builder.Services.AddTransient<IReservations, Reservations>();
builder.Services.AddTransient<IUsers, Users>();
builder.Services.AddTransient<IVisits, Visits>();
builder.Services.AddScoped<DataManager>();
builder.Services.AddHostedService<DeleteDataBackgroundService>();
var connection = builder.Configuration["ConnectionStrings:MSSQL"];
builder.Services.AddDbContext<EFDBContext>(options =>
{
    options.UseSqlServer(connection);
});
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<CreateReservationRequestConsumer>().Endpoint(e => e.Name = "create-reservation-requests");
    x.AddConsumer<DeleteReservationRequestConsumer>().Endpoint(e => e.Name = "delete-reservation-requests");
    x.AddConsumer<GetReservationByIdRequestConsumer>().Endpoint(e => e.Name = "get-reservation-by-id-requests");
    x.AddConsumer<GetReservationsRequestConsumer>().Endpoint(e => e.Name = "get-reservations-requests");
    x.AddConsumer<UpdateReservationRequestConsumer>().Endpoint(e => e.Name = "update-reservation-requests");
    x.AddConsumer<GetReservatedTimesRequestConsumer>().Endpoint(e => e.Name = "get-reservated-times-requests");
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "vh9", h=>
        {
            h.Username("guest");
            h.Password("guest");
        });
        cfg.ExchangeType = ExchangeType.Fanout;
        cfg.ConfigureEndpoints(context);
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Reservation API ver. 1.1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
