using AutoMapper;
using Kentico.Kontent.Delivery;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KenticoKontentModels
{
	public class VisitorRestrictionsConverter : IValueConverter<IEnumerable<ContentItem>, List<DontParkHere.Models.VisitorRestriction>>
	{
		public List<DontParkHere.Models.VisitorRestriction> Convert(IEnumerable<ContentItem> sourceMember, ResolutionContext context)
		{
			var list = new List<DontParkHere.Models.VisitorRestriction>();
			foreach (var item in sourceMember)
			{
				if (item.System.Type == VisitorRestriction.Codename)
				{
					list.Add(context.Mapper.Map<DontParkHere.Models.VisitorRestriction>(item));
				}
			}
			return list;
		}
	}
}
