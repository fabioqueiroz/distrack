﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using DisTrack.Data;

namespace Rubrics.Data.Access
{
    public class Context : DbContext
    {
        public IConfiguration Configuration { get; }

        public Context() : base()
        {

        }
        public Context(IConfiguration configuration) : base()
        {
            Configuration = configuration;
        }

        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

        public Context(DbContextOptions<Context> options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    // local connection
                    //.UseSqlServer("Data Source=LAPTOP-JEDG5RJB\\LAPSQLSERVER;Initial Catalog=DisTrack;Integrated Security=False;User Id=sa;Password=fabio1980;MultipleActiveResultSets=True");
                    .UseSqlServer(Configuration.GetConnectionString("DefaultConnection")); // TODO: add appsettings.json to gitignore

                    // azure
                    //.UseSqlServer("Server=tcp:distrack.database.windows.net,1433;Initial Catalog=distrackdb;Persist Security Info=False;User ID=fabioqueiroz;Password=Fabio1980!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }


        // tables for the database
        public DbSet<User> Users { get; set; }
        public DbSet<Trip> Trips { get; set; }

    }
}

