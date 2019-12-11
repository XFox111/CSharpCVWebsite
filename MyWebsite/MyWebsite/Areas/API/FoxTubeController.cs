using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MyWebsite.Areas.API.Models;

namespace MyWebsite.Areas.API
{
    [Route("API/[controller]")]
    [Area("API")]
    [ApiController]
    public class FoxTubeController : ControllerBase
    {
        FoxTubeDatabaseContext db;
        public FoxTubeController(FoxTubeDatabaseContext context) =>
            db = context;

        [HttpPost("AddMetrics")]
        public string AddMetrics()
        {
            MetricsPackage metrics = new MetricsPackage
            {
                Title = Request.Form["Title"],
                Content = Request.Form["Content"],
                Version = Request.Form["Version"],
                TimeStamp = DateTime.Now
            };

            db.Metrics.Add(metrics);
            db.SaveChanges();

            return db.Metrics.FirstOrDefault(i => i.TimeStamp == metrics.TimeStamp)?.Id.ToString();
        }

        [HttpGet("Messages")]
        public object Messages(bool toast = false, DateTime? publishedAfter = null, string lang = "en-US")
        {
            if(toast)
            {
                Message message = publishedAfter.HasValue ? 
                    db.Messages.FirstOrDefault(i => i.PublishedAt >= publishedAfter && i.PublishedAt <= DateTime.Now && i.Language == lang) : 
                    db.Messages.OrderByDescending(i => i.PublishedAt).FirstOrDefault();

                if (message == null)
                    return NoContent();

                return $@"<toast activationType='foreground' launch='inbox|{message.Id}'>
                            <visual>
                                <binding template='ToastGeneric'>
                                    <image placement='hero' src='{message.HeroImage}'/>
                                    <image placement='appLogoOverride' hint-crop='circle' src='{message.Icon}'/>
                                    <text>{message.Title}</text>
                                    <text hint-maxLines='5'>{message.Description}</text>
                                </binding>
                            </visual>
                        </toast>";
            }
            else
            {
                List<Message> messages = db.Messages.Where(i => i.PublishedAt <= DateTime.Now).ToList();

                return messages;
            }
        }

        [HttpGet("Changelogs")]
        public object Changelogs(bool toast = false, string version = null, string lang = "en-US")
        {
            if (string.IsNullOrWhiteSpace(version))
                throw new ArgumentNullException("You must specify required version number");

            if(toast)
            {
                Changelog changelog = db.Changelogs.FirstOrDefault(i => i.Version == version && i.Language == lang);
                if (changelog == null)
                    return NoContent();

                return $@"<toast activationType='foreground' launch='changelog|{changelog.Id}'>
                            <visual>
                                <binding template='ToastGeneric'>
                                    <image placement='hero' src='{changelog.HeroImage}'/>
                                    <image placement='appLogoOverride' hint-crop='circle' src='{changelog.Icon}'/>
                                    <text>{changelog.Description}</text>
                                    <text>{changelog.Title}</text>
                                </binding>
                            </visual>
                        </toast>";
            }
            else
            {
                List<Changelog> changelogs = db.Changelogs.Where(i => !IsVersionGreater(i.Version, version)).ToList();

                return changelogs;
            }
        }

        private static bool IsVersionGreater(string versionOne, string versionTwo)
        {
            string[] v1 = versionOne.Split('.');
            string[] v2 = versionTwo.Split('.');

            for (int i = 0; i < v1.Length && i < v2.Length; i++)
                if (byte.Parse(v1[i]) > byte.Parse(v2[i]))
                    return true;

            return false;
        }
    }
}