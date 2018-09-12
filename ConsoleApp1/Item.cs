namespace ConsoleApp1
{
    public class Item
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }


        public override string ToString()
        {

            return string.Format($"Id:{Id}, Name:{Name}, Iscompelte:{IsComplete}");

        }

    }
}