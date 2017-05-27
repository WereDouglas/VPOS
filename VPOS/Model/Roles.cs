﻿using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPOS.Model
{
    public class Roles
    {
        private string id;
        private string title;
        private string views;
        private string actions;
        private string created;
        private string orgID;

        public string Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public string Title
        {
            get
            {
                return title;
            }

            set
            {
                title = value;
            }
        }

        public string Views
        {
            get
            {
                return views;
            }

            set
            {
                views = value;
            }
        }

        public string Actions
        {
            get
            {
                return actions;
            }

            set
            {
                actions = value;
            }
        }

        public string Created
        {
            get
            {
                return created;
            }

            set
            {
                created = value;
            }
        }

        public string OrgID
        {
            get
            {
                return orgID;
            }

            set
            {
                orgID = value;
            }
        }

        public Roles(string id, string title, string views, string actions, string created, string orgID)
        {
            this.Id = id;
            this.Title = title;
            this.Views = views;
            this.Actions = actions;
            this.Created = created;
            this.OrgID = orgID;
        }

        public static List<Roles> ListRoles()
        {
            DBConnect.OpenConn();
            List<Roles> categories = new List<Roles>();
            string SQL = "SELECT * FROM roles";
            NpgsqlCommand command = new NpgsqlCommand(SQL, DBConnect.conn);
            NpgsqlDataReader Reader = command.ExecuteReader();
            while (Reader.Read())
            {
                Roles p = new Roles(Reader["id"].ToString(), Reader["title"].ToString(), Reader["views"].ToString(), Reader["actions"].ToString(), Reader["created"].ToString(), Reader["orgID"].ToString());
                categories.Add(p);
            }
            DBConnect.CloseConn();
            return categories;

        }
    }



}