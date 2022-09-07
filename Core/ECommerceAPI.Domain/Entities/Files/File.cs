using ECommerceAPI.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceAPI.Domain.Entities.Files;
public class File : BaseEntity {
    public String FileName { get; set; }
    public String Path { get; set; }
    public String Storage { get; set; }

    [NotMapped]
    public override DateTime UpdatedDate { get; set; }
}