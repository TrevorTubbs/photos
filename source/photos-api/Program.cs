using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using photos_api.Utilities;

namespace photos_api {
	public class Program {
		public static void Main(string[] args) {
			var host = CreateHostBuilder(args).Build();

			var scopeFactory = host.Services.GetRequiredService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<StorageContext>();
                if (db.Database.EnsureCreated())
                {
					AlbumGenerator.CreateFromDirectory(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyPictures), db);	
				}
            }

			host.Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder => {
					webBuilder.UseStartup<Startup>();
				});
	}
}
