// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Utils.cs" company="">
//   
// </copyright>
// <summary>
//   Summary description for Utils.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace WF.EAI.BLL.cas
{
    using System;
    using System.Collections;
    using System.Globalization;
    using System.Net;
    using System.Reflection;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Xml;

    using Common.Logging;

    using WF.EAI.BLL.cas.Services.CWS;
    using WF.EAI.Entities.domain.cas;
    using WF.UAP.UASF.CrossCutting.Logging;
    using WF.EAI.Entities.constants;
    using System.Collections.Generic;

    /// <summary>
    ///     Summary description for Utils.
    /// </summary>
    public class CASUtils
    {
        #region Constants

        /// <summary>
        /// The autofinance.
        /// </summary>
        public const string AUTOFINANCE = "AUTO FINANCE";

        /// <summary>
        /// The ca s_ pc m_ search.
        /// </summary>
        public const string CAS_PCM_SEARCH = "CAS_PCM_SEARCH";

        /// <summary>
        /// The creditcard.
        /// </summary>
        public const string CREDITCARD = "CREDIT CARD";

        /// <summary>
        /// The homeequity.
        /// </summary>
        public const string HOMEEQUITY = "HOME EQUITY";

        /// <summary>
        /// The pll.
        /// </summary>
        public const string PLL = "PLL";

        /// <summary>
        /// The savedapp.
        /// </summary>
        public const string SAVEDAPP = "SAVEDAPP";

        #endregion

        #region Static Fields

        /// <summary>
        /// The locker.
        /// </summary>
        private static string Locker = "Locker";

        /// <summary>
        /// The instance_id.
        /// </summary>
        private static ulong instance_id;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The clean input.
        /// </summary>
        /// <param name="inputString">
        /// The input string.
        /// </param>
        /// <param name="maxLength">
        /// The max length.
        /// </param>
        /// <returns>
        /// The clean input.
        /// </returns>
        public static string CleanInput(string inputString, int maxLength)
        {
            StringBuilder retVal = new StringBuilder();

            // check incoming parameters for null or blank string
            if ((inputString != null) && (inputString != string.Empty))
            {
                inputString = inputString.Trim();

                // chop the string incase the client-side max length
                // fields are bypassed to prevent buffer over-runs
                if (inputString.Length > maxLength)
                {
                    inputString = inputString.Substring(0, maxLength);
                }

                // convert some harmful symbols incase the regular
                // expression validators are changed
                for (int i = 0; i < inputString.Length; i++)
                {
                    switch (inputString[i])
                    {
                        case '"':
                            retVal.Append("&quot;");
                            break;
                        case '<':
                            retVal.Append("&lt;");
                            break;
                        case '>':
                            retVal.Append("&gt;");
                            break;
                        default:
                            retVal.Append(inputString[i]);
                            break;
                    }
                }

                // Replace single quotes with white space
                retVal.Replace("'", " ");
            }

            return retVal.ToString();
        }


        public static string GetStatusCode(string statusCode)
        {
            string status = string.Empty;

                switch(statusCode)
                {
                    case "AP":
                        status = "AP";
                        break;
                    case "AR":
                        status = "AP";
                        break;
                    case"FA"	:
                        status = "AP";
                        break;
                    case "LP":
                        status = "AP";
                        break;
                    case "DP":
                        status = "AP";
                        break;
                    case  "ZP":
                        status = "AP";
                        break;
                    case "DT" :	
                         status = "AP";
                        break;
                    case "ZK":
                        status = "AP";
                        break;                       
                    case "RD":
                         status = "AP";
                        break; 
                    case "D1"	:
                      status = "AP";
                     break; 
                    case "ZE":
                        status = "AP";
                     break;
                    case "B1":	
                          status = "AP";
                     break;
                    case "AF":
                           status = "AP";
                     break;
                    case "AU":
                     status = "AP";
                     break;
                    case "FP":
                     status = "AP";
                     break;
                    case "Z1":
                     status = "AP";
                     break;
                    case "Z3":
                     status = "AP";
                     break;
                    case "ZB":
                     status = "AP";
                     break;
                    case "ZS":
                     status = "AP";
                     break;
                    case "V1":                    
                  status = "AS";
                     break;
                    case "IN":
                         status = "AS";
                     break;   
                    case "CP":
                       status = "AS";
                     break;
                    case"AS":
                         status = "AS";
                     break;
                    case "CO":
                         status = "CO";
                     break;
                    case "SJ":
                         status = "SJ";
                     break;
                    case "TD":
                     status = "TD";
                     break;

                }
                return status;
        }

        /// <summary>
        /// gets auth levels required for the approver
        /// to approve a rate exception request
        /// </summary>
        /// <param name="lob"></param>
        /// <param name="amount"></param>
        /// <param name="rateVariance"></param>
        /// <returns></returns>
        public List<string> GetAuthLevelsRequired(string lob, double amount, double rateVariance)
        {
            List<string> authLevels = null;

            switch (lob)
            {
                case "WMG":
                    authLevels = GetAuthLevelsRequiredForWmg(amount,rateVariance);
                    break;

                case "WFA":
                    authLevels = GetAuthLevelsRequiredForWfa(amount, rateVariance);
                    break;

                case "AD":
                case "LSG":
                    authLevels = GetTable1AuthLevels(rateVariance);
                    break;

                default:
                    break;
            }

            return authLevels;
        }

        private List<string> GetAuthLevelsRequiredForWmg(double amount, double rateVariance)
        {
            List<string> authLevels = null;
            if (amount > 250000)
                authLevels = GetTable1AuthLevels(rateVariance);
            else
                authLevels = GetTable2AuthLevels(rateVariance);

            return authLevels;
        }

        private List<string> GetAllEligibleAuthLevels(string minLevel)
        {
            char minLevelChar = minLevel.ToCharArray()[0];
            var list = new List<string>();
            for (char c = 'A'; c <= minLevelChar; c++)
            {
                list.Add(c.ToString());
            }
            return list;
        }


        private List<string> GetTable2AuthLevels(double rateVariance)
        {
            string minAuthLevel = string.Empty;
            if (rateVariance < 0.125)
                minAuthLevel = "G";
            else if (rateVariance < 0.25)
                minAuthLevel = "F";
            else if (rateVariance < 0.375)
                minAuthLevel = "E";
            else if (rateVariance < 0.5)
                minAuthLevel = "D";
            //else if (rateVariance < 0.75)
            //    authLevel = "C";
            else if (rateVariance < 1)
                minAuthLevel = "B";
            else
                minAuthLevel = "A";
            List<string> authLevels = GetAllEligibleAuthLevels(minAuthLevel);
            authLevels.Add("C");
            return authLevels;
        }

        private List<string> GetTable3AuthLevels(double rateVariance)
        {
            string minAuthLevel = string.Empty;
            if (rateVariance < 0.125)
                minAuthLevel = "G";
            else if (rateVariance < 0.25)
                minAuthLevel = "F";
            else if (rateVariance < 0.375)
                minAuthLevel = "E";
            else if (rateVariance < 0.5)
                minAuthLevel = "D";
            else if (rateVariance < 0.75)
                minAuthLevel = "C";
            else if (rateVariance < 1)
                minAuthLevel = "B";
            else
                minAuthLevel = "A";
            List<string> authLevels = GetAllEligibleAuthLevels(minAuthLevel);
            if (!authLevels.Contains("D"))
                authLevels.Add("D");

            return authLevels;
        }
        private List<string> GetAuthLevelsRequiredForWfa(double amount, double rateVariance)
        {
            List<string> authLevels = null;
            if (amount > 250000)
                authLevels = GetTable1AuthLevels(rateVariance);
            else
                authLevels = GetTable3AuthLevels(rateVariance);

            return authLevels;
        }

        private static string GetAuthLevelRequiredForAd(decimal amount, double rateVariance)
        {
            string authLevel = string.Empty;

            return authLevel;

        }


        private static string GetAuthLevelRequiredForLsg(decimal amount, double rateVariance)
        {
            string authLevel = string.Empty;

            return authLevel;

        }

        private List<string> GetTable1AuthLevels(double rateVariance)
        {
            string authLevel = string.Empty;

            if (rateVariance < 0.125)
                authLevel = "G";
            else if (rateVariance < 0.25)
                authLevel = "F";
            else if (rateVariance < 0.375)
                authLevel = "E";
            else if (rateVariance < 0.5)
                authLevel = "D";
            else if (rateVariance < 0.75)
                authLevel = "C";
            else if (rateVariance < 1)
                authLevel = "B";
            else
                authLevel = "A";

            return GetAllEligibleAuthLevels(authLevel);  
        }

        public static string LineorLoan(string productId, string loanType, string lineIncrease)
        {
            string LINEorLOAN = "LINE";
            switch (productId)
            {
                // PCM
                case "UNSLN":
                    LINEorLOAN = "LOAN";
                    break;
                case "UNSLC":
                    LINEorLOAN = "LINE";
                    break;
                case "UNSLI":
                    LINEorLOAN = "LINE";
                    break;
                case "SECLC":
                    LINEorLOAN = "LINE";
                    break;
                case "SECLN":
                    LINEorLOAN = "LOAN";
                    break;
                case "SECLI":
                   LINEorLOAN = "LINE";
                    break;
                case "SECFN":
                   LINEorLOAN = "LOAN";
                    break;
                case "SECMV":
                    LINEorLOAN = "LOAN";
                    break;
                case "UNSLF":
                    LINEorLOAN = "LINE";
                    break;
            }

            if ((loanType == "LINE") && (lineIncrease == "Y"))
                LINEorLOAN = "LINEINCREASE";

            return LINEorLOAN;
        }

        /// <summary>
        /// The clean output.
        /// </summary>
        /// <param name="inputString">
        /// The input string.
        /// </param>
        /// <param name="maxLength">
        /// The max length.
        /// </param>
        /// <returns>
        /// The clean output.
        /// </returns>
        public static string CleanOutput(string inputString, int maxLength)
        {
            StringBuilder retVal = new StringBuilder();

            // check incoming parameters for null or blank string
            if ((inputString != null) && (inputString != string.Empty))
            {
                inputString = inputString.Trim();
                inputString = inputString.TrimStart(' ');

                // chop the string incase the client-side max length
                // fields are bypassed to prevent buffer over-runs
                if (inputString.Length > maxLength)
                {
                    inputString = inputString.Substring(0, maxLength);
                }

                // convert some harmful symbols incase the regular
                // expression validators are changed
                for (int i = 0; i < inputString.Length; i++)
                {
                    switch (inputString[i])
                    {
                        case '"':
                            retVal.Append("&quot;");
                            break;
                        case '<':
                            retVal.Append("&lt;");
                            break;
                        case '>':
                            retVal.Append("&gt;");
                            break;
                        default:
                            retVal.Append(inputString[i]);
                            break;
                    }
                }

                // Replace single quotes with white space
                retVal.Replace("'", " ");
            }

            return retVal.ToString();
        }

        /// <summary>
        /// The finalize data.
        /// </summary>
        /// <param name="obj">
        /// The obj.
        /// </param>
        public static void FinalizeData(object obj)
        {
            if (obj == null)
            {
                return;
            }

            Type type = obj.GetType();

            foreach (FieldInfo field in type.GetFields())
            {
                Type field_type = field.FieldType;
                if (field_type.FullName == "System.String")
                {
                    string value = (string)field.GetValue(obj);
                    if (value != null)
                    {
                        field.SetValue(obj, value.ToUpper());
                    }
                }
                else if (field_type.IsArray)
                {
                    object[] array = (object[])field.GetValue(obj);
                    if (array != null)
                    {
                        for (int i = 0; i < array.Length; i++)
                        {
                            if (field_type.GetElementType().FullName == "System.String")
                            {
                                string value = (string)array[i];
                                if (value != null)
                                {
                                    array[i] = value.ToUpper();
                                }
                            }
                            else
                            {
                                FinalizeData(array[i]);
                            }
                        }
                    }
                }
                else
                {
                    // if( field_type )
                    object _obj = field.GetValue(obj);
                    FinalizeData(_obj);
                }
            }
        }

        /// <summary>
        /// The format date.
        /// </summary>
        /// <param name="date">
        /// The date.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string FormatDate(string date)
        {
            if (string.IsNullOrEmpty(date))
            {
                return string.Empty;
            }

            if (date.Length > 0)
            {
                DateTime dummyDate;
                try
                {
                    dummyDate = DateTime.Parse(date);
                }
                catch
                {
                    return string.Empty;
                }

                return date;
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// The format date.
        /// </summary>
        /// <param name="date">
        /// The date.
        /// </param>
        /// <param name="input_format">
        /// The input_format.
        /// </param>
        /// <param name="output_format">
        /// The output_format.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string FormatDate(string date, string input_format, string output_format)
        {
            if (date.Trim() == "/__/" || date.Trim() == "00/00/00")
            {
                date = "00000000";
            }

            if (date != null && date.Trim().Length == input_format.Trim().Length && date != "00000000")
            {
                try
                {
                    DateTime d = DateTime.ParseExact(date, input_format, null);
                    return d.ToString(output_format);
                }
                catch (Exception ex)
                {
                    //Logger.Instance.Error("Exception occured in Util Format Date", ex);
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// The format day.
        /// </summary>
        /// <param name="day">
        /// The day.
        /// </param>
        /// <returns>
        /// The format day.
        /// </returns>
        public static string FormatDay(string day)
        {
            if (day != null && day.TrimStart('0').Length > 0)
            {
                double d;

                if (double.TryParse(day.Trim(), NumberStyles.Any, null, out d))
                {
                    if ((d > 3 && d < 21) || (d > 23 && d < 31))
                    {
                        return day.TrimStart('0').Trim() + "<sup>th</sup>";
                    }
                    else if (d == 1 || d == 21)
                    {
                        return d + "<sup>st</sup>";
                    }
                    else if (d == 3 || d == 23)
                    {
                        return d + "<sup>rd</sup>";
                    }
                }

                day.TrimStart('0').Trim();
            }

            return string.Empty;
        }

        /// <summary>
        /// The format decimal.
        /// </summary>
        /// <param name="places">
        /// The places.
        /// </param>
        /// <param name="target">
        /// The target.
        /// </param>
        /// <returns>
        /// The format decimal.
        /// </returns>
        public static string FormatDecimal(int places, decimal target)
        {
            string format = "{0:0." + string.Empty.PadLeft(places, '0') + "}";
            return string.Format(format, target);
        }

        /// <summary>
        /// The format phone.
        /// </summary>
        /// <param name="phoneNumber">
        /// The phone number.
        /// </param>
        /// <returns>
        /// The format phone.
        /// </returns>
        public static string FormatPhone(string phoneNumber)
        {
            if (phoneNumber != null && phoneNumber.Trim().Length == 10)
            {
                phoneNumber = phoneNumber.Insert(3, "-");
                phoneNumber = phoneNumber.Insert(7, "-");
                return phoneNumber;
            }

            return string.Empty;
        }

        /// <summary>
        /// The format time.
        /// </summary>
        /// <param name="time">
        /// The time.
        /// </param>
        /// <returns>
        /// The format time.
        /// </returns>
        public static string FormatTime(string time)
        {
            if (time != null && time.Length == 6 && time != "000000")
            {
                int hour = int.Parse(time.Substring(0, 2));

                return time.Substring(0, 2) + ":" + time.Substring(2, 2) + (hour >= 12 ? "PM" : "AM") + " PST";
            }

            return string.Empty;
        }

        /// <summary>
        /// The get compass user transaction id.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetCompassUserTransactionId()
        {
            string TransactionId = string.Empty;
            IPHostEntry serverIp = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddr = serverIp.AddressList.Length > 0 ? serverIp.AddressList[0].ToString() : "0.0.0.0";
            TransactionId = Guid.NewGuid() + ":" + Dns.GetHostName() + "/" + ipAddr;
            TransactionId = TransactionId.Length > 100 ? TransactionId.Substring(0, 100) : TransactionId;

            return TransactionId;
        }

        /// <summary>
        /// The get unique instance id.
        /// </summary>
        /// <returns>
        /// The <see cref="ulong"/>.
        /// </returns>
        public static ulong GetUniqueInstanceID()
        {
            lock (Locker)
            {
                instance_id++;
            }

            return instance_id;
        }

        /// <summary>
        /// The initialize data.
        /// </summary>
        /// <param name="obj">
        /// The obj.
        /// </param>
        public static void InitializeData(object obj)
        {
            Type type = obj.GetType();

            foreach (FieldInfo field in type.GetFields())
            {
                Type field_type = field.FieldType;
                if (field_type.FullName == "System.String")
                {
                    field.SetValue(obj, string.Empty);
                }
                else if (field_type.IsArray)
                {
                    object[] attributes = field.GetCustomAttributes(typeof(ArrayLengthAttribute), false);
                    if (attributes.Length > 0)
                    {
                        int array_length = ((ArrayLengthAttribute)attributes[0]).Length;

                        object[] array =
                            (object[])
                            field_type.InvokeMember(
                                null, BindingFlags.CreateInstance, null, null, new object[] { array_length });
                        field.SetValue(obj, array);
                        for (int i = 0; i < array.Length; i++)
                        {
                            if (field_type.GetElementType().FullName == "System.String")
                            {
                                array[i] = string.Empty;
                            }
                            else
                            {
                                object __obj = field_type.GetElementType()
                                                         .InvokeMember(
                                                             null, BindingFlags.CreateInstance, null, null, null);
                                array[i] = __obj;
                                InitializeData(__obj);
                            }
                        }
                    }
                }
                else
                {
                    // if( field_type )
                    object _obj = field_type.InvokeMember(null, BindingFlags.CreateInstance, null, null, null);
                    field.SetValue(obj, _obj);
                    InitializeData(_obj);
                }
            }
        }

        /// <summary>
        /// The is expired.
        /// </summary>
        /// <param name="date">
        /// The date.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsExpired(string date)
        {
            if (date != null && date.Trim().Length == 8 && date != "00000000")
            {
                try
                {
                    DateTime d = DateTime.ParseExact(date.Trim(), "yyyyMMdd", null);

                    // 					d.AddDays( 10 )
                    if (d.Date < DateTime.Now.Date)
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    //Logger.Instance.Error(ex);
                }
            }

            return false;
        }

        /// <summary>
        /// The join.
        /// </summary>
        /// <param name="seperator">
        /// The seperator.
        /// </param>
        /// <param name="line1">
        /// The line 1.
        /// </param>
        /// <param name="line2">
        /// The line 2.
        /// </param>
        /// <param name="line3">
        /// The line 3.
        /// </param>
        /// <param name="line4">
        /// The line 4.
        /// </param>
        /// <returns>
        /// The join.
        /// </returns>
        public static string Join(string seperator, string line1, string line2, string line3, string line4)
        {
            StringBuilder output = new StringBuilder();
            if (line1 != null && line1.Trim().Length > 0)
            {
                output.Append(line1.Trim());
            }

            if (line2 != null && line2.Trim().Length > 0)
            {
                output.Append(seperator);
                output.Append(line2.Trim());
            }

            if (line3 != null && line3.Trim().Length > 0)
            {
                output.Append(seperator);
                output.Append(line3.Trim());
            }

            if (line4 != null && line4.Trim().Length > 0)
            {
                output.Append(seperator);
                output.Append(line4.Trim());
            }

            return output.ToString();
        }

        /// <summary>
        /// The loan or line.
        /// </summary>
        /// <param name="code">
        /// The code.
        /// </param>
        /// <returns>
        /// The loan or line.
        /// </returns>
        public static string LoanOrLine(string code)
        {
            if (code != null && code.Trim().Length > 0)
            {
                if (code.ToUpper().IndexOf("LINE") != -1)
                {
                    return "LINE";
                }
                else if ((code.ToUpper().IndexOf("LOAN") != -1) || (code.ToUpper().IndexOf(" LN ") != -1))
                {
                    return "LOAN";
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// The redraw doc.
        /// </summary>
        /// <param name="appId">
        /// The app id.
        /// </param>
        /// <param name="locId">
        /// The loc id.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="hris">
        /// The hris.
        /// </param>
        /// <param name="AU">
        /// The au.
        /// </param>
        public static void RedrawDoc(string appId, string locId, string userId, string hris, string AU)
        {
            CwsUpdateHelper updateHelper = CwsUpdateHelper.Get();
            if (updateHelper != null)
            {
                updateHelper.BeginInquiry(appId, locId, userId, hris, AU, false, false);
                updateHelper.EndInquiry();
                if (updateHelper[CwsUpdateFieldMap.NoteDate] != null)
                {
                    updateHelper[CwsUpdateFieldMap.NoteDate].value = DateTime.Now.Date.ToString("MM/dd/yy");
                    CwsUpdateHelper.Set(updateHelper);
                }

                updateHelper.BeginUpdate(null, null, appId, AU, locId, userId, hris);
                updateHelper.EndUpdate();
                CwsUpdateHelper.Set(null);
            }
        }

        /// <summary>
        /// The redraw doc.
        /// </summary>
        /// <param name="appId">
        /// The app id.
        /// </param>
        /// <param name="locId">
        /// The loc id.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="hris">
        /// The hris.
        /// </param>
        /// <param name="AU">
        /// The au.
        /// </param>
        /// <param name="eSignFlg">
        /// The e sign flg.
        /// </param>
        public static void RedrawDoc(string appId, string locId, string userId, string hris, string AU, string eSignFlg)
        {
            CwsUpdateHelper updateHelper = CwsUpdateHelper.Get();
            if (updateHelper != null)
            {
                updateHelper.BeginInquiry(appId, locId, userId, hris, AU, false, false);
                updateHelper.EndInquiry();
                if (updateHelper[CwsUpdateFieldMap.NoteDate] != null)
                {
                    updateHelper[CwsUpdateFieldMap.NoteDate].value = DateTime.Now.Date.ToString("MM/dd/yy");
                }

                CwsUpdateHelper.Set(updateHelper);

                updateHelper.BeginUpdate(null, null, appId, AU, locId, userId, hris);
                updateHelper.EndUpdate();
                CwsUpdateHelper.Set(null);
            }
        }

        /// <summary>
        /// The spp plan desc.
        /// </summary>
        /// <param name="code">
        /// The code.
        /// </param>
        /// <returns>
        /// The spp plan desc.
        /// </returns>
        public static string SPPPlanDesc(string code)
        {
            switch (code)
            {
                case "N":
                    return "None";
                case "1":
                    return "WFSPP Platinum Joint";
                case "2":
                    return "WFSPP Platinum Single";
                case "3":
                    return "WFSPP Gold Joint";
                case "4":
                    return "WFSPP Gold Single";
                case "":
                    return "Does Not Qualify for Plan";
                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// The spp plans.
        /// </summary>
        /// <param name="code">
        /// The code.
        /// </param>
        /// <returns>
        /// The spp plans.
        /// </returns>
        public static string SPPPlans(string code)
        {
            switch (code)
            {
                case "N":
                    return "None";
                case "1":
                    return "WFSPP Platinum Joint";
                case "2":
                    return "WFSPP Platinum Single";
                case "3":
                    return "WFSPP Gold Joint";
                case "4":
                    return "WFSPP Gold Single";
                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// The baid message.
        /// </summary>
        /// <param name="code">
        /// The code.
        /// </param>
        /// <returns>
        /// The baid message.
        /// </returns>
        public static string baidMessage(string code)
        {
            if (code != null && code.Trim().Length > 0)
            {
                switch (code.Trim())
                {
                    case "IN":
                        return "INCOMPLETE";
                    case "AR":
                        return "APPROVAL RECOMMENDED";
                    case "TR":
                        return "TURNDOWN RECOMMENDED";
                    case "LO":
                        return "IN UNDERWRITING";
                    case "MI":
                        return "MISSING INFORMATION";
                    case "AS":
                        return "APPROVED -SUBJECT TO (PLL ONLY)";
                    case "CN":
                        return "CANCELED - DUPLICATE";
                    case "PC":
                        return "PCA PROVISIONAL APPLICATION";
                    case "CP":
                        return "COMPLETE PACKAGE";
                    case "SD":
                        return "SET CLOSING DATE";
                    case "DP":
                        return "DOCS READY FOR PRINTING";
                    case "BO":
                        return "BRANCH OVERRIDE";
                    case "ZB":
                        return "DOCS POSTED EDD";
                    case "LD":
                        return "FUNDS DISBURSED";
                    case "BK":
                        return "BOOKED";
                    case "HB":
                        return "BOOKED ACCOUNT RESCINDED";
                    case "AF":
                        return "APPROVED NOT FUNDED";

                    default:
                        return code.Trim();
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// The banker note read indicator.
        /// </summary>
        /// <param name="senderIndicator">
        /// The sender indicator.
        /// </param>
        /// <param name="readIndicator">
        /// The read indicator.
        /// </param>
        /// <returns>
        /// The banker note read indicator.
        /// </returns>
        public static string bankerNoteReadIndicator(string senderIndicator, string readIndicator)
        {
            if (senderIndicator != null && readIndicator != null && senderIndicator.Trim().Length > 0
                && readIndicator.Trim().Length > 0)
            {
                if (senderIndicator.Trim() == "B" && readIndicator.Trim() == "READ")
                {
                    return "Y";
                }
                else if (senderIndicator.Trim() == "B" && readIndicator.Trim() != "READ")
                {
                    return "N";
                }
                else
                {
                    return "&nbsp;";
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// The broker validation text.
        /// </summary>
        /// <param name="code">
        /// The code.
        /// </param>
        /// <returns>
        /// The broker validation text.
        /// </returns>
        public static string brokerValidationText(string code)
        {
            if (code == null || code.Trim().Length == 0)
            {
                return string.Empty;
            }

            switch (code.Trim())
            {
                case "N":
                    return "Not a broker application";
                case "1":
                    return "Valid Broker";
                case "2":
                    return "Not a Valid Broker";
                case "3":
                    return "Broker ID Not Found";
                case "4":
                    return "Communication error accessing broker lookup service";
                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// The convert to currency.
        /// </summary>
        /// <param name="val">
        /// The val.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string convertToCurrency(string val)
        {
            if (val == null || val.Length == 0)
            {
                return string.Empty;
            }

            double tempVal;
            NumberFormatInfo decimalFormat = new CultureInfo("en-US", false).NumberFormat;
            if (double.TryParse(val.Trim(), NumberStyles.Any, decimalFormat, out tempVal))
            {
                return tempVal.ToString("C");
            }
            else
            {
                return val.Trim();
            }
        }

        /// <summary>
        /// The convert to float.
        /// </summary>
        /// <param name="val">
        /// The val.
        /// </param>
        /// <param name="decimalDigits">
        /// The decimal digits.
        /// </param>
        /// <returns>
        /// The convert to float.
        /// </returns>
        public static string convertToFloat(string val, int decimalDigits)
        {
            if (val == null)
            {
                return string.Empty;
            }

            double tempVal;
            NumberFormatInfo decimalFormat = new CultureInfo("en-US", false).NumberFormat;
            decimalFormat.NumberDecimalDigits = decimalDigits;
            if (double.TryParse(val.Trim(), NumberStyles.Any, decimalFormat, out tempVal))
            {
                return tempVal.ToString("N", decimalFormat);
            }
            else
            {
                return val.Trim();
            }
        }

        /// <summary>
        /// The convert to percent.
        /// </summary>
        /// <param name="val">
        /// The val.
        /// </param>
        /// <param name="decimalDigits">
        /// The decimal digits.
        /// </param>
        /// <returns>
        /// The convert to percent.
        /// </returns>
        public static string convertToPercent(string val, int decimalDigits)
        {
            if (val == null)
            {
                return string.Empty;
            }

            double tempVal;
            NumberFormatInfo decimalFormat = new CultureInfo("en-US", false).NumberFormat;
            decimalFormat.NumberDecimalDigits = decimalDigits;
            if (double.TryParse(val.Trim(), NumberStyles.Any, decimalFormat, out tempVal))
            {
                return tempVal.ToString("N", decimalFormat) + "%";
            }
            else
            {
                return val.Trim();
            }
        }

        /// <summary>
        /// The convert to percent.
        /// </summary>
        /// <param name="val">
        /// The val.
        /// </param>
        /// <param name="decimalDigits">
        /// The decimal digits.
        /// </param>
        /// <param name="multiply">
        /// The multiply.
        /// </param>
        /// <returns>
        /// The convert to percent.
        /// </returns>
        public static string convertToPercent(string val, int decimalDigits, bool multiply)
        {
            if (val == null)
            {
                return string.Empty;
            }

            double tempVal;
            NumberFormatInfo decimalFormat = new CultureInfo("en-US", false).NumberFormat;
            decimalFormat.NumberDecimalDigits = decimalDigits;
            if (double.TryParse(val.Trim(), NumberStyles.Any, decimalFormat, out tempVal))
            {
                if (multiply)
                {
                    tempVal = tempVal * 100;
                }

                return tempVal.ToString("N", decimalFormat) + "%";
            }
            else
            {
                return val.Trim();
            }
        }

        /// <summary>
        /// The convert to percent.
        /// </summary>
        /// <param name="val">
        /// The val.
        /// </param>
        /// <returns>
        /// The convert to percent.
        /// </returns>
        public static string convertToPercent(string val)
        {
            if (val == null)
            {
                return string.Empty;
            }

            double tempVal;
            NumberFormatInfo decimalFormat = new CultureInfo("en-US", false).NumberFormat;
            decimalFormat.NumberDecimalDigits = 2;
            if (double.TryParse(val.Trim(), NumberStyles.Any, decimalFormat, out tempVal))
            {
                return tempVal.ToString("N", decimalFormat) + "%";
            }
            else
            {
                return val.Trim();
            }
        }

        /// <summary>
        /// The format data.
        /// </summary>
        /// <param name="val">
        /// The val.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string formatData(string val)
        {
            if (val != null && val != string.Empty)
            {
                val = val.Trim();
                int index = val.IndexOf(".");
                if (index != -1)
                {
                    val = val.Substring(0, index);
                }

                val = trimLeadingZeros(trimLeadingSign(val));
            }
            else
            {
                val = string.Empty;
            }

            return val;
        }

        /// <summary>
        /// The get protected evnts.
        /// </summary>
        /// <param name="sppPlan">
        /// The spp plan.
        /// </param>
        /// <returns>
        /// The get protected evnts.
        /// </returns>
        public static string getProtectedEvnts(string sppPlan)
        {
            if (sppPlan == "1" || sppPlan == "2")
            {
                // Platinum
                return "Life, Disability, Hospitalization, Involuntary Unemployment";
            }
            else if (sppPlan == "3" || sppPlan == "4")
            {
                // Gold
                return "Life, Disability, Hospitalization";
            }

            return string.Empty;
        }

        /// <summary>
        /// The hash to string.
        /// </summary>
        /// <param name="ht">
        /// The ht.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string hashToString(Hashtable ht)
        {
            string str = string.Empty;
            IDictionaryEnumerator e = ht.GetEnumerator();
            while (e.MoveNext())
            {
                str = str + e.Key + "=" + e.Value + ",";
            }

            return str;
        }

        public static string ConvertToSentenceCase(string str)
        {
            var r = new Regex(@"(^[a-z])|\.\s+(.)", RegexOptions.ExplicitCapture);
            var result = r.Replace(str.ToLower(), s => s.Value.ToUpper());
            return result;
        }

        /// <summary>
        /// The is line.
        /// </summary>
        /// <param name="code">
        /// The code.
        /// </param>
        /// <returns>
        /// The is line.
        /// </returns>
        public static bool isLine(string code)
        {
            if (code != null && code.Trim().Length > 0)
            {
                switch (code.Trim())
                {
                    case "EQHST":
                    case "EQHSR":
                    case "EQLN":
                    case "EQIMP":
                    case "EQRFM":
                    case "EQHLN":
                    case "INFBB":
                    case "INFSF":
                    case "UNSLL":
                    case "UXSLN":
                    case "UNSLN":
                    case "SECMV":
                    case "SECFI":
                    case "SECFN":
                    case "SECIN":
                        return false;

                    case "EQINC":
                    case "EQLBB":
                    case "EQHLC":
                    case "EQHMA":
                    case "EQLI":
                    case "EQLOC":
                    case "EQCBB":
                    case "EQCEL":
                    case "UNSLC":
                    case "UNSLI":
                    case "UXSLC":
                    case "UNSLF":
                    case "UNSIF":
                    case "SECIL":
                    case "SECLC":
                    case "SECLI":
                    case "EQWFS":
                    case "EQWFE":
                        return true;
                    default:
                        return false;
                }
            }

            return false;
        }

        /// <summary>
        /// The is loan.
        /// </summary>
        /// <param name="code">
        /// The code.
        /// </param>
        /// <returns>
        /// The is loan.
        /// </returns>
        public static bool isLoan(string code)
        {
            return !isLine(code);
        }

        /// <summary>
        /// The is ppi app.
        /// </summary>
        /// <param name="insCode">
        /// The ins code.
        /// </param>
        /// <returns>
        /// The is ppi app.
        /// </returns>
        public static bool isPPIApp(string insCode)
        {
            string sppPlans = "6|K|F|A|B|C|L|E|M|7|J|H|P|Q|R|S|D|T|G|U|V|W|X|Y|I|Z";

            if (insCode != null && insCode.Trim().Length > 0 && sppPlans.IndexOf(insCode) >= 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// The is rate visible.
        /// </summary>
        /// <param name="Location">
        /// The location.
        /// </param>
        /// <returns>
        /// The is rate visible.
        /// </returns>
        public static bool isRateVisible(string Location)
        {
            string visibleStates = "TX|AZ|UT|CA|WA|ID|OR|MN|NM|ND|NV|CO|WY|IA|NE|SD|IL|IN|MT|OH|WI|MI";

            if (Location != null && Location.Trim().Length > 0 && visibleStates.IndexOf(Location) > 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// The is spp plan.
        /// </summary>
        /// <param name="insCode">
        /// The ins code.
        /// </param>
        /// <returns>
        /// The is spp plan.
        /// </returns>
        public static bool isSppPlan(string insCode)
        {
            string sppPlans = "1|2|3|4|0|N";

            if (insCode != null && insCode.Trim().Length > 0 && sppPlans.IndexOf(insCode) >= 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// The layout code.
        /// </summary>
        /// <param name="productCode">
        /// The product code.
        /// </param>
        /// <returns>
        /// The layout code.
        /// </returns>
        public static string layoutCode(string productCode)
        {
            if (productCode != null && productCode.Trim().Length > 0)
            {
                switch (productCode.Trim())
                {
                    case "EQLBB":
                        return "C";
                    case "INLBB":
                        return "C";
                    case "EQHST":
                        return "C";
                    case "INHST":
                        return "C";
                    case "EQHSR":
                        return "A";
                    case "INHSR":
                        return "A";
                    case "INLNI":
                        return "A";
                    case "EQLNI":
                        return "A";
                    case "EQLN":
                        return "A";
                    case "INLN":
                        return "A";
                    case "EQIMP":
                        return "A";
                    case "INIMP":
                        return "A";
                    case "EQRFM":
                        return "A";
                    case "INRFM":
                        return "A";
                    case "EQINC":
                        return "C";
                    case "ININC":
                        return "B";
                    case "EQWFE":
                        return "B";
                    case "EQHLN":
                        return "A";
                    case "INHLN":
                        return "A";
                    case "EQHLC":
                        return "C";
                    case "INHLC":
                        return "C";
                    case "EQHMA":
                        return "C";
                    case "INHMA":
                        return "B";
                    case "EQWFS":
                        return "B";
                    case "EQLI":
                        return "A";
                    case "INLI":
                        return "A";
                    case "INFBB":
                        return "B";
                    case "INFSF":
                        return "B";
                    case "EQFSF":
                        return "B";
                    case "EQLOC":
                        return "C";
                    case "INLOC":
                        return "C";
                    case "EQCBB":
                        return "B";
                    case "INCBB":
                        return "B";
                    case "EQFBB":
                        return "B";
                    case "EQCEL":
                        return "B";
                    case "INCEL":
                        return "B";
                    case "UNSLL":
                        return "A";
                    case "UNSLF":
                        return "C";
                    case "UNSIF":
                        return "C";
                    case "UXSLN":
                        return "A";
                    case "UNSLN":
                        return "A";
                    case "UNSLC":
                        return "C";
                    case "UNSLI":
                        return "C";
                    case "UXSLC":
                        return "C";
                    case "SECMV":
                        return "A";
                    case "SECFI":
                        return "A";
                    case "SECFN":
                        return "A";
                    case "SECIN":
                        return "A";
                    case "SECIL":
                        return "C";
                    case "SECLC":
                        return "C";
                    case "SECLI":
                        return "C";
                    case "EQWFC":
                        return "B";
                    case "INWFC":
                        return "B";
                    case "INWFE":
                        return "B";
                    case "INWFS":
                        return "B";

                        // ***************
                        // //P0024132 Floor Rate Project
                        // // CR 1670465 WMG Floor Rate
                    case "XX30C": // Equity Line Platinum 
                        return "B";
                    case "XX30D": // Equity Line Platinum
                        return "B";

                        // *
                    default:
                        return "A";
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// The payment frequency message.
        /// </summary>
        /// <param name="code">
        /// The code.
        /// </param>
        /// <returns>
        /// The payment frequency message.
        /// </returns>
        public static string paymentFrequencyMessage(string code)
        {
            if (code != null && code.Trim().Length > 0)
            {
                switch (code.Trim())
                {
                    case "M":
                        return "Monthly";
                    case "Q":
                        return "Quarterly";
                    case "S":
                        return "Semi-Annual";
                    case "A":
                        return "Annual";
                    default:
                        return code;
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// The rate buy down message.
        /// </summary>
        /// <param name="code">
        /// The code.
        /// </param>
        /// <returns>
        /// The rate buy down message.
        /// </returns>
        public static string rateBuyDownMessage(string code)
        {
            if (code != null && code.Trim().Length > 0)
            {
                double testVal = 0;
                if (double.TryParse(code.Trim(), NumberStyles.Any, null, out testVal))
                {
                    switch (Convert.ToInt32(testVal))
                    {
                        case 0:
                            return "0.00%";
                        case 1:
                            return "-0.25%";
                        case 2:
                            return "-0.50%";
                        case 3:
                            return "-0.75%";
                    }
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// The status message.
        /// </summary>
        /// <param name="code">
        /// The code.
        /// </param>
        /// <returns>
        /// The status message.
        /// </returns>
        public static string statusMessage(string code)
        {
            if (code != null && code.Trim().Length > 0)
            {
                switch (code.Trim())
                {
                    case "AP":
                        return "Approved";
                    case "TD":
                        return "Application Declined";
                    case "AW":
                        return "Application Withdrawn";
                    case "EN":
                        return "Pending Decision";
                    case "S":
                        return "DECLINED";
                    default:
                        return code.Trim();
                }
            }

            return string.Empty;
        }


        /// <summary>
        /// The stip message.
        /// </summary>
        /// <param name="code">
        /// The code.
        /// </param>
        /// <returns>
        /// The stip message.
        /// </returns>
        public static string stipMessage(string code)
        {
            if (code != null && code.Trim().Length > 0)
            {
                switch (code.Trim())
                {
                    case "MT":
                        return "Stip Met";
                    case "NM":
                        return "Not Met";
                    default:
                        return code;
                }
            }

            return string.Empty;
        }
        /// <summary>
        /// returns true or false if given location
        /// is a Training Bank Location
        /// </summary>
        /// <param name="loc"></param>
        /// <returns></returns>
        public static bool IsTrainingBankLoc(string loc)
        {
            if (
                (loc.Equals(CASConstants.TrainingBankLocation1)) || 
                (loc.Equals(CASConstants.TrainingBankLocation2))
                )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// The string to hash.
        /// </summary>
        /// <param name="str">
        /// The str.
        /// </param>
        /// <returns>
        /// The <see cref="Hashtable"/>.
        /// </returns>
        public static Hashtable stringToHash(string str)
        {
            Hashtable updateVariables = null;
            if (str != null)
            {
                updateVariables = new Hashtable();
                string[] array = str.Split(',');
                for (int i = 0; i < array.Length; i++)
                {
                    if (array[i] != null && array[i].IndexOf("=") >= 0)
                    {
                        string[] arrItem = array[i].Split('=');
                        if (arrItem != null && arrItem.Length > 0)
                        {
                            updateVariables.Add(arrItem[0], arrItem[1]);
                        }
                    }
                }
            }

            return updateVariables;
        }

        /// <summary>
        /// The trim leading sign.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string trimLeadingSign(string input)
        {
            if (input != null && (input.Trim().StartsWith("+") || input.Trim().StartsWith("-")))
            {
                return input.Remove(0, 1);
            }

            return input;
        }

        /// <summary>
        /// The trim leading trailing zeroes.
        /// </summary>
        /// <param name="val">
        /// The val.
        /// </param>
        /// <returns>
        /// The trim leading trailing zeroes.
        /// </returns>
        public static string trimLeadingTrailingZeroes(string val)
        {
            return Regex.Replace(val, @"^(0+)|(?<=\.\d+)(0+)$", string.Empty);
        }

        /// <summary>
        /// The trim leading zeros.
        /// </summary>
        /// <param name="val">
        /// The val.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string trimLeadingZeros(string val)
        {
            if (val != null)
            {
                return val.TrimStart('0');
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// The trim trailing sign.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The trim trailing sign.
        /// </returns>
        public static string trimTrailingSign(string input)
        {
            if (input != null && (input.Trim().EndsWith("+") || input.Trim().EndsWith("-")))
            {
                return input.Remove(input.Length - 1, 1);
            }

            return input;
        }

        /// <summary>
        /// The valuation type.
        /// </summary>
        /// <param name="valuationCode">
        /// The valuation code.
        /// </param>
        /// <returns>
        /// The valuation type.
        /// </returns>
        public static string valuationType(string valuationCode)
        {
            if (valuationCode != null && valuationCode.Length > 0)
            {
                switch (valuationCode.ToUpper())
                {
                    case "FULL":
                        return "Full Appraisal";
                    case "WALK":
                        return "Limited Walkthrough";
                    case "DRIV":
                        return "Driveby Appraisal";
                    case "CDSK":
                        return "Central Desktop Appraisal";
                    case "DESK":
                        return "Desktop Apraisal";
                    case "LDSK":
                        return "Local Desktop Appraisal";
                    case "IBPO":
                        return "Interior Brokers Price Opinion";
                    case "BPO":
                        return "Brokers Price Opinion";
                    case "EBPO":
                        return "Exterior Broker's Price Option";
                    case "AVM":
                        return "Automated Valuation Model";
                    case "STAT":
                        return "Statistical";
                    case "TAV":
                        return "Tax Assessed Value";
                    case "PRIC":
                        return "Documented Sale or Purchase Price";
                    case "BNKV":
                        return "Banker Valuation (Applies to Alaska Only)";
                    case "OEV":
                        return "Customer Stated Value";
                    case "ADJV":
                        return "Adjusted Evaluation Value";
                    default:
                        return valuationCode;
                }
            }

            return null;
        }

        /// <summary>
        /// The get text.
        /// </summary>
        /// <param name="xNode">
        /// The x node.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string getText(XmlNode xNode)
        {
            if (xNode != null)
            {
                return xNode.InnerText;
            }
            else
            {
                return string.Empty;
            }
        }

        #endregion

        /// <summary>
        /// The array length attribute.
        /// </summary>
        public class ArrayLengthAttribute : Attribute
        {
            #region Fields

            /// <summary>
            /// The length.
            /// </summary>
            public int Length;

            #endregion

            #region Constructors and Destructors

            /// <summary>
            /// Initializes a new instance of the <see cref="ArrayLengthAttribute"/> class.
            /// </summary>
            /// <param name="length">
            /// The length.
            /// </param>
            public ArrayLengthAttribute(int length)
            {
                this.Length = length;
            }

            #endregion
        }
    }
}