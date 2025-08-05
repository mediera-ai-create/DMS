using DMS.Application.Interfaces;
using DMS.Application.Services;
using DMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Infrastructure;

QuestPDF.Settings.License = LicenseType.Community;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IDealerService, DealerService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ISaleService, SaleService>();
builder.Services.AddScoped<IProductMovementService, ProductMovementService>();
builder.Services.AddScoped<IServiceAppointmentService, ServiceAppointmentService>();
builder.Services.AddScoped<IJobCardService, JobCardService>();
builder.Services.AddScoped<IWarrantyClaimService, WarrantyClaimService>();
builder.Services.AddScoped<ILeadService, LeadService>();
builder.Services.AddScoped<ICampaignService, CampaignService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IFeedbackService, FeedbackService>();

builder.Services.AddDbContext<DmsDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
           .LogTo(Console.WriteLine, LogLevel.Information));


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
