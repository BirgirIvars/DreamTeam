﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Hooli.Models;


// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Hooli.Controllers
{
    public class GroupController : Controller
    {
        [FromServices]
        public HooliContext DbContext { get; set; }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreatGroup(Group group)
        {
            var groupData = new Group
            {
                GroupName = group.GroupName,
                Description = group.Description,
                Private = group.Private, 
                DateCreated = group.DateCreated,
                Members = group.Members
            };
            DbContext.Group.Add(groupData);
            DbContext.SaveChanges();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditGroup(Group group)
        {
            var groupData = DbContext.Group.Single(groupTable => groupTable.GroupId == group.GroupId);

            groupData.GroupName = group.GroupName;
            groupData.Description = group.Description;
            groupData.Private = group.Private;
            groupData.DateCreated = group.DateCreated;
            groupData.Members = group.Members;
            groupData.GroupPicture = group.GroupPicture;

            DbContext.SaveChanges();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddPostToGroup(int groupId, Post post)
        {
            var group = DbContext.Group.Single(groupTable => groupTable.GroupId == groupId);
            group.Posts.Add(post);

            DbContext.SaveChanges();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult BanUser(int groupId, ApplicationUser user)
        {
            var group = DbContext.Group.Single(groupTable => groupTable.GroupId == groupId);
            group.BannedUsers.Add(user);

            DbContext.SaveChanges();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UnBanUser(int groupId, ApplicationUser user)
        {
            var group = DbContext.Group.Single(groupTable => groupTable.GroupId == groupId);
            group.BannedUsers.Remove(user);

            DbContext.SaveChanges();
            return View();
        }


    }
}