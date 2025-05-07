namespace TodoApi; 
public class TodoItem {

    public int Id { get; set; }
    public string? Name { get; set; }
    public bool IsComplete { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? CompletedAt { get; set; }

}
