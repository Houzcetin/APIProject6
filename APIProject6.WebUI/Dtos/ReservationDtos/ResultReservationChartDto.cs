namespace APIProject6.WebUI.Dtos.ReservationDtos
{
    public class ResultReservationChartDto
    {
        public List<string> Labels { get; set; } = new();

        public List<int> Approved { get; set; } = new();
        public List<int> WaitingForApproval { get; set; } = new();
        public List<int> Cancelled { get; set; } = new();
        public List<int> Completed { get; set; } = new();
        public List<int> NoShow { get; set; } = new();

        public int TotalReservations { get; set; }
        public int TotalGuests { get; set; }

        public int ApprovedReservations { get; set; }
        public int WaitingReservations { get; set; }
        public int CancelledReservations { get; set; }
        public int CompletedReservations { get; set; }
        public int NoShowReservations { get; set; }

        public int NewCustomers { get; set; }
    }
}
