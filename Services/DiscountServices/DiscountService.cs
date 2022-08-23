//using BanooClub.Models;
//using Infrastructure;
//using Microsoft.AspNetCore.Http;
//using MoreLinq;
//using BanooClub.Services.DiscountServices;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace BanooClub.Services.DiscountServices
//{
//    public class DiscountService : IDiscountService
//    {
//        private readonly IBanooClubEFRepository<Discount> discountRepository;
//        private readonly IBanooClubEFRepository<Media> mediaRepository;
//        private readonly IBanooClubEFRepository<View> viewRepository;
//        private readonly IHttpContextAccessor _accessor;

//        public DiscountService(IBanooClubEFRepository<Discount> discountRepository, IBanooClubEFRepository<View> viewRepository, IHttpContextAccessor accessor, IBanooClubEFRepository<Media> mediaRepository)
//        {
//            this.discountRepository = discountRepository;
//            this.mediaRepository = mediaRepository;
//            _accessor = accessor;
//            this.viewRepository = viewRepository;
//        }
//        public async Task SendNewsLetter()
//        {
//            var dbClubMembersEmail = newsLetterRepository.GetQuery().Select(z => z.Email).ToList();
//            foreach (var email in dbClubMembersEmail)
//            {
//                var subject = "تخفیف های جدید";
//                var content = $"<html><head></head><body>" +
//                    //$"<div style='width:100%'> <img src='https://simatestapi.simagar.com/Images/footer-logo.png' style='width:100%;height:Auto;'></div><br>" +
//                    $"<h4 style='text-align:left;color:black !important;'>کاربر گرامی تخفیف های جدید در سایت قرار داده شده است</h4><br>" +
//                    $"<h4 style='text-align: left'>برای دیدن تخفیف ها بر روی لینک زیر کلیک کنید :</h4><br>" +
//                    $"<button style='font-size: 25px;border-radius: 5px;border-color: #3498db;position: relative;'><a href='https://BanooClub.com/NewDiscounts' target='_blank'>تخفیف های جدید</a></button>" +
//                    $"</body></html>";
//                await emailService.SendMail(subject, content, email);
//            }
//        }
//        public async Task<object> Create(DiscountDto inputDto)
//        {
//            var FailedPriceIds = new List<long>();
//            int Response = 1;
//            foreach (var priceId in inputDto.PriceIds)
//            {
//                var LastDbDiscount = discountRepository.GetQuery().FirstOrDefault(z => z.PriceId == priceId);
//                if (LastDbDiscount == null)
//                {
//                    var dbDiscount = new Discount()
//                    {
//                        DiscountId = 0,
//                        CreateDate = DateTime.Now,
//                        ExpireDate = inputDto.ExpireDate,
//                        IsDeleted = false,
//                        PriceId = priceId,
//                        Type = inputDto.Type,
//                        Value = inputDto.Value,
//                        Refraction = inputDto.Refraction,
//                        StartDate = inputDto.StartDate
//                    };
//                    discountRepository.Insert(dbDiscount);
//                    await SendNewsLetter();
//                }
//                else
//                {
//                    FailedPriceIds.Add(priceId);
//                    Response = 2;
//                }
//            }
//            var obj = new
//            {
//                FailedIds = FailedPriceIds,
//                Message = Response
//            };
//            return obj;
//        }
//        public async Task<Discount> Update(Discount item)
//        {
//            await discountRepository.Update(item);
//            return item;
//        }
//        public string GetOptionValueParents(long? optionValueId, string ParentName)
//        {
//            var dbValue = optionValueRepository.GetQuery().First(z => z.OptionValueId == optionValueId);
//            ParentName = ParentName == "" ? dbValue.Value : ParentName + " _ " + dbValue.Value;
//            if (dbValue.OptionValueParentId != null && dbValue.OptionValueParentId != 0)
//            {
//                return GetOptionValueParents(dbValue.OptionValueParentId, ParentName);
//            }
//            return ParentName;
//        }
//        public async Task<object> GetAll(int pageNumber, int count)
//        {
//            var discounts = discountRepository.GetQuery().OrderByDescending(z => z.CreateDate).Skip((pageNumber - 1) * count).Take(count).ToList();
//            var DiscountCount = discountRepository.GetQuery().Count();
//            foreach (var discount in discounts)
//            {
//                discount.PriceInfo = priceRepository.GetQuery().FirstOrDefault(z => z.PriceId == discount.PriceId);
//                //discount.PriceInfo.ParentsName =GetOptionValueParents(discount.PriceInfo.OptionValueId, "");
//                discount.PriceInfo.ProductInfo = productRepository.GetQuery().FirstOrDefault(z => z.ProductId == discount.PriceInfo.ProductId);
//            }
//            var obj = new
//            {
//                Discounts = discounts,
//                Counts = DiscountCount
//            };
//            return obj;
//        }
//        public async Task<object> GetLatestDiscount(int count)
//        {
//            var dbPriceIds = discountRepository.GetQuery().Where(x => x.ExpireDate > DateTime.Now).OrderByDescending(z => z.CreateDate).Select(x => x.PriceId).ToList();
//            var dbProductIds = priceRepository.GetQuery().Where(z => dbPriceIds.Contains(z.PriceId)).Select(x => x.ProductId).Distinct().Take(count).ToList();
//            var dbProducts = productRepository.GetQuery().Where(z => dbProductIds.Contains(z.ProductId)).ToList();
//            foreach (var dbProduct in dbProducts)
//            {
//                dbProduct.Prices = priceRepository.GetQuery().Where(z => z.ProductId == dbProduct.ProductId).OrderBy(x => x.PriceValue).ToList();
//                var optionValuesList = new List<OptionValue>();
//                foreach (var prc in dbProduct.Prices)
//                {
//                    prc.Discount = discountRepository.GetQuery().FirstOrDefault(z => z.PriceId == prc.PriceId && z.ExpireDate > DateTime.Now);
//                    var dbValuePrices = valuePriceRepository.GetQuery().Where(z => z.PriceId == prc.PriceId).Select(x => x.OptionValueId).ToList();
//                    prc.OptionValues = optionValueRepository.GetQuery().Where(z => dbValuePrices.Contains(z.OptionValueId)).ToList();
//                    optionValuesList.AddRange(prc.OptionValues);
//                }
//                var optionIds = optionValuesList.Select(z => z.OptionId).Distinct().ToList();
//                dbProduct.Options = optionRepository.GetQuery().Where(z => optionIds.Contains(z.OptionId)).ToList();
//                foreach (var option in dbProduct.Options)
//                {
//                    option.OptionValueList = optionValuesList.Where(z => z.OptionId == option.OptionId).DistinctBy(x => x.OptionValueId).ToList();
//                }

