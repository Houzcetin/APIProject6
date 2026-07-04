namespace APIProject6.WebAPI.Entities
{
    public class EmployeeTask
    {
        public int EmployeeTaskId { get; set; }

        public string TaskName { get; set; }

        public byte TaskStatusValue { get; set; }

        public DateTime AssignDate { get; set; }

        public DateTime DueTime { get; set; }

        public string Priority { get; set; }

        public string TaskStatus { get; set; }

        public List<EmployeeTaskChef> EmployeeTaskChefs { get; set; } = new();

    }
}
