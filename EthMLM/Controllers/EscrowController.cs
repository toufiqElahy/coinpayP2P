using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EthMLM.Models;
using EthMLM.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.NodeServices;
using Nethereum.Web3;
using Newtonsoft.Json.Linq;

namespace EthMLM.Controllers
{
    [Authorize]
    public class EscrowController : Controller
    {
        public IActionResult Index()
        {
            string email = User.Identity.Name;
            var userWallet = UserWalletModel._userWallet.FirstOrDefault(x=>x.Email==email);
            if (userWallet == null)
            {
                userWallet = new UserWallet { Email = email };
                UserWalletModel._userWallet.Add(userWallet);
            }
            return View(userWallet);
        }
        [HttpPost]
        public IActionResult Index(string coin,double amnt)
        {
            string email = User.Identity.Name;
            var userWallet = UserWalletModel._userWallet.FirstOrDefault(x => x.Email == email);
            if (coin == "BTC")
            {
                userWallet.BTC += amnt;
            }
            else if (coin == "ETH")
            {
                userWallet.ETH += amnt;
            }
            else
            {
                userWallet.USD += amnt;
            }
            return View(userWallet);
        }
        public IActionResult BuySell(string type,string coin,string paymentMethod)
        {
            if (coin != null)
            {
                return View(OfferModel._offers.Where(x => x.Type == type && x.IsOpen &&x.Coin==coin&&x.PaymentMethod==paymentMethod).ToList());
            }
            return View(OfferModel._offers.Where(x => x.Type == type && x.IsOpen).ToList());
        }
        public IActionResult CreateOffer()
        {
            string email = User.Identity.Name;
            var userWallet = UserWalletModel._userWallet.FirstOrDefault(x => x.Email == email);
            return View(userWallet);
        }
        [HttpPost]
        public IActionResult CreateOffer(Offer mOffer)
        {
            mOffer.Email = User.Identity.Name;
            OfferModel._offers.Add(mOffer);
            return RedirectToAction("Dashboard");
        }
        public IActionResult Dashboard()
        {
            return View(OfferModel._offers.Where(x=>x.Email==User.Identity.Name).ToList());
        }
        public IActionResult OfferIsOpenToggle(bool status,DateTime date)
        {
            string email = User.Identity.Name;
            var mOffer = OfferModel._offers.FirstOrDefault(x => x.Email == email && x.CreationTime.ToString().Contains(date.ToString()));
            //operate
            mOffer.IsOpen = !status;
            return RedirectToAction("Dashboard");
        }
        public IActionResult DeleteOffer(DateTime date)
        {
            string email = User.Identity.Name;
            var mOffer = OfferModel._offers.FirstOrDefault(x => x.Email == email && x.CreationTime.ToString().Contains(date.ToString()));
            //operate
            OfferModel._offers.Remove(mOffer);
            return RedirectToAction("Dashboard");
        }

        public IActionResult RepayLoan(DateTime date)
        {
            string email = User.Identity.Name;
            var mloan = LoanModel._loans.FirstOrDefault(x => x.Email == email  && x.date.ToString().Contains(date.ToString()));

            var userWallet = UserWalletModel._userWallet.FirstOrDefault(x => x.Email == email);
            

            if (mloan.rPay > userWallet.USD)
            {
                TempData["msg"] = "U Have Only : " + userWallet.USD + " USD";
                return View("BorrowerForm", mloan);
            }

            //operate
            userWallet.USD -= mloan.rPay;
            var lenderWallet = UserWalletModel._userWallet.FirstOrDefault(x => x.Email == mloan.LenderEmail);
            lenderWallet.USD += mloan.rPay;

            if (mloan.coin == "ETH")
            {
                userWallet.ETH += mloan.amnt;
            }
            else
            {
                userWallet.BTC += mloan.amnt;
            }
            LoanModel._loans.Remove(mloan);


            return RedirectToAction("OrderHistory");
        }
        public IActionResult LendUSD()
        {
            //return View(LoanModel._loans.Where(x => x.status == "Pending").ToList());
            return View(LoanModel._loans.Where(x => x.status == "Pending" &&x.Email!=User.Identity.Name).ToList());
        }
        public IActionResult Lend(DateTime date)
        {
            string email = User.Identity.Name;
            var mloan = LoanModel._loans.FirstOrDefault(x => x.status == "Pending" && x.date.ToString().Contains(date.ToString()));

            var userWallet = UserWalletModel._userWallet.FirstOrDefault(x => x.Email == mloan.Email);
            var lenderWallet = UserWalletModel._userWallet.FirstOrDefault(x => x.Email == email);


            if (mloan.usd > lenderWallet.USD)
            {
                TempData["msg"] = "U Have Only : " + userWallet.USD + " USD";
                return View("BorrowerForm", mloan);
            }

            //operate
            lenderWallet.USD -= mloan.usd;
            userWallet.USD += mloan.usd;
            mloan.status = "Approved";
            mloan.LenderEmail = email;
           


            return RedirectToAction("LendHistory");
        }
        public IActionResult LendHistory()
        {
            return View(LoanModel._loans.Where(x=>x.status=="Approved"&&x.LenderEmail==User.Identity.Name).ToList());
        }
        public IActionResult FailToRepay(DateTime date)
        {
            var mloan = LoanModel._loans.FirstOrDefault(x => x.date.ToString().Contains(date.ToString()));
            var userWallet = UserWalletModel._userWallet.FirstOrDefault(x => x.Email == mloan.Email);
            var lenerWallet = UserWalletModel._userWallet.FirstOrDefault(x => x.Email == mloan.LenderEmail);

            //sell btc,eth at market price
            if (mloan.coin == "ETH")
            {
                var price = PriceByCoin("ethereum");//usd
                var rpayCoin = mloan.rPay / price;
                LoanModel._coinpayUserWallet.ETH += rpayCoin;//selling
                userWallet.ETH += (mloan.amnt-rpayCoin);

            }
            else
            {
                var price = PriceByCoin("bitcoin");
                var rpayCoin = mloan.rPay / price;
                LoanModel._coinpayUserWallet.BTC += rpayCoin;//selling
                userWallet.BTC += (mloan.amnt - rpayCoin);
            }

            LoanModel._coinpayUserWallet.USD -= mloan.rPay;
            lenerWallet.USD += mloan.rPay;

            //operate
            LoanModel._loans.Remove(mloan);



            return RedirectToAction("Index");
        }
        private double PriceByCoin(string coin)
        {
            using (WebClient wc = new WebClient())
            {
                var json = wc.DownloadString("https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd&ids="+coin);
                dynamic data = JObject.Parse(json.TrimStart('[').TrimEnd(']'));
                return (double)data.current_price;
            }
        }
        public IActionResult Coinpay()
        {
            

            return View(LoanModel._coinpayUserWallet);
        }
        [HttpPost]
        public IActionResult Coinpay(double iRate,int cRate,int aRate)
        {
            LoanModel._dailyInterestRate = iRate / 100;
            LoanModel._cRate = cRate;
            LoanModel._allowanceRate = aRate;
            return View(LoanModel._coinpayUserWallet);
        }
        public IActionResult LiquidateUsers()
        {
            return View(LoanModel._loans.Where(x => x.status == "Approved").ToList());
        }
        [HttpPost]
        public IActionResult LiquidateUsers(string coin,double price)
        {
            //var ratio = 40 / 200;
            var loans = LoanModel._loans.Where(x => x.coin == coin && x.status == "Approved" && (x.coinPrice - (x.coinPrice * 0.2))>=price).ToList();
            foreach (var mloan in loans)
            {
                //var mloan = LoanModel._loans.FirstOrDefault(x => x.date.ToString().Contains(date.ToString()));
                var userWallet = UserWalletModel._userWallet.FirstOrDefault(x => x.Email == mloan.Email);
                var lenerWallet = UserWalletModel._userWallet.FirstOrDefault(x => x.Email == mloan.LenderEmail);

                //sell btc,eth at market price
                if (mloan.coin == "ETH")
                {
                    //var price = PriceByCoin("ethereum");//usd
                    var rpayCoin = mloan.rPay / price;
                    LoanModel._coinpayUserWallet.ETH += rpayCoin;//selling
                    userWallet.ETH += (mloan.amnt - rpayCoin);

                }
                else
                {
                    //var price = PriceByCoin("bitcoin");
                    var rpayCoin = mloan.rPay / price;
                    LoanModel._coinpayUserWallet.BTC += rpayCoin;//selling
                    userWallet.BTC += (mloan.amnt - rpayCoin);
                }

                LoanModel._coinpayUserWallet.USD -= mloan.rPay;
                lenerWallet.USD += mloan.rPay;

                //operate
                LoanModel._loans.Remove(mloan);
            }
            return RedirectToAction("LiquidateUsers");
        }
    }
}