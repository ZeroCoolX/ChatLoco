﻿
namespace ChatLoco.Models.User_Model
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public RoleLevel Role { get; set; }
        public UserSettingsModel Settings { get; set; }
        public bool IsActivated { get; set; }
    }
    public enum RoleLevel
    {
        User, Admin, Blocked
    };
}
