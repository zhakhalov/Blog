using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc4;
using Blog.Repository.Managers;
using System.Configuration;
using Blog.Repository.Repositories;
using Blog.WebUI.Code.Services;

namespace Blog.WebUI
{
    public static class Bootstrapper
    {
        public static IUnityContainer Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            return container;
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();    
            RegisterTypes(container);

            return container;
        }

        public static void RegisterTypes(IUnityContainer container)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["BlogNoSQL"].ConnectionString;
            string avatarPath = ConfigurationManager.AppSettings["avatarPath"];

            container.RegisterType<IArticleManager, ArticleManager>(new InjectionConstructor(connectionString));
            container.RegisterType<IUserRepository, UserRepository>(new InjectionConstructor(connectionString));
            container.RegisterType<ITagRepository, TagRepository>(new InjectionConstructor(connectionString));
            container.RegisterType<ITransliterationService, TransliterationService>();
            container.RegisterType<IAvatarService, AvatarService>(new InjectionConstructor(avatarPath));
        }
    }
}