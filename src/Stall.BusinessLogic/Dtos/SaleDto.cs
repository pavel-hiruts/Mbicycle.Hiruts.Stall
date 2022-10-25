namespace Stall.BusinessLogic.Dtos;

public class SaleDto
{
    public int Id { get; set; }

    public string ProductName { get; set; }

    public DateTime Date { get; set; }

    public decimal Price { get; set; }

    public int Count { get; set; }
}