﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TravelManager.Modal;
using TravelManager.Modal.Services;
using TravelManager.VIewModal;
using Unity;
using Unity.Injection;

namespace TravelManager.View
{
    class Pages
    {
        private static IUnityContainer container;
        private static LoginWindow loginWindow;
        private static MainWindow mainWindow;
        
        static Pages()
        {
            loginWindow = App.Current.Windows.OfType<LoginWindow>().FirstOrDefault();
            TravelContext travelContext = new TravelContext();

            container = new UnityContainer();

            container.RegisterType<IService<Country>, CountryService>(new InjectionConstructor(new object[] { travelContext }));
            container.RegisterType<IService<City>, CityService>(new InjectionConstructor(new object[] { travelContext }));
            container.RegisterType<IService<Order>, OrderService>(new InjectionConstructor(new object[] { travelContext }));
            container.RegisterType<IService<User>,UserService>(new InjectionConstructor(new object[] { travelContext }));
            container.RegisterType<IService<InternalUserData>, UserDataService>(new InjectionConstructor(new object[] { travelContext }));

            container.RegisterType<IService<Tourist>, TouristService>(new InjectionConstructor(new object[] { travelContext }));


            container.RegisterType<IService<Hotel>, HotelService>(new InjectionConstructor(new object[] { travelContext }));
            container.RegisterType<IService<Route>, RouteService>(new InjectionConstructor(new object[] { travelContext }));
            container.RegisterType<IViewModal<UpdateServiceViewModal>, UpdateServiceViewModal>(new InjectionConstructor(new object[] { container.Resolve<IService<Country>>(), container.Resolve<IService<Route>>(), container.Resolve<IService<City>>(), container.Resolve<IService<Hotel>>(), container.Resolve<PlacesViewModal>() }));
            container.RegisterType<IViewModal<RoutesViewModal>, RoutesViewModal>(new InjectionConstructor(new object[] { container.Resolve<IService<Route>>() }));
            container.RegisterType<IViewModal<CreateUserViewModal>, CreateUserViewModal>(new InjectionConstructor(new object[] { container.Resolve<IService<User>>() ,container.Resolve<IService<InternalUserData>>()}));




        }
        private static MainControl mainControl;
        private static Menu menu;
        public static Menu Menu
        {

            get
            {
                // MenuViewModal menuViewModel = container.Resolve<MenuViewModal>();

                if (menu == null)
                {
                    menu = container.Resolve<Menu>();


                }
                Main.Width = 1080;
                Main.Height = 640;
                // main.GridMenu = menu.GridMenu;
                return menu;
            }

        }
        
        private static AdminMenu adminmenu;
        public static AdminMenu AdminMenu
        {

            get
            {
                // AdminViewModel adminViewModel = container.Resolve<AdminViewModel>();
                if (adminmenu == null)
                {
                    adminmenu = container.Resolve<AdminMenu>();
                }
                Main.Width = 1080;
                Main.Height = 640;
                return adminmenu;
            }
        }

        private static Routes routes;
        public static MainControl MainControl
        {
            get
            {
                // AdminViewModel adminViewModel = container.Resolve<AdminViewModel>();

                mainControl = container.Resolve<MainControl>();
                return mainControl;
            }
        }


        

        public static MainWindow Main
        {
            get
            {

                
                if (mainWindow == null)
                    mainWindow = new MainWindow();
                return mainWindow;
                
            }
            
        }

        public static Routes Routes
        {
            get
            {
                routes = container.Resolve<Routes>();
                return routes;
            }
        }
        public static OrderTicket orderticket;
        public static OrderTicket OrderTiket
        {
            get
            {
                container.RegisterType<IViewModal<OrderTicketViewModal>, OrderTicketViewModal>(new InjectionConstructor(new object[] { container.Resolve<IService<Route>>(), container.Resolve<IService<Order>>(), container.Resolve<IService<Tourist>>()}));

                orderticket = container.Resolve<OrderTicket>();
                return orderticket;
            }
        }

        private static Countries countries;
        public static Countries Countries
        {
            get
            {
                
                countries = container.Resolve<Countries>();
                return countries;
            }
        }
        private static UpdateService updateService;
        public static UpdateService UpdateService
        {
            get
            {

                updateService = container.Resolve<UpdateService>();
                return updateService;
            }
        }
        private static CreateUser createUser;
        public static CreateUser CreateUser
        {
            get
            {

                createUser = container.Resolve<CreateUser>();
                return createUser;
            }
        }

        public static OrderRoute OrderRoute { get => orderRoute; set => orderRoute = value; }

        private static OrderRoute orderRoute;


        public static void SetPage(UserControl control)
        {
            Main.SetPage(control);
        }

        public static void SetMenu(UserControl control)
        {
            Main.SetMenu(control);
        }
        public static void OpenWindow(Route r)
        {
            container.RegisterType<IViewModal<OrderViewModal>, OrderViewModal>(new InjectionConstructor(new object[] { container.Resolve<IService<Route>>(),container.Resolve<IService<Order>>(),container.Resolve<IService<Tourist>>(),r }));
            OrderRoute = container.Resolve<OrderRoute>();
            OrderRoute.Owner = App.Current.MainWindow;

            OrderRoute.Show();

        }
        public static void SetWindow(Window w)
        {
            App.Current.MainWindow = w ;
            w.Loaded += (sender, args) => loginWindow.Close();

            w.Show();
        }

      

    }
    
}