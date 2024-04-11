using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebMusic.DAL.EF;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Proxies;

namespace WebMusic.BLL.Infrastructure
{
    public static class SoccerContextExtensions
    {
        public static void AddWebMusicContext(this IServiceCollection services, string connection)
        {

            services.AddDbContext<WebMusicContext>(options => options.UseLazyLoadingProxies().UseSqlServer(connection));
        }
    }
}
