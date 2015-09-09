using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using WebsiteDatabase;
namespace WebsiteShifa.Models
{
    public class RepertoryModel : Repertory
    {
        public void fill()
        {
            foreach (Repertory item in Repertory)
            {
                if (!item.remedies.Contains(":")) break;
                item.remedies = HTMLRemedies(item.remedies);
            }
        }
        public IEnumerable<Repertory> Repertory { get; set; }
        public string HTMLRemedies(string RemediesData)
        {     
                
                var Remedies = RemediesData.Split(':');
                StringBuilder SBRemedies = new StringBuilder();
                foreach (string word in Remedies)
                {
                    if (word == "") break;
                    if (word.Split(',')[1].Equals("1"))
                    {
                        SBRemedies.Append(word + " ");
                    }
                    else if (word.Split(',')[1].Equals("2"))
                    {
                        SBRemedies.Append("<font color='blue'>" + word + "</font> ");
                    }
                    else if (word.Split(',')[1].Equals("3"))
                    {
                        SBRemedies.Append("<font color='red'>" + word + "</font> ");
                    }
                }
                return SBRemedies.ToString();
                //return "";
        }
    }
}