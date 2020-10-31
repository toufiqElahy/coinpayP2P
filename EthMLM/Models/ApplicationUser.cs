﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EthMLM.Models
{
	public class ApplicationUser : IdentityUser
	{
		public string ChatId { get; set; }
		public string Passwrd { get; set; }
		public string RefId { get; set; }
		public string EthAddress { get; set; }
		//public Wallet Wallet { get; set; }
	}
}
