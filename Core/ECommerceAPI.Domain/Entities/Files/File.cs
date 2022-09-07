using ECommerceAPI.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceAPI.Domain.Entities.Files;
public class File : BaseEntity {
    public string FileName { get; set; }
    public string Path { get; set; }

    [NotMapped]
    public override DateTime UpdatedDate { get; set; }
}