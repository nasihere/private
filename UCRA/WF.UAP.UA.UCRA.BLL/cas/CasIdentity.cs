// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CasIdentity.cs" company="">
//   
// </copyright>
// <summary>
//   Summary description for CasIdentity.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace WF.EAI.BLL.cas
{
    using System;
    using System.Security.Principal;
    using System.Text;

    /// <summary>
    ///     Summary description for CasIdentity.
    /// </summary>
    [Serializable]
    public class CasIdentity : IPrincipal
    {
        #region Fields

        /// <summary>
        /// The _au.
        /// </summary>
        private string _au = string.Empty;

        /// <summary>
        /// The _bankername.
        /// </summary>
        private string _bankername = string.Empty;

        /// <summary>
        /// The _bankerphone.
        /// </summary>
        private string _bankerphone = string.Empty;

        /// <summary>
        /// The _channel.
        /// </summary>
        private string _channel = string.Empty;

        /// <summary>
        /// The _compass user.
        /// </summary>
        private string _compassUser;

        /// <summary>
        /// The _hris.
        /// </summary>
        private string _hris = string.Empty;

        /// <summary>
        /// The _identity.
        /// </summary>
        private IIdentity _identity;

        /// <summary>
        /// The _listener.
        /// </summary>
        private string _listener = string.Empty;

        /// <summary>
        /// The _loc.
        /// </summary>
        private string _loc = string.Empty;

        /// <summary>
        /// The _login id.
        /// </summary>
        private string _loginId = string.Empty;

        /// <summary>
        /// The _officer.
        /// </summary>
        private string _officer = string.Empty;

        /// <summary>
        /// The _pcs.
        /// </summary>
        private string _pcs = string.Empty;

        /// <summary>
        /// The _roles.
        /// </summary>
        private string[] _roles;

        /// <summary>
        /// The _storename.
        /// </summary>
        private string _storename = string.Empty;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CasIdentity"/> class.
        /// </summary>
        /// <param name="Identity">
        /// The identity.
        /// </param>
        /// <param name="Roles">
        /// The roles.
        /// </param>
        /// <param name="Listener">
        /// The listener.
        /// </param>
        /// <param name="HRIS">
        /// The hris.
        /// </param>
        /// <param name="Loc">
        /// The loc.
        /// </param>
        /// <param name="LoginId">
        /// The login id.
        /// </param>
        /// <param name="Au">
        /// The au.
        /// </param>
        /// <param name="PCS">
        /// The pcs.
        /// </param>
        /// <param name="Channel">
        /// The channel.
        /// </param>
        public CasIdentity(
            IIdentity Identity, 
            string[] Roles, 
            string Listener, 
            string HRIS, 
            string Loc, 
            string LoginId, 
            string Au, 
            string PCS, 
            string Channel)
        {
            this._identity = Identity;
            this._roles = new string[Roles.Length];
            Roles.CopyTo(this._roles, 0);
            Array.Sort(this._roles);
            this._listener = Listener;
            this._hris = HRIS;
            this._loc = Loc;
            this._loginId = LoginId;
            this._au = Au;
            this._pcs = PCS;
            this._channel = Channel;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CasIdentity"/> class.
        /// </summary>
        /// <param name="Identity">
        /// The identity.
        /// </param>
        /// <param name="Roles">
        /// The roles.
        /// </param>
        /// <param name="Listener">
        /// The listener.
        /// </param>
        /// <param name="HRIS">
        /// The hris.
        /// </param>
        /// <param name="Loc">
        /// The loc.
        /// </param>
        /// <param name="LoginId">
        /// The login id.
        /// </param>
        /// <param name="Au">
        /// The au.
        /// </param>
        /// <param name="PCS">
        /// The pcs.
        /// </param>
        /// <param name="Channel">
        /// The channel.
        /// </param>
        /// <param name="Bankername">
        /// The bankername.
        /// </param>
        /// <param name="Officer">
        /// The officer.
        /// </param>
        /// <param name="Storename">
        /// The storename.
        /// </param>
        /// <param name="Bankerphone">
        /// The bankerphone.
        /// </param>
        public CasIdentity(
            IIdentity Identity, 
            string[] Roles, 
            string Listener, 
            string HRIS, 
            string Loc, 
            string LoginId, 
            string Au, 
            string PCS, 
            string Channel, 
            string Bankername, 
            string Officer, 
            string Storename, 
            string Bankerphone)
        {
            this._identity = Identity;
            this._roles = new string[Roles.Length];
            Roles.CopyTo(this._roles, 0);
            Array.Sort(this._roles);
            this._listener = Listener;
            this._hris = HRIS;
            this._loc = Loc;
            this._loginId = LoginId;
            this._au = Au;
            this._pcs = PCS;
            this._channel = Channel;
            this._bankername = Bankername;
            this._officer = Officer;
            this._storename = Storename;
            this._bankerphone = Bankerphone;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CasIdentity"/> class.
        /// </summary>
        /// <param name="Identity">
        /// The identity.
        /// </param>
        /// <param name="Roles">
        /// The roles.
        /// </param>
        /// <param name="Listener">
        /// The listener.
        /// </param>
        /// <param name="HRIS">
        /// The hris.
        /// </param>
        /// <param name="Loc">
        /// The loc.
        /// </param>
        /// <param name="LoginId">
        /// The login id.
        /// </param>
        /// <param name="Au">
        /// The au.
        /// </param>
        /// <param name="PCS">
        /// The pcs.
        /// </param>
        /// <param name="Channel">
        /// The channel.
        /// </param>
        /// <param name="UserName">
        /// The user name.
        /// </param>
        public CasIdentity(
            IIdentity Identity, 
            string[] Roles, 
            string Listener, 
            string HRIS, 
            string Loc, 
            string LoginId, 
            string Au, 
            string PCS, 
            string Channel, 
            string UserName)
        {
            this._identity = Identity;
            this._roles = new string[Roles.Length];
            Roles.CopyTo(this._roles, 0);
            Array.Sort(this._roles);
            this._listener = Listener;
            this._hris = HRIS;
            this._loc = Loc;
            this._loginId = LoginId;
            this._au = Au;
            this._pcs = PCS;
            this._channel = Channel;
            if (Channel.Equals("SFE"))
            {
                this._compassUser = UserName;
            }
            else
            {
                this._bankername = UserName;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CasIdentity"/> class.
        /// </summary>
        /// <param name="Identity">
        /// The identity.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        public CasIdentity(IIdentity Identity, string args)
        {
            this._identity = Identity;

            string[] argList = args.Split(':');

            if (argList != null && argList.Length == 8)
            {
                this._loginId = argList[0];
                this._au = argList[1];
                this._loc = argList[2];
                this._hris = argList[3];
                this._pcs = argList[4];
                this._channel = argList[5];
                if (argList[6].Length > 1)
                {
                    this._compassUser = argList[6];
                }

                this._roles = argList[7].Split('|');
            }

            // for ACR project
            if (argList != null && argList.Length == 12)
            {
                this._loginId = argList[0];
                this._au = argList[1];
                this._loc = argList[2];
                this._hris = argList[3];
                this._pcs = argList[4];
                this._channel = argList[5];
                if (argList[6].Length > 1)
                {
                    this._compassUser = argList[6];
                }

                if (argList[7].Length > 1)
                {
                    this._bankername = argList[7];
                }

                if (argList[8].Length > 1)
                {
                    this._officer = argList[8];
                }

                if (argList[9].Length > 1)
                {
                    this._storename = argList[9];
                }

                if (argList[10].Length > 1)
                {
                    this._bankerphone = argList[10];
                }

                this._roles = argList[11].Split('|');
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the au.
        /// </summary>
        public string Au
        {
            get
            {
                return this._au;
            }

            set
            {
                this._au = value;
            }
        }

        /// <summary>
        /// Gets or sets the bankername.
        /// </summary>
        public string Bankername
        {
            get
            {
                return this._bankername;
            }

            set
            {
                this._bankername = value;
            }
        }

        /// <summary>
        /// Gets or sets the bankerphone.
        /// </summary>
        public string Bankerphone
        {
            get
            {
                return this._bankerphone;
            }

            set
            {
                this._bankerphone = value;
            }
        }

        /// <summary>
        /// Gets or sets the channel.
        /// </summary>
        public string Channel
        {
            get
            {
                return this._channel;
            }

            set
            {
                this._channel = value;
            }
        }

        /// <summary>
        /// Gets or sets the compass user.
        /// </summary>
        public string CompassUser
        {
            get
            {
                return this._compassUser;
            }

            set
            {
                this._compassUser = value;
            }
        }

        /// <summary>
        /// Gets or sets the hris.
        /// </summary>
        public string Hris
        {
            get
            {
                return this._hris;
            }

            set
            {
                this._hris = value;
            }
        }

        /// <summary>
        /// Gets or sets the identity.
        /// </summary>
        public IIdentity Identity
        {
            get
            {
                return this._identity;
            }

            set
            {
                this._identity = value;
            }
        }

        /// <summary>
        /// Gets or sets the listener.
        /// </summary>
        public string Listener
        {
            get
            {
                return this._listener;
            }

            set
            {
                this._listener = value;
            }
        }

        /// <summary>
        /// Gets or sets the loc.
        /// </summary>
        public string Loc
        {
            get
            {
                return this._loc;
            }

            set
            {
                this._loc = value;
            }
        }

        /// <summary>
        /// Gets or sets the login id.
        /// </summary>
        public string LoginId
        {
            get
            {
                return this._loginId;
            }

            set
            {
                this._loginId = value;
            }
        }

        /// <summary>
        /// Gets or sets the officer.
        /// </summary>
        public string Officer
        {
            get
            {
                return this._officer;
            }

            set
            {
                this._officer = value;
            }
        }

        /// <summary>
        /// Gets or sets the pcs.
        /// </summary>
        public string PCS
        {
            get
            {
                return this._pcs;
            }

            set
            {
                this._pcs = value;
            }
        }

        /// <summary>
        /// Gets the role.
        /// </summary>
        public string Role
        {
            get
            {
                return string.Empty;
            }
 // roles are not supported in CAS
        }

        /// <summary>
        /// Gets or sets the storename.
        /// </summary>
        public string Storename
        {
            get
            {
                return this._storename;
            }

            set
            {
                this._storename = value;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The is in all roles.
        /// </summary>
        /// <param name="roles">
        /// The roles.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsInAllRoles(params string[] roles)
        {
            foreach (string searchRole in roles)
            {
                if (Array.BinarySearch(this._roles, searchRole) < 0)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// The is in role.
        /// </summary>
        /// <param name="role">
        /// The role.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsInRole(string role)
        {
            return Array.BinarySearch(this._roles, role) >= 0 ? true : false;
        }

        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public override string ToString()
        {
            string sep = string.Empty;
            StringBuilder builder = new StringBuilder();
            foreach (string role in this._roles)
            {
                builder.Append(sep);
                builder.Append(role);
                sep = "|";
            }

            return string.Format(
                "{0}:{1}:{2}:{3}:{4}:{5}:{6}:{7}:{8}:{9}:{10}:{11}", 
                this._loginId, 
                this._au, 
                this._loc, 
                this._hris, 
                this._pcs, 
                this._channel, 
                this._compassUser, 
                this._bankername, 
                this._officer, 
                this._storename, 
                this._bankerphone, 
                builder);
        }

        /// <summary>
        /// The is in any roles.
        /// </summary>
        /// <param name="roles">
        /// The roles.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool isInAnyRoles(params string[] roles)
        {
            foreach (string searchRole in roles)
            {
                if (Array.BinarySearch(this._roles, searchRole) > 0)
                {
                    return true;
                }
            }

            return false;
        }

        #endregion
    }
}