namespace Test2.DTOs;

public class AddBookRequest
{
    public string Name { get; set; }
    public DateTime ReleaseDate { get; set; }
    public int PublishingHouseId { get; set; }
    public string PublishingHouseName { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public List<int> AuthorIds { get; set; }
    public List<int> GenreIds { get; set; }
}