using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace Infrastructure.Models;

[Table("Tasks")]
public class TodoTask : BaseModel
{
    [PrimaryKey("id", false)]
    public int Id { get; set; }

    [Column("description")]
    public string Description { get; set; } = "NONE";

    public override string ToString()
    {
        return $"{Id} - Task: {Description}";
    }
}   
