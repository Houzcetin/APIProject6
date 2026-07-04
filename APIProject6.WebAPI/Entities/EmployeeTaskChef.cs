namespace APIProject6.WebAPI.Entities
{
    public class EmployeeTaskChef
    {
        public int EmployeeTaskId { get; set; }
        public EmployeeTask EmployeeTask { get; set; }

        public int ChefId { get; set; }
        public Chef Chef { get; set; }
    }
}
