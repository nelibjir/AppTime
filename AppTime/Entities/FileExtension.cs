using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppTime.Entities
{
	[Table("datasets")]
	public partial class FileExtension : IBaseEntity
	{
		[Column("id")]
		public int Id { get; set; }

		[Required]
		[Column("format_name")]
		public string FormatName { get; set; } // TODO enum

		[Required]
		[Column("source")]
		public string SourcePath { get; set; }

		[Required]
		[Column("destination")]
		public string DestinationPath { get; set; }

		[Required]
		[Column("name")]
		public string Name { get; set; }

		[Required]
		[Column("is_local")]
		public bool IsLocal { get; set; }

		[Required]
		[Column("is_cloud")]
		public bool IsCloud { get; set; }
	}
}
