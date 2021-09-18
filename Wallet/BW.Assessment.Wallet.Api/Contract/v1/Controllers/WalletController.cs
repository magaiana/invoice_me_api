using AutoMapper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BW.Assessment.Wallet.Core.Models;
using Microsoft.AspNetCore.Authorization;
using BW.Assessment.Wallet.Core.Services;
using BW.Wallet.Authentication.Core.Models;
using BW.Wallet.Wallet.Api.Contract.v1.Request;
using BW.Wallet.Wallet.Api.Contract.v1.Response;

namespace BW.Wallet.Wallet.Api.Contract.v1.Controllers
{
	[ApiVersion("1")]
	[Route("api/v{version:apiVersion}/[controller]")]
	[ApiController]
	[Authorize]
	public class WalletController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly IWalletService _walletService;

		public WalletController(IWalletService walletService, IMapper mapper)
		{
			_mapper = mapper;
			_walletService = walletService;
		}

		[HttpPost]
		[Route("Create/{userId}")]
		public async Task<IActionResult> CreateWallet([FromBody] CreateWalletRequest request)
		{
			var requestDto = _mapper.Map<WalletDetailsDto>(request);
			var success = await _walletService.CreateWalletAsync(requestDto);
			if (success)
			{
				return StatusCode(201, "Wallet created successfully!");
			}

			return BadRequest("Wallet creation failed");
		}

		[HttpPut]
		[Route("Deposit")]
		public async Task<IActionResult> DepositIntoWallet([FromBody] DepositRequest request)
		{
			var success = await _walletService.DepositIntoWalletAsync(_mapper.Map<DepositRequest, DepositRequestDto>(request));
			if (success)
			{
				return StatusCode(201, "Deposit was successful");
			}

			return BadRequest("Deposit failed");
		}

		[HttpGet]
		[Route("Balance/{userId}")]
		public async Task<ActionResult<WalletBalanceResponse>> QueryBalance(string userId)
		{			
			var wallet = await _walletService.GetWalletBalanceForUserAsync(userId);
			if (wallet != null)
			{
				return Ok(_mapper.Map<WalletDetailsDto, WalletBalanceResponse>(wallet));
			}

			return BadRequest("Not balance information found for this user.");
		}

		[HttpGet]
		[Route("Withdraw/{userId}")]
		public async Task<ActionResult<WalletBalanceResponse>> RequestWithdrawal([FromBody] WithdrawalRequest request)
		{
			var success = await _walletService.RequestWithdrawal(_mapper.Map<WithdrawalRequest, WithdrawalRequestDto>(request));
			if (success)
			{
				return Ok($"You withdrawal of R{request.Amount:C} has been processed.");
			}

			return BadRequest("Your withdrawal cannot be processed at this time.");
		}
	}
}
