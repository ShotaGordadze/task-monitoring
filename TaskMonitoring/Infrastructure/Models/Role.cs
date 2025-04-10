using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace Infrastructure.Models;

[System.ComponentModel.DataAnnotations.Schema.Table("Roles")]
public class Role : BaseModel
{
    [PrimaryKey("id", false)]
    public int Id { get; set; }
    
    [Column("role")]
    public Roles RoleName { get; set; }
}