﻿using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using RestEase;

namespace TeamScreen.Plugin.TeamCity.Integration
{
    public interface ITeamCityService
    {
        Task<BuildJob[]> GetBuilds(string path, string username, string password);
    }

    public class TeamCityService : ITeamCityService
    {
        public async Task<BuildJob[]> GetBuilds(string path, string username, string password)
        {
            var api = RestClient.For<ITeamCityClient>(path);
            var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));
            api.Authorization = new AuthenticationHeaderValue("Basic", credentials);

            var projects = await api.GetProjectsAsync();
            return projects.Projects
                .Where(x => x.Id != Const.RootProject)
                .SelectMany(x => api.GetLastBuildForProjectAsync(x.Id).Result.BuildJobs)
                .ToArray();
        }
    }
}