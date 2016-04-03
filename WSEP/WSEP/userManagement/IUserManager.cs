﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSEP.userManagement
{
    public interface IUserManager
    {
        bool addForum(string forumName);
        bool addSubForum(string forumName, string subForumName, string adminUsername);
        bool registerMemberToForum(string forumName, string username, string password, string eMail);
        bool aggainAdmin(string forumName, string username);
        bool unassignAdmin(string forumName, string username);
        bool assignModerator(string forumName, string subForumName, string username);
        bool unassignModerator(string forumName, string subForumName, string username);
        void getUserPermissionsForForum(string forumName, string username);
        void getUserPermissionsForSubForum(string forumName, string subForumName, string username);
    }
}
