class Program
{
  public static void Main()
  {
    double price;
    int quantity;
    double discount;

    Console.WriteLine("Enter thr price");
    while(!double.TryParse(Console.ReadLine(),out price)|| price < 0)
    {
     Console.Write("Invalid Price. Enter again: ");

    }

    Console.WriteLine("Enter the Quantity");

    while(!int.TryParse(Console.ReadLine(), out quantity)|| quantity < 0)
    {
      Console.WriteLine("Quantity can't be negative");
    }

    Console.WriteLine("ENter the disscount");

    discount=double.Parse(Console.ReadLine());

    double subTotal=price*quantity;
    double discountAmount=subTotal*discount/100;

    double finalAmmount=subTotal-discountAmount;

    Console.WriteLine("Subtotal is "+subTotal);
    Console.WriteLine("Discount Amount is "+discountAmount);
    Console.WriteLine("Final AMount is "+finalAmmount);


    
  }
}