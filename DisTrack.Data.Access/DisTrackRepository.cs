using DisTrack.Data.Access.RepositoryInterfaces;
using Microsoft.Extensions.Configuration;
using Rubrics.Data.Access;
using System;
using System.Collections.Generic;
using System.Text;

namespace DisTrack.Data.Access
{
    public class DisTrackRepository : GenericRepository<Context>, IDisTrackRepository, IDisposable
    {
        public Context DisTrackContext { get => this.Context; }
        public DisTrackRepository() : base()
        {

        }

        public DisTrackRepository(IConfiguration configuration) : base()
        {
            this.Context = new Context(configuration);
        }

        protected virtual void Dispose()
        {
            this.Context.Dispose();
        }

        ~DisTrackRepository()
        {
            this.Context.Dispose();
        }
    }
}
