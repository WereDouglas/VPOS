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
        public static List<Store> _store;


        public static void LoadData()
        {
           
            if (Helper.Type.Contains("Lite"))
            {
                _users = new List<Users>(Users.ListUsersLite());
                _org = new List<Organisation>(Organisation.ListOrganisationLite());
                _store = new List<Store>(Store.ListStoreLite());
                _roles = new List<Roles>(Roles.ListRolesLite());
                _category = new List<Category>(Category.ListCategoryLite());
                _billings = new List<Billing>(Billing.ListBillingLite());
                _item = new List<Item>(Item.ListItemLite());
                _purchase = new List<Purchase>(Purchase.ListPurchaseLite());
                _quantitys = new List<Quantity>(Quantity.ListQuantityLite());
                _sale = new List<Sale>(Sale.ListSaleLite());
                _stock = new List<Stock>(Stock.ListStockLite());
                _transactor = new List<Transactor>(Transactor.ListTransactorLite());
                _org = new List<Organisation>(Organisation.ListOrganisationLite());
                _expense = new List<Expense>(Expense.ListExpenseLite());
                _payment = new List<Payment>(Payment.ListPaymentLite());
                _taxes = new List<Tax>(Tax.ListTaxLite());
                _taking = new List<Taking>(Taking.ListTakingLite());
                _store = new List<Store>(Store.ListStoreLite());

            }
            else {
                _users = new List<Users>(Users.ListUsers());
                _roles = new List<Roles>(Roles.ListRoles());
                _category = new List<Category>(Category.ListCategory());
                _billings = new List<Billing>(Billing.ListBilling());
                _item = new List<Item>(Item.ListItem());
                _purchase = new List<Purchase>(Purchase.ListPurchase());
                _quantitys = new List<Quantity>(Quantity.ListQuantity());
                _sale = new List<Sale>(Sale.ListSale());
                _stock = new List<Stock>(Stock.ListStock());
                _transactor = new List<Transactor>(Transactor.ListTransactor());
                _org = new List<Organisation>(Organisation.ListOrganisation());
                _expense = new List<Expense>(Expense.ListExpense());
                _payment = new List<Payment>(Payment.ListPayment());
                _taxes = new List<Tax>(Tax.ListTax());
                _taking = new List<Taking>(Taking.ListTaking());
                _store = new List<Store>(Store.ListStore());

            }

        }
        public static void LoadVital()
        {

            _item = new List<Item>(Item.ListItem());
            _expense = new List<Expense>(Expense.ListExpense());
        }
    }
}