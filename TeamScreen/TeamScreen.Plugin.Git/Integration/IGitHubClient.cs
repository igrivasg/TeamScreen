﻿using System.Net.Http.Headers;
using System.Threading.Tasks;
using RestEase;

namespace TeamScreen.Plugin.Git.Integration
{
    [Header("User-Agent", "TeamScreen")]//GitHub requires user-agent
    public interface IGitHubClient
    {
        [Header("Authorization")]
        AuthenticationHeaderValue Authorization { get; set; }

        [Get("/repos/{owner}/{repo}/commits")]
        Task<GetCommitsResponse[]> GetCommitsAsync([Path]string owner, [Path]string repo);
    }
}