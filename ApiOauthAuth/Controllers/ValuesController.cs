﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using ApiOauthAuth.Attributes;

namespace ApiOauthAuth.Controllers
{
    //[RequireHttps]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [Authorize]
    public class ValuesController : ApiController
    {
        private static List<string> values = new List<string>();
        // GET api/values
        public IEnumerable<string> Get()
        {
            return values;
        }

        // GET api/values/?index=3
        public string Get(int index)
        {
            return values.ElementAtOrDefault(index);
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
            values.Add(value);
        }

        // PUT api/values/5
        public void Put(int index, [FromBody]string value)
        {
            if (values.Count > index)
                values[index] = value;
            else
                Post(value);
        }

        // DELETE api/values/5
        public void Delete(int index)
        {
            if (values.Count > index)
                values.RemoveAt(index);
        }
    }
}
