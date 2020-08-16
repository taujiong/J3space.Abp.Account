using Volo.Abp;
using Volo.Abp.Testing;

namespace J3space.Abp.Account.Application.Test
{
    public class AbpAccountApplicationTestBase : AbpIntegratedTest<AbpAccountApplicationTestModule>
    {
        protected override void SetAbpApplicationCreationOptions(AbpApplicationCreationOptions options)
        {
            options.UseAutofac();
        }
    }
}