using AutoMapper;
using BLL.Model;
using FSInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Infrastructure
{
    class BLLProfile : Profile
    {
        public BLLProfile()
        {
            CreateMap<FSItemInfo, FSItemModel>();
        }
    }
}
