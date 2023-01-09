using Microsoft.Extensions.Configuration;
using System.Reflection.PortableExecutable;
using System.Transactions;
using VBPaymentDbLib;
using VBPaymentDbLib.Model;

namespace PaymentConsole
{
    internal class Program
    {
        private static IConfiguration? Configuration;
        static void Main(string[] args)
        {
            BuildConfig();

            var _VBPaymentDbApi = new VBPaymentDb(Configuration?.GetConnectionString("RealtaHotelDb"));

            // ----------- FindAllBank -------------
            //var listOfBank = _VBPaymentDbApi.RepositoryManager.Bank.FindAll(limit: 5);
            //foreach (var bank in listOfBank)
            //    Console.WriteLine(bank);

            // ----------- FindBank -------------
            //var findBank = _VBPaymentDbApi.RepositoryManager.Bank.FindById(3);
            //Console.WriteLine(findBank);

            // ---------- CreateBank ------------
            //var createBank = _VBPaymentDbApi.RepositoryManager.Bank.Create(new Bank() { Code = "426", Name = "BANK MEGA" });
            //Console.WriteLine(createBank);

            // ---------- UpdateBank ------------
            //var updateBank = _VBPaymentDbApi.RepositoryManager.Bank.Update(id: 18, code: "114", name: "BANK JATIM");
            //Console.WriteLine(updateBank);

            // ---------- DeleteBank ------------
            //var deleteBank = _VBPaymentDbApi.RepositoryManager.Bank.Delete(id: 18);
            //Console.WriteLine($"Bank Rows delete affected : {deleteBank}");

            // ----------- FindAllPayment -------------
            //var listOfPayment = _VBPaymentDbApi.RepositoryManager.Payment.FindAll(2);
            //foreach (var payment in listOfPayment)
            //{
            //    Console.WriteLine(payment);
            //}

            // ----------- FindPayment -------------
            //var findPayment = _VBPaymentDbApi.RepositoryManager.Payment.FindById(12);
            //Console.WriteLine(findPayment);

            // ---------- CreatePayment ------------
            //var createPayment = _VBPaymentDbApi.RepositoryManager.Payment.Create(new Payment() { Code = "DOMPETKU", Name = "PT. BUKAN DOMPETMU" });
            //Console.WriteLine(createPayment);

            // ---------- UpdatePayment ------------
            //var updatePayment = _VBPaymentDbApi.RepositoryManager.Payment.Update(id: 17, code: "FLOP", name: "PT. DOMPET DIGITAL FLOP.ID");
            //Console.WriteLine(updatePayment);

            // ---------- DeletePayment ------------
            //var deletePayment = _VBPaymentDbApi.RepositoryManager.Payment.Delete(id: 17);
            //Console.WriteLine($"Bank Rows delete affected : {deletePayment}");


            // ----------- FindAllUserAccounts -------------
            //var listOfAccount = _VBPaymentDbApi.RepositoryManager.Account.FindAll(2);
            //foreach (var account in listOfAccount)
            //{
            //    Console.WriteLine(account);
            //}

            // ----------- FindUserAccount -------------
            //var findUserAccount = _VBPaymentDbApi.RepositoryManager.Account.FindById(13, 13);
            //Console.WriteLine(findUserAccount);

            // ---------- CreateUserAccount ------------
            //var createAccount = _VBPaymentDbApi.RepositoryManager.Account.Create(new Account() { EntityId = 27, UserId = 15, AccountNumber = "890128311111", Saldo= 7263000, Type = "payment", ExpMonth = 0, ExpYear = 0 });
            //Console.WriteLine(createAccount);

            // ---------- UpdateUserAccount ------------
            //var updateUserAccount = _VBPaymentDbApi.RepositoryManager.Account.Update(entityId: 2, userId: 2, accountNumber: "988123362313", saldo: 50000); 
            //Console.WriteLine(updateUserAccount);

            // ---------- DeletePayment ------------
            //var deleteUserAccount = _VBPaymentDbApi.RepositoryManager.Account.Delete(entityId: 2, userId: 2);
            //Console.WriteLine($"Bank Rows delete affected : {deleteUserAccount}");

            // ----------- FindAllTransaction -------------
            //var listOfTransaction = _VBPaymentDbApi.RepositoryManager.Transaction.FindAll(limit: 5);
            //foreach (var transaction in listOfTransaction)
            //{
            //    Console.WriteLine(transaction);
            //}

            // ----------- FindTransaction -------------
            //var findTransaction = _VBPaymentDbApi.RepositoryManager.Transaction.FindById(2);
            //Console.WriteLine(findTransaction);

            // ---------- CreateTransaction ------------
            //var createTransaction = _VBPaymentDbApi.RepositoryManager.Transaction.Create(new PaymentTransact()
            //{ 
            //    TrxNumber = "ORM#20221127-0029",
            //    Debet = 13000,
            //    Credit = 50000,
            //    Type = "ORM",
            //    Note = "Customer hutang",
            //    OrderNumber = "BO#20221127-0004",
            //    SourceId = 1,
            //    TargetId = 2,
            //    TrxNumberRef = "ORM#20221127-0029",
            //    UserId = 2
            //});
            //Console.WriteLine(createTransaction);

            // ---------- UpdateUserAccount ------------
            //var updateTransaction = _VBPaymentDbApi.RepositoryManager.Transaction.Update(id: 2, debet: 23000, credit: 9500, trxNumberRef: "TRB#20221127-0099");
            //Console.WriteLine(updateTransaction);

            // ---------- DeletePayment ------------
            //var deleteTransaction = _VBPaymentDbApi.RepositoryManager.Transaction.Delete(7);
            //Console.WriteLine($"Bank Rows delete affected : {deleteTransaction}");

        }

        static void BuildConfig()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(path: "AppSetting.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();
        }
    }
}