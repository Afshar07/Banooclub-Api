using System;

namespace BanooClub.Models
{
    public class CNTRY : BaseEntity
    {
        public long CountryId { get; set; }
        public string Name { get; set; }
        public string iso3 { get; set; }
        public string NumericCode { get; set; }
        public string iso2 { get; set; }
        public string PhoneCode { get; set; }
        public string Capital { get; set; }
        public string Currency { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencySymbol { get; set; }
        public string Tld { get; set; }
        public string Native { get; set; }
        public string Region { get; set; }
        public string Subregion { get; set; }
        public string TimeZones { get; set; }
        public string Translations { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public string Emoji { get; set; }
        public string EmojiU { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int Flag { get; set; }
        public string WikiDataId { get; set; }
    }
}
