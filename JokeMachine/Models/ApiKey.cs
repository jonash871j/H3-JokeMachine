﻿namespace JokeMachine.Models
{
    public class ApiKey
    {
        public ApiKey(string owner, string key)
        {
            Owner = owner ?? throw new ArgumentNullException(nameof(owner));
            Key = key ?? throw new ArgumentNullException(nameof(key));
        }

        public string Owner { get; }
        public string Key { get; }
    }
}
