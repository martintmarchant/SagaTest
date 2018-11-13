using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Saga.Api.Transactions
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var csvFileSourceSettings = new Transaction.Model.FileSource();
            Configuration.Bind("BasketCsvFileSource", csvFileSourceSettings);

            // Configure DI
            services
                .AddSingleton(csvFileSourceSettings)
                .AddScoped<Transaction.Csv.IFileConverter, Transaction.Csv.FileConverter>()
                .AddScoped<Transaction.IBasketReadOnlyOperation, Transaction.BasketReadOnlyOperation>();

            services.AddMvc();
 
            // Add Swagger UI
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Saga Test",
                    Description = "Basket Transactions API",
                    TermsOfService = "None",
                    Contact = new Contact()
                                {
                                    Name = "Developer: Martin Marchant",
                                    Email = "Martin.Marchant@gmail.com",
                                    Url = "https:///confluence.com/documentaiton=somelink" }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddFile(@"c:\temp\ApiTransactionService-{Date}.txt");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Basket Transactions API");
            });
            app.UseMvc();
        }
    }
}
