using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Security.Cryptography;
using System.IdentityModel.Tokens.Jwt;
using RestSharp;

namespace Upbit_proj.Models.Api
{
    public class Param
    {

        private DateTime dt_1970_01_01;   //timestamp를 계산하기 위한 변수
        private const string baseUrl = "https://api.upbit.com";


        public Param()
        {
            this.dt_1970_01_01 = new DateTime(1970, 01, 01);
        }

        public string Get(string path, Dictionary<string, string> parameters, Method method)
        {

            StringBuilder queryStringSb = GetQueryString(parameters);

            queryStringSb.Insert(0, "?");      // 링크에 ?를 붙임으로 파라미터를 사용한다는 의미
            queryStringSb.Insert(0, path);

            var client = new RestClient(baseUrl + queryStringSb);   
            var request = new RestRequest(method);
            request.AddHeader("Content-Type", "application/json");

            
            var response = client.Execute(request);

            try
            {
                if (response.IsSuccessful)
                {
                    return response.Content;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }

        }
        public StringBuilder GetQueryString(Dictionary<string, string> parameters)
        {
            // Dictionary 형태로 받은 key = value 형태를 
            // ?key1=value1&key2=value2 ... 형태로 만들어줌
            StringBuilder builder = new StringBuilder();
            foreach (KeyValuePair<string, string> pair in parameters)
            {
                builder.Append(pair.Key).Append("=").Append(pair.Value).Append("&");
            }

            if (builder.Length > 0)
            {
                builder.Length = builder.Length - 1; // 마지막 &를 제거하기 위함.
            }
            return builder;
        }
    }
}