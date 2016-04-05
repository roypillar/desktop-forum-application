﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    namespace WSEP.forumManagement.forumHandler
    {
        public class SubForumPolicy
        {
            private string name;
            public string Name
            {
                get
                {
                    return name;
                }

                set
                {
                    checkPolicyName(value);
                    name = value;
                }
            }


            private int minModerators;
            public int MinModerators
            {
                get { return minModerators; }
                set
                {
                    if (value < 1)
                        throw new Exception(
                            "Minimum number of moderators cannot be smaller than 1.");

                    minModerators = value;
                }
            }

            private int maxModerators;
            public int MaxModerators
            {
                get { return maxModerators; }
                set
                {
                    if (value < minModerators)
                        throw new Exception(
                            "Maximum number of moderators cannot be smaller than minimal number of moderators");
                    maxModerators = value;
                }
            }

            private string subForumRules;
            public string SubForumRules
            {
                get { return subForumRules; }
                set { subForumRules = value; }
            }

            public SubForumPolicy(string name)
            {
                checkPolicyName(name);
                this.Name = name;
                this.MinModerators = 1;
                this.MaxModerators = 10;//default values
                this.SubForumRules = "Have Fun!";

            }


            private void checkPolicyName(string name)
            {
                if (name == null)
                    throw new InvalidNameException("Name of the policy cannot be null");

                if (name.Equals(""))
                    throw new InvalidNameException("Name of the policy cannot be empty");

                if (name.Contains("%") || name.Contains("&") || name.Contains("@"))
                    throw new InvalidNameException("Name of the policy contains illegal character");

                if (name[0].Equals(' '))
                    throw new InvalidNameException("Name of the policy cannot begin with a space character");

            }

        }
    }


