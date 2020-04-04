using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompleteDemo.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompleteDemo.Controllers
{
    /// <summary>
    /// web api 入参测试
    /// </summary>
    [EnableCors("any")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ChargingController : ControllerBase
    {
        /// <summary>
        /// get请求测试1
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string GetAllChargingData(int id, string name)
        {
            return $"ChargingData:id:{id},name:{name}";
        }

        /// <summary>
        /// get请求测试2
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string GetByModel(string strQuery)
        {
            TB_CHARGING oData = Newtonsoft.Json.JsonConvert.DeserializeObject<TB_CHARGING>(strQuery);
            return "ChargingData" + oData.ID;
        }

        /// <summary>
        /// post请求测试1
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost]
        public object SaveData(dynamic obj)
        {
            var strName = obj.NAME.ToString();
            return strName;
        }

        /// <summary>
        /// post请求测试2
        /// </summary>
        /// <param name="oData"></param>
        /// <returns></returns>
        [HttpPost]
        public bool SaveData2(TB_CHARGING oData)
        {
            return true;
        }
    }
}