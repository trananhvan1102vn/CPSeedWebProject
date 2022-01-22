using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CPSeed.Helpers {
    public class ReCaptchaHelper {
        //Function ... you can create it in a different class  
        public static ReCaptchaResponse VerifyCaptcha(string secret, string request) {
            if (request != null) {
                using (System.Net.Http.HttpClient hc = new System.Net.Http.HttpClient()) {
                    var values = new Dictionary<string, string> {
                        {"secret", secret},
                        {"response", request}
                    };
                    var content = new System.Net.Http.FormUrlEncodedContent(values);
                    var Response = hc.PostAsync("https://www.google.com/recaptcha/api/siteverify", content).Result;
                    var responseString = Response.Content.ReadAsStringAsync().Result;
                    if (!string.IsNullOrWhiteSpace(responseString)) {
                        ReCaptchaResponse response = JsonConvert.DeserializeObject<ReCaptchaResponse>(responseString);
                        return response;
                    } else {
                        //Throw error as required  
                    }
                }
            } else {
                //Throw error as required
            }
            return null;
        }
    }
    //You can use http://json2csharp.com/ to create C# classes from JSON  
    //Note error-codes is an invalid name for a variable in C# so we use _ and then add JsonProperty to it  
    public class ReCaptchaResponse {
        public bool success {
            get;
            set;
        }
        public string challenge_ts {
            get;
            set;
        }
        public string hostname {
            get;
            set;
        }
        [JsonProperty(PropertyName = "error-codes")]
        public List<string> error_codes {
            get;
            set;
        }
    }
}