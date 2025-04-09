using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace Infrastructure.Models;

[Table("Tasks")]
public class TodoTask : BaseModel
{
    [PrimaryKey("id", false)]
    public int Id { get; set; }

    [Column("initiator")]
    public string Initiator { get; set; } = "NONE";
    
    [Column("project")]
    public string Project { get; set; }
    
    [Column("content")]
    public string Content { get; set; }
    
    [Column("start_date")]
    public DateTime StartDate { get; set; }
    
    [Column("end_date")]
    public DateTime EndDate { get; set; }
    
    [Column("status")]
    public string Status { get; set; }
    
    [Column("comment")]
    public string Comment { get; set; }
    
    public override string ToString()
    {
        return $"Task ID: {Id}\n" +
               $"Initiator: {Initiator}\n" +
               $"Project: {Project}\n" +
               $"Content: {Content}\n" +
               $"Start Date: {StartDate:yyyy-MM-dd}\n" +
               $"End Date: {EndDate:yyyy-MM-dd}\n" +
               $"Status: {Status}\n" +
               $"Comment: {Comment}";
    }
}   
