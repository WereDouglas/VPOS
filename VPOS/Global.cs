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
        public static List<Category> _category;
        public static List<Billing> _billings;
        public static List<Payment> _payment;
        //  public static List<Users> _users;
        public static List<Item> _item;
        public static List<Purchase> _purchase;
        public static List<Quantity> _quantitys;
        public static List<Sale> _sale;
        public static List<Stock> _stock;
        public static List<Transactor> _transactor;
        public static List<Roles> _roles;
        public static List<Users> _users;
        public static List<Organisation> _org;
        public static List<Expense> _expense;
        public static List<Tax> _taxes;
        public static List<Taking> _taking;


        public static void LoadData()
        {
            _roles = new List<Roles>(Roles.ListRoles());
            _category = new List<Category>(Category.ListCategory());
            _billings = new List<Billing>(Billing.ListBilling());
            _item = new List<Item>(Item.ListItem());
            _purchase = new List<Purchase>(Purchase.ListPurchase());
            _quantitys = new List<Quantity>(Quantity.ListQuantity());
            _sale = new List<Sale>(Sale.ListSale());
            _stock = new List<Stock>(Stock.ListStock());
            _transactor = new List<Transactor>(Transactor.ListTransactor());
            _users = new List<Users>(Users.ListUsers());
            _org = new List<Organisation>(Organisation.ListOrganisation());
            _expense = new List<Expense>(Expense.ListExpense());
            _payment = new List<Payment>(Payment.ListPayment());
            _taxes = new List<Tax>(Tax.ListTax());
            _taking = new List<Taking>(Taking.ListTaking());

        }
        public static void LoadVital()
        {

            _item = new List<Item>(Item.ListItem());
            _expense = new List<Expense>(Expense.ListExpense());
        }
    }
}