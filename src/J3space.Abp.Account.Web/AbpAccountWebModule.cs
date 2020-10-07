using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Account.Localization;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace J3space.Abp.Account.Web
{
    [DependsOn(
        typeof(Volo.Abp.Account.Web.AbpAccountWebModule)
    )]
    public class AbpAccountWebModule : AbpModule
    {
        // public override void PreConfigureServices(ServiceConfigurationContext context)
        // {
        //     context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
        //     {
        //         options.AddAssemblyResource(typeof(AccountResource),
        //             typeof(AbpAccountWebModule).Assembly);
        //     });
        //
        //     PreConfigure<IMvcBuilder>(mvcBuilder =>
        //     {
        //         mvcBuilder.AddApplicationPartIfNotExists(typeof(AbpAccountWebModule).Assembly);
        //     });
        // }
        //
        // public override void ConfigureServices(ServiceConfigurationContext context)
        // {
        //     Configure<AbpVirtualFileSystemOptions>(options =>
        //     {
        //         options.FileSets.AddEmbedded<AbpAccountWebModule>("J3space.Abp.Account.Web");
        //     });
        // }
    }
}