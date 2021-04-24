using System;
using System.Collections.Generic;
using Common.Domain;

namespace Common.Networking
{
    [Serializable]
    public class GetAllByDateResponse:Response
    {
        private IList<Show> _shows;

        public IList<Show> Shows
        {
            get => _shows;
            set => _shows = value;
        }

        public GetAllByDateResponse(IList<Show> shows)
        {
            _shows = shows;
        }
    }
}