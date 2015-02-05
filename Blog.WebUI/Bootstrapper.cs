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
            // repository config
            string connectionString = ConfigurationManager.ConnectionStrings["BlogNoSQL"].ConnectionString;
            // user config
            string avatarPath = ConfigurationManager.AppSettings["avatarPath"];
            string defaultAvatar = ConfigurationManager.AppSettings["defaultAvatar"];
            int summaryLimit = int.Parse(ConfigurationManager.AppSettings["summaryLimit"]);
            // article config
            int titleLimit = int.Parse(ConfigurationManager.AppSettings["titleLimit"]);
            int pageLimit = int.Parse(ConfigurationManager.AppSettings["pageLimit"]);
            int commentLimit = int.Parse(ConfigurationManager.AppSettings["commentLimit"]);
            int shortContentLimit = int.Parse(ConfigurationManager.AppSettings["shortContentLimit"]);
            int asideLimit = int.Parse(ConfigurationManager.AppSettings["asideLimit"]);

            container.RegisterType<IArticleManager, ArticleManager>(new InjectionConstructor(connectionString));
            container.RegisterType<IUserRepository, UserRepository>(new InjectionConstructor(connectionString));
            container.RegisterType<ITagRepository, TagRepository>(new InjectionConstructor(connectionString));
            container.RegisterType<ITransliterationService, TransliterationService>();
            container.RegisterType<IUserConfigService, UserConfigService>(new InjectionConstructor(
                new InjectionParameter(avatarPath),
                new InjectionParameter(defaultAvatar),
                new InjectionParameter(summaryLimit)));
            container.RegisterType<IArticleConfigService, ArticleConfigService>(new InjectionConstructor(
                new InjectionParameter(titleLimit),
                new InjectionParameter(pageLimit),
                new InjectionParameter(commentLimit),
                new InjectionParameter(shortContentLimit),
                new InjectionParameter(asideLimit)));
        }
    }
}