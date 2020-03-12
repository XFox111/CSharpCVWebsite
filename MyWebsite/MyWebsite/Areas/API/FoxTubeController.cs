using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MyWebsite.Areas.API.Models;
using System.Globalization;
using MyWebsite.Models.Databases;

namespace MyWebsite.Areas.API
{
	[ApiController]
	[Route("API/[controller]")]
	public class FoxTubeController : ControllerBase
	{
		readonly FoxTubeDatabaseContext db;
		public FoxTubeController(FoxTubeDatabaseContext context) =>
			db = context;

		[HttpPost]
		[Route("AddMetrics")]
		public IActionResult AddMetrics(MetricsPackage package)
		{
			Guid id = db.Metrics.Add(package).Entity.Id;
			db.SaveChanges();

			return Accepted(value: id.ToString());
		}

		[HttpGet]
		[Route("Messages")]
		public IActionResult Messages(bool toast = false, DateTime? publishedAfter = null, string lang = "en-US")
		{
			if (toast)
			{
				Message message = publishedAfter.HasValue ?
					db.Messages.FirstOrDefault(i => i.PublishedAt >= publishedAfter && i.PublishedAt <= DateTime.Now && i.Language == lang) :
					db.Messages.OrderByDescending(i => i.PublishedAt).FirstOrDefault();

				if (message == null)
					return NoContent();

				return Ok($@"<toast activationType='foreground' launch='inbox|{message.Id}'>
                            <visual>
                                <binding template='ToastGeneric'>
                                    <image placement='hero' src='{message.HeroImage}'/>
                                    <image placement='appLogoOverride' hint-crop='circle' src='{message.Icon}'/>
                                    <text>{message.Title}</text>
                                    <text hint-maxLines='5'>{message.Description}</text>
                                </binding>
                            </visual>
                        </toast>");
			}
			else
				return Ok(db.Messages.Where(i => i.PublishedAt <= DateTime.Now).ToList());
		}

		[HttpGet]
		[Route("Changelogs")]
		public IActionResult Changelogs(string version, bool toast = false, string lang = "en-US")
		{
			if (toast)
			{
				Changelog changelog = db.Changelogs.FirstOrDefault(i => i.Version == version && i.Language == lang);
				if (changelog == null)
					return NoContent();

				return Ok($@"<toast activationType='foreground' launch='changelog|{changelog.Id}'>
                            <visual>
                                <binding template='ToastGeneric'>
                                    <image placement='hero' src='{changelog.HeroImage}'/>
                                    <image placement='appLogoOverride' hint-crop='circle' src='{changelog.Icon}'/>
                                    <text>{changelog.Description}</text>
                                    <text>{changelog.Title}</text>
                                </binding>
                            </visual>
                        </toast>");
			}
			else
				return Ok(db.Changelogs.Where(i => !IsVersionGreater(i.Version, version)).ToList());
		}

		private static bool IsVersionGreater(string versionOne, string versionTwo)
		{
			versionOne = versionOne.Replace(".", "", StringComparison.InvariantCulture);
			versionTwo = versionTwo.Replace(".", "", StringComparison.InvariantCulture);

			return short.Parse(versionOne, NumberStyles.Integer, CultureInfo.InvariantCulture) > short.Parse(versionTwo, NumberStyles.Integer, CultureInfo.InvariantCulture);
		}
	}
}