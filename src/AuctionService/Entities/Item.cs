using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuctionService.Entities;

[Table("Items")]
public class Item
{
    public Guid Id { get; set; }
    [MaxLength(25)] public string Make { get; set; }
    [MaxLength(50)] public string Model { get; set; }
    public int Year { get; set; }
    [MaxLength(25)] public string Color { get; set; }
    public int Mileage { get; set; }
    public string ImageUrl { get; set; }
    public Auction Auction { get; set; }
    public Guid AuctionId { get; set; }
}