//                var dbRate = await rateService.GetByProductId(dbProduct.ProductId);
//                dbProduct.Rate = dbRate.Data.Average;
//                dbProduct.CategoryName = productCategoryRepository.GetQuery().FirstOrDefault(z => z.ProductCategoryId == dbProduct.ProductCategoryId).Name;
//                var photos = mediaRepository.GetQuery().Where(z => z.ObjectId == dbProduct.ProductId && z.Type == MediaTypes.Product).ToList();
//                List<FileData> files = new List<FileData>();
//                foreach (var photo in photos)
//                {
//                    var file = new FileData()
//                    {
//                        Base64 = photo.PictureUrl,
//                        Priority = (int)photo.Priority
//                    };
//                    files.Add(file);
//                }
//                dbProduct.Photos = files;
//                //dbProduct.Prices=priceRepository.GetQuery().Where(z => z.ProductId == dbProduct.ProductId).ToList();
//                //dbProduct.Prices.ForEach(z => z.ParentsName =GetOptionValueParents(z.OptionValueId, ""));
//                dbProduct.Prices.ForEach(z => z.Discount = discountRepository.GetQuery().FirstOrDefault(x => x.PriceId == z.PriceId && x.ExpireDate > DateTime.Now));
//                dbProduct.OptionValues = optionValueRepository.GetQuery().Where(z => z.ProductId == dbProduct.ProductId).ToList();
//                dbProduct.VendorOptions = vendorOptionRepository.GetQuery().Where(z => z.ProductId == dbProduct.ProductId).ToList();
//                dbProduct.VendorOptions.ForEach(z => z.OptionName = optionRepository.GetQuery().FirstOrDefault(x => x.OptionId == z.OptionId).Name);

//                var dbViews = viewRepository.GetQuery().Where(z => z.ObjectId == dbProduct.ProductId && z.Type == ViewsType.Product).ToList();
//                long dbViewCount = 0;
//                foreach (var view in dbViews)
//                {
//                    dbViewCount += view.Count;
//                }
//                dbProduct.ViewsCount = dbViewCount;
//            }
//            return dbProducts;
//        }
//        public async Task<object> GetAllRefraction(int pageNumber, int count)
//        {
//            try
//            {
//                var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
//                    ? _accessor.HttpContext.User.Identity.GetUserId()
//                    : 0;

