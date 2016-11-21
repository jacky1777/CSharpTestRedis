using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Redis {
    class Program {
        static ConnectionMultiplexer redisConn = ConnectionMultiplexer.Connect("127.0.0.1:6379");
        static void Main(string[] args) {
            while (true) {
                var data = GetKey("AAA");
                if (data == null) {
                    SetKey("AAA", "12345");
                    Console.WriteLine($"Add to Cache");
                }
                else {
                    Console.WriteLine($"Get From Cache");
                }
                Thread.Sleep(1000);
            }
        }

        static string GetKey(string key) {
            var redisDatabase = redisConn.GetDatabase();

            // 取得快取資料
            var value = redisDatabase.StringGet(key);
            return value;
        }
        static void SetKey(string key, string value) {

            var redisDatabase = redisConn.GetDatabase();
            redisDatabase.StringSet(key, value, TimeSpan.FromSeconds(10));
        }
    }
}
