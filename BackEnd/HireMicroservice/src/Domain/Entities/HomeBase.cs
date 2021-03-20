namespace Domain.Entities
{
    public class HomeBase : BaseEntity
    {
        public int Capacity { get; set; }

        public int NumOfBikes { get; set; }
    }
}