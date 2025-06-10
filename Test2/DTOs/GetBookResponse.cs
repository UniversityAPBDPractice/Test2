namespace Test2.DTOs;

public class GetBookResponse
{
    public string BookName { get; set; }
    public DateTime ReleaseDate { get; set; }
    public PublishingHouseDto PublishingHouse { get; set; }
    public List<string> Genres { get; set; }
    public List<AuthorDto> Authors { get; set; }
}