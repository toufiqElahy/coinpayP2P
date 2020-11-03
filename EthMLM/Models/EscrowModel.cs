using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EthMLM.Models
{
	public class Offer
	{
		public string Type { get; set; }//Buying/Selling
		public string Coin { get; set; } //BTC/ETH/USD
		public string Location { get; set; } = "";
		public string LocalCurrency { get; set; } = "";//BDT
		public string PaymentMethod { get; set; } //Skrill/Payoneer/Paypal ...
		public double SetPricePercentage { get; set; } //2% above/ -2% (2% below)
		public double MinTradeLimit { get; set; } = 100;//100-1000 BDT
		public double MaxTradeLimit { get; set; } = 5000;//100-1000 BDT
		public string TradeCondition { get; set; } = "";
		public string AvailableToTrade { get; set; } = "";

		public bool IsOpen { get; set; } = true; //Open/Pause
		public string TradeStatus { get; set; } = ""; //Active/Completed/Cancelled/Expired

		public string Email = "";//UserIdentity
		public DateTime CreationTime = DateTime.UtcNow;
	}
	public static class Coin
	{
		public static string BTC = "BTC";
		public static string ETH= "ETH";
		public static string USD = "USD";
	}
	public static class PaymentMethod
	{
		public static string Skrill = "Skrill";
		public static string Payoneer = "Payoneer";
		public static string Paypal = "Paypal";
	}
	public static class OfferType
	{
		public static string Buying = "Buying";
		public static string Selling = "Selling";
	}
	public static class LocalCurrency
	{
		public static string BDT = "BDT";
		public static string USD = "USD";
	}

	public static class OfferModel
	{
		public static List<Offer> _offers = new List<Offer>();
		
	}

	public class UserWallet
	{
		public string Email { get; set; }
		public double BTC { get; set; } = 1;
		public double ETH { get; set; } = 1;
		public double USD { get; set; } = 1000;
	}
	public static class UserWalletModel
	{
		public static List<UserWallet> _userWallet = new List<UserWallet>();
	}
	
}
