using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using Core.Common.LinqExtensions;
using Core.Repository;
using Core.Service.Models;
using Core.Service.Models.IconsViewModel;
using Core.Models;
using Core.Service.Adapter;

namespace Core.Service.Imp
{
    public class IconsService : IIConsService
    {
        private IIconsRepository _iconsRepository;

        public IconsService(IIconsRepository iconsRepository)
        {
            _iconsRepository = iconsRepository;
        }

        public ResultDataModel FindIconByKey(string keywords)
        {
            var resultData = new ResultDataModel();
            if (string.IsNullOrWhiteSpace(keywords))
            {
                resultData.SetFailed("没有查询到数据！");
                return resultData;
            }
            keywords = keywords.Trim();
            var data = _iconsRepository.Entities.Where(f => f.Code.Contains(keywords)).Select(x => new { x.Code, x.Color, x.Size }).ToList();
            resultData.SetData(data);
            return resultData;
        }

        public ResultDataModel GetList(IconsRequestModel model)
        {
            Expression<Func<Icons, bool>> filter = null;
            if (!string.IsNullOrWhiteSpace(model.Kw))
            {
                model.Kw = model.Kw.Trim();
                filter = f => f.Code.Contains(model.Kw);
            }
            if (model.IsDeleted > Common.Enums.IsDeleted.All)
            {
                if (filter != null)
                {
                    filter = filter.And(f => f.IsDeleted == model.IsDeleted);
                }
                else
                {
                    filter = f => f.IsDeleted == model.IsDeleted;
                }
            }
            if (model.Status > Common.Enums.Status.All)
            {
                if (filter != null)
                {
                    filter = filter.And(f => f.Status == model.Status);
                }
                else
                {
                    filter = f => f.Status == model.Status;
                }
            }
            int total = 0;
            var list = _iconsRepository.GetPages(filter, model.CurrentPage, model.PageSize, out total).ProjectedAsCollection<IconJsonModel,Guid>();
            var resultData = new ResultDataModel();
            resultData.SetData(list, total);
            return resultData;
        }
    }
}
