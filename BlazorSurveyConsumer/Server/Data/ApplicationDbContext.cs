using BlazorSurvey.Shared;
using BlazorSurveyConsumer.Server.Models;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorSurveyConsumer.Server.Data
{
	public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
	{
		public ApplicationDbContext(
			DbContextOptions options,
			IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
		{
		}
		public DbSet<Survey> Surveys { get; set; }
		public DbSet<SurveyAnswer> SurveyAnswers { get; set; }
		public DbSet<SurveyItem> SurveyItems { get; set; }
		public DbSet<SurveyItemOption> SurveyItemOptions { get; set; } /*  */

	}
}
