using System.Net.Mail;

var builder = WebApplication.CreateBuilder(args);


var smtpSection = builder.Configuration.GetSection("SmtpSettings");

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddFluentEmail(smtpSection["Username"])
    .AddSmtpSender(() => new SmtpClient(smtpSection["Host"])
    {
        Port = int.Parse(smtpSection["Port"]),
        Credentials = new System.Net.NetworkCredential(smtpSection["Username"], smtpSection["Password"]),
        EnableSsl = bool.Parse(smtpSection["EnableSSL"])
    });

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
