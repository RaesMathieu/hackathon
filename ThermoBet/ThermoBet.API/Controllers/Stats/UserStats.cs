namespace ThermoBet.API.Controllers
{
    public class UserStats
    {
        public int UserId { get; set; }
        public int MonthlySwipesCount { get; set; }
        public int AllSwipesCount { get; set; }
        public int SucceedSwipesCount { get; set; }
        public int SucceedPercentage { get; set; }
    }
}
