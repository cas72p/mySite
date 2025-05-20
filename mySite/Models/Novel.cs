namespace mySite.Models
{
    public class Novel
    {
        public int ID { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public int Chapter { get; set; }
        public string Text { get; set; }

        public Novel()
        {
            
        }
    }
}
