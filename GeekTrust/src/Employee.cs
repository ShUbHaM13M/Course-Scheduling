namespace GeekTrust
{
    public struct Employee(string email)
    {
        public string Email { get; set; } = email;
        public string Name { get; set; } = email.Split("@")[0];
    }
}