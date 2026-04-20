namespace ClickParty.Models;

public class Event
{
  public int Id { get; set; }
  public string Title { get; set; }
  public string Description { get; set; }
  public DateOnly Date { get; set; }
  public string Location { get; set; }
  public string Status { get; set; }
  public string Category { get; set; }
  public string Image_url { get; set; }
}