using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Account.Web;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace J3space.Abp.Account.Web
{
    [DependsOn(
        typeof(AbpAccountWebModule)
    )]
    public class AbpAccountWebWrapperModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(AbpAccountWebWrapperModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<AbpAccountWebWrapperModule>("J3space.Abp.Account.Web");
            });
        }
    }
}