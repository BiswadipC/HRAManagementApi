using Domain.SignalR;
using Services.Common;
using Services.SignalR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.InjectDependency(builder.Configuration);
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();
app.UseHttpsRedirection();
app.UseCors("corsapp");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<ChatHub>("/chathub");
app.MapHub<DesignationHub>("/designationhub");
app.MapHub<PayScaleHub>("/payscalehub");

app.Run();
