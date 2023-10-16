namespace True_Final_Project.Models
{
    public class CostVal
    {
        public string Month { get; set; }
        public int PurchesID { get; set; }
        public string PurchesName { get; set; }
        public double Cost { get; set; }

        public IEnumerable<Months> Months { get; set; }
    }
}
