namespace Datalayer
{
    public enum ProductCategory
    {
        Food,
        Electronics,
        Clothing,
        Beauty,
        Home,
        Other
    }

    public class Product
    {
        public double Price { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public byte[] Picture { get; set; }
        public ProductCategory Category { get; set; }
        public float Grading { get; set; }

        public Product(double price, string name, string company, byte[] picture, ProductCategory category, float grading)
        {
            Price = price;
            Name = name;
            Company = company;
            Picture = picture;
            Category = category;
            Grading = grading;
        }
    }
}