using BL;

namespace WebApp.Models
{
    public class IndexViewModel
    {
        public IEnumerable<MessageViewModel> Messages { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
