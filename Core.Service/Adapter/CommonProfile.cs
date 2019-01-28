using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Core.Models;
using Core.Service.Models.MenuView;

namespace Core.Service.Adapter
{
    public class CommonProfile:Profile, IProfile
    {
        public CommonProfile()
        {
            CreateMap<Users, Models.UserView.UserViewModel>().ForMember(f=>f.UserId,s=>s.MapFrom(s1=> s1.Id)).ForMember(f=>f.UserName,s=>s.MapFrom(s1=>s1.DisplayName))
                .ForMember(f=>f.IsSuperAdministrator,s=>s.MapFrom(s1=>s1.UserRoles.IsSuperAdministrator));
            CreateMap<Menus, Models.MenuView.MenuViewModel>();
            CreateMap<MenuCreateViewModel, Menus>(); 
            CreateMap<Icons, Models.IconsViewModel.IconJsonModel>();
        }
    }
}
