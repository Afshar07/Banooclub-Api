﻿using BanooClub.Extentions;
using BanooClub.Models;
using BanooClub.Models.Enums;
using BanooClub.Models.Urls;
using BanooClub.Services.Common;
using BanooClub.Services.SocialMediaServices;
using BanooClub.Services.UserServices;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BanooClub.Services.TicketServices
{
    public class TicketService : ITicketService
    {
        private readonly IBanooClubEFRepository<Ticket> _ticketRepository;
        private readonly IHttpContextAccessor _accessor;
        private readonly IBanooClubEFRepository<SocialMedia> _mediaRepository;
        private readonly ISocialMediaService _mediaService;
        private readonly IBanooClubEFRepository<User> _userRepository;
        private readonly IUserService userService;
        public TicketService(IBanooClubEFRepository<Ticket> ticketRepository
            , IBanooClubEFRepository<SocialMedia> mediaRepository
            , IBanooClubEFRepository<User> userRepository
            , IHttpContextAccessor accessor
            , IUserService userService
            , ISocialMediaService mediaService)
        {
            _ticketRepository = ticketRepository;
            _accessor = accessor;
            _mediaRepository = mediaRepository;
            _userRepository = userRepository;
            _mediaService = mediaService;
            this.userService = userService;
        }
        public async Task<IServiceResult<Ticket>> FindAsync(long ticketId)
            => new ServiceResult<Ticket>().Ok(_ticketRepository.GetQuery().First(z => z.TicketId == ticketId));

        public IServiceResult<object> GetAll(int pageNumber, int count, string searchCommand, byte ticketType)
        {
            var query = _ticketRepository.GetQuery().Where(z => z.ParentId == 0);

            if (!string.IsNullOrWhiteSpace(searchCommand))
            {
                var users = _userRepository.GetQuery()
                    .Where(x => x.Mobile.Contains(searchCommand) || x.UserName.Contains(searchCommand))
                    .Select(x => x.UserId)
                    .ToList();

                query = query.Where(x => users.Any(z => x.UserId == z));
            }

            if (ticketType > 0)
                query = query.Where(x => x.Type == ticketType);

            var tickets = query.OrderByDescending(x => x.CreateDate)
                .Skip((pageNumber - 1) * count)
                .Take(count).ToList();

            var dbTicketCount = _ticketRepository.GetQuery().Where(z => z.ParentId == 0).Count();

            foreach (var parent in tickets)
            {
                var dbMedia = _mediaRepository.GetQuery().FirstOrDefault(z => z.ObjectId == parent.TicketId && z.Type == MediaTypes.Ticket);
                parent.FileData = dbMedia == null ? "" : "media/gallery/Ticket/" + dbMedia.PictureUrl;
                var dbCustomerChildren = _ticketRepository.GetQuery().Where(z => z.ParentId == parent.TicketId && z.UserType == 1).ToList();
                dbCustomerChildren.Add(parent);
                parent.UnRead = dbCustomerChildren.Any(z => z.IsRead == false) ? true : false;
                parent.UserInfo = userService.Get(parent.UserId);

                if (parent.RecipientUserId != null && parent.RecipientUserId != 0)
                    parent.RecipientUserName = _userRepository.GetQuery()
                        .FirstOrDefault(x => x.UserId == parent.RecipientUserId)?.UserName;
            }

            var obj = new
            {
                Tickets = tickets,
                TicketCount = dbTicketCount,
            };

            return new ServiceResult<object>().Ok(obj);
        }
        public IServiceResult<object> GetAllForCustomer(int pageNumber, int count)
        {
            var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;

            var dbTickets = _ticketRepository.GetQuery().Where(z => (z.UserId == userId && z.ParentId == 0) || z.RecipientUserId == userId).
                OrderByDescending(x => x.CreateDate).Skip((pageNumber - 1) * count).Take(count).ToList();
            var TicketsCount = _ticketRepository.GetQuery().Where(z => z.UserId == userId && z.ParentId == 0).Count();
            foreach (var parent in dbTickets)
            {
                var dbMedia = _mediaRepository.GetQuery().FirstOrDefault(z => z.ObjectId == parent.TicketId && z.Type == MediaTypes.Ticket);
                parent.FileData = dbMedia == null ? "" : "media/gallery/Ticket/" + dbMedia.PictureUrl;
                var dbAdminChildren = _ticketRepository.GetQuery().Where(z => z.ParentId == parent.TicketId && z.UserType == 2).ToList();
                dbAdminChildren.Add(parent);
                parent.UnRead = dbAdminChildren.Any(z => z.IsRead == false) ? true : false;
            }
            var dbUserInfo = userService.Get(userId);
            var obj = new
            {
                UserInfo = dbUserInfo,
                Tickets = dbTickets,
                TicketsCount = TicketsCount
            };
            return new ServiceResult<object>().Ok(obj);
        }

        public int UserUnreadTicketCounts()
        {
            var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;
            return _ticketRepository.GetQuery().Where(z => z.UserId == userId && z.IsRead == false).Count();
        }

        public async Task<IServiceResult<object>> GetAllByParentId(long parentId)
        {
            var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;
            var dbUser = _userRepository.GetQuery().FirstOrDefault(z => z.UserId == userId);
            var Role = dbUser.Type == 1 ? 2 : 1;

            var dbParentTicket = _ticketRepository.GetQuery().FirstOrDefault(z => z.TicketId == parentId);
            if (dbParentTicket != null && dbParentTicket.UserType != dbUser.Type)
            {
                dbParentTicket.IsRead = true;
                await _ticketRepository.Update(dbParentTicket);
            }
            var dbChildren = _ticketRepository.GetQuery().Where(z => z.ParentId == parentId && z.UserType == Role && z.IsRead == false).ToList();
            foreach (var child in dbChildren)
            {
                child.IsRead = true;
                await _ticketRepository.Update(child);
            }

            var dbTickets = _ticketRepository.GetQuery().Where(z => z.ParentId == parentId).ToList();
            if (dbParentTicket != null)
            {
                dbTickets.Insert(0, dbParentTicket);
            }
            foreach (var ticket in dbTickets)
            {
                var dbMedia = _mediaRepository.GetQuery().FirstOrDefault(z => z.ObjectId == ticket.TicketId && z.Type == MediaTypes.Ticket);
                ticket.FileData = dbMedia == null ? "" : "media/gallery/Ticket/" + dbMedia.PictureUrl;
                var dbSelfie = _mediaRepository.GetQuery().FirstOrDefault(z => z.ObjectId == ticket.UserId && z.Type == MediaTypes.Profile);
                ticket.UserInfo = _userRepository.GetQuery().FirstOrDefault(z => z.UserId == ticket.UserId);
                ticket.UserInfo.SelfieFileData = dbSelfie == null ? "" : dbSelfie.PictureUrl;
                ticket.UserInfo.Password = null;
            }
            return new ServiceResult<object>().Ok(dbTickets);
        }

        public async Task<IServiceResult> DeleteTicket(long ticketId)
        {
            var dbTicket = _ticketRepository.GetQuery().First(z => z.TicketId == ticketId);
            await _ticketRepository.Delete(dbTicket);
            return new ServiceResult().Ok();
        }

        public async Task<IServiceResult<List<long>>> CreateTicket(Ticket model)
        {
            var userId = _accessor.HttpContext.User.GetUserId();

            var tickets = new List<Ticket>();

            var dbUser = _userRepository.GetQuery().FirstOrDefault(z => z.UserId == userId);
            model.UserId = userId;
            model.IsDeleted = false;
            model.IsRead = false;
            model.UserType = dbUser.Type;

            if (model.Type is 0)
            {
                foreach (var item in _userRepository.GetQuery())
                {
                    var ticket = AdaptFrom(model);
                    ticket.RecipientUserId = item.UserId;

                    tickets.Add(ticket);
                }
            }
            else if (model.RecipientIds != null && model.RecipientIds.Any())
            {
                foreach (var item in _userRepository.GetQuery().Where(x => model.RecipientIds.Any(z => x.UserId == z)))
                {
                    var ticket = AdaptFrom(model);
                    ticket.RecipientUserId = item.UserId;

                    tickets.Add(ticket);
                }
            }
            else
            {
                tickets.Add(model);
            }

            tickets.ForEach(x =>
            {
                var dbCreation = _ticketRepository.Insert(x);
                SetMediaFor(x);
            });

            return new ServiceResult<List<long>>().Ok(tickets.Select(x => x.TicketId).ToList());
        }

        public IServiceResult<Ticket> GetById(long id)
        {
            var dbTicket = _ticketRepository.GetQuery().FirstOrDefault(z => z.TicketId == id);
            var dbMedia = _mediaRepository.GetQuery().FirstOrDefault(z => z.ObjectId == id && z.Type == MediaTypes.Ticket);
            dbTicket.FileData = dbMedia == null ? "" : "media/gallery/Ticket/" + dbMedia.PictureUrl;
            dbTicket.UserInfo = userService.Get(dbTicket.UserId);
            return new ServiceResult<Ticket>().Ok(dbTicket);
        }

        public IServiceResult<bool> ChangeTicketStatus(long parentId)
        {
            var dbParent = _ticketRepository.GetQuery().FirstOrDefault(z => z.TicketId == parentId);
            dbParent.IsRead = true;
            _ticketRepository.Update(dbParent);

            var dbChildren = _ticketRepository.GetQuery().Where(z => z.ParentId == parentId).ToList();
            foreach (var ticket in dbChildren)
            {
                ticket.IsRead = true;
                _ticketRepository.Update(ticket);
            }
            return new ServiceResult<bool>().Ok(true);
        }

        public async Task<IServiceResult<bool>> CloseTicket(long parentId)
        {
            var dbParent = _ticketRepository.GetQuery().FirstOrDefault(z => z.TicketId == parentId);
            dbParent.IsClosed = true;
            await _ticketRepository.Update(dbParent);

            var dbChildren = _ticketRepository.GetQuery().Where(z => z.ParentId == parentId).ToList();
            foreach (var ticket in dbChildren)
            {
                ticket.IsClosed = true;
                await _ticketRepository.Update(ticket);
            }
            return new ServiceResult<bool>().Ok(true);
        }

        public IServiceResult<object> GetTicketsByFilter(long? UserType, int? Type, bool? isRead, int pageNumber, int count, string search)
        {
            var dbTickets = _ticketRepository.GetQuery();
            if (UserType != null)
            {
                dbTickets = dbTickets.Where(z => z.UserType == UserType);
            }
            if (Type != null)
            {
                dbTickets = dbTickets.Where(z => z.Type == Type);
            }
            if (isRead != null)
            {
                dbTickets = dbTickets.Where(z => z.IsRead == isRead);
            }
            var result = dbTickets.OrderByDescending(x => x.CreateDate).Where(z => z.Content.Contains(search) || z.Title.Contains(search)).Skip((pageNumber - 1) * count).Take(count).ToList();
            foreach (var ticket in result)
            {
                var dbMedia = _mediaRepository.GetQuery().FirstOrDefault(z => z.ObjectId == ticket.TicketId && z.Type == MediaTypes.Ticket);
                ticket.FileData = dbMedia == null ? "" : "media/gallery/Ticket/" + dbMedia.PictureUrl;
                var dbSelfie = _mediaRepository.GetQuery().FirstOrDefault(z => z.ObjectId == ticket.UserId && z.Type == MediaTypes.Profile);
                ticket.UserInfo = _userRepository.GetQuery().FirstOrDefault(z => z.UserId == ticket.UserId);
                ticket.UserInfo.SelfieFileData = dbSelfie == null ? "" : dbSelfie.PictureUrl;
            }
            var ResultCount = dbTickets.Where(z => z.Content.Contains(search) || z.Title.Contains(search)).Count();
            var obj = new
            {
                TicketCounts = ResultCount,
                Tickets = result
            };
            return new ServiceResult<object>().Ok(obj);
        }

        public IServiceResult<bool> ChangeTicketType(long parentId, byte ticketType)
        {
            var parent = _ticketRepository.GetQuery().Where(x => x.TicketId == parentId).FirstOrDefault();
            if (parent != null)
                parent.Type = ticketType;

            _ticketRepository.Save();
            return new ServiceResult<bool>().Ok(true);
        }

        #region Utilities

        private void SetMediaFor(Ticket model)
        {
            if (!string.IsNullOrWhiteSpace(model.FileData))
            {
                var outPut = _mediaService.SaveImage(model.FileData, EntityUrls.TicketDoc);
                SocialMedia dbMedia = new SocialMedia()
                {
                    IsDeleted = false,
                    ObjectId = model.TicketId,
                    PictureUrl = outPut.ImageName,
                    Type = MediaTypes.Ticket,
                    MediaId = 0
                };

                _mediaRepository.Insert(dbMedia);
            }
        }

        private static Ticket AdaptFrom(Ticket input)
        {
            return new Ticket
            {
                IsClosed = input.IsClosed,
                IsDeleted = input.IsDeleted,
                IsRead = input.IsRead,
                UnRead = input.UnRead,
                ParentId = input.ParentId,
                RecipientIds = input.RecipientIds,
                Content = input.Content,
                CreateDate = input.CreateDate,
                FileData = input.FileData,
                TicketId = input.TicketId,
                Title = input.Title,
                Type = input.Type,
                UserType = input.UserType,
                UserId = input.UserId,
                RecipientUserId = input.RecipientUserId,
                UserInfo = input.UserInfo
            };
        }

        #endregion
    }
}