using System.ComponentModel.DataAnnotations.Schema;

namespace Agency_Web_App.Models
{
    public class Portofolio
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string  WebsiteType { get; set; }
        public string? ImgUrl { get; set; }

        [NotMapped]
        public IFormFile? ImgFile { get; set; }
    }
}
