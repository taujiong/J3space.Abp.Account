using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Shouldly;
using Volo.Abp.Identity;
using Xunit;

namespace J3space.Abp.Account.Application.Test
{
    public sealed class AccountAppServiceTests : AbpAccountApplicationTestBase
    {
        private readonly IAccountAppService _accountAppService;
        private readonly IIdentityUserRepository _identityUserRepository;
        private readonly ILookupNormalizer _lookupNormalizer;
        private readonly IdentityUserManager _userManager;

        public AccountAppServiceTests()
        {
            _accountAppService = GetRequiredService<IAccountAppService>();
            _identityUserRepository = GetRequiredService<IIdentityUserRepository>();
            _lookupNormalizer = GetRequiredService<ILookupNormalizer>();
            _userManager = GetRequiredService<IdentityUserManager>();
        }

        [Fact]
        public async Task RegisterAsync()
        {
            var registerDto = new RegisterDto
            {
                UserName = "bob.lee",
                EmailAddress = "bob.lee@abp.io",
                Password = "P@ssW0rd"
            };

            #region success

            await _accountAppService.RegisterAsync(registerDto);

            var user = await _identityUserRepository.FindByNormalizedUserNameAsync(
                _lookupNormalizer.NormalizeName("bob.lee"));

            user.ShouldNotBeNull();
            user.UserName.ShouldBe("bob.lee");
            user.Email.ShouldBe("bob.lee@abp.io");

            (await _userManager.CheckPasswordAsync(user, "P@ssW0rd")).ShouldBeTrue();

            #endregion

            #region fail

            // 用户已被注册
            try
            {
                await _accountAppService.RegisterAsync(registerDto);
            }
            catch (AbpIdentityResultException e)
            {
                e.Code.ShouldBe("Identity.DuplicateUserName");
            }

            // 邮箱已被注册
            registerDto.UserName = "bob";
            try
            {
                await _accountAppService.RegisterAsync(registerDto);
            }
            catch (AbpIdentityResultException e)
            {
                e.Code.ShouldBe("Identity.DuplicateEmail");
            }

            // 密码不规范
            registerDto.EmailAddress = "bob@abp.io";
            registerDto.Password = "Password";
            try
            {
                await _accountAppService.RegisterAsync(registerDto);
            }
            catch (AbpIdentityResultException e)
            {
                e.Code.ShouldContain("Identity.Password");
            }

            #endregion
        }
    }
}
