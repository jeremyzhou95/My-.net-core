using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace OptionDemo.Service
{
    public interface IOrderService
    {
        int ShowMaxOrderCount();
    }
    public class OrderService : IOrderService
    {
        //IOptions<OrderServiceOption> _options;      //使用IOptions接口
        //IOptionsSnapshot<OrderServiceOption> _options;      //使用IOptionsSnapshot接口 (热更新，每次注册会重新读取配置，不能用在单例)
        IOptionsMonitor<OrderServiceOption> _options;      //使用IOptionsMonitor接口 (热更新，每次注册会重新读取配置，可以用在单例)
        public OrderService(IOptionsMonitor<OrderServiceOption> options)
        {
            this._options = options;

            _options.OnChange(options=> {
                Console.WriteLine($"配置发生了变化：{options.MaxOrderCount}");      //在单例模式中可以监听到Options的变化
            });
        }
        public int ShowMaxOrderCount()
        {
            return _options.CurrentValue.MaxOrderCount;
        }
    }

    public class OrderServiceOption
    {
        private int maxOrderCount=100;

        public int MaxOrderCount { get => maxOrderCount; set => maxOrderCount = value; }
    }
}
