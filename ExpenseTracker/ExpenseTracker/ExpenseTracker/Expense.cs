using System;
using System.Collections.Generic;
using System.Text;

namespace ExpenseTracker
{
    public class Expense
    {
        public int ExpenseID { get; set; }
        public DateTime ExpenseDate { get; set; }
        public string ExpenseItem { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; } 
        public decimal Amount { get; set; }
    }
}
