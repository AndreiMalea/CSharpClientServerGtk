using System;
using Common.Domain;

namespace Common.Networking
{
    [Serializable]
    public class FindOneResponse:Response
    {
        private Show _show;

        public Show Show
        {
            get => _show;
            set => _show = value;
        }

        public FindOneResponse(Show show)
        {
            _show = show;
        }
    }
}