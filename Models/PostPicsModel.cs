using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EffectiveWebProg.Models
{
    public class PostPicsModel
    {
        [Key]
        [Column(TypeName = "char(36)")]
        public Guid PicID { get; set; } // Primary Key
        
        [Column(TypeName = "char(36)")]
        public Guid PostID { get; set; } // Foreign Key From PostsModel

        [StringLength(255)]
        [Column(TypeName = "varchar(255)")]
        public string ImageURL { get; set; }

        // Navigation properties
        [ForeignKey("PostID")]
        public PostsModel? Post { get; set; }

        public PostPicsModel()
        {
            PicID = Guid.NewGuid();
        }
        public PostPicsModel(string filename)
        {
            PicID = Guid.NewGuid();
            ImageURL = PicID.ToString() + Path.GetExtension(filename);
        }
    }
}
