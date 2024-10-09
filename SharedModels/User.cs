namespace SharedModels
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public override string ToString()
        {
            return $"{this.Id} {this.Name} {this.Email}";
        }
    }
}
