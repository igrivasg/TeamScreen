﻿using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TeamScreen.Data.Context;
using TeamScreen.Data.Entities;

namespace TeamScreen.Data.Services
{
    public interface ISettingsService
    {
        Task<T> Get<T>(string plugin) where T : ISettings<T>, new();
        Task Set<T>(string plugin, T value) where T : ISettings<T>, new();
    }

    public class SettingsService : ISettingsService
    {
        private readonly AppDbContext _db;

        public SettingsService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<T> Get<T>(string plugin) where T : ISettings<T>, new()
        {
            var setting = await _db.PluginSettings.FirstOrDefaultAsync(x => x.Plugin == plugin);
            if (setting == null)
                return new T().WithDefaultValues();

            var deserialized = JsonConvert.DeserializeObject(setting.Value, typeof(T));
            return (T)deserialized;
        }

        public async Task Set<T>(string plugin, T value) where T : ISettings<T>, new()
        {
            var jsonValue = JsonConvert.SerializeObject(value);
            var existing = await _db.PluginSettings.FirstOrDefaultAsync(x => x.Plugin == plugin);
            if (existing != null)
            {
                existing.Value = jsonValue;
                _db.Entry(existing).State = EntityState.Modified;
            }
            else
            {
                await _db.PluginSettings.AddAsync(PluginSetting.Create(plugin, jsonValue));
            }

            await _db.SaveChangesAsync();
        }
    }
}