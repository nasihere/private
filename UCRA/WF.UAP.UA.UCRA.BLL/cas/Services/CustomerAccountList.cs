// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomerAccountList.cs" company="">
//   
// </copyright>
// <summary>
//   Summary description for CustomerAccountList.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace WF.EAI.BLL.cas.Services
{
    using System.Collections;
    using System.Text;

    /// <summary>
    ///     Summary description for CustomerAccountList.
    /// </summary>
    public class CustomerAccountList
    {
        #region Fields

        /// <summary>
        /// The _accounts.
        /// </summary>
        private ArrayList _accounts;

        /// <summary>
        /// The account seperator.
        /// </summary>
        private string accountSeperator = ":";

        /// <summary>
        /// The account string.
        /// </summary>
        private string accountString = "{0},{1}-{2}";

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerAccountList"/> class.
        /// </summary>
        public CustomerAccountList()
        {
            this._accounts = new ArrayList();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The add.
        /// </summary>
        /// <param name="Account">
        /// The account.
        /// </param>
        public void Add(CustomerAccount Account)
        {
            this._accounts.Add(Account);
        }

        /// <summary>
        /// The contains.
        /// </summary>
        /// <param name="Account">
        /// The account.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool Contains(string Account)
        {
            CustomerAccount[] _temp = (CustomerAccount[])this._accounts.ToArray(typeof(CustomerAccount));
            for (int x = 0; x < _temp.Length; x++)
            {
                if (_temp[x].AccountNumber == Account)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public override string ToString()
        {
            StringBuilder output = new StringBuilder();

            string spaceHolder = string.Empty;

            foreach (CustomerAccount account in this._accounts)
            {
                output.Append(spaceHolder);
                output.Append(
                    string.Format(
                        this.accountString, account.AccountNumber, account.AccountNumber, account.RoutingNumber));
                spaceHolder = this.accountSeperator;
            }

            return output.ToString();
        }

        #endregion
    }

    /// <summary>
    /// The customer account.
    /// </summary>
    public class CustomerAccount
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerAccount"/> class.
        /// </summary>
        public CustomerAccount()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerAccount"/> class.
        /// </summary>
        /// <param name="AccountNumber">
        /// The account number.
        /// </param>
        public CustomerAccount(string AccountNumber)
        {
            this.AccountNumber = AccountNumber;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerAccount"/> class.
        /// </summary>
        /// <param name="AccountNumber">
        /// The account number.
        /// </param>
        /// <param name="RoutingNumber">
        /// The routing number.
        /// </param>
        public CustomerAccount(string AccountNumber, string RoutingNumber)
        {
            this.RoutingNumber = RoutingNumber;
            this.AccountNumber = AccountNumber;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the account number.
        /// </summary>
        public string AccountNumber { get; set; }

        /// <summary>
        /// Gets or sets the product code.
        /// </summary>
        public string ProductCode { get; set; }

        /// <summary>
        /// Gets or sets the routing number.
        /// </summary>
        public string RoutingNumber { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the sub product code.
        /// </summary>
        public string SubProductCode { get; set; }

        #endregion
    }
}