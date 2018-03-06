using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Product : IComparable<Product>
{

    public string Name { get; set; }
    public double Price { get; set; }
    public string Producer { get; set; }

    public Product(string name, double price, string producer)
    {
        this.Name = name;
        this.Price = price;
        this.Producer = producer;
    }

    public int CompareTo(Product other)
    {

        int compare = this.Name.CompareTo(other.Name);

        if (compare == 0)
        {
            compare = this.Producer.CompareTo(other.Producer);
            if (compare == 0)
            {
                return this.Price.CompareTo(other.Price);
            }
        }
        return compare;
    }

    public override string ToString()
    {
        return "{" + $"{this.Name};{this.Producer};{this.Price:f2}" + "}";
    }
}
