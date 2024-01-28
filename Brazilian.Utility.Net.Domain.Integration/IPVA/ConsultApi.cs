using Flurl;
using Flurl.Http;
using Brazilian.Utility.Net.Domain.Common.Configs;
using Microsoft.Extensions.Options;
using System.Net;
using Brazilian.Utility.Net.Domain.Vehycle.Entities;
using Brazilian.Utility.Net.Domain.Vehycle.Integration;
using HtmlAgilityPack;

namespace Brazilian.Utility.Net.Domain.Integration.IPVA
{
    public class IpvaApi : IIpvaApi
    {

        private readonly BaseUrlConfig _baseUrlConfig;

        public IpvaApi(IOptions<BaseUrlConfig> baseUrlConfig)
        {
            _baseUrlConfig = baseUrlConfig.Value;
        }
        public async Task<VehycleUtility> GetIpvaValues(string licensePlate)
        {
            string html = await MakeRequestAsync(licensePlate);

            if (!string.IsNullOrEmpty(html))
            {
                return new VehycleUtility(GetVehycleData(html), GetIpvaData(html));
            }

            return new VehycleUtility(null, null);
        }

        public async Task<string> MakeRequestAsync(string licensePlate)
        {
            string uri = _baseUrlConfig.IpvaUrl.AppendPathSegment(licensePlate);

            var request = _baseUrlConfig.IpvaUrl.AppendPathSegment(licensePlate).WithHeaders(new { Accept = "application/json", User_Agent = "Flur" }).GetAsync().GetAwaiter().GetResult();

            if (request.ResponseMessage.StatusCode == HttpStatusCode.OK)
                return await request.ResponseMessage.Content.ReadAsStringAsync();

            return string.Empty;

        }

        public VehycleData GetVehycleData(string html)
        {
            try
            {
                HtmlDocument htmlVehycle = new HtmlDocument();

                htmlVehycle.LoadHtml(html);

                HtmlNodeCollection VehycleData = htmlVehycle.DocumentNode.SelectNodes("//table[@class='fipeTablePriceDetail']/tr");

                string brand = VehycleData[0].SelectNodes("td")[1].InnerText.Trim();
                string model = VehycleData[2].SelectNodes("td")[1].InnerText.Trim();

                int year = int.Parse(VehycleData[5].SelectNodes("td")[1].InnerText);

                string color = VehycleData[6].SelectNodes("td")[1].InnerText.Trim();
                string cylinderCapability = VehycleData[7].SelectNodes("td")[1].InnerText.Replace("cc", "").Trim();
                string fuel, state, city;
                if (html.Contains("moto"))
                {
                    fuel = VehycleData[8].SelectNodes("td")[1].InnerText.Trim();
                    state = VehycleData[12].SelectNodes("td")[1].InnerText.Trim();
                    city = VehycleData[13].SelectNodes("td")[1].InnerText.Trim();
                }
                else
                {
                    fuel = VehycleData[9].SelectNodes("td")[1].InnerText.Trim();
                    state = VehycleData[13].SelectNodes("td")[1].InnerText.Trim();
                    city = VehycleData[14].SelectNodes("td")[1].InnerText.Trim();

                }

                return new VehycleData(brand, model, year, color, cylinderCapability, fuel, city, state);

            }
            catch
            {
                return new VehycleData(null, null, 0, null, null, null, null, null);
            }
        }

        public Ipva GetIpvaData(string html)
        {
            try
            {
                HtmlDocument htmlIpva = new HtmlDocument();

                htmlIpva.LoadHtml(html);

                HtmlNodeCollection IpvaData = htmlIpva.DocumentNode.SelectNodes("//td[@class='fipeValorVeiculo']");


                float purchasableValue = float.Parse(IpvaData[0].InnerText.Replace("R$", "").Replace(".", "").Replace(",", "."));
                float aliquot = float.Parse(IpvaData[1].InnerText.Replace("%", "").Replace(",", "."));
                float ipvaValue = float.Parse(IpvaData[2].InnerText.Replace("R$", "").Replace(".", "").Replace(",", "."));

                return new Ipva(aliquot, purchasableValue, ipvaValue);
            }
            catch
            {
                return new Ipva(0, 0, 0);
            }

        }
    }
}
