using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CincyGeeksWebsite.Models.Shared
{
    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        [UIHint("Timezone")]
        public String Timezone { get; set; }
        public string PhoneNumber { get; set; }
        public string SteamHandle { get; set; }
        public bool ListInDirectory { get; set; }
        public string Signature { get; set; }
        public string AvatarFileName { get; set; }
        [UIHint("Date")]
        public DateTime? BanExpireDate { get; set; }

        public UserProfile() { }

        public UserProfile(CGDataEntities.UserProfile profileEntity)
        {
            this.AvatarFileName = profileEntity.AvatarFileName;
            this.BanExpireDate = profileEntity.BanExpireDate;
            this.Email = profileEntity.Email;
            this.ListInDirectory = profileEntity.ListInDirectory;
            this.PhoneNumber = profileEntity.PhoneNumber;
            this.Signature = profileEntity.Signature;
            this.SteamHandle = profileEntity.SteamHandle;
            this.Timezone = profileEntity.Timezone;
            this.UserId = profileEntity.UserId;
            this.UserName = profileEntity.UserName;
        }
    }
}