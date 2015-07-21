// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PCMCollateral.cs" company="">
//   
// </copyright>
// <summary>
//   Summary description for PCMCollateral.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace WF.EAI.BLL.cas.Services.Collateral
{
    using System;

    /// <summary>
    ///     Summary description for PCMCollateral.
    /// </summary>
    [Serializable]
    public class PCMCollateral
    {
        #region Fields

        /// <summary>
        /// The ab c_ ab c_ col l_ ind.
        /// </summary>
        public string ABC_ABC_COLL_IND = string.Empty;

        /// <summary>
        /// The ab c_ ab c_ col l_ make.
        /// </summary>
        public string ABC_ABC_COLL_MAKE = string.Empty;

        /// <summary>
        /// The ab c_ ab c_ col l_ model.
        /// </summary>
        public string ABC_ABC_COLL_MODEL = string.Empty;

        /// <summary>
        /// The ab c_ ab c_ col l_ mode l_ yr.
        /// </summary>
        public string ABC_ABC_COLL_MODEL_YR = string.Empty;

        // public string  ABC_ABC_COLL_SERIAL_NUM = string.Empty;

        // public string ABC_COLL_SELLER_RELATION = string.Empty;
        // public string ABC_VEH_LIC_NUMBER = string.Empty;
        /// <summary>
        /// The ab c_ ab c_ col l_ ne w_ used.
        /// </summary>
        public string ABC_ABC_COLL_NEW_USED = string.Empty;

        /// <summary>
        /// The ab c_ ab c_ col l_ seria l_ num.
        /// </summary>
        public string ABC_ABC_COLL_SERIAL_NUM = string.Empty;

        /// <summary>
        /// The ab c_ col l_ boa t_ g t_5 tons.
        /// </summary>
        public string ABC_COLL_BOAT_GT_5TONS = string.Empty;

        /// <summary>
        /// The ab c_ col l_ deale r_ addr.
        /// </summary>
        public string ABC_COLL_DEALER_ADDR = string.Empty;

        /// <summary>
        /// The ab c_ col l_ deale r_ city.
        /// </summary>
        public string ABC_COLL_DEALER_CITY = string.Empty;

        /// <summary>
        /// The ab c_ col l_ deale r_ state.
        /// </summary>
        public string ABC_COLL_DEALER_STATE = string.Empty;

        /// <summary>
        /// The ab c_ col l_ deale r_ zi p_ code.
        /// </summary>
        public string ABC_COLL_DEALER_ZIP_CODE = string.Empty;

        /// <summary>
        /// The ab c_ col l_ dl r_ name.
        /// </summary>
        public string ABC_COLL_DLR_NAME = string.Empty;

        /// <summary>
        /// The ab c_ col l_ hul l_ mat.
        /// </summary>
        public string ABC_COLL_HULL_MAT = string.Empty;

        /// <summary>
        /// The ab c_ col l_ length.
        /// </summary>
        public string ABC_COLL_LENGTH = string.Empty;

        /// <summary>
        /// The ab c_ col l_ lie n_ addr.
        /// </summary>
        public string ABC_COLL_LIEN_ADDR = string.Empty;

        /// <summary>
        /// The ab c_ col l_ lie n_ city.
        /// </summary>
        public string ABC_COLL_LIEN_CITY = string.Empty;

        /// <summary>
        /// The ab c_ col l_ lie n_ hname.
        /// </summary>
        public string ABC_COLL_LIEN_HNAME = string.Empty;

        /// <summary>
        /// The ab c_ col l_ lie n_ state.
        /// </summary>
        public string ABC_COLL_LIEN_STATE = string.Empty;

        /// <summary>
        /// The ab c_ col l_ lie n_ zi p_ code.
        /// </summary>
        public string ABC_COLL_LIEN_ZIP_CODE = string.Empty;

        /// <summary>
        /// The ab c_ col l_ mileage.
        /// </summary>
        public string ABC_COLL_MILEAGE = string.Empty;

        /// <summary>
        /// The ab c_ col l_ plan e_ hsp.
        /// </summary>
        public string ABC_COLL_PLANE_HSP = string.Empty;

        /// <summary>
        /// The ab c_ col l_ r v_ type.
        /// </summary>
        public string ABC_COLL_RV_TYPE = string.Empty;

        /// <summary>
        /// The ab c_ col l_ sale s_ price.
        /// </summary>
        public string ABC_COLL_SALES_PRICE = string.Empty;

        /// <summary>
        /// The ab c_ col l_ selle r_ addr.
        /// </summary>
        public string ABC_COLL_SELLER_ADDR = string.Empty;

        /// <summary>
        /// The ab c_ col l_ selle r_ city.
        /// </summary>
        public string ABC_COLL_SELLER_CITY = string.Empty;

        /// <summary>
        /// The ab c_ col l_ selle r_ name.
        /// </summary>
        public string ABC_COLL_SELLER_NAME = string.Empty;

        /// <summary>
        /// The ab c_ col l_ selle r_ nam e 2.
        /// </summary>
        public string ABC_COLL_SELLER_NAME2 = string.Empty;

        /// <summary>
        /// The ab c_ col l_ selle r_ state.
        /// </summary>
        public string ABC_COLL_SELLER_STATE = string.Empty;

        /// <summary>
        /// The ab c_ col l_ selle r_ type.
        /// </summary>
        public string ABC_COLL_SELLER_TYPE = string.Empty;

        /// <summary>
        /// The ab c_ col l_ selle r_ zi p_ code.
        /// </summary>
        public string ABC_COLL_SELLER_ZIP_CODE = string.Empty;

        /// <summary>
        /// The ab c_ col l_ s t_ regis.
        /// </summary>
        public string ABC_COLL_ST_REGIS = string.Empty;

        /// <summary>
        /// The ab c_ col l_ weight.
        /// </summary>
        public string ABC_COLL_WEIGHT = string.Empty;

        /// <summary>
        /// The ab c_ col l_ width.
        /// </summary>
        public string ABC_COLL_WIDTH = string.Empty;

        /// <summary>
        /// The ab c_ inbr d_ mak e_1.
        /// </summary>
        public string ABC_INBRD_MAKE_1 = string.Empty;

        /// <summary>
        /// The ab c_ inbr d_ mak e_2.
        /// </summary>
        public string ABC_INBRD_MAKE_2 = string.Empty;

        /// <summary>
        /// The ab c_ inbr d_ mode l_1.
        /// </summary>
        public string ABC_INBRD_MODEL_1 = string.Empty;

        /// <summary>
        /// The ab c_ inbr d_ mode l_2.
        /// </summary>
        public string ABC_INBRD_MODEL_2 = string.Empty;

        /// <summary>
        /// The ab c_ inbr d_ moto r_ s n_1.
        /// </summary>
        public string ABC_INBRD_MOTOR_SN_1 = string.Empty;

        /// <summary>
        /// The ab c_ inbr d_ moto r_ s n_2.
        /// </summary>
        public string ABC_INBRD_MOTOR_SN_2 = string.Empty;

        /// <summary>
        /// The ab c_ inbr d_ moto r_ yea r_1.
        /// </summary>
        public string ABC_INBRD_MOTOR_YEAR_1 = string.Empty;

        /// <summary>
        /// The ab c_ inbr d_ moto r_ yea r_2.
        /// </summary>
        public string ABC_INBRD_MOTOR_YEAR_2 = string.Empty;

        /// <summary>
        /// The ab c_ inbr d_ ne w_ use d_1.
        /// </summary>
        public string ABC_INBRD_NEW_USED_1 = string.Empty;

        /// <summary>
        /// The ab c_ inbr d_ ne w_ use d_2.
        /// </summary>
        public string ABC_INBRD_NEW_USED_2 = string.Empty;

        /// <summary>
        /// The ab c_ o b_ moto r_ make.
        /// </summary>
        public string ABC_OB_MOTOR_MAKE = string.Empty;

        /// <summary>
        /// The ab c_ o b_ moto r_ model.
        /// </summary>
        public string ABC_OB_MOTOR_MODEL = string.Empty;

        /// <summary>
        /// The ab c_ o b_ moto r_ number.
        /// </summary>
        public string ABC_OB_MOTOR_NUMBER = string.Empty;

        /// <summary>
        /// The ab c_ o b_ moto r_ year.
        /// </summary>
        public string ABC_OB_MOTOR_YEAR = string.Empty;

        /// <summary>
        /// The ab c_ to t_ hs p_ amount.
        /// </summary>
        public string ABC_TOT_HSP_AMOUNT = string.Empty;

        /// <summary>
        /// The ab c_ traile r_ axles.
        /// </summary>
        public string ABC_TRAILER_AXLES = string.Empty;

        /// <summary>
        /// The ab c_ traile r_ li c_ num.
        /// </summary>
        public string ABC_TRAILER_LIC_NUM = string.Empty;

        /// <summary>
        /// The ab c_ traile r_ make.
        /// </summary>
        public string ABC_TRAILER_MAKE = string.Empty;

        /// <summary>
        /// The ab c_ traile r_ model.
        /// </summary>
        public string ABC_TRAILER_MODEL = string.Empty;

        /// <summary>
        /// The ab c_ traile r_ mode l_ yr.
        /// </summary>
        public string ABC_TRAILER_MODEL_YR = string.Empty;

        /// <summary>
        /// The ab c_ traile r_ seria l_ num.
        /// </summary>
        public string ABC_TRAILER_SERIAL_NUM = string.Empty;

        /// <summary>
        /// The ac down payment.
        /// </summary>
        public string ACDownPayment = string.Empty;

        /// <summary>
        /// The ac l_ ac l_ ne w_ cas h_ dow n_ amt.
        /// </summary>
        public string ACL_ACL_NEW_CASH_DOWN_AMT = string.Empty;

        /// <summary>
        /// The ac l_ ac l_ spee d_ type.
        /// </summary>
        public string ACL_ACL_SPEED_TYPE = string.Empty;

        /// <summary>
        /// The ac l_ w f_ ac l_ fee s_ financed.
        /// </summary>
        public string ACL_WF_ACL_FEES_FINANCED = string.Empty;

        /// <summary>
        /// The ac l_ w f_ ac l_ payof f_ price.
        /// </summary>
        public string ACL_WF_ACL_PAYOFF_PRICE = string.Empty;

        /// <summary>
        /// The ac sales price.
        /// </summary>
        public string ACSalesPrice = string.Empty;

        /// <summary>
        /// The a cengine horsepower.
        /// </summary>
        public string ACengineHorsepower = string.Empty;

        /// <summary>
        /// The a cmake.
        /// </summary>
        public string ACmake = string.Empty;

        /// <summary>
        /// The a cmodel.
        /// </summary>
        public string ACmodel = string.Empty;

        /// <summary>
        /// The a cmodel year.
        /// </summary>
        public string ACmodelYear = string.Empty;

        /// <summary>
        /// The a cnew vehicle indicator.
        /// </summary>
        public string ACnewVehicleIndicator = string.Empty;

        /// <summary>
        /// The a cserial num.
        /// </summary>
        public string ACserialNum = string.Empty;

        /// <summary>
        /// The a ctype purchase.
        /// </summary>
        public string ACtypePurchase = string.Empty;

        /// <summary>
        /// The bk g_ pc m_ col l_ i s_ primar y_ res.
        /// </summary>
        public string BKG_PCM_COLL_IS_PRIMARY_RES = string.Empty;

        /// <summary>
        /// The b ydisplacement weight.
        /// </summary>
        public string BYdisplacementWeight = string.Empty;

        /// <summary>
        /// The b ydown payment.
        /// </summary>
        public string BYdownPayment = string.Empty;

        /// <summary>
        /// The b yengine 1 horsepower.
        /// </summary>
        public string BYengine1Horsepower = string.Empty;

        /// <summary>
        /// The b yengine 1 serial num.
        /// </summary>
        public string BYengine1SerialNum = string.Empty;

        /// <summary>
        /// The b yengine 1 type.
        /// </summary>
        public string BYengine1Type = string.Empty;

        /// <summary>
        /// The b yengine 1 year.
        /// </summary>
        public string BYengine1Year = string.Empty;

        /// <summary>
        /// The b yengine 1 manufacturer.
        /// </summary>
        public string BYengine1manufacturer = string.Empty;

        /// <summary>
        /// The b yengine 1 model.
        /// </summary>
        public string BYengine1model = string.Empty;

        /// <summary>
        /// The b yengine 1 new or used indicator.
        /// </summary>
        public string BYengine1newOrUsedIndicator = string.Empty;

        /// <summary>
        /// The b yengine 2 horsepower.
        /// </summary>
        public string BYengine2Horsepower = string.Empty;

        /// <summary>
        /// The b yengine 2 serial num.
        /// </summary>
        public string BYengine2SerialNum = string.Empty;

        /// <summary>
        /// The b yengine 2 type.
        /// </summary>
        public string BYengine2Type = string.Empty;

        /// <summary>
        /// The b yengine 2 year.
        /// </summary>
        public string BYengine2Year = string.Empty;

        /// <summary>
        /// The b yengine 2 manufacturer.
        /// </summary>
        public string BYengine2manufacturer = string.Empty;

        /// <summary>
        /// The b yengine 2 model.
        /// </summary>
        public string BYengine2model = string.Empty;

        /// <summary>
        /// The b yengine 2 new or used indicator.
        /// </summary>
        public string BYengine2newOrUsedIndicator = string.Empty;

        /// <summary>
        /// The b yexisting lien payoff.
        /// </summary>
        public string BYexistingLienPayoff = string.Empty;

        /// <summary>
        /// The b yhull material.
        /// </summary>
        public string BYhullMaterial = string.Empty;

        /// <summary>
        /// The b ylength.
        /// </summary>
        public string BYlength = string.Empty;

        /// <summary>
        /// The b ymanufacturer.
        /// </summary>
        public string BYmanufacturer = string.Empty;

        /// <summary>
        /// The b ymodel.
        /// </summary>
        public string BYmodel = string.Empty;

        /// <summary>
        /// The b ymodel year.
        /// </summary>
        public string BYmodelYear = string.Empty;

        /// <summary>
        /// The b ynew vehicle indicator.
        /// </summary>
        public string BYnewVehicleIndicator = string.Empty;

        /// <summary>
        /// The b yregistration fees financed yn.
        /// </summary>
        public string BYregistrationFeesFinancedYN = string.Empty;

        /// <summary>
        /// The b ysales price.
        /// </summary>
        public string BYsalesPrice = string.Empty;

        /// <summary>
        /// The b yserial number.
        /// </summary>
        public string BYserialNumber = string.Empty;

        /// <summary>
        /// The b ytrailer axel ind.
        /// </summary>
        public string BYtrailerAxelInd = string.Empty;

        /// <summary>
        /// The b ytrailer manufacturer.
        /// </summary>
        public string BYtrailerManufacturer = string.Empty;

        /// <summary>
        /// The b ytrailer model year.
        /// </summary>
        public string BYtrailerModelYear = string.Empty;

        /// <summary>
        /// The b ytrailer serial num.
        /// </summary>
        public string BYtrailerSerialNum = string.Empty;

        /// <summary>
        /// The cd c_ c d_ ma t_ dat e_1.
        /// </summary>
        public string CDC_CD_MAT_DATE_1 = string.Empty;

        /// <summary>
        /// The cd c_ c d_ ma t_ dat e_2.
        /// </summary>
        public string CDC_CD_MAT_DATE_2 = string.Empty;

        /// <summary>
        /// The cd c_ c d_ ma t_ dat e_3.
        /// </summary>
        public string CDC_CD_MAT_DATE_3 = string.Empty;

        /// <summary>
        /// The cd c_ c d_ ma t_ dat e_4.
        /// </summary>
        public string CDC_CD_MAT_DATE_4 = string.Empty;

        /// <summary>
        /// The cd c_ c d_ sa v_ in d_1.
        /// </summary>
        public string CDC_CD_SAV_IND_1 = string.Empty;

        /// <summary>
        /// The cd c_ c d_ sa v_ in d_2.
        /// </summary>
        public string CDC_CD_SAV_IND_2 = string.Empty;

        /// <summary>
        /// The cd c_ c d_ sa v_ in d_3.
        /// </summary>
        public string CDC_CD_SAV_IND_3 = string.Empty;

        /// <summary>
        /// The cd c_ c d_ sa v_ in d_4.
        /// </summary>
        public string CDC_CD_SAV_IND_4 = string.Empty;

        /// <summary>
        /// The cd c_ col l_ acc t_ nu m_1.
        /// </summary>
        public string CDC_COLL_ACCT_NUM_1 = string.Empty;

        /// <summary>
        /// The cd c_ col l_ acc t_ nu m_2.
        /// </summary>
        public string CDC_COLL_ACCT_NUM_2 = string.Empty;

        /// <summary>
        /// The cd c_ col l_ acc t_ nu m_3.
        /// </summary>
        public string CDC_COLL_ACCT_NUM_3 = string.Empty;

        /// <summary>
        /// The cd c_ col l_ acc t_ nu m_4.
        /// </summary>
        public string CDC_COLL_ACCT_NUM_4 = string.Empty;

        /// <summary>
        /// The cd c_ sa v_ c d_ am t_1.
        /// </summary>
        public string CDC_SAV_CD_AMT_1 = string.Empty;

        /// <summary>
        /// The cd c_ sa v_ c d_ am t_2.
        /// </summary>
        public string CDC_SAV_CD_AMT_2 = string.Empty;

        /// <summary>
        /// The cd c_ sa v_ c d_ am t_3.
        /// </summary>
        public string CDC_SAV_CD_AMT_3 = string.Empty;

        /// <summary>
        /// The cd c_ sa v_ c d_ am t_4.
        /// </summary>
        public string CDC_SAV_CD_AMT_4 = string.Empty;

        /// <summary>
        /// The cd c_ sa v_ c d_ nam e_1.
        /// </summary>
        public string CDC_SAV_CD_NAME_1 = string.Empty;

        /// <summary>
        /// The cd c_ sa v_ c d_ nam e_2.
        /// </summary>
        public string CDC_SAV_CD_NAME_2 = string.Empty;

        /// <summary>
        /// The cd c_ sa v_ c d_ nam e_3.
        /// </summary>
        public string CDC_SAV_CD_NAME_3 = string.Empty;

        /// <summary>
        /// The cd c_ sa v_ c d_ nam e_4.
        /// </summary>
        public string CDC_SAV_CD_NAME_4 = string.Empty;

        /// <summary>
        /// The cd savings coll item list.
        /// </summary>
        public CDSavingsCollateralItem[] CDSavingsCollItemList;

        /// <summary>
        /// The m cdown payment.
        /// </summary>
        public string MCdownPayment = string.Empty;

        /// <summary>
        /// The m cexisting lien payoff.
        /// </summary>
        public string MCexistingLienPayoff = string.Empty;

        /// <summary>
        /// The m cmake.
        /// </summary>
        public string MCmake = string.Empty;

        /// <summary>
        /// The m cmileage.
        /// </summary>
        public string MCmileage = string.Empty;

        /// <summary>
        /// The m cmodel.
        /// </summary>
        public string MCmodel = string.Empty;

        /// <summary>
        /// The m cmodel year.
        /// </summary>
        public string MCmodelYear = string.Empty;

        /// <summary>
        /// The m cnew vehicle indicator.
        /// </summary>
        public string MCnewVehicleIndicator = string.Empty;

        /// <summary>
        /// The m cregistration fees financed yn.
        /// </summary>
        public string MCregistrationFeesFinancedYN = string.Empty;

        /// <summary>
        /// The m csales price.
        /// </summary>
        public string MCsalesPrice = string.Empty;

        /// <summary>
        /// The m ctype purchase.
        /// </summary>
        public string MCtypePurchase = string.Empty;

        /// <summary>
        /// The m cvehicle id num.
        /// </summary>
        public string MCvehicleIDNum = string.Empty;

        /// <summary>
        /// The sb c_ bon d_ am t_1.
        /// </summary>
        public string SBC_BOND_AMT_1 = string.Empty;

        /// <summary>
        /// The sb c_ bon d_ am t_2.
        /// </summary>
        public string SBC_BOND_AMT_2 = string.Empty;

        /// <summary>
        /// The sb c_ bon d_ am t_3.
        /// </summary>
        public string SBC_BOND_AMT_3 = string.Empty;

        /// <summary>
        /// The sb c_ bon d_ am t_4.
        /// </summary>
        public string SBC_BOND_AMT_4 = string.Empty;

        /// <summary>
        /// The sb c_ cusi p_ nu m_1.
        /// </summary>
        public string SBC_CUSIP_NUM_1 = string.Empty;

        /// <summary>
        /// The sb c_ cusi p_ nu m_2.
        /// </summary>
        public string SBC_CUSIP_NUM_2 = string.Empty;

        /// <summary>
        /// The sb c_ cusi p_ nu m_3.
        /// </summary>
        public string SBC_CUSIP_NUM_3 = string.Empty;

        /// <summary>
        /// The sb c_ cusi p_ nu m_4.
        /// </summary>
        public string SBC_CUSIP_NUM_4 = string.Empty;

        /// <summary>
        /// The sb c_ maturin g_ dat e_1.
        /// </summary>
        public string SBC_MATURING_DATE_1 = string.Empty;

        /// <summary>
        /// The sb c_ maturin g_ dat e_2.
        /// </summary>
        public string SBC_MATURING_DATE_2 = string.Empty;

        /// <summary>
        /// The sb c_ maturin g_ dat e_3.
        /// </summary>
        public string SBC_MATURING_DATE_3 = string.Empty;

        /// <summary>
        /// The sb c_ maturin g_ dat e_4.
        /// </summary>
        public string SBC_MATURING_DATE_4 = string.Empty;

        /// <summary>
        /// The sb c_ secu r_ issue r_ des c_1_1.
        /// </summary>
        public string SBC_SECUR_ISSUER_DESC_1_1 = string.Empty;

        /// <summary>
        /// The sb c_ secu r_ issue r_ des c_1_2.
        /// </summary>
        public string SBC_SECUR_ISSUER_DESC_1_2 = string.Empty;

        /// <summary>
        /// The sb c_ secu r_ issue r_ des c_1_3.
        /// </summary>
        public string SBC_SECUR_ISSUER_DESC_1_3 = string.Empty;

        /// <summary>
        /// The sb c_ secu r_ issue r_ des c_1_4.
        /// </summary>
        public string SBC_SECUR_ISSUER_DESC_1_4 = string.Empty;

        /// <summary>
        /// The sb c_ stc k_ bon d_ nu m_ shr s_1.
        /// </summary>
        public string SBC_STCK_BOND_NUM_SHRS_1 = string.Empty;

        /// <summary>
        /// The sb c_ stc k_ bon d_ nu m_ shr s_2.
        /// </summary>
        public string SBC_STCK_BOND_NUM_SHRS_2 = string.Empty;

        /// <summary>
        /// The sb c_ stc k_ bon d_ nu m_ shr s_3.
        /// </summary>
        public string SBC_STCK_BOND_NUM_SHRS_3 = string.Empty;

        /// <summary>
        /// The sb c_ stc k_ bon d_ nu m_ shr s_4.
        /// </summary>
        public string SBC_STCK_BOND_NUM_SHRS_4 = string.Empty;

        /// <summary>
        /// The acregistration fees financed yn.
        /// </summary>
        public string acregistrationFeesFinancedYN = string.Empty;

        /// <summary>
        /// The auto crt yn new.
        /// </summary>
        public string autoCrtYNNew = string.Empty;

        /// <summary>
        /// The auto existing lien payoff.
        /// </summary>
        public string autoExistingLienPayoff = string.Empty;

        /// <summary>
        /// The auto purpose new.
        /// </summary>
        public string autoPurposeNew = string.Empty;

        /// <summary>
        /// The auto trans type new.
        /// </summary>
        public string autoTransTypeNew = string.Empty;

        /// <summary>
        /// The autodown payment new.
        /// </summary>
        public string autodownPaymentNew = string.Empty;

        /// <summary>
        /// The automake new.
        /// </summary>
        public string automakeNew = string.Empty;

        /// <summary>
        /// The automileage new.
        /// </summary>
        public string automileageNew = string.Empty;

        /// <summary>
        /// The automodel new.
        /// </summary>
        public string automodelNew = string.Empty;

        /// <summary>
        /// The automodel year new.
        /// </summary>
        public string automodelYearNew = string.Empty;

        /// <summary>
        /// The autonew vehicle indicator new.
        /// </summary>
        public string autonewVehicleIndicatorNew = string.Empty;

        /// <summary>
        /// The autoregistration fees financed yn new.
        /// </summary>
        public string autoregistrationFeesFinancedYNNew = string.Empty;

        /// <summary>
        /// The autosales price new.
        /// </summary>
        public string autosalesPriceNew = string.Empty;

        /// <summary>
        /// The autotype purchase new.
        /// </summary>
        public string autotypePurchaseNew = string.Empty;

        /// <summary>
        /// The autovehicle id num new.
        /// </summary>
        public string autovehicleIDNumNew = string.Empty;

        /// <summary>
        /// The coll primary resi ques.
        /// </summary>
        public string collPrimaryResiQues = string.Empty;

        /// <summary>
        /// The collateral type.
        /// </summary>
        public string collateralType = string.Empty;

        /// <summary>
        /// The dealer address.
        /// </summary>
        public string dealerAddress = string.Empty;

        /// <summary>
        /// The dealer city.
        /// </summary>
        public string dealerCity = string.Empty;

        /// <summary>
        /// The dealer name.
        /// </summary>
        public string dealerName = string.Empty;

        /// <summary>
        /// The dealer state.
        /// </summary>
        public string dealerState = string.Empty;

        /// <summary>
        /// The dealer zip.
        /// </summary>
        public string dealerZip = string.Empty;

        /// <summary>
        /// The four by four indicator.
        /// </summary>
        public string fourByFourIndicator = string.Empty;

        /// <summary>
        /// The lien holder address.
        /// </summary>
        public string lienHolderAddress = string.Empty;

        /// <summary>
        /// The lien holder city.
        /// </summary>
        public string lienHolderCity = string.Empty;

        /// <summary>
        /// The lien holder name.
        /// </summary>
        public string lienHolderName = string.Empty;

        /// <summary>
        /// The lien holder state.
        /// </summary>
        public string lienHolderState = string.Empty;

        /// <summary>
        /// The lien holder zip.
        /// </summary>
        public string lienHolderZip = string.Empty;

        /// <summary>
        /// The mix facct num 1.
        /// </summary>
        public string mixFacctNum1 = string.Empty;

        /// <summary>
        /// The mix facct num 2.
        /// </summary>
        public string mixFacctNum2 = string.Empty;

        /// <summary>
        /// The mix facct num 3.
        /// </summary>
        public string mixFacctNum3 = string.Empty;

        /// <summary>
        /// The mix facct num 4.
        /// </summary>
        public string mixFacctNum4 = string.Empty;

        /// <summary>
        /// The mix facct sbs num 1.
        /// </summary>
        public string mixFacctSBSNum1 = string.Empty;

        /// <summary>
        /// The mix facct sbs num 2.
        /// </summary>
        public string mixFacctSBSNum2 = string.Empty;

        /// <summary>
        /// The mix facct sbs num 3.
        /// </summary>
        public string mixFacctSBSNum3 = string.Empty;

        /// <summary>
        /// The mix facct sbs num 4.
        /// </summary>
        public string mixFacctSBSNum4 = string.Empty;

        /// <summary>
        /// The mix famount 1.
        /// </summary>
        public string mixFamount1 = string.Empty;

        /// <summary>
        /// The mix famount 2.
        /// </summary>
        public string mixFamount2 = string.Empty;

        /// <summary>
        /// The mix famount 3.
        /// </summary>
        public string mixFamount3 = string.Empty;

        /// <summary>
        /// The mix famount 4.
        /// </summary>
        public string mixFamount4 = string.Empty;

        /// <summary>
        /// The mix ffinancial coll category 1.
        /// </summary>
        public string mixFfinancialCollCategory1 = string.Empty;

        /// <summary>
        /// The mix ffinancial coll category 2.
        /// </summary>
        public string mixFfinancialCollCategory2 = string.Empty;

        /// <summary>
        /// The mix ffinancial coll category 3.
        /// </summary>
        public string mixFfinancialCollCategory3 = string.Empty;

        /// <summary>
        /// The mix ffinancial coll category 4.
        /// </summary>
        public string mixFfinancialCollCategory4 = string.Empty;

        /// <summary>
        /// The mix fin name of 1.
        /// </summary>
        public string mixFinNameOf1 = string.Empty;

        /// <summary>
        /// The mix fin name of 2.
        /// </summary>
        public string mixFinNameOf2 = string.Empty;

        /// <summary>
        /// The mix fin name of 3.
        /// </summary>
        public string mixFinNameOf3 = string.Empty;

        /// <summary>
        /// The mix fin name of 4.
        /// </summary>
        public string mixFinNameOf4 = string.Empty;

        /// <summary>
        /// The mix fmaturing date 1.
        /// </summary>
        public string mixFmaturingDate1 = string.Empty;

        /// <summary>
        /// The mix fmaturing date 2.
        /// </summary>
        public string mixFmaturingDate2 = string.Empty;

        /// <summary>
        /// The mix fmaturing date 3.
        /// </summary>
        public string mixFmaturingDate3 = string.Empty;

        /// <summary>
        /// The mix fmaturing date 4.
        /// </summary>
        public string mixFmaturingDate4 = string.Empty;

        /// <summary>
        /// The mob hdown payment.
        /// </summary>
        public string mobHdownPayment = string.Empty;

        /// <summary>
        /// The mob hlength.
        /// </summary>
        public string mobHlength = string.Empty;

        /// <summary>
        /// The mob hmake.
        /// </summary>
        public string mobHmake = string.Empty;

        /// <summary>
        /// The mob hmodel.
        /// </summary>
        public string mobHmodel = string.Empty;

        /// <summary>
        /// The mob hmodel year.
        /// </summary>
        public string mobHmodelYear = string.Empty;

        /// <summary>
        /// The mob hnew vehicle indicator.
        /// </summary>
        public string mobHnewVehicleIndicator = string.Empty;

        /// <summary>
        /// The mob hregistration fees financed yn.
        /// </summary>
        public string mobHregistrationFeesFinancedYN = string.Empty;

        /// <summary>
        /// The mob hsales p rice.
        /// </summary>
        public string mobHsalesPRice = string.Empty;

        /// <summary>
        /// The mob htype purchase.
        /// </summary>
        public string mobHtypePurchase = string.Empty;

        /// <summary>
        /// The mob hvehicle id num.
        /// </summary>
        public string mobHvehicleIDNum = string.Empty;

        /// <summary>
        /// The mot hdown payment.
        /// </summary>
        public string motHdownPayment = string.Empty;

        /// <summary>
        /// The mot hlength.
        /// </summary>
        public string motHlength = string.Empty;

        /// <summary>
        /// The mot hmanufacturer.
        /// </summary>
        public string motHmanufacturer = string.Empty;

        /// <summary>
        /// The mot hmileage.
        /// </summary>
        public string motHmileage = string.Empty;

        /// <summary>
        /// The mot hmodel.
        /// </summary>
        public string motHmodel = string.Empty;

        /// <summary>
        /// The mot hmodel year.
        /// </summary>
        public string motHmodelYear = string.Empty;

        /// <summary>
        /// The mot hnew vehicle indicator.
        /// </summary>
        public string motHnewVehicleIndicator = string.Empty;

        /// <summary>
        /// The mot hregistration fees financed yn.
        /// </summary>
        public string motHregistrationFeesFinancedYN = string.Empty;

        /// <summary>
        /// The mot hsales price.
        /// </summary>
        public string motHsalesPrice = string.Empty;

        /// <summary>
        /// The mot htype purchase.
        /// </summary>
        public string motHtypePurchase = string.Empty;

        /// <summary>
        /// The mot hvehicle id num.
        /// </summary>
        public string motHvehicleIDNum = string.Empty;

        /// <summary>
        /// The purchase purpose code.
        /// </summary>
        public string purchasePurposeCode = string.Empty;

        /// <summary>
        /// The self titling ind.
        /// </summary>
        public string selfTitlingInd = string.Empty;

        /// <summary>
        /// The seller 1 address.
        /// </summary>
        public string seller1Address = string.Empty;

        /// <summary>
        /// The seller 1 city.
        /// </summary>
        public string seller1City = string.Empty;

        /// <summary>
        /// The seller 1 name.
        /// </summary>
        public string seller1Name = string.Empty;

        /// <summary>
        /// The seller 1 state.
        /// </summary>
        public string seller1State = string.Empty;

        /// <summary>
        /// The seller 1 zip.
        /// </summary>
        public string seller1Zip = string.Empty;

        /// <summary>
        /// The seller 2 name.
        /// </summary>
        public string seller2Name = string.Empty;

        /// <summary>
        /// The t 5 vcp existing lien payoff.
        /// </summary>
        public string t5vcpExistingLienPayoff = string.Empty;

        /// <summary>
        /// The t 5 vcp vehicle type.
        /// </summary>
        public string t5vcpVehicleType = string.Empty;

        /// <summary>
        /// The t 5 vcpdown payment.
        /// </summary>
        public string t5vcpdownPayment = string.Empty;

        /// <summary>
        /// The t 5 vcplength.
        /// </summary>
        public string t5vcplength = string.Empty;

        /// <summary>
        /// The t 5 vcpmake.
        /// </summary>
        public string t5vcpmake = string.Empty;

        /// <summary>
        /// The t 5 vcpmileage.
        /// </summary>
        public string t5vcpmileage = string.Empty;

        /// <summary>
        /// The t 5 vcpmodel.
        /// </summary>
        public string t5vcpmodel = string.Empty;

        /// <summary>
        /// The t 5 vcpmodel year.
        /// </summary>
        public string t5vcpmodelYear = string.Empty;

        /// <summary>
        /// The t 5 vcpnew vehicle indicator.
        /// </summary>
        public string t5vcpnewVehicleIndicator = string.Empty;

        /// <summary>
        /// The t 5 vcpregistration fees financed yn.
        /// </summary>
        public string t5vcpregistrationFeesFinancedYN = string.Empty;

        /// <summary>
        /// The t 5 vcpsales price.
        /// </summary>
        public string t5vcpsalesPrice = string.Empty;

        /// <summary>
        /// The t 5 vcptype purchase.
        /// </summary>
        public string t5vcptypePurchase = string.Empty;

        /// <summary>
        /// The t 5 vcpvehicle id num.
        /// </summary>
        public string t5vcpvehicleIDNum = string.Empty;

        #endregion
    }
}