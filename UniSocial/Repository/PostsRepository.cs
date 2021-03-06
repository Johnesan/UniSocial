﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UniSocial.Data;
using UniSocial.Models;

namespace UniSocial.Repository
{
    public class PostsRepository
    {
        private PostContext db = new PostContext();
        private static List<BlogPost> blogPosts = new List<BlogPost>();
        public PostsRepository()
        {

            InitAsync();
        }

        public async void InitAsync()
        {
            var blogWebsites = db.BlogWebsites.Include(b => b.School).ToList();

            foreach (BlogWebsite blogWebsite in blogWebsites)
            {
                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(blogWebsite.Url);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await httpClient.GetAsync("/wp-json/wp/v2/posts?_embed");
                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content;
                    var json = await content.ReadAsStringAsync();
                    var postsJson = JsonConvert.DeserializeObject<dynamic>(json);
                    JArray jsonArray = JArray.Parse(postsJson.ToString());

                    foreach (var item in jsonArray)
                    {
                        try
                        {
                            var blogPost = new BlogPost();
                            blogPost.Id = Guid.NewGuid();
                            blogPost.BlogWebsiteId = blogWebsite.Id;
                            blogPost.Title = item.Value<JObject>("title").Value<string>("rendered");
                            blogPost.Link = item.Value<string>("link");
                            blogPost.Excerpt = item.Value<JObject>("excerpt").Value<string>("rendered");
                            blogPost.Date = item.Value<DateTime>("date");
                            blogPost.FeaturedImage =
                                item?.Value<JObject>("_embedded")?.Value<JArray>("wp:featuredmedia")[0]
                                    ?.Value<string>("source_url") ?? " ";
                            if (blogPosts.Find(s => s.Title == blogPost.Title) == null)
                            {
                                blogPosts.Add(blogPost);
                            }

                        }
                        catch (Exception e)
                        {
                            continue;
                        }
                    }
                }
                httpClient.Dispose();
            }
            var OrderedBlogPosts = blogPosts.OrderByDescending(s => s.Date).ToList();
            foreach (var blogPostX in db.BlogPosts)
            {
                db.BlogPosts.Remove(blogPostX);
            }
            db.BlogPosts.AddRange(OrderedBlogPosts);
            db.SaveChanges();
            var FinalBlogPosts = db.BlogPosts.ToList().OrderByDescending(s => s.Date);
        }
    }
}