namespace photos_library.Models {
	public class Album {
		public long ID { get; set; }
		public string Title { get; set; }
		public string Path {
			get { return $"photo-album/{ID}"; }
		}
	}
}
