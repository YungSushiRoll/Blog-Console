﻿using System;
using System.Data.Entity;

namespace Blogs_Console.Models
{
    public class BloggingContext : DbContext
    {
        public BloggingContext() : base("name=BlogContext") { }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        public void AddBlog(Blog blog)
        {
            this.Blogs.Add(blog);
            this.SaveChanges();
        }

        public void AddPost(Post post)
        {
            this.Posts.Add(post);
            this.SaveChanges();
        }

        public void DeletePost(Post post)
        {
            this.Posts.Remove(post);
            this.SaveChanges();
        }
    }
}