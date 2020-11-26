using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorSurvey.Shared;
using BlazorSurveyConsumer.Server.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BlazorSurveyConsumer.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SurveysController : ControllerBase
	{
		private readonly ApplicationDbContext _context;

		public SurveysController(ApplicationDbContext context)
		{
			_context = context;
		}
		#region Surveys
		#region Get
		// GET: api/surveys
		[HttpGet()]
		public async Task<ActionResult<List<Survey>>> GetSurveys()
		{
			return await _context.Surveys.ToListAsync();
		}
		// GET: api/surveys/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Survey>> GetSurveys(int id)
		{
			Survey survey = await _context.Surveys.FindAsync(id);
			if (survey == null)
			{
				return NotFound();
			}

			return survey;
		}
		#endregion

		#region Put
		// PUT: api/Users/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutSurvey(int id, Survey survey)
		{
			if (id != survey.Id)
			{
				return BadRequest();
			}
			_context.Entry(survey).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!SurveyExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return NoContent();
		}
		#endregion

		#region Post
		// POST: api/Users
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<Survey>> PostSurvey(Survey survey)
		{
			survey.UserId = GetUserId();
			if (SurveyExists(survey.Id))
			{
				await PutSurvey(survey.Id, survey);
				return Ok(survey);
			}
			else
			{
				_context.Surveys.Add(survey);
				try
				{
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateException)
				{
					if (SurveyExists(survey.Id))
					{
						// This shouldn't really be hit, given the previous check.
						return Conflict();
					}
					else
					{
						throw;
					}
				}
				return CreatedAtAction("GetSurvey", new { id = survey.Id }, survey);
			}
		}
		#endregion

		#region Delete
		// DELETE: api/Surveys/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteSurvey(int id)
		{
			var survey = await _context.Surveys.FindAsync(id);
			if (survey == null)
			{
				return NotFound();
			}

			_context.Surveys.Remove(survey);
			await _context.SaveChangesAsync();

			return NoContent();
		}
		#endregion

		#region Helpers
		private bool SurveyExists(int id)
		{
			return _context.Surveys.Any(e => e.Id == id);
		}

		private string GetUserId()
		{
			foreach (Claim claim in HttpContext.User.Claims)
			{
				Console.WriteLine(claim.Type + ":\t\t-\t" + claim.Value);
			}
			return HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
		}
		#endregion
		#endregion

		#region Survey Answers
		#endregion

		#region SurveyItems
		#endregion
	}
}
