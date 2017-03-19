﻿using System;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using RestEase;

namespace TeamScreen.JIRA
{
    public class JiraService
    {
        public async Task<GetIssuesForSprintResponse> GetIssues(string path, string username, string password, int boardId, int sprintId)
        {
            var api = RestClient.For<IJiraClient>(path);
            var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));
            api.Authorization = new AuthenticationHeaderValue("Basic", credentials);

            return await api.GetIssuesForSprint(boardId, sprintId);
        }
    }
}