// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CASCollateral.cs" company="">
//   
// </copyright>
// <summary>
//   Summary description for Collateral.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace WF.EAI.BLL.cas.Services.Collateral
{
    using System;

    /// <summary>
    ///     Summary description for Collateral.
    /// </summary>
    [Serializable]
    public class CASCollateral
    {
        #region Fields

        /// <summary>
        /// The collateral one type.
        /// </summary>
        public string collateralOneType = string.Empty;

        /// <summary>
        /// The collateral two type.
        /// </summary>
        public string collateralTwoType = string.Empty;

        /// <summary>
        /// The down payment.
        /// </summary>
        public string downPayment = string.Empty;

        /// <summary>
        /// The fee financed.
        /// </summary>
        public string feeFinanced = string.Empty;

        /// <summary>
        /// The pay off price.
        /// </summary>
        public string payOffPrice = string.Empty;

        /// <summary>
        /// The pcm collaterals.
        /// </summary>
        public PCMCollateral[] pcmCollaterals = new PCMCollateral[2];

        /// <summary>
        /// The primary residence.
        /// </summary>
        public string primaryResidence = string.Empty;

        /// <summary>
        /// The transmission type.
        /// </summary>
        public string transmissionType = string.Empty;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The get collateral one.
        /// </summary>
        /// <returns>
        /// The <see cref="PCMCollateral"/>.
        /// </returns>
        public PCMCollateral getCollateralOne()
        {
            return this.pcmCollaterals[0];
        }

        /// <summary>
        /// The get collateral two.
        /// </summary>
        /// <returns>
        /// The <see cref="PCMCollateral"/>.
        /// </returns>
        public PCMCollateral getCollateralTwo()
        {
            return this.pcmCollaterals[1];
        }

        /// <summary>
        /// The set collateral two.
        /// </summary>
        /// <param name="pcmCollateral">
        /// The pcm collateral.
        /// </param>
        public void setCollateralTwo(PCMCollateral pcmCollateral)
        {
            this.pcmCollaterals[1] = pcmCollateral;
        }

        /// <summary>
        /// The set collatral one.
        /// </summary>
        /// <param name="pcmCollateral">
        /// The pcm collateral.
        /// </param>
        public void setCollatralOne(PCMCollateral pcmCollateral)
        {
            this.pcmCollaterals[0] = pcmCollateral;
        }

        #endregion
    }
}