using AutoMapper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BW.Assessment.Core.Models;
using BW.Assessment.Core.Services;
using Microsoft.AspNetCore.Authorization;
using BW.Assessment.Shop.Api.Contract.v1.Request;
using BW.Assessment.Shop.Api.Contract.v1.Response;

namespace BW.Wallet.Wallet.Api.Contract.v1.Controllers
{
	[ApiVersion("1")]
	[Route("api/v{version:apiVersion}/[controller]")]
	[ApiController]
	[Authorize]
	public class ShopController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly IShopService _shopService;

		public ShopController(IShopService shopService, IMapper mapper)
		{
			_mapper = mapper;
			_shopService = shopService;
		}

		[HttpPost]
		[Route("Add")]
		[ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
		[ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
		[ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
		public async Task<IActionResult> AddStock([FromBody] AddStockRequest request)
		{
			var requestDto = _mapper.Map<StockDto>(request);
			var success = await _shopService.UpdateStock(requestDto);
			if (success)
			{
				return StatusCode(201, "Stock added successfully!");
			}
			return BadRequest("Failed to add stock");
		}

		[HttpGet]
		[Route("GetStock/{productId}")]
		[ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
		[ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
		[ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
		public async Task<IActionResult> GetStock(int productId)
		{
			var result  = await _shopService.GetStock(productId);
			if (result != null)
			{
				var stock = _mapper.Map<StockDto, StockResponse>(result);
				return Ok(stock);
			}
			return BadRequest("No stock found for this product");
		}
	}
}
