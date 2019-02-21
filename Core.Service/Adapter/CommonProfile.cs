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
          
            CreateMap<Menus, Models.MenuView.MenuViewModel>();
            CreateMap<MenuCreateViewModel, Menus>(); 
            CreateMap<Icons, Models.IconsViewModel.IconJsonModel>();

            CreateMap<Users, Models.UserView.UserViewModel>().ForMember(f => f.UserId, s => s.MapFrom(s1 => s1.Id)).ForMember(f => f.UserName, s => s.MapFrom(s1 => s1.DisplayName))
              .ForMember(f => f.IsSuperAdministrator, s => s.MapFrom(s1 => s1.UserRoles.IsSuperAdministrator));
            CreateMap<Users, Models.UserView.UserEditViewModel>().ForMember(f => f.Guid, s => s.MapFrom(s1 => s1.Id));
            CreateMap<Models.UserView.UserEditViewModel, Users>().ForMember(f=>f.CreatedByUserGuid,s=>s.MapFrom(s1=>s1.CreateBy)).ForMember(f=>f.Id,s=>s.MapFrom(s1=>s1.Guid));
            CreateMap<Users, Models.UserView.UserViewPageModel>().ForMember(f => f.CreatedOn, s => s.MapFrom(s1 => s1.CreateDate)).ForMember(f => f.ModifiedOn, s => s.MapFrom(s1 => s1.UpdateDate))
                .ForMember(f => f.CreatedByUserName, s => s.MapFrom(s1 => s1.CreateUser.DisplayName)).ForMember(f => f.Guid, s => s.MapFrom(s1 => s1.Id))
                .ForMember(f => f.ModifiedByUserName, s => s.MapFrom(s1 => s1.ModifiedUser.DisplayName));

            CreateMap<Roles, Models.RoleView.RoleEditView>();
            CreateMap<Models.RoleView.RoleEditView, Roles>();
            CreateMap<Roles, Models.RoleView.RoleSelectView>().ForMember(f=>f.RoleName,s=>s.MapFrom(s1=>s1.Name));
            CreateMap<Roles, Models.RoleView.RolePageView>().ForMember(f=>f.CreatedOn ,s=>s.MapFrom(s1=>s1.CreateDate)).ForMember(f => f.ModifiedOn, s => s.MapFrom(s1 => s1.UpdateDate))
                .ForMember(f=>f.CreatedByUserName, s => s.MapFrom(s1 => s1.CreateUser.DisplayName))
                .ForMember(f => f.ModifiedByUserName, s => s.MapFrom(s1 => s1.ModifiedUser.DisplayName));
        }
    }
}
