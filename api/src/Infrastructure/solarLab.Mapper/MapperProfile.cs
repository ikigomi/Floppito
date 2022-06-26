using AutoMapper;
using Microsoft.AspNetCore.Identity;
using solarLab.Contracts.Category;
using solarLab.Contracts.Chat;
using solarLab.Contracts.Comment;
using solarLab.Contracts.Enums;
using solarLab.Contracts.Identity;
using solarLab.Contracts.Message;
using solarLab.Contracts.Post;
using solarLab.Contracts.User;
using solarLab.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace solarLab.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CommentEntity, CommentDto>()
                .ForMember(destination => destination.Author,
                    source => source.MapFrom(c => c.User));

            CreateMap<CommentDto, CommentEntity>()
                .ForMember(_=>_.CreatorId,
                _=>_.MapFrom(_=>_.Author.Id));


            CreateMap<PostEntity, PostDto>()
                .ForMember(destination => destination.Comments,
                    source => source.MapFrom(p => p.Comments))
                .ForMember(destination => destination.Author,
                    source => source.MapFrom(p => p.User))
                .ForMember(dest => dest.CategoryTitle,
                    source => source.MapFrom(p=>p.Category.Title));

            CreateMap<PostDto, PostEntity>()
                .ForMember(_=>_.CreatorId,
                _=>_.MapFrom(_=>_.Author.Id));


            CreateMap<ApplicationUser, UserDto>()
                .ForMember(dest => dest.Roles,
                    source => source.MapFrom(u => u.Roles.Select(r => Enum.Parse(typeof(Roles), r))));

            CreateMap<UserDto, ApplicationUser>()
                .ForMember(dest => dest.Roles,
                    source => source.MapFrom(u => u.Roles.Select(r => r.ToString())));


            CreateMap<CategoryEntity, CategoryDto>();
            CreateMap<CategoryDto, CategoryEntity>();


            CreateMap<RegisterDto, ApplicationUser>()
                .ForMember(dest=>dest.Avatar, 
                    source => source.MapFrom(d=>Convert.FromBase64String(d.Avatar)));


            CreateMap<MessageEntity, MessageDto>()
                .ForMember(_ => _.Author,
                _ => _.MapFrom(_ => _.Sender));

            CreateMap<MessageDto, MessageEntity>()
                .ForMember(_=>_.SenderId,
                _=>_.MapFrom(_=>_.Author.Id));


            CreateMap<ApplicationUser, AuthorDto>();
            CreateMap<AuthorDto, ApplicationUser>();


            CreateMap<ChatEntity, ChatDto>()
                .ForMember(_=>_.Messages,
                _=>_.MapFrom(_=>_.Messages));

            CreateMap<ChatDto, ChatEntity>();

        }
    }
}
