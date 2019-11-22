namespace photos_library.Models {
	public class Photo {
		public long ID { get; set; }
		public string Title { get; set; }
		public long AlbumID { get; set; }
		public string FullFilePath { get; set; }
		public string Path {
			get { return $"photo/{ID}"; }
		}
	}
}
