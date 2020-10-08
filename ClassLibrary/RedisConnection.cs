using StackExchange.Redis;
using System;

namespace ClassLibrary
{
    public sealed class RedisConnection
    {
        private static string _settingOption;
        public readonly ConnectionMultiplexer ConnectionMultiplexer;

        public static RedisConnection RedisConnectionInstance
        {
            get
            {
                return lazyRedisConnection.Value;
            }
        }

        private RedisConnection()
        {
            //Set ConnectionMultiplexer
            ConnectionMultiplexer = ConnectionMultiplexer.Connect(_settingOption);
        }

        public static void Init(string settingOption)
        {
            _settingOption = settingOption;
        }

        //Open RedisConnection until need it by using Lazy<> to cost down source
        private static Lazy<RedisConnection> lazyRedisConnection = new Lazy<RedisConnection>(() =>
        {
            if (String.IsNullOrEmpty(_settingOption))
            {
                throw new InvalidOperationException("Please call Init() first.");
            }
            return new RedisConnection();
        });
    }
}