//                var dbPriceIds = discountRepository.GetQuery().Where(z => z.Refraction == true && z.ExpireDate > DateTime.Now).Select(x => x.PriceId).ToList();
//                var dbproIds = priceRepository.GetQuery().Where(z => dbPriceIds.Contains(z.PriceId)).Select(x => x.ProductId).Distinct().ToList();
//                var ProductsCount = productRepository.GetQuery().Where(z => dbproIds.Contains(z.ProductId)).Count();
//                var dbProducts = productRepository.GetQuery().Where(z => dbproIds.Contains(z.ProductId)).OrderByDescending(x => x.CreateDate).Skip((pageNumber - 1) * count).Take(count).ToList();
//                foreach (var dbProduct in dbProducts)
//                {
//                    dbProduct.Prices = priceRepository.GetQuery().Where(z => z.ProductId == dbProduct.ProductId).OrderBy(x => x.PriceValue).ToList();
//                    var optionValuesList = new List<OptionValue>();
//                    foreach (var prc in dbProduct.Prices)
//                    {
//                        prc.Discount = discountRepository.GetQuery().FirstOrDefault(z => z.PriceId == prc.PriceId && z.ExpireDate > DateTime.Now);
//                        var dbValuePrices = valuePriceRepository.GetQuery().Where(z => z.PriceId == prc.PriceId).Select(x => x.OptionValueId).ToList();
//                        prc.OptionValues = optionValueRepository.GetQuery().Where(z => dbValuePrices.Contains(z.OptionValueId)).ToList();
//                        optionValuesList.AddRange(prc.OptionValues);
//                    }
//                    var optionIds = optionValuesList.Select(z => z.OptionId).Distinct().ToList();
//                    dbProduct.Options = optionRepository.GetQuery().Where(z => optionIds.Contains(z.OptionId)).ToList();

//                    foreach (var option in dbProduct.Options)
//                    {
//                        option.OptionValueList = optionValuesList.Where(z => z.OptionId == option.OptionId).DistinctBy(x => x.OptionValueId).ToList();
//                    }

//                    var dbRate = await rateService.GetByProductId(dbProduct.ProductId);
//                    dbProduct.Rate = dbRate.Data.Average;
//                    var proCatName = productCategoryRepository.GetQuery().FirstOrDefault(z => z.ProductCategoryId == dbProduct.ProductCategoryId);
//                    dbProduct.CategoryName = proCatName == null ? "" : proCatName.Name;
//                    var photos = mediaRepository.GetQuery().Where(z => z.ObjectId == dbProduct.ProductId && z.Type == MediaTypes.Product).ToList();
//                    List<FileData> files = new List<FileData>();
//                    foreach (var photo in photos)
//                    {
//                        var file = new FileData()
//                        {
//                            Base64 = photo.PictureUrl,
//                            Priority = (int)photo.Priority
//                        };
//                        files.Add(file);
//                    }
//                    dbProduct.Photos = files;
//                    //dbProduct.Prices=priceRepository.GetQuery().Where(z => z.ProductId == dbProduct.ProductId).ToList();
//                    //dbProduct.Prices.ForEach(z => z.ParentsName =GetOptionValueParents(z.OptionValueId, ""));
//                    dbProduct.Prices.ForEach(z => z.Discount = discountRepository.GetQuery().FirstOrDefault(x => x.PriceId == z.PriceId && x.ExpireDate > DateTime.Now));
//                    dbProduct.OptionValues = optionValueRepository.GetQuery().Where(z => z.ProductId == dbProduct.ProductId).ToList();
//                    dbProduct.VendorOptions = vendorOptionRepository.GetQuery().Where(z => z.ProductId == dbProduct.ProductId).ToList();
//                    dbProduct.VendorOptions.ForEach(z => z.OptionName = optionRepository.GetQuery().FirstOrDefault(x => x.OptionId == z.OptionId).Name);

//                    var dbViews = viewRepository.GetQuery().Where(z => z.ObjectId == dbProduct.ProductId && z.Type == ViewsType.Product).ToList();
//                    long dbViewCount = 0;
//                    foreach (var view in dbViews)
//                    {
//                        dbViewCount += view.Count;
//                    }
//                    dbProduct.ViewsCount = dbViewCount;

//                }
//                var obj = new
//                {
//                    Products = dbProducts,
//                    ProCount = ProductsCount
//                };
//                return obj;
//            }
//            catch (Exception ex)
//            {
//                throw;
//            }

//        }

//        public async Task Delete(long id)
//        {
//            var discount = discountRepository.GetQuery().FirstOrDefault(z => z.DiscountId == id);
//            await discountRepository.Delete(discount);
//        }

//        public async Task<Discount> Get(long id)
//        {
//            var discount = discountRepository.GetQuery().FirstOrDefault(z => z.DiscountId == id);
//            return discount;
//        }

//    }
//}
