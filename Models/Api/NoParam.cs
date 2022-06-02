using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IdentityModel.Tokens.Jwt;
using RestSharp;

namespace Upbit_proj.Models.Api
{
    public class NoParam
    {

        private DateTime dt_1970_01_01;   //timestamp를 계산하기 위한 변수

        public NoParam()
        {
            this.dt_1970_01_01 = new DateTime(1970, 01, 01);
        }

        public string Get(string path, Method method, string URL)
        {
            
            var client = new RestClient(URL);       // RestSharp 클라이언트 생성
            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json"); // 컨텐츠 타입이 json이라고 서버측에 알려줌

            var response = client.Execute(request); // 요청을 서버측에 보내 응답을 받음

            try
            {
                if (response.IsSuccessful)
                { // 응답을 받는데 성공한 경우
                    return response.Content;  // 응답받은 요청정보를 string형태로 넘겨준다.
                }
                else
                {
                    return null; // 응답을 받는데 실패시 null값을 반환
                }
            }
            catch
            {
                return null; // 오류시 null값을 반환
            }
        }
    }
}