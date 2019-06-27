using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Arena42.Services.EF.Configuration;

namespace Arena42.Services
{
    public class Adriana42Context : DbContext
    {
        private readonly object _lockObj = new object();


        #region Ctor
        /// <summary>
        /// Specify the name of the connection string
        /// </summary>
        public Adriana42Context()
            : base("adriana42")
        {
            this.Configuration.UseDatabaseNullSemantics = true;
            this.Configuration.LazyLoadingEnabled = true;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Configure each entity 
        /// </summary>
        /// <param name="modelBuilder">Define the model for the context being created</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<Adriana42Context>(null);

            modelBuilder.Configurations.Add(new TournamentMapping());
            modelBuilder.Configurations.Add(new MarketMapping());


            // modelBuilder.Configurations.Add(new TournamentResultMapping());

        }


        #endregion
    }
}