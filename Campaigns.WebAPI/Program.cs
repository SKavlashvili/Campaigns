using Campaigns.WebAPI.Extensions;
using Campaigns.WebAPI.Ifrastructure.BackgroundWorkers;
using Campaigns.WebAPI.Ifrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

//baclground services
builder.Services.AddHostedService<DateCheckerService>();


//Services
builder.Services.AddControllers();
builder.Services.AddCustomServices();


//swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();



//swagger
app.UseSwagger();
app.UseSwaggerUI();

//Error handler
app.UseGlobalErrorHandler();


//Destoryer
app.UseConnectionDestroyer();

//MapControllers
app.UseRouting();
app.UseEndpoints((IEndpointRouteBuilder endpointBuilder) =>
{
    endpointBuilder.MapControllers();
});

//Run
app.Run();
