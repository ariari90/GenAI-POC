using Common;
using Common.Entities;
using JWT;
using JWT.Algorithms;
using JWT.Exceptions;
using JWT.Serializers;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Xml.Serialization;

namespace AggregatorSvcAuth
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class AggregatorAuthService : IAggregatorAuthService
    {
        private readonly string _secret;
        private readonly int _expireDuration;
        private readonly IAggregatorLog _log;

        public AggregatorAuthService()
        {
            _secret = ConfigurationManager.AppSettings["Secret"];
            _expireDuration = Convert.ToInt32(ConfigurationManager.AppSettings["ExpireDuration"]);
            _log = new AggregatorLog<AggregatorAuthService>();
        }

        public string GetToken(Stream data)
        {
            _log.LogMessage("GetToken started.");
            AuthInfo authInfo = GetDataFromStream<AuthInfo>(data); ;
            
            LoginService.LoginServiceClient loginService = new LoginService.LoginServiceClient();
            if(!loginService.Authenticate(authInfo))
            {
                _log.LogError("GetToken failed: Invalid username or password.");
                throw new WebFaultException(System.Net.HttpStatusCode.Unauthorized);
            }

            _log.LogMessage("Generating token");
            return GenerateToken(authInfo.UserName);
        }

        public AggregatorResponse GetData(Stream data)
        {
            _log.LogMessage("GetData started.");
            AggregatorResponse response = null;

            AggregatorRequest request = GetDataFromStream<AggregatorRequest>(data);

            if (!AuthenticateJWT(WebOperationContext.Current.IncomingRequest))
            {
                _log.LogError("GetData: Unauthorised access: JWT token rejected");
                throw new WebFaultException(System.Net.HttpStatusCode.Unauthorized);
            }

            _log.LogMessage("Initiating orchestration process");
            AggregatorSvc.AggregatorSvcClient client = new AggregatorSvc.AggregatorSvcClient();
            try
            {
                response = client.GetData(request);
                _log.LogMessage("Response received.");
            }
            catch (FaultException e)
            {
                throw new WebFaultException<string>(e.Message, HttpStatusCode.InternalServerError);
            }
            catch (Exception e)
            {
                throw new WebFaultException(HttpStatusCode.InternalServerError);
            }
            return response;
        }

        private T GetDataFromStream<T>(Stream data)
        {
            StreamReader reader = new StreamReader(data);

            string xmlString = reader.ReadToEnd();

            using (var stringReader = new System.IO.StringReader(xmlString))
            {
                var xmlSerializer = new XmlSerializer(typeof(T));
                return (T) xmlSerializer.Deserialize(stringReader);
            }
        }

        private string GenerateToken(string userName)
        {
            IDateTimeProvider provider = new UtcDateTimeProvider();
            var now = provider.GetNow().AddMinutes(_expireDuration); // token to expire after _expireDuration minutes

            double secondsSinceEpoch = UnixEpoch.GetSecondsSince(now);

            var payload = new Dictionary<string, object>
            {
                { "claim", userName },
                { "exp", secondsSinceEpoch }
            };

            var keyBytes = System.Text.Encoding.UTF8.GetBytes(_secret);

            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

            var token = encoder.Encode(payload, keyBytes);
            Console.WriteLine(token);
            return token;
        }

        private bool IsJWTTokenValid(string token)
        {
            
            bool isValid = false;

            string json = String.Empty;
            try
            {
                var serializer = new JsonNetSerializer();
                var provider = new UtcDateTimeProvider();
                IJwtValidator validator = new JwtValidator(serializer, provider);
                var urlEncoder = new JwtBase64UrlEncoder();
                IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
                IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder, algorithm);

                json = decoder.Decode(token, _secret, verify: true);
                if(!String.IsNullOrEmpty(json))
                {
                    isValid = true;
                }
                Console.WriteLine(json);
            }
            catch (TokenNotYetValidException)
            {
                _log.LogError("Token is not valid yet");
                isValid = false;
            }
            catch (TokenExpiredException)
            {
                _log.LogError("Token has expired");
                isValid = false;
            }
            catch (SignatureVerificationException)
            {
                _log.LogError("Token has invalid signature");
                isValid = false;
            }
            catch (InvalidTokenPartsException)
            {
                _log.LogError("Token has an invalid part");
                isValid = false;
            }
            _log.LogMessage("JWT token accepted.");
            return isValid;
        }

        private bool AuthenticateJWT(IncomingWebRequestContext context)
        {
            string authHeader = context.Headers["Authorization"];

            if (String.IsNullOrEmpty(authHeader))
            {
                return false;
            }

            if(!authHeader.Contains("Bearer "))
            {
                return false;
            }

            string token = authHeader.Replace("Bearer ", "");

            return IsJWTTokenValid(token);
        }
        
    }
}
