using System;
using System.Collections.Generic;
using Common.Domain;

namespace Common.Networking
{
    [Serializable]
    public class GetAllShowsResponse:Response
    {
        private IList<Show> shows;

        public GetAllShowsResponse(IList<Show> shows)
        {
            this.shows = shows;
        }

        public IList<Show> Shows
        {
            get => shows;
            set => shows = value;
        }
    }
}