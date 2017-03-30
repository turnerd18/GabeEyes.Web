using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net;
using GabeEyes.Web.Models;
using GabeEyes.Web.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;

namespace GabeEyes.Web.Controllers
{
    [Route("api/[controller]")]
    public class QuoteController : Controller
    {
		private readonly GabeEyesContext _context;

		public QuoteController(GabeEyesContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<IEnumerable<Quote>> Get()
		{
			return await _context.Quotes.AsNoTracking()
				.OrderByDescending(q => q.UpVotes - q.DownVotes)
				.ToListAsync();
		}

        [HttpPost]
        public async Task<Quote> Post([FromBody]Quote quote)
        {
			var totalQuotes = await _context.Quotes.CountAsync();
			if (totalQuotes >= 1000)
			{
				throw new Exception("Gabe has imparted too much knowledge! Tell him to give it a rest.");
			}

			if (quote.Id == default(int))
			{
				_context.Quotes.Add(quote);
			}
			else
			{
				_context.Quotes.Update(quote);
			}
			await _context.SaveChangesAsync();

			return quote;
        }

        [HttpGet("upvote/{id}")]
        public async Task<Quote> UpVote(int id)
        {
			var quote = await _context.Quotes.SingleOrDefaultAsync(q => q.Id == id);
			quote.UpVotes++;
			await _context.SaveChangesAsync();

			return quote;
		}

		[HttpGet("downvote/{id}")]
		public async Task<Quote> DownVote(int id)
		{
			var quote = await _context.Quotes.SingleOrDefaultAsync(q => q.Id == id);
			quote.DownVotes++;
			await _context.SaveChangesAsync();

			return quote;
		}

		public HttpResponseMessage Options()
		{
			var responseMessage = new HttpResponseMessage { StatusCode = HttpStatusCode.OK };
			responseMessage.Headers.Add("Access-Control-Allow-Origin", "http://localhost:3000");
			return responseMessage;
		}
	}
}
