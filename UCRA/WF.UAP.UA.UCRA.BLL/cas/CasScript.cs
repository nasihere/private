using System;

using WF.EAI.Data.caching.HelperClasses;
using WF.EAI.Entities.constants;

namespace WF.EAI.BLL.cas
{
    using WF.UAP.UA.Global.BLL;

    public class CasScript
    {
        public string Language { get; set; }
        public string StatusCode { get; set; }
        public string ProdType { get; set; }

        private Constants.CacheScriptLangaugeEnum LanguageEnum
        {
            get
            {
                return (Constants.CacheScriptLangaugeEnum)Enum.Parse(typeof(Constants.CacheScriptLangaugeEnum), 
                    CASUtils.ConvertToSentenceCase(Language));
            }
        }

        private Constants.CacheAppStatusEnum StatusCodeEnum { 
            get {
                return (Constants.CacheAppStatusEnum)Enum.Parse(typeof(Constants.CacheAppStatusEnum), StatusCode);
            } 
        }

        private Constants.CacheScriptProductTypeEnum ProdTypeEnum
        {
            get
            {
                return (Constants.CacheScriptProductTypeEnum)Enum.Parse(typeof(Constants.CacheScriptProductTypeEnum), ProdType);
            }
        }

        const string ENGLISH = "ENGLISH";
        const string SPANISH = "SPANISH";

        public string GetScript()
        {
            string script = string.Empty;
            //if (StatusCodeEnum.Equals(Constants.CacheAppStatusEnum.CO))
            //    StatusCode = Constants.CacheAppStatusEnum.SJ.ToString();
            script = CachingUtils.GetDisclosure(eFlowConstants.CacheGroupEnum.CAS, Constants.CacheLOBTypeEnum.PCM,
              GetScriptKey(), LanguageEnum, StatusCodeEnum);

            return script;
        }

        private Constants.CacheScriptAbbreviationEnum GetScriptKey()
        {
            string abbrKey = string.Empty;

            switch (StatusCode)
            {
                case "AP":
                    if (ProdTypeEnum == Constants.CacheScriptProductTypeEnum.LINEINCREASE)
                        abbrKey = Constants.CacheScriptAbbreviationEnum.APLOCI.ToString();
                    else if (ProdTypeEnum == Constants.CacheScriptProductTypeEnum.LOAN)
                        abbrKey = Constants.CacheScriptAbbreviationEnum.AL.ToString();
                    else if (ProdTypeEnum == Constants.CacheScriptProductTypeEnum.LINE)
                        abbrKey = Constants.CacheScriptAbbreviationEnum.ALOC.ToString();
                    break;
                case "AS":
                    if (ProdTypeEnum == Constants.CacheScriptProductTypeEnum.LINEINCREASE)
                        abbrKey = Constants.CacheScriptAbbreviationEnum.AWSLCI.ToString();
                    else if (ProdTypeEnum == Constants.CacheScriptProductTypeEnum.LOAN)
                        abbrKey = Constants.CacheScriptAbbreviationEnum.AWSL.ToString();
                    else if (ProdTypeEnum == Constants.CacheScriptProductTypeEnum.LINE)
                        abbrKey = Constants.CacheScriptAbbreviationEnum.AWSLOC.ToString();                  
                    break;
                case "CO":
                    if (ProdTypeEnum == Constants.CacheScriptProductTypeEnum.LINEINCREASE)
                        abbrKey = Constants.CacheScriptAbbreviationEnum.COLOCI.ToString();
                    else if (ProdTypeEnum == Constants.CacheScriptProductTypeEnum.LOAN)
                        abbrKey = Constants.CacheScriptAbbreviationEnum.COWOSL.ToString();
                    else if (ProdTypeEnum == Constants.CacheScriptProductTypeEnum.LINE)
                        abbrKey = Constants.CacheScriptAbbreviationEnum.COWOSLOC.ToString();
                    break;
                case "SJ":
                    if (ProdTypeEnum == Constants.CacheScriptProductTypeEnum.LINEINCREASE)
                        abbrKey = Constants.CacheScriptAbbreviationEnum.COLOCI.ToString();
                    else if (ProdTypeEnum == Constants.CacheScriptProductTypeEnum.LOAN)
                        abbrKey = Constants.CacheScriptAbbreviationEnum.COWSL.ToString();
                    else if (ProdTypeEnum == Constants.CacheScriptProductTypeEnum.LINE)
                        abbrKey = Constants.CacheScriptAbbreviationEnum.COWSLOC.ToString();
                    break;              
              
                case "TD":
                      if (ProdTypeEnum == Constants.CacheScriptProductTypeEnum.LINEINCREASE)
                        abbrKey = Constants.CacheScriptAbbreviationEnum.DALOCI.ToString();
                    else if (ProdTypeEnum == Constants.CacheScriptProductTypeEnum.LOAN || ProdTypeEnum == Constants.CacheScriptProductTypeEnum.LINE)
                        abbrKey = Constants.CacheScriptAbbreviationEnum.DA.ToString();                  
                    break;               
              
                default:
                    break;
            }

           
            var keyEnum = (Constants.CacheScriptAbbreviationEnum)Enum.Parse(typeof(Constants.CacheScriptAbbreviationEnum), abbrKey);

            return keyEnum;
        }
    }
}
