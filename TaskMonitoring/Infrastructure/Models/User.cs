using System.Reflection;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace Infrastructure.Models;

[Table("Users")]
public class User : BaseModel
{
    [PrimaryKey("id", false)]
    public int Id { get; set; }
    
    [Column("name")]
    public string Name { get; set; } 
    
    [Column("last_name")]
    public string LastName { get; set; } 
    
    [Column("user_name")]
    public string UserName { get; set; } 
    
    [Column("password")]
    public string Password { get; set; } 
    
    [Column("role_id")]
    public int RoleId { get; set; }
}