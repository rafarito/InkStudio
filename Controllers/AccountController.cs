using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using InkStudio.Models;
using Microsoft.AspNetCore.Identity;

namespace InkStudio.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<IdentityUser> userManager;
    private readonly SignInManager<IdentityUser> signInManager;

    public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
    }

    //get :Register
    public IActionResult Register(){
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model){

        if(ModelState.IsValid){

            //copia os dados do REgisterViewModel para o IdentityUser
            var user = new IdentityUser{
                UserName = model.Email,
                Email = model.Email
            };

            // Armazena os dados do usuário na tabela AspNetUsers
            var result = await userManager.CreateAsync(user, model.Password);

            // se o usuário foi criado com sucesso, faz login do usuário
            // usando o serviço SignInManager e redireciona para o método acion index
            if(result.Succeeded){
                await userManager.AddToRoleAsync(user, "User");
                await signInManager.SignInAsync(user, isPersistent:false);
                return RedirectToAction("Index", "home");
            }

            // Se houver erros então inclui no ModelState
            //que será exibido pela tag helpers summary na validação
            foreach(var error in result.Errors){
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        return View(model);
    }

    //get :Login
    public IActionResult Login(){
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model){

        if(ModelState.IsValid){
            var result = await signInManager.PasswordSignInAsync(
                model.Email, model.Password, model.RememberMe, false
            );

            if(result.Succeeded){
                return RedirectToAction("Index" , "home");
            }

            ModelState.AddModelError(string.Empty, "Login Inválido");
        }
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Logout(){
        await signInManager.SignOutAsync();
        return RedirectToAction("index", "home");
    }

    [Route("/Account/AccessDenied")]
    public ActionResult AccessDenied()
    {
        return View();
    }
}
