using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPOS.Model;

namespace VPOS
{
    public static class Global
    {
        public static List<Category> category;
        public static List<Billing> billings;
        public static List<Payment> payment;       
        public static List<Item> item;
        public static List<Purchase> purchase;
        public static List<Quantity> quantitys;
        public static List<Sale> sale;
        public static List<Stock> stock;
       
        public static List<Roles> roles;
        public static List<Users> users;
        public static List<Organisation> org;
        public static List<Expense> expense;
        public static List<Invoice> invoices;
        public static List<Taking> taking;
        public static List<Store> store;
        public static List<Customer> customer;
        public static List<Supplier> supplier;
        public static List<Sub> sub;


        public static void LoadData(string start, string end)
        {
            
            org = new List<Organisation>(Organisation.ListOrganisation());
          
            payment = new List<Payment>(Payment.ListPayment());
            category = new List<Category>(Category.ListCategory());           
            users = new List<Users>(Users.ListUsers());
            roles = new List<Roles>(Roles.ListRoles());
            billings = new List<Billing>(Billing.ListBilling(start, end));

            item = new List<Item>(Item.ListItem());
            purchase = new List<Purchase>(Purchase.ListPurchase());
            quantitys = new List<Quantity>(Quantity.ListQuantity());
            sale = new List<Sale>(Sale.ListSale());
            org = new List<Organisation>(Organisation.ListOrganisation());
            expense = new List<Expense>(Expense.ListExpense());
            sub = new List<Sub>(Sub.ListSub());

            supplier = new List<Supplier>(Supplier.ListSupplier());
            customer = new List<Customer>(Customer.ListCustomer());
            taking = new List<Taking>(Taking.ListTaking());
            store = new List<Store>(Store.ListStore());
            stock = new List<Stock>(Stock.ListStock());
            invoices = new List<Invoice>(Invoice.ListInvoice());



        }
        public static void LoadVital()
        {

            users = new List<Users>(Users.ListUsers());
            item = new List<Item>(Item.ListItem());
            expense = new List<Expense>(Expense.ListExpense());


        }
       
    }
}