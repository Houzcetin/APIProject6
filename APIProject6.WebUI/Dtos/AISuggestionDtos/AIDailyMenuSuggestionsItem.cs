namespace APIProject6.WebUI.Dtos.AISuggestionDtos
{
    public class AIDailyMenuSuggestionsItem
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Reason { get; set; } = string.Empty;
        public string ImageSearchQuery { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
    }
}
