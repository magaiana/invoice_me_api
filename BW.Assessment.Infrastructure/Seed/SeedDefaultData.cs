﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using BW.Assessment.Infrastructure.Models;

namespace BW.Assessment.Infrastructure.Seed
{
	public class SeedDefaultData
	{
		public static async Task SeedDataAsync(UserManager<UserProfile> userManager)
		{
            var adminUserName = "admin@mail.com";
            var defaultPassword = "P@ssword01";
            var adminUser = new UserProfile
            {
                UserName = adminUserName,
                Email = adminUserName,
                EmailConfirmed = true,                
            };
            await userManager.CreateAsync(adminUser, defaultPassword);            
        }
	}
}
