using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyWebsite.Models.Databases;
using MyWebsite.ViewModels;
using Newtonsoft.Json;
using Google.Apis.Blogger.v3.Data;

namespace MyWebsite.Controllers
{
	public class BlogController : ExtendedController
	{
		private string apiKey = Startup.BlogspotAPI;
		private const string blogId = "8566398713922921363";

		public BlogController(DatabaseContext context) : base(context) { }

		public async Task<IActionResult> Index(int pageNumber = 0)
		{
			using HttpClient client = new HttpClient();
			string query = $"https://blogger.googleapis.com/v3/blogs/{blogId}/posts?fetchBodies=false&fetchImages=true&maxResults=10&orderBy=PUBLISHED&view=READER&key={apiKey}";
			string response;
			PostList list;

			for (int k = 0; k < pageNumber; k++)
			{
				response = await client.GetStringAsync(new Uri(query)).ConfigureAwait(false);
				list = JsonConvert.DeserializeObject<PostList>(response);

				if (string.IsNullOrWhiteSpace(list.NextPageToken))
					return RedirectToAction("Index", routeValues: "pageNumber=" + k);

				if (query.IndexOf("&pageToken", StringComparison.InvariantCulture) > -1)
					query = query.Remove(query.IndexOf("&pageToken=", StringComparison.InvariantCulture));
				query += $"&pageToken={list.NextPageToken}";
			}

			response = await client.GetStringAsync(new Uri(query)).ConfigureAwait(false);
			list = JsonConvert.DeserializeObject<PostList>(response);

			BlogListViewModel viewModel = new BlogListViewModel(Database, list) { PageNumber = pageNumber };

			return View(viewModel);
		}

		public async Task<IActionResult> Tags(string id)
		{
			using HttpClient client = new HttpClient();
			string query = $"https://blogger.googleapis.com/v3/blogs/{blogId}/posts?fetchBodies=false&fetchImages=true&maxResults=500&orderBy=PUBLISHED&view=READER&key={apiKey}&labels={id}";

			string response = await client.GetStringAsync(new Uri(query)).ConfigureAwait(false);
			PostList list = JsonConvert.DeserializeObject<PostList>(response);

			BlogListViewModel viewModel = new BlogListViewModel(Database, list) { SearchTerm = "#" + id };

			return View(viewName: "~/Views/Blog/Index.cshtml", viewModel);
		}

		public async Task<IActionResult> Post(string id)
		{
			using HttpClient client = new HttpClient();
			string query = $"https://www.googleapis.com/blogger/v3/blogs/{blogId}/posts/{id}?fetchBody=true&fetchImages=true&maxComments=500&view=READER&key={apiKey}";

			string response = await client.GetStringAsync(new Uri(query)).ConfigureAwait(false);
			Post post = JsonConvert.DeserializeObject<Post>(response);

			BlogPostViewModel viewModel = new BlogPostViewModel(Database, post);

			return View(viewModel);
		}
	}
}