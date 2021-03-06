var ContractAddress = "0xCA6d6989519E2e0f32b6013Cff0dF2B874254353";

var abi = [{ "constant": true, "inputs": [], "name": "mainAddress", "outputs": [{ "name": "", "type": "address" }], "payable": false, "stateMutability": "view", "type": "function" }, { "constant": true, "inputs": [], "name": "totalFees", "outputs": [{ "name": "", "type": "uint256" }], "payable": false, "stateMutability": "view", "type": "function" }, { "constant": false, "inputs": [{ "name": "_paused", "type": "bool" }], "name": "setPaused", "outputs": [], "payable": false, "stateMutability": "nonpayable", "type": "function" }, { "constant": true, "inputs": [{ "name": "", "type": "address" }], "name": "userList", "outputs": [{ "name": "", "type": "uint256" }], "payable": false, "stateMutability": "view", "type": "function" }, { "constant": true, "inputs": [], "name": "allowSponsorChange", "outputs": [{ "name": "", "type": "bool" }], "payable": false, "stateMutability": "view", "type": "function" }, { "constant": true, "inputs": [], "name": "total", "outputs": [{ "name": "", "type": "uint256" }], "payable": false, "stateMutability": "view", "type": "function" }, { "constant": false, "inputs": [{ "name": "_userID", "type": "uint256" }, { "name": "_wallet", "type": "address" }, { "name": "_referrerID", "type": "uint256" }, { "name": "_introducerID", "type": "uint256" }, { "name": "_referral1", "type": "address" }, { "name": "_referral2", "type": "address" }, { "name": "_referral3", "type": "address" }, { "name": "star", "type": "uint256" }], "name": "setUserData", "outputs": [], "payable": false, "stateMutability": "nonpayable", "type": "function" }, { "constant": true, "inputs": [{ "name": "", "type": "uint256" }], "name": "users", "outputs": [{ "name": "isExist", "type": "bool" }, { "name": "wallet", "type": "address" }, { "name": "referrerID", "type": "uint256" }, { "name": "introducerID", "type": "uint256" }], "payable": false, "stateMutability": "view", "type": "function" }, { "constant": true, "inputs": [{ "name": "", "type": "uint256" }], "name": "STAR_FEE", "outputs": [{ "name": "", "type": "uint256" }], "payable": false, "stateMutability": "view", "type": "function" }, { "constant": true, "inputs": [], "name": "paused", "outputs": [{ "name": "", "type": "bool" }], "payable": false, "stateMutability": "view", "type": "function" }, { "constant": false, "inputs": [{ "name": "_star", "type": "uint256" }, { "name": "_price", "type": "uint256" }], "name": "setStarFee", "outputs": [], "payable": false, "stateMutability": "nonpayable", "type": "function" }, { "constant": true, "inputs": [{ "name": "_user", "type": "uint256" }, { "name": "_star", "type": "uint256" }], "name": "viewUserStarActive", "outputs": [{ "name": "", "type": "bool" }], "payable": false, "stateMutability": "view", "type": "function" }, { "constant": true, "inputs": [{ "name": "", "type": "uint256" }], "name": "STAR_PRICE", "outputs": [{ "name": "", "type": "uint256" }], "payable": false, "stateMutability": "view", "type": "function" }, { "constant": true, "inputs": [], "name": "currentUserID", "outputs": [{ "name": "", "type": "uint256" }], "payable": false, "stateMutability": "view", "type": "function" }, { "constant": true, "inputs": [], "name": "tempReferrerID", "outputs": [{ "name": "", "type": "uint256" }], "payable": false, "stateMutability": "view", "type": "function" }, { "constant": false, "inputs": [{ "name": "_id", "type": "uint256" }], "name": "changeReferrerID", "outputs": [], "payable": false, "stateMutability": "nonpayable", "type": "function" }, { "constant": false, "inputs": [{ "name": "_allowSponsorChange", "type": "bool" }], "name": "setAllowSponsorChange", "outputs": [], "payable": false, "stateMutability": "nonpayable", "type": "function" }, { "constant": true, "inputs": [], "name": "deployer", "outputs": [{ "name": "", "type": "address" }], "payable": false, "stateMutability": "view", "type": "function" }, { "constant": false, "inputs": [{ "name": "_currentUserID", "type": "uint256" }], "name": "setCurrentUserID", "outputs": [], "payable": false, "stateMutability": "nonpayable", "type": "function" }, { "constant": false, "inputs": [{ "name": "_mainAddress", "type": "address" }], "name": "setMainAddress", "outputs": [], "payable": false, "stateMutability": "nonpayable", "type": "function" }, { "constant": true, "inputs": [{ "name": "_user", "type": "uint256" }], "name": "viewUserReferrals", "outputs": [{ "name": "", "type": "address[]" }], "payable": false, "stateMutability": "view", "type": "function" }, { "constant": false, "inputs": [{ "name": "_star", "type": "uint256" }, { "name": "_price", "type": "uint256" }], "name": "setStarPrice", "outputs": [], "payable": false, "stateMutability": "nonpayable", "type": "function" }, { "constant": true, "inputs": [{ "name": "_user", "type": "uint256" }], "name": "findFreeReferrer", "outputs": [{ "name": "", "type": "address" }], "payable": false, "stateMutability": "view", "type": "function" }, { "constant": false, "inputs": [{ "name": "_owner", "type": "address" }], "name": "transferOwnership", "outputs": [], "payable": false, "stateMutability": "nonpayable", "type": "function" }, { "inputs": [], "payable": false, "stateMutability": "nonpayable", "type": "constructor" }, { "payable": true, "stateMutability": "payable", "type": "fallback" }, { "anonymous": false, "inputs": [{ "indexed": true, "name": "_user", "type": "uint256" }, { "indexed": true, "name": "_referrer", "type": "uint256" }, { "indexed": true, "name": "_introducer", "type": "uint256" }, { "indexed": false, "name": "_time", "type": "uint256" }], "name": "Register", "type": "event" }, { "anonymous": false, "inputs": [{ "indexed": true, "name": "_user", "type": "uint256" }, { "indexed": true, "name": "_referrer", "type": "uint256" }, { "indexed": true, "name": "_introducer", "type": "uint256" }, { "indexed": false, "name": "_time", "type": "uint256" }], "name": "SponsorChange", "type": "event" }, { "anonymous": false, "inputs": [{ "indexed": true, "name": "_user", "type": "uint256" }, { "indexed": false, "name": "_star", "type": "uint256" }, { "indexed": false, "name": "_price", "type": "uint256" }, { "indexed": false, "name": "_time", "type": "uint256" }], "name": "Upgrade", "type": "event" }, { "anonymous": false, "inputs": [{ "indexed": true, "name": "_user", "type": "uint256" }, { "indexed": true, "name": "_referrer", "type": "uint256" }, { "indexed": true, "name": "_introducer", "type": "uint256" }, { "indexed": false, "name": "_star", "type": "uint256" }, { "indexed": false, "name": "_money", "type": "uint256" }, { "indexed": false, "name": "_fee", "type": "uint256" }, { "indexed": false, "name": "_time", "type": "uint256" }], "name": "Payment", "type": "event" }, { "anonymous": false, "inputs": [{ "indexed": true, "name": "_referrer", "type": "uint256" }, { "indexed": true, "name": "_referral", "type": "uint256" }, { "indexed": false, "name": "_star", "type": "uint256" }, { "indexed": false, "name": "_money", "type": "uint256" }, { "indexed": false, "name": "_time", "type": "uint256" }], "name": "LostMoney", "type": "event" }, { "anonymous": false, "inputs": [{ "indexed": true, "name": "_referrer", "type": "uint256" }, { "indexed": true, "name": "_referral", "type": "uint256" }, { "indexed": false, "name": "_star", "type": "uint256" }, { "indexed": false, "name": "_money", "type": "uint256" }, { "indexed": false, "name": "_time", "type": "uint256" }], "name": "IntroducerLostMoney", "type": "event" }];

var provider = ethers.getDefaultProvider();

var contract = new ethers.Contract(ContractAddress, abi, provider);

var etherscanTx = 'https://etherscan.io/tx/';


async function getStarPrice(lvl) {
	var price = await contract.STAR_PRICE(lvl);
	let wei = ethers.utils.bigNumberify(price.toString());

	return ethers.utils.formatEther(wei);
}
async function getIdbyAddress(addr) {
	var id = await contract.userList(addr);
	return id.toString();
}

async function getAddressById(id) {
	var user = await contract.users(id);
	return user.wallet;
}

async function starLevel(id) {
	var star = 2;
	for (var i = 2; i <= 6; i++) {
		var bol = await contract.viewUserStarActive(id, i);
		//console.log(bol);
		if (bol == false) {

			break;
		}
		star++;
	}

	return star;
}

async function idsByReferrals(addresses) {

	var ids = [];
	for (var i = 0; i < addresses.length; i++) {
		ids.push(await getIdbyAddress(addresses[i]));
	}
	//console.log(ids);
	return ids;
}


