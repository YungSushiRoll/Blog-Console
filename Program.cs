using NLog;
using Blogs_Console.Models;
using System;
using System.Linq;
using System.Data.Entity.Core.Metadata.Edm;
using System.Collections.Generic;

namespace Blogs_Console
{
    class MainClass
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public static void Main(string[] args)
        {
            logger.Info("Program started");
            try
            {
                String option;

                do
                {
                    Console.WriteLine("1) Display all Blogs");
                    Console.WriteLine("2) Add Blog");
                    Console.WriteLine("3) New Post");
                    Console.WriteLine("4) Show Posts");
                    Console.WriteLine("Press Enter to exit");
                    option = Console.ReadLine();
                    if (option == "1")
                    {
                        var db = new BloggingContext();
                        // Display all Blogs from the database
                        List<Blog> blogs = db.Blogs.OrderBy(b => b.Name).ToList();

                        Console.WriteLine("All blogs in the database:");
                        foreach (var item in blogs)
                        {
                            Console.WriteLine(item.Name);
                        }
                    }
                    else if (option == "2")
                    {
                        var db = new BloggingContext();
                        // Create and save a new Blog
                        Console.Write("Enter a name for a new Blog: ");
                        var name = Console.ReadLine();

                        var blog = new Blog { Name = name };

                        db.AddBlog(blog);
                        logger.Info("Blog added - {name}", name);
                    }
                    else if (option == "3")
                    {
                        var db = new BloggingContext();
                        String resp;
                        // Display all Blogs from the database
                        List<Blog> blogs = db.Blogs.OrderBy(b => b.Name).ToList();

                        Console.WriteLine("What blog would you like to post to?");
                        foreach (var item in blogs)
                        {
                            Console.WriteLine(item.BlogId + ": " + item.Name);
                        }
                        resp = Console.ReadLine();
                        bool isValid = false;
                        foreach (var id in blogs)
                        {
                            if (int.Parse(resp) == id.BlogId)
                            {
                                isValid = true;
                            }
                        }
                        if (isValid)
                        {
                            Post post = new Post();
                            post.BlogId = int.Parse(resp);
                            MakePost(post);
                            db.AddPost(post);
                            logger.Info("Post added - {title}", post.Title);
                        }
                        else
                        {
                            Console.WriteLine("Invalid Blog Id");
                        }
                        db.SaveChanges();
                    }
                } while (option.ToLower() != "");
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
            logger.Info("Program ended");
        }
        public static void MakePost(Post post)
        {
            Console.WriteLine("Post Title: ");
            post.Title = Console.ReadLine();
            Console.WriteLine("Post Text: ");
            post.Content = Console.ReadLine();
        }
    }
}