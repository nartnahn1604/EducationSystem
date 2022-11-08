using Refit;
using IT008_UIT.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT008_UIT.Queries
{
    public interface IGetSecretMessageQuery
    {
        [Get("/")]
        Task<SecretMessageResponse> Execute();
    }
}
