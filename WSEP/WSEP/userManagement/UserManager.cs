﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WSEP.userManagement
{
    public class UserManager : IUserManager
    {

        // add logger

        private User _superAdmin;
        private List<Tuple<string, List<User>>> _forumsMembers; // forum name and a list of members
        private List<Tuple<string, List<User>>> _forumsAdmins; // forum name and a list of admins
        private List<Tuple<string, string, List<User>>> _subForumsModerators; // forum name, sub forum name and a list of moderators

        private const string SUCCESS = "true";
        private const string FUNCTION_ERRROR = "An error has occured with C# internal function.";
        private const string INVALID_FORUM_NAME = "Invalid forum name. Forum name cannot be null, \"null\" or empty.";
        private const string INVALID_SUB_FORUM_NAME = "Invalid sub forum name. Sub forum name cannot be null, \"null\" or empty.";
        private const string INVALID_USERNAME = "Invalid username. Username cannot be null, \"null\" or empty.";
        private const string WRONG_FORUM_NAME = "Wrong forum name. No forum found with such a name.";
        private const string WRONG_USERNAME = "Wrong username. There is no member of this forum with that username.";
        private const string WRONG_SUB_FORUM_NAME = "Wrong sub forum name. No sub forum found with such a name found in this forum.";
        private const string ILLEGAL_ACTION = "Action is illegal according to forum policy.";

        public UserManager()
        {
            _forumsMembers = new List<Tuple<string, List<User>>>();
            _forumsAdmins = new List<Tuple<string, List<User>>>();
            _subForumsModerators = new List<Tuple<string, string, List<User>>>();
            // WTF should I do with the super admin?
            _superAdmin = User.create("superAdmin", "superAdmin", "superAdmin@gmail.com");
        }

        public string addForum(string forumName)
        {
            string forumNameTaken = "This forum name is already in use. Please select another name.";
            if (forumName == null || forumName.Equals("null") || forumName.Equals(""))
            {
                return INVALID_FORUM_NAME;
            }
            // verify there is no forum with that name
            foreach (Tuple<string, List<User>> list in _forumsMembers)
            {
                if (list.Item1.Equals(forumName))
                {
                    return forumNameTaken;
                }
            }
            foreach (Tuple<string, List<User>> list in _forumsAdmins)
            {
                if (list.Item1.Equals(forumName))
                {
                    return forumNameTaken;
                }
            }
            // add new forum lists to the DB
            List<User> admins = new List<User>();
            admins.Add(_superAdmin);
            if (!admins.Contains(_superAdmin))
            {
                return FUNCTION_ERRROR;
            }
            Tuple<string, List<User>> newForumAdmins = new Tuple<string, List<User>>(forumName, admins);
            _forumsAdmins.Add(newForumAdmins);
            if (!_forumsAdmins.Contains(newForumAdmins))
            {
                return FUNCTION_ERRROR;
            }
            Tuple<string, List<User>> newForumMembers = new Tuple<string, List<User>>(forumName, new List<User>());
            _forumsMembers.Add(newForumMembers);
            if (!_forumsMembers.Contains(newForumMembers))
            {
                return FUNCTION_ERRROR;
            }
            return SUCCESS;
        }

        public string addSubForum(string forumName, string subForumName, List<string> setModerators, int minNumOfModerators, int maxNumOfModerators)
        {
            string subForumNameTaken = "This sub forum name is already in use in that forum. Please select another name.";
            if (forumName == null || forumName.Equals("null") || forumName.Equals(""))
            {
                return INVALID_FORUM_NAME;
            }
            if (subForumName == null || subForumName.Equals("null") || subForumName.Equals(""))
            {
                return INVALID_SUB_FORUM_NAME;
            }
            if (setModerators == null || setModerators.Contains(null) || setModerators.Contains("null") || setModerators.Contains(""))
            {
                return INVALID_USERNAME;
            }
            if (setModerators.Count < minNumOfModerators || setModerators.Count > maxNumOfModerators)
            {
                return ILLEGAL_ACTION;
            }
            // verify there is a forum with that name
            List<User> admins = getForumAdmins(forumName);
            List<User> members = getForumMembers(forumName);
            if (admins == null || members == null)
            {
                return WRONG_FORUM_NAME;
            }
            // verify there is no sub forum with that name under a forum with that name
            foreach (Tuple<string, string, List<User>> subForum in _subForumsModerators)
            {
                if (subForum.Item1.Equals(forumName) && subForum.Item2.Equals(subForumName))
                {
                    return subForumNameTaken;
                }
            }
            // verify all new moderators are registered to the forum and add to moderators list
            List<User> moderators = new List<User>();
            foreach (string newModerator in setModerators)
            {
                User user = getUser(admins, newModerator);
                if (user == null)
                {
                    user = getUser(members, newModerator);
                }
                if (user == null)
                {
                    return "Moderators list contains a non existing user: " + newModerator;
                }
                moderators.Add(user);
                if (!moderators.Contains(user))
                {
                    return FUNCTION_ERRROR;
                }
            }
            // add new sub forum list to the DB
            Tuple<string, string, List<User>> newSubForumModerators = new Tuple<string,string,List<User>>(forumName, subForumName, moderators);
            _subForumsModerators.Add(newSubForumModerators);
            if (!_subForumsModerators.Contains(newSubForumModerators))
            {
                return FUNCTION_ERRROR;
            }
            return SUCCESS;
        }

        // should be synchronized
        [MethodImpl(MethodImplOptions.Synchronized)]
        public string registerMemberToForum(string forumName, string username, string password, string eMail)
        {
            string invalidPassword = "Invalid password. Password cannot be null, \"null\" or empty, or contain any of the following:\nSpace";
            string invalidEMail = "Invalid eMail. eMail cannot be null, \"null\" or empty.";
            string usernameTaken = "This username is already in use in that forum. Please select another name.";
            string wrongEMail = "Illegal eMail address. Please enter a corrent eMail address.";
            string eMailTaken = "This eMail address is already in use in that forum. Please enter another eMail address.";
            if (forumName == null || forumName.Equals("null") || forumName.Equals(""))
            {
                return INVALID_FORUM_NAME;
            }
            if (username == null || username.Equals("null") || username.Equals(""))
            {
                return INVALID_USERNAME;
            }
            if (password == null || password.Equals("null") || password.Equals("") || password.Contains(" "))
            {
                return invalidPassword;
            }
            if (eMail == null || eMail.Equals("null") || eMail.Equals(""))
            {
                return invalidEMail;
            }
            if (username.IndexOf(' ') == 0)
            {
                return "Username cannot begin with a space.";
            }
            #region valid username (also verifies valid forum name)
            List<User> admins = getForumAdmins(forumName);
            List<User> members = getForumMembers(forumName);
            // if list not found return false (wrong forum name)
            if (admins == null || members == null)
            {
                return WRONG_FORUM_NAME;
            }
            // check if there is an admin with that username
            foreach (User admin in admins)
            {
                if (username.Equals(admin.getUsername()))
                {
                    return usernameTaken;
                }
                if (eMail.Equals(admin.getEMail()))
                {
                    return eMailTaken;
                }
            }
            // check if there is a member with that username
            foreach (User member in members)
            {
                if (username.Equals(member.getUsername()))
                {
                    return usernameTaken;
                }
                if (eMail.Equals(member.getEMail()))
                {
                    return eMailTaken;
                }
            }
            #endregion
            // verify password match the forum's policy
            #region valid eMail
            if (!eMail.Contains("@"))
            {
                return wrongEMail;
            }
            if (eMail.IndexOf('@') == 0)
            {
                return wrongEMail;
            }
            string eMailSuffix = eMail.Substring(eMail.IndexOf('@') + 1);
            if (eMailSuffix.Contains("@") || !eMailSuffix.Contains(".") || eMailSuffix.IndexOf('.') == 0 || eMailSuffix.IndexOf('.') == eMailSuffix.Length - 1)
            {
                return wrongEMail;
            }
            #endregion
            User newMember = User.create(username, password, eMail);
            members.Add(newMember);
            if (!members.Contains(newMember))
            {
                return FUNCTION_ERRROR;
            }
            return SUCCESS;
        }

        public string assignAdmin(string forumName, string username, int maxNumOfAdmins)
        {
            string inputStatus = adminsAssignmentInputValidation(forumName, username);
            if (!inputStatus.Equals(SUCCESS))
            {
                return inputStatus;
            }
            List<User> admins = getForumAdmins(forumName);
            List<User> members = getForumMembers(forumName);
            if (admins == null || members == null)
            {
                return WRONG_FORUM_NAME;
            }
            User admin = getAdmin(admins, username);
            // if found return true
            if (admin != null)
            {
                return SUCCESS;
            }
            User member = getMember(members, username);
            // if not found return false
            if (member == null)
            {
                return WRONG_USERNAME;
            }
            // add user to list of admins
            if (admins.Count >= maxNumOfAdmins)
            {
                return ILLEGAL_ACTION;
            }
            admins.Add(member);
            // if not added return false
            if (!admins.Contains(member))
            {
                return FUNCTION_ERRROR;
            }
            // remove user from regular members list
            members.Remove(member);
            // if not removed throw exception - fatal error
            if (members.Contains(member))
            {
                string str1 = "User " + username + " was added as an admin to forum " + forumName + " but could not be removed from the regular members list.\n";
                string str2 = "This created an illegal situation where the same user was in both data bases where a user can only be in one.\n";
                string str3 = "Hence a fatal error occured.";
                throw new Exception (str1 + str2 + str3);
            }
            // success
            return SUCCESS;
        }

        public string unassignAdmin(string forumName, string username, int minNumOfAdmins)
        {
            string inputStatus = adminsAssignmentInputValidation(forumName, username);
            if (!inputStatus.Equals(SUCCESS))
            {
                return inputStatus;
            }
            if (username.Equals(_superAdmin.getUsername()))
            {
                return "Cannot unassign the super admin.";
            }
            List<User> admins = getForumAdmins(forumName);
            List<User> members = getForumMembers(forumName);
            if (admins == null || members == null)
            {
                return WRONG_FORUM_NAME;
            }
            User admin = getAdmin(admins, username);
            // if not found return true
            if (admin == null)
            {
                return SUCCESS;
            }
            if (admins.Count <= minNumOfAdmins)
            {
                return ILLEGAL_ACTION;
            }
            // add user to regular members list
            members.Add(admin);
            // if not added return false
            if (!members.Contains(admin))
            {
                return FUNCTION_ERRROR;
            }
            // remove user from admins list
            admins.Remove(admin);
            // if not removed throw exception - fatal error
            if (admins.Contains(admin))
            {
                string str1 = "User " + username + " is no longer an admin to forum " + forumName + " and added to the regular members list, but could not be removed from the admins list.\n";
                string str2 = "This created an illegal situation where the same user was in both data bases where a user can only be in one.\n";
                string str3 = "Hence a fatal error occured.";
                throw new Exception(str1 + str2 + str3);
            }
            // success
            return SUCCESS;
        }

        public string assignModerator(string forumName, string subForumName, string username, int maxNumOfModerators)
        {
            string inputStatus = moderatorsAssignmentInputValidation(forumName, subForumName, username);
            if (!inputStatus.Equals(SUCCESS))
            {
                return inputStatus;
            }
            // verify correct forum name
            List<User> admins = getForumAdmins(forumName);
            List<User> members = getForumMembers(forumName);
            if (admins == null || members == null)
            {
                return WRONG_FORUM_NAME;
            }
            // verify user exists in this forum
            User moderator = getAdmin(admins, username);
            if (moderator == null)
            {
                moderator = getMember(members, username);
            }
            if (moderator == null)
            {
                return WRONG_USERNAME;
            }
            // verify correct sub forum name
            List<User> moderators = getSubForumModerators(forumName, subForumName);
            if (moderators == null)
            {
                return WRONG_SUB_FORUM_NAME;
            }
            // if user is a moderator return success
            if (moderators.Contains(moderator))
            {
                return SUCCESS;
            }
            if (moderators.Count >= maxNumOfModerators)
            {
                return ILLEGAL_ACTION;
            }
            // set user as moderator
            moderators.Add(moderator);
            if (!moderators.Contains(moderator))
            {
                return FUNCTION_ERRROR;
            }
            return SUCCESS;
        }

        public string unassignModerator(string forumName, string subForumName, string username, int minNumOfModerators)
        {
            string inputStatus = moderatorsAssignmentInputValidation(forumName, subForumName, username);
            if (!inputStatus.Equals(SUCCESS))
            {
                return inputStatus;
            }
            // verify correct forum name
            List<User> members = getForumMembers(forumName);
            if (members == null)
            {
                return WRONG_FORUM_NAME;
            }
            // verify correct sub forum name
            List<User> moderators = getSubForumModerators(forumName, subForumName);
            if (moderators == null)
            {
                return WRONG_SUB_FORUM_NAME;
            }
            // if username is not a moderator than it is a success
            User moderator = getUser(moderators, username);
            if (moderator == null)
            {
                return SUCCESS;
            }
            if (moderators.Count <= minNumOfModerators)
            {
                return ILLEGAL_ACTION;
            }
            // remove user from moderators list
            moderators.Remove(moderator);
            if (moderators.Contains(moderator))
            {
                return FUNCTION_ERRROR;
            }
            return SUCCESS;
        }

        public permission getUserPermissionsForForum(string forumName, string username)
        {
            string inputStatus = adminsAssignmentInputValidation(forumName, username);
            if (!inputStatus.Equals(SUCCESS))
            {
                return permission.INVALID;
            }
            // verify correct forum name
            List<User> admins = getForumAdmins(forumName);
            List<User> members = getForumMembers(forumName);
            if (admins == null || members == null)
            {
                return permission.INVALID;
            }
            // check if the user is the super admin
            if (username.Equals(_superAdmin.getUsername()))
            {
                return permission.SUPER_ADMIN;
            }
            // check if the user is an admin
            User user = getUser(admins, username);
            if (user != null)
            {
                return permission.ADMIN;
            }
            // check if the user is a member
            user = getUser(members, username);
            if (user != null)
            {
                return permission.MEMBER;
            }
            return permission.GUEST;
        }

        public permission getUserPermissionsForSubForum(string forumName, string subForumName, string username)
        {
            string inputStatus = moderatorsAssignmentInputValidation(forumName, subForumName, username);
            if (!inputStatus.Equals(SUCCESS))
            {
                return permission.INVALID;
            }
            // verify correct forum name
            List<User> admins = getForumAdmins(forumName);
            List<User> moderators = getSubForumModerators(forumName, subForumName);
            List<User> members = getForumMembers(forumName);
            if (admins == null || members == null || moderators == null)
            {
                return permission.INVALID;
            }
            // check if the user is the super admin
            if (username.Equals(_superAdmin.getUsername()))
            {
                return permission.SUPER_ADMIN;
            }
            // check if the user is an admin
            User user = getUser(admins, username);
            if (user != null)
            {
                return permission.ADMIN;
            }
            // check if the user is a moderator
            user = getUser(moderators, username);
            if (user != null)
            {
                return permission.MODERATOR;
            }
            // check if the user is a member
            user = getUser(members, username);
            if (user != null)
            {
                return permission.MEMBER;
            }
            return permission.GUEST;
        }

        public string sendPM(string forumName, string from, string to, string msg)
        {
            if (forumName == null || forumName.Equals("null") || forumName.Equals(""))
            {
                return INVALID_FORUM_NAME;
            }
            if (from == null || from.Equals("null") || from.Equals(""))
            {
                return INVALID_USERNAME;
            }
            if (to == null || to.Equals("null") || to.Equals(""))
            {
                return INVALID_USERNAME;
            }
            if (msg == null || msg.Equals("null") || msg.Equals(""))
            {
                return INVALID_USERNAME;
            }
            // get both users
            List<User> admins = getForumAdmins(forumName);
            List<User> members = getForumMembers(forumName);
            if (admins == null || members == null)
            {
                return WRONG_FORUM_NAME;
            }
            User sender = getUser(admins, from);
            if (sender == null)
            {
                sender = getUser(members, from);
            }
            if (sender == null)
            {
                return from + " - " + WRONG_USERNAME;
            }
            User receiver = getUser(admins, to);
            if (receiver == null)
            {
                receiver = getUser(members, to);
            }
            if (receiver == null)
            {
                return to + " - " + WRONG_USERNAME;
            }
            // add msg to both users PM collections
            if (!receiver.getMessage(from, msg).Equals(SUCCESS))
            {
                return FUNCTION_ERRROR;
            }
            if (!sender.sendMessage(to, msg).Equals(SUCCESS))
            {
                string str1 = "Private Message was added only to one user collection of private messages.\n";
                string str2 = "This  caused a critical error.\n";
                throw new Exception(str1 + str2);
            }
            return SUCCESS;
        }

        public string checkForumPolicy(string forumName, int minNumOfAdmins, int maxNumOfAdmins)
        {
            if (forumName == null || forumName.Equals("null") || forumName.Equals(""))
            {
                return INVALID_FORUM_NAME;
            }
            List<User> admins = getForumAdmins(forumName);
            if (admins == null)
            {
                return WRONG_FORUM_NAME;
            }
            if (admins.Count < minNumOfAdmins)
            {
                return "Forum have " + admins.Count + " admins. Cannot edit policy so minimum amout of admin will be higher than current amount.";
            }
            if (admins.Count > maxNumOfAdmins)
            {
                return "Forum have " + admins.Count + " admins. Cannot edit policy so maximum amout of admin will be lower than current amount.";
            }
            return SUCCESS;
            //Thanks Gal, glhf 
            //only need to check stuff like if the minAdmins is 2 and there is 
            //currently only one, shit like that. 
            //also accordring to UC3, 2.1.2 the user needs to be presented with
            //which attribute of the new policy creates a problem, so i know you
            //won't like it, but could you just throw an exception such as:
            //throw new Exception("Cannot set new policy - Conflicting minimum number of Moderators");
            //and if everything is okay return true?
            //Thanks!
            //Roy
        }

        private List<User> getForumAdmins(string forumName)
        {
            List<User> admins = null;
            foreach (Tuple<string, List<User>> t in _forumsAdmins)
            {
                if (forumName.Equals(t.Item1))
                {
                    admins = t.Item2;
                    break;
                }
            }
            return admins;
        }

        private List<User> getForumMembers(string forumName)
        {
            List<User> members = null;
            foreach (Tuple<string, List<User>> t in _forumsMembers)
            {
                if (forumName.Equals(t.Item1))
                {
                    members = t.Item2;
                    break;
                }
            }
            return members;
        }

        private User getAdmin(List<User> admins, string username)
        {
            User admin = null;
            foreach (User u in admins)
            {
                if (username.Equals(u.getUsername()))
                {
                    admin = u;
                    break;
                }
            }
            return admin;
        }

        private User getMember(List<User> members, string username)
        {
            User member = null;
            foreach (User u in members)
            {
                if (username.Equals(u.getUsername()))
                {
                    member = u;
                    break;
                }
            }
            return member;
        }

        private List<User> getSubForumModerators(string forumName, string subForumName)
        {
            List<User> moderators = null;
            foreach (Tuple<string, string, List<User>> subForumModerators in _subForumsModerators)
            {
                if (forumName.Equals(subForumModerators.Item1) && subForumName.Equals(subForumModerators.Item2))
                {
                    moderators = subForumModerators.Item3;
                    break;
                }
            }
            return moderators;
        }

        private User getUser(List<User> users, string username)
        {
            User user = null;
            foreach (User u in users)
            {
                if (username.Equals(u.getUsername()))
                {
                    user = u;
                    break;
                }
            }
            return user;
        }

        private string adminsAssignmentInputValidation(string forumName, string username)
        {
            if (forumName == null || forumName.Equals("null") || forumName.Equals(""))
            {
                return INVALID_FORUM_NAME;
            }
            if (username == null || username.Equals("null") || username.Equals(""))
            {
                return INVALID_USERNAME;
            }
            return SUCCESS;
        }

        private string moderatorsAssignmentInputValidation(string forumName, string subForumName, string username)
        {
            if (forumName == null || forumName.Equals("null") || forumName.Equals(""))
            {
                return INVALID_FORUM_NAME;
            }
            if (subForumName == null || subForumName.Equals("null") || subForumName.Equals(""))
            {
                return INVALID_SUB_FORUM_NAME;
            }
            if (username == null || username.Equals("null") || username.Equals(""))
            {
                return INVALID_USERNAME;
            }
            return SUCCESS;
        }
    }
}
