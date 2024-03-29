using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using System.Linq;
using System.Net.Mime;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCompression;

namespace photos_api {
	public class Startup {
		public Startup(IConfiguration configuration) {
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services) {
			services.AddCors();

			services.AddControllers();

			services.AddDbContext<StorageContext>(options => options.UseSqlite("Data Source=photos.db"));
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			}

			app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod());

			app.UseRouting();

			app.UseAuthorization();
			app.UseClientSideBlazorFiles<photos.Startup>();

			app.UseEndpoints(endpoints => {
				endpoints.MapControllers();
				endpoints.MapFallbackToClientSideBlazor<photos.Startup>("index.html");
			});
		}
	}
}