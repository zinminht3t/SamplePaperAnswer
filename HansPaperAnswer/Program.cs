using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HansPaperAnswer
{
    public class MainClass
    {

        static List<string> items = new List<string>();
        static List<int> qty = new List<int>();
        static List<double> unitPrice = new List<double>();
        static List<double> cost = new List<double>();
        static List<double> discountPrice = new List<double>();
        static List<double> netPrice = new List<double>();
        static int count = 0;
        static string memberid;
        public static void Main(string[] args)
        {
            InputItems();
            bool member = AskMember();
            double grossTotal = CalculateGrossTotal();
            double memberDiscount = CalculateMemberDiscount(grossTotal, member);
            double gstTax = CalculateTax(grossTotal, memberDiscount);
            double total = CalculateTotal(grossTotal, memberDiscount, gstTax);
            ShowInvoice(grossTotal, memberDiscount, gstTax, total);

        }

        static void InputItems()
        {
            bool pressN = false;
            while (!pressN)
            {

                Console.Write("Enter Item Code: ");
                items.Add(Console.ReadLine());

                Console.Write("Enter Qty: ");
                qty.Add(Convert.ToInt32(Console.ReadLine()));

                Console.Write("Enter Price: ");
                unitPrice.Add(Convert.ToDouble(Console.ReadLine()));

                cost.Add((qty[count] * unitPrice[count]));

                if (Convert.ToInt32(DateTime.Today.DayOfWeek) == 2 && items[count].Substring(0, 1) == "F")
                {
                    discountPrice.Add((cost[count] * 0.2));
                }
                else
                {
                    discountPrice.Add(0.00);
                }

                netPrice.Add((cost[count] - discountPrice[count]));

                Console.Write("Add more item? ");
                if (Console.ReadLine() == "N")
                {
                    pressN = true;
                }
                count++;
            }
        }

        static bool AskMember()
        {
            Console.Write("Member? ");

            if (Console.ReadLine() == "Y")
            {
                Console.Write("Member ID : ");
                memberid = Console.ReadLine();
                return true;
            }

            return false;
        }

        static double CalculateGrossTotal()
        {
            double total = 0;
            foreach (double price in netPrice)
            {
                total += price;
            }
            return total;
        }

        static double CalculateMemberDiscount(double grossTotal, bool isMember)
        {
            if (isMember)
            {
                return grossTotal * 0.10;
            }
            return 0;
        }

        static double CalculateTax(double grossTotal, double memberDiscount)
        {

            return (grossTotal - memberDiscount) * 0.07;
        }

        static double CalculateTotal(double grossTotal, double memberDiscount, double tax)
        {
            return (grossTotal + tax) - memberDiscount;
        }

        static void ShowInvoice(double grossTotal, double memberDisocunt, double tax, double total)
        {
            Console.WriteLine("\t\t\t\t\t\tABC Store\t\t\t\t\t\t\t");
            Console.WriteLine("\t\t\t\t\t\tSINGAPORE\t\t\t\t\t\t\t");
            Console.WriteLine("\t\t\t\t\t\tINVOICE\t\t\t\t\t\t\t");
            Console.WriteLine("Date of Purchase : {0} {1}", DateTime.Today.ToShortDateString(), DateTime.Today.DayOfWeek.ToString());

            Console.WriteLine("SNo\tItem Code\tQty\tUnitPrice\tCost\tDiscount\tNet");

            int itemCount = items.Count;

            for (int i = 0; i < itemCount; i++)
            {
                Console.WriteLine("{0}\t{1}\t\t{2}\t{3}\t\t{4}\t{5}\t\t{6}", i, items[i], qty[i], unitPrice[i], cost[i], discountPrice[i], netPrice[i]);
        
            }
            Console.WriteLine();
            Console.WriteLine("Gross Total\t\t\t\t\t\t{0:C}", grossTotal);
            Console.WriteLine("Member Discount\t\t\t\t\t\t{0:C}", memberDisocunt);
            Console.WriteLine("GST @ 7%l\t\t\t\t\t\t{0:C}", tax);
            Console.WriteLine("Please Pay\t\t\t\t\t\t{0:C}", total);

        }
    }
}
