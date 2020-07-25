﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace J3space.Abp.Account.Web.Pages.Account
{
    public class Login : PageModel
    {
        protected readonly IAccountAppService AccountAppService;

        protected Login(
            IAccountAppService accountAppService
        )
        {
            AccountAppService = accountAppService;
        }

        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public string ReturnUrl { get; set; }

        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public string ReturnUrlHash { get; set; }

        [BindProperty] public LoginDto LoginInput { get; set; }

        public AbpLoginResult LoginResult { get; set; } =
            new AbpLoginResult(LoginResultType.Success);

        public virtual async Task<IActionResult> OnGetAsync()
        {
            LoginInput = new LoginDto();
            return Page();
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            // if (action == "Register")
            // {
            //     if (ModelState.GetFieldValidationState(nameof(RegisterInput)) !=
            //         ModelValidationState.Valid)
            //         return Page();
            //
            //     await AccountAppService.RegisterAsync(RegisterInput);
            //     LoginInput = new LoginDto
            //     {
            //         Password = RegisterInput.Password,
            //         RememberMe = false,
            //         UserNameOrEmailAddress = RegisterInput.UserName
            //     };
            // }

            if (!ModelState.IsValid)
                return Page();

            LoginResult = await AccountAppService.Login(LoginInput);

            if (LoginResult.Result == LoginResultType.Success) return Redirect(ReturnUrl ?? "/");

            return Page();
        }
    }
}
