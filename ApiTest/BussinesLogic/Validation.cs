using Jalasoft.NullableTypes.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jalasoft.NullableTypes.Api
{
    public class Validation
    {
        // User user = new User();
        internal string VerifyCondition(int minValueCi, int maxValueCi, int minValueCel, int maxValueCel, User user)
        {
            string response = "Ok";
            //Verify Ci
            if (minValueCi + 1 < user.Ci || user.Ci > maxValueCi + 1 || !user.Ci.HasValue)
            {
                return response = "The Ci must be a number between 1000000 and 8000000";
            }

            //Verify lenght Ci
            if (user.Name.Length < 5 || user.Name == null)
            {
                //Return is not possivel
                return response = "The name must have at least 5 characteres";
            }

            if (minValueCel > user.CelPhone && user.CelPhone > maxValueCel)
            {
                //Return is not possivel
                return response = "The cel number must be between 6000000 and 8000000 ";
            }

            return response;
        }
    }
}
