namespace WF.EAI.BLL.cas.Services
{
    using System;
    using System.Xml.Serialization;

    /// <summary>
    /// The acctdetai l 01.
    /// </summary>
    [Serializable]
    public class ACCTDETAIL01
    {
        #region Fields

        /// <summary>
        /// The account open date.
        /// </summary>
        public string AccountOpenDate;

        /// <summary>
        /// The account status code.
        /// </summary>
        public string AccountStatusCode;

        /// <summary>
        /// The account title list.
        /// </summary>
        [XmlArrayItem("AccountTitle", typeof(AccountTitle), IsNullable = false)]
        public AccountTitle[] AccountTitleList;

        /// <summary>
        /// The amount.
        /// </summary>
        public string Amount;

        /// <summary>
        /// The appraised value.
        /// </summary>
        public string AppraisedValue;

        /// <summary>
        /// The city.
        /// </summary>
        public string City;

        /// <summary>
        /// The current balance.
        /// </summary>
        public string CurrentBalance;

        /// <summary>
        /// The interest rate.
        /// </summary>
        public string InterestRate;

        /// <summary>
        /// The maturity date.
        /// </summary>
        public string MaturityDate;

        /// <summary>
        /// The payment frequency code.
        /// </summary>
        public string PaymentFrequencyCode;

        /// <summary>
        /// The postal code.
        /// </summary>
        public string PostalCode;

        /// <summary>
        /// The relationship.
        /// </summary>
        public string Relationship;

        /// <summary>
        /// The relationship code.
        /// </summary>
        public string RelationshipCode;

        /// <summary>
        /// The second product type.
        /// </summary>
        public string SecondProductType;

        /// <summary>
        /// The state.
        /// </summary>
        public string State;

        /// <summary>
        /// The street address.
        /// </summary>
        public string StreetAddress;

        /// <summary>
        /// The type.
        /// </summary>
        public string Type;

        #endregion
    }
}