﻿namespace BW.Assessment.Authentication.Infrastructure.Models
{
	public class UserResponseDto
	{
		public UserResponseDto(string id, string email)
		{
			Id = id;
			Email = email;
			UserName = email;
		}

		public string Id { get; }
		public string Email { get; }
		public string UserName { get; set; }
	}
}
