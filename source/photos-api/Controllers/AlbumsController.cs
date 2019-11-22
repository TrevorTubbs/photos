using Microsoft.AspNetCore.Mvc;
using photos_library.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace photos_api.Controllers {
	[ApiController]
	[Route("api/[controller]")]
	public class AlbumsController : ControllerBase {
		private readonly StorageContext _db;

		public AlbumsController(StorageContext db) {
			_db = db;
		}

		[HttpGet]
		public IEnumerable<Album> Get() {
			return _db.Albums.OrderBy(a => a.Title);
		}

		[HttpGet]
		[Route("{albumID}")]
		public Album GetAlbum(long albumID) {
			return _db.Albums.Where(a => a.ID == albumID).First();
		}

		[HttpGet]
		[Route("photos/{albumID}")]
		public IEnumerable<Photo> GetAlbumPhotos(long albumID) {
			return _db.Photos.Where(p => p.AlbumID == albumID).OrderBy(p => p.Title);
		}

		[HttpGet]
		[Route("photo/{photoID}")]
		public IActionResult GetPhoto(long photoID) {
			var photo = _db.Photos.Where(p => p.ID == photoID).First();
			var info = new FileInfo(photo.FullFilePath);

			var bytes = System.IO.File.ReadAllBytes(info.FullName);

			return File(bytes, $"image/{info.Extension.Substring(1).ToLower()}", info.Name);
		}
	}
}
