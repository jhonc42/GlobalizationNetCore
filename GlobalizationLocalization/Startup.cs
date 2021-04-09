using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace GlobalizationLocalization
{
    public class Startup
    {
        // ESTA ES LA CONSTANTE QUE SE LEERÍA DESDE BD PARA ELEGIR EL IDIOMA A CARGAR:
        // private const string enUSCulture = "fr";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(); 

            // Globalization and localization
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddMvc()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization();


            // OJO PARA MIRAR DESPUÉS, SEGÚN LA DOCUMENTACIÓN SE PUEDE PERMITIR QUE EL CLIENTE ELIJA LA CONFIGURACIÓN DEL IDIOMA QUE QUIERA Y LEERLA CON LO SIGUIENTE:
            // AUNQUE SE PODRÍA ENVIAR DESDE EL CLIENTE POR EJEMPLO ANGULAR EN LA CABECERA DE LA PETICIÓN DEL CONTROLADOR ASI COMO ESTÁ SIN NECESIDAD DE LO DE ABAJO.
            //services.Configure<RequestLocalizationOptions>(options =>
            //{
            //    var supportedCultures = new[]
            //    {
            //        new CultureInfo(enUSCulture),
            //        new CultureInfo("fr")
            //    };

            //    options.DefaultRequestCulture = new RequestCulture(culture: enUSCulture, uiCulture: enUSCulture);
            //    options.SupportedCultures = supportedCultures;
            //    options.SupportedUICultures = supportedCultures;

            //    options.AddInitialRequestCultureProvider(new CustomRequestCultureProvider(async context =>
            //    {
            //        // My custom request culture logic
            //        return new ProviderCultureResult("en");
            //    }));
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            // Globalization and localization
            var cultures = new List<CultureInfo> {
                new CultureInfo("en"),
                new CultureInfo("fr"),
                new CultureInfo("es")
            };
            app.UseRequestLocalization(options => {
                options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en");
                options.SupportedCultures = cultures;
                options.SupportedUICultures = cultures;
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
