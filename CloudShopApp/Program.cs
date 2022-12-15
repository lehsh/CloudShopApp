using CloudShopApp.Model;
using CloudShopApp.Model.Entity;
using CloudShopApp.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CloudShopDbContext>();
builder.Services.AddTransient<IOrderServices, OrdersServices>();

var app = builder.Build();

app.MapGet("/", () => "pong");
app.MapGet("/ping", () => "pong");

app.MapGet("/all", async (HttpContext context, IOrderServices service) =>
{
    await context.Response.WriteAsJsonAsync(service.GetAllOrders());
});

app.MapPost("/add", async (HttpContext context, IOrderServices service) =>
{
    string feedbackContact = context.Request.Form["feedbackContact"];
    string description = context.Request.Form["description"];
    Order newOrder = service.AddOrder(new Order { FeedbackContact = feedbackContact, Description = description });
    await context.Response.WriteAsJsonAsync(newOrder);
});

app.MapGet("/order", async (HttpContext context, IOrderServices service) =>
{
    int id = Convert.ToInt32(context.Request.Query["id"]);
    await context.Response.WriteAsJsonAsync(service.GetOrderById(id));
});

app.MapGet("/remove", async (HttpContext context, IOrderServices service) =>
{
    int id = Convert.ToInt32(context.Request.Query["id"]);
    service.RemoveOrderById(id);
});

app.MapPost("/update", async (HttpContext context, IOrderServices service) =>
{
    Order orderForUpdate = await context.Request.ReadFromJsonAsync<Order>();
    orderForUpdate = service.UpdateOrder(orderForUpdate);
    await context.Response.WriteAsJsonAsync(orderForUpdate);
});

app.Run();
