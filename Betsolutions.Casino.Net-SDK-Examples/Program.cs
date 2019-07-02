using System;
using Betsolutions.Casino.SDK;
using Betsolutions.Casino.SDK.DTO.Rake;
using Betsolutions.Casino.SDK.Exceptions;
using Betsolutions.Casino.SDK.Services;
using Betsolutions.Casino.SDK.Wallet.DTO;
using Betsolutions.Casino.SDK.Wallet.Services;
using Newtonsoft.Json;

namespace Betsolutions.Casino.Net_SDK_Examples
{
    internal static class Program
    {
        private const int MerchantId = 1;
        private const string PrivateKey = "PrivateKey";
        private const string ApiUrl = "Api url";

        private static MerchantAuthInfo _merchantAuthInfo;

        private static void Main()
        {
            _merchantAuthInfo = new MerchantAuthInfo(MerchantId, PrivateKey, ApiUrl);

            GetGamesExample();
            GetRakeExample();
            GetRakeDetailedExample();
            DepositExample();
            WithdrawExample();
            GetBalanceExample();

            Console.WriteLine("done");
            Console.ReadLine();
        }
        private static void GetGamesExample()
        {
            var gameService = new GameService(_merchantAuthInfo);

            try
            {
                var response = gameService.GetGames();

                if (response.StatusCode == StatusCodes.Success)
                {
                    var products = response.Data.Products;

                    foreach (var product in products)
                    {
                        Console.WriteLine($"{nameof(product.ProductId)}: {product.ProductId}");

                        var games = product.Games;

                        foreach (var game in games)
                        {
                            Console.WriteLine($"{nameof(game)}: {game.Name}");
                        }
                    }
                }
            }
            catch (CantConnectToServerException ex)
            {
                Console.WriteLine(ex);
            }
        }
        private static void GetRakeExample()
        {
            var rakeService = new RakeService(_merchantAuthInfo);

            try
            {
                var response = rakeService.GetRake(new GetRakeRequest());

                if (response.StatusCode == StatusCodes.Success)
                {
                    var rakeData = response.Data.RakeData;

                    foreach (var i in rakeData)
                    {
                        Console.WriteLine(JsonConvert.SerializeObject(i));
                    }
                }
            }
            catch (CantConnectToServerException ex)
            {
                Console.WriteLine(ex);
            }
        }
        private static void GetRakeDetailedExample()
        {
            var rakeService = new RakeService(_merchantAuthInfo);

            try
            {
                var response = rakeService.GetRakeDetailed(new GetRakeDetailedRequest());

                if (response.StatusCode == StatusCodes.Success)
                {
                    var rakeData = response.Data.RakeData;

                    foreach (var i in rakeData)
                    {
                        Console.WriteLine(JsonConvert.SerializeObject(i));
                    }
                }
            }
            catch (CantConnectToServerException ex)
            {
                Console.WriteLine(ex);
            }
        }
        private static void DepositExample()
        {
            var walletService = new WalletService(_merchantAuthInfo);

            try
            {
                var response = walletService.Deposit(new DepositRequest
                {
                    Amount = 100,
                    TransactionId = Guid.NewGuid().ToString(),
                    Currency = "TRY",
                    UserId = "111450",
                    Token = "private token"
                });

                if (response.StatusCode == StatusCodes.Success)
                {
                    var result = response.Data;

                    Console.WriteLine($"{nameof(result.TransactionId)}: {result.TransactionId}, {nameof(result.Balance)}: {result.Balance}");
                }
            }
            catch (CantConnectToServerException ex)
            {
                Console.WriteLine(ex);
            }
        }
        private static void WithdrawExample()
        {
            var walletService = new WalletService(_merchantAuthInfo);

            try
            {
                var response = walletService.Withdraw(new WithdrawRequest
                {
                    Amount = 100,
                    TransactionId = Guid.NewGuid().ToString(),
                    Currency = "TRY",
                    UserId = "111450",
                    Token = "private token"
                });

                if (response.StatusCode == StatusCodes.Success)
                {
                    var result = response.Data;

                    Console.WriteLine($"{nameof(result.TransactionId)}: {result.TransactionId}, {nameof(result.Balance)}: {result.Balance}");
                }
            }
            catch (CantConnectToServerException ex)
            {
                Console.WriteLine(ex);
            }
        }
        private static void GetBalanceExample()
        {
            var walletService = new WalletService(_merchantAuthInfo);

            try
            {
                var response = walletService.GetBalance(new GetBalanceRequest
                {
                    Currency = "TRY",
                    UserId = "111450",
                    Token = "private token"
                });

                if (response.StatusCode == StatusCodes.Success)
                {
                    var result = response.Data;

                    Console.WriteLine($"{nameof(result.Balance)}: {result.Balance}");
                }
            }
            catch (CantConnectToServerException ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
