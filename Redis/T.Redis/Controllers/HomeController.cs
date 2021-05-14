using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using EasyCaching.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using T.Redis.Models;

namespace T.Redis.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEasyCachingProvider _cache;
        private readonly IEasyCachingProviderFactory _easyCachingProviderFactory;

        public HomeController(ILogger<HomeController> logger, IEasyCachingProviderFactory easyCachingProviderFactory)
        {
            _logger = logger;
            _easyCachingProviderFactory = easyCachingProviderFactory;
            _cache = easyCachingProviderFactory.GetCachingProvider("RedisExample");
        }

        public IActionResult Index()
        {
            var dic = new Dictionary<int, string>();
            dic.Add(1, "a");

            _cache.Set("Tjl", dic, TimeSpan.FromDays(100));

            var res = _cache.Get<Dictionary<int, string>>("Tjl");


            _cache.Remove("zaranet use easycaching");


            return View();
        }

        public IActionResult Privacy()
        {
            //订阅端（获取消息，根据消息执行一些操作）
            using (var redis = ConnectionMultiplexer.Connect("192.168.153.131:6379"))
            {
                var sub = redis.GetSubscriber();

                //订阅名为 messages 的通道

                sub.Subscribe("redisChat", (channel, message) => {

                    //输出收到的消息
                    Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] {message}");

                    if (message == "yes")
                    {
                        //todo Something
                    }
                    else
                    {
                        Console.WriteLine("未获取指定的信息，什么也不做！");
                    }
                });
                Console.WriteLine("已订阅 redisChat");
                Console.ReadKey();
            }


            //发布端（发布消息，触发源）
            using (var redis = ConnectionMultiplexer.Connect("192.168.153.131:6379"))
            {
                var sub = redis.GetSubscriber();

                Console.WriteLine("请输入任意字符，输入exit退出");

                string input;

                do
                {
                    input = Console.ReadLine();
                    sub.Publish("redisChat", input);
                } while (input != "exit");
            }
            
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
