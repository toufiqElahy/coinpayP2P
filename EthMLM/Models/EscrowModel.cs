using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EthMLM.Models
{
	public class Trade
	{
		public Guid Id { get; set; } = Guid.NewGuid();
		public Guid OfferId { get; set; }//offer class
	
		public double usd { get; set; }
		public double amnt { get; set; }//lockedAmount
										//public double lockedAmount { get; set; }

		public string Status { get; set; } = TradeStatus.Active; //Active/Completed/Cancelled/Expired

		public string Email = "";//UserIdentity (trade creator)
		public string OfferEmail = "";//UserIdentity (offer creator)
		public DateTime CreationTime = DateTime.UtcNow;

		public string Message { get; set; } = "";
		//public int ExpireInMinutes { get; set; }

		public Offer offer { get; set; }
	}
	public static class TradeStatus
	{
		public static string Active = "Active";
		public static string Completed = "Completed";
		public static string Cancelled = "Cancelled";
		public static string Expired = "Expired";
	}
	public class Offer
	{
		public Guid Id { get; set; } = Guid.NewGuid();
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

		public int ExpireInMinutes { get; set; } = 30;//by default 30 minutes

		public bool IsOpen { get; set; } = true; //Open/Pause
		//public string TradeStatus { get; set; } = ""; //Active/Completed/Cancelled/Expired

		public string Email = "";//UserIdentity (offer creator)
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

	public static class EscrowModel
	{
		public static List<Offer> _offers = new List<Offer>();
		public static List<Trade> _trades = new List<Trade>();
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


	public class ChatHub : Hub
	{
		public async Task SendMessage(string tradeId,string user, string message)
		{
			await Clients.All.SendAsync(tradeId, user, message);
		}
	}

}
