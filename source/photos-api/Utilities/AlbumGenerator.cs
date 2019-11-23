using photos_library.Models;
using System.IO;
using System.Linq;

namespace photos_api.Utilities {
	public static class AlbumGenerator {
		public static Album CreateFromDirectory(string directoryPath, StorageContext context) {
			var info = new DirectoryInfo(directoryPath);
			var albumEntity = context.Albums.Add(new Album() { Title = info.Name });
			context.SaveChanges();

			foreach (var file in info.EnumerateFiles()) {
				if (!IsImageFile(file)) {
					continue;
				}
				context.Photos.Add(new Photo() { 
					AlbumID = albumEntity.Entity.ID,
					Title = file.Name,
					FullFilePath = file.FullName
				});
			}

			context.SaveChanges();
			return albumEntity.Entity;			
		}

		private static bool IsImageFile(FileInfo info) {
			return imageExtensions.Contains(info.Extension.ToLower());
		}
		private static string[] imageExtensions = { ".png", ".jpg", ".jpeg" };
	}
}
