//using Infra.oAuthService;

//namespace Troupon.Catalog.Api.Test
//{
//    public class BaseTest
//    {
//        //protected Client _client { get; private set; }
//        //protected AssetteApiClient _client { get; private set; }//Uncomment to use assette API
//        protected IClient _client { get; private set; }//Uncomment to use Addenda API
//        protected Generators _generators;

//        public BaseTest() : base()
//        {
//            //InitSDKClient(TargetApi.Addenda).Wait();// Choose Assette Enum to run on Assette API
//            //InitAssetteSDKClient().Wait();
//            _client = InitAddendaSDKClient().Result;
//            _generators = new Generators();
//        }
//        private async Task<IClient> InitAddendaSDKClient()
//        {
//            var authSettings = new APIKeySettings()
//            {
//                ClientId = "SECRET",
//                ClientSecret = "SECRET",
//                TokenUrl = "https://dev-SECRET.okta.com/oauth2/default/v1/token"
//            };
//            var manager = new PortalClientSDKManager();
//            return await manager.GetAddendaClient(authSettings, "https://localhost:44324/");//Uncomment for Addenda API

//        }

//    }
//}


