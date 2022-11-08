using Firebase.Auth;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using IT008_UIT.Stores;
namespace IT008_UIT.Http
{
    public class FirebaseAuthHttpMessageHandler : DelegatingHandler
    {
        private readonly AuthenticationStore _authenticationStore;

        public FirebaseAuthHttpMessageHandler(AuthenticationStore authenticationStore)
        {
            _authenticationStore = authenticationStore;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            FirebaseAuthLink firebaseAuthLink = await _authenticationStore.GetFreshAuthAsync();

            if(firebaseAuthLink != null)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", firebaseAuthLink.FirebaseToken);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
