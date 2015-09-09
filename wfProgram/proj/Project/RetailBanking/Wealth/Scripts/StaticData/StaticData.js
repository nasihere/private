//var Common = {

//    "ddBalloonOption":
//    {
//        "element":
//            [
//                { "key": null, "value": "Select" },
//                { "key": "0", "value": "None" },
//                { "key": "15", "value": "15 Yrs." },
//                { "key": "30", "value": "30 Yrs." }
//            ]
//    }
//};
var CommonNew = {};
var Common =
    {
        SelectYesNo:
        [
            { key: '', value: 'Select' },
            { key: 'Y', value: 'Yes' },
            { key: 'N', value: 'No' }
        ],

        SelectBackgroundList: [
           { key: 'bankruptcyInd', value: 'Have you ever filed bankruptcy or have you ever been a Principal or Guarantor of a business entity that filed Bankruptcy, or was the debt in an involuntary bankruptcy case?' },
           { key: 'felonyInd', value: 'Have you ever been convicted of a felony?' },
           { key: 'lawsuitInd', value: 'Are you a party to any claims or lawsuits?' }
        ],

        SelectYesNoNotRequired:
        [
            { key: '', value: 'Select' },
            { key: 'Y', value: 'Yes' },
            { key: 'N', value: 'No' },
            { key: 'NR', value: 'Not Required' }
        ],

        TextWIDisclosureContent: "If you are married, by submitting your loan application you are confirming that this loan obligation is being incurred in the interest of your marriage and your family. No provision of any marital property agreement (pre-marital agreement), unilateral statement under 766.59 of the Wisconsin Statutes, or a court decree under 766.70 adversely affects the interest of the lender unless the lender, prior to the time the credit is granted, is furnished a copy of the agreement, statement or decrees or has actual knowledge of the adverse provision. If the loan for which you are applying is granted, you will notify the Bank if you have a spouse who needs to receive notification that credit has been extended to you.",

        SelectYesNoNotApplicable:
        [
            { key: '', value: 'Select' },
            { key: 'Y', value: 'Yes' },
            { key: 'N', value: 'No' },
            { key: 'NA', value: 'Not Applicable' }
        ],

        SelectYesNoNotProvided:
        [
            { key: '', value: 'Select' },
            { key: 'Y', value: 'Yes' },
            { key: 'N', value: 'No' },
            { key: 'NP', value: 'Information not provided by Applicant in person, mail, internet or telephone application' }
        ],

        SelectPreferredTimeToCall:
        [
            { key: '', value: 'Select' },
            { key: 'M', value: 'Morning' },
            { key: 'A', value: 'Afternoon' },
            { key: 'E', value: 'Evening' }
        ],

        SelectAppSpouseJoint:
        [
           { key: '', value: 'Select' },
           { key: 'A', value: 'Applicant' },
           { key: 'S', value: 'Spouse' },
           { key: 'J', value: 'Joint' }
        ],

        DefaultCheckingAcc:
        [
            { acctVal: '', acctNum: 'Select' },
            { acctVal: '56456456', acctNum: '456456456' },
            { acctVal: '34474565', acctNum: '344745655' },
            { acctVal: '78678678', acctNum: '786786784' },
            { acctVal: 'O', acctNum: 'Other' }
        ],

        usStates: [{ name: "Select", abbreviation: "" }, { name: "ALABAMA", abbreviation: "AL" }, { name: "ALASKA", abbreviation: "AK" }, { name: "AMERICAN SAMOA", abbreviation: "AS" }, { name: "ARIZONA", abbreviation: "AZ" }, { name: "ARKANSAS", abbreviation: "AR" }, { name: "ARMED FORCES IN THE AMERICAS EXCEPT CANADA", abbreviation: "AA" }, { name: "ARMED FORCES IN EUROPE, MIDDLE EAST, AFRICA AND CANADA", abbreviation: "AE" }, { name: "ARMED FORCES IN THE PACIFIC", abbreviation: "AP" }, { name: "CALIFORNIA", abbreviation: "CA" }, { name: "COLORADO", abbreviation: "CO" }, { name: "CONNECTICUT", abbreviation: "CT" }, { name: "DELAWARE", abbreviation: "DE" }, { name: "DISTRICT OF COLUMBIA", abbreviation: "DC" }, { name: "FEDERATED STATES OF MICRONESIA", abbreviation: "FM" }, { name: "FLORIDA", abbreviation: "FL" }, { name: "GEORGIA", abbreviation: "GA" }, { name: "GUAM", abbreviation: "GU" }, { name: "HAWAII", abbreviation: "HI" }, { name: "IDAHO", abbreviation: "ID" }, { name: "ILLINOIS", abbreviation: "IL" }, { name: "INDIANA", abbreviation: "IN" }, { name: "IOWA", abbreviation: "IA" }, { name: "KANSAS", abbreviation: "KS" }, { name: "KENTUCKY", abbreviation: "KY" }, { name: "LOUISIANA", abbreviation: "LA" }, { name: "MAINE", abbreviation: "ME" }, { name: "MARSHALL ISLANDS", abbreviation: "MH" }, { name: "MARYLAND", abbreviation: "MD" }, { name: "MASSACHUSETTS", abbreviation: "MA" }, { name: "MICHIGAN", abbreviation: "MI" }, { name: "MINNESOTA", abbreviation: "MN" }, { name: "MISSISSIPPI", abbreviation: "MS" }, { name: "MISSOURI", abbreviation: "MO" }, { name: "MONTANA", abbreviation: "MT" }, { name: "NEBRASKA", abbreviation: "NE" }, { name: "NEVADA", abbreviation: "NV" }, { name: "NEW HAMPSHIRE", abbreviation: "NH" }, { name: "NEW JERSEY", abbreviation: "NJ" }, { name: "NEW MEXICO", abbreviation: "NM" }, { name: "NEW YORK", abbreviation: "NY" }, { name: "NORTH CAROLINA", abbreviation: "NC" }, { name: "NORTH DAKOTA", abbreviation: "ND" }, { name: "NORTHERN MARIANA ISLANDS", abbreviation: "MP" }, { name: "OHIO", abbreviation: "OH" }, { name: "OKLAHOMA", abbreviation: "OK" }, { name: "OREGON", abbreviation: "OR" }, { name: "PALAU", abbreviation: "PW" }, { name: "PENNSYLVANIA", abbreviation: "PA" }, { name: "PUERTO RICO", abbreviation: "PR" }, { name: "RHODE ISLAND", abbreviation: "RI" }, { name: "SOUTH CAROLINA", abbreviation: "SC" }, { name: "SOUTH DAKOTA", abbreviation: "SD" }, { name: "TENNESSEE", abbreviation: "TN" }, { name: "TEXAS", abbreviation: "TX" }, { name: "UTAH", abbreviation: "UT" }, { name: "VERMONT", abbreviation: "VT" }, { name: "VIRGIN ISLANDS", abbreviation: "VI" }, { name: "VIRGINIA", abbreviation: "VA" }, { name: "WASHINGTON", abbreviation: "WA" }, { name: "WEST VIRGINIA", abbreviation: "WV" }, { name: "WISCONSIN", abbreviation: "WI" }, { name: "WYOMING", abbreviation: "WY" }],

        DefaultACHPaymentType:
        [
            { key: '', value: 'Select' },
            { key: '1', value: 'Payment' },
            { key: '3', value: 'Payment + Additional Principal' },
            { key: '5', value: "Preferred Fixed Payment" }
        ],

        ProductItem:
        [
            { key: '', value: 'Select' },
            { key: 'STVI', value: 'Secured Term Loan (Variable) – Interest Only' },
            { key: 'PU', value: 'Primeline (Unsecured)' }
        ],

        DD_Intermediary:
        [
           { key: '', value: 'Select' },
           { key: 'WELLS FARGO ADVISORS, LLC', value: 'xxxxProjectxxx Advisors, LLC' },
            { key: 'WELLS FARGO BANK, N.A.', value: 'xxxxProjectxxx Bank, N.A.' },
           { key: 'ABBOT DOWNING', value: 'Abbot Downing' },
           { key: 'WELLS FARGO SECURITIES, LLC', value: 'xxxxProjectxxx Securities, LLC' },          
           { key: 'FCCS BROKERAGE', value: 'First Clearing Correspondent Services (FCCS) Brokerage' }
        ],
        DD_SecurityType:
        [
            { key: '', value: 'Select' },
            { key: 'TRUST', value: 'Trust' },
            { key: 'AGENCY', value: 'Agency' },
            { key: 'CUSTODY', value: 'Custody' },
            { key: 'BROKERAGE', value: 'Brokerage' },
            { key: 'ABBOT DOWNING', value: 'Abbot Downing' },
             { key: 'INSTITUTIONAL BROKERAGE', value: 'Institutional Brokerage' },
            { key: 'FCCS BROKERAGE', value: 'First Clearing Correspondent Services (FCCS) Brokerage' }
        ],

        DD_ProductPurposeItem:
        [
            { key: '', value: 'Select' },
            { key: 'REFN', value: 'Refinance' },
            { key: 'HIMP', value: 'Home Improvements' },
            { key: "STOC", value: 'Purchase or Carry Margin Stock' },
            { key: 'BOHO', value: 'Boat/Dwelling' },
            { key: 'BRDG', value: 'Bridge' },
            { key: 'BUSE', value: 'Business Expense' },
            { key: 'CARE', value: 'Cash Reserves' },
            { key: 'CONS', value: 'Consolidations' },
            { key: 'CTOP', value: 'Construction To Perm' },
            { key: 'EDUC', value: 'Education' },
            { key: 'FURN', value: 'Home Furnishings' },
            { key: 'LOTL', value: 'Lot' },
            { key: 'PERS', value: 'Personal' },
            { key: 'PRDW', value: 'Purchase Dwelling' },
            { key: 'PMSD', value: 'Purchase Money 2nd' },
            { key: 'BOAT', value: 'Recreational Boat' },
            { key: 'RECV', value: 'Recreational Vehicle' },
            { key: 'RVHO', value: 'Rec Veh/Dwelling' },
            { key: 'OTHR', value: 'Other' }
        ],

        DD_ProductStateLoanWillCloseIn: [
            { key: '', value: 'Select' },
            { key: 'AL', value: 'Alabama (AL)' },
            { key: 'AK', value: 'Alaska (AK)' },
            { key: 'AZ', value: 'Arizona (AZ)' },
            { key: 'AS', value: 'American Samoa (AS)' },
            { key: 'AR', value: 'Arkansas (AR)' },
            { key: "AA", value: "Armed Forces in the Americas except Canada (AA)" },
            { key: "AE", value: "Armed Forces in Europe, Middle East, Africa and Canada (AE)" },
            { key: "AP" , value: "Armed Forces in the Pacific (AP)"},
            { key: 'CA', value: 'California (CA)' },
            { key: 'CO', value: 'Colorado (CO)' },
            { key: 'CT', value: 'Connecticut (CT)' },
            { key: 'DE', value: 'Delaware (DE)' },
            { key: 'DC', value: 'District of Columbia (DC)' },
            { key: 'FL', value: 'Florida (FL)' },
            { key: 'FM', value: 'Federated States of Micronesia (FM)' },
      
            { key: 'GA', value: 'Georgia (GA)' },
            { key: 'HI', value: 'Hawaii (HI)' },
            { key: 'ID', value: 'Idaho (ID)' },
            { key: 'IL', value: 'Illinois (IL)' },
            { key: 'IN', value: 'Indiana (IN)' },
            { key: 'IA', value: 'Iowa (IA)' },
            { key: 'KS', value: 'Kansas (KS)' },
            { key: 'KY', value: 'Kentucky (KY)' },
            { key: 'LA', value: 'Louisiana (LA)' },
            { key: 'ME', value: 'Maine (ME)' },
            { key: 'MD', value: 'Maryland (MD)' },
            { key: 'MA', value: 'Massachusetts (MA)' },
            { key: 'MI', value: 'Michigan (MI)' },
            { key: 'MN', value: 'Minnesota (MN)' },
            { key: 'MT', value: 'Montana (MT)' },
            { key: 'MS', value: 'Mississippi (MS)' },
            { key: 'MO', value: 'Missouri (MO)' },
            { key: 'MH', value: 'Marshall Islands (MH)' },
            
            
            { key: 'NE', value: 'Nebraska (NE)' },
            { key: 'NV', value: 'Nevada (NV)' },
            { key: 'NE', value: 'Nebraska (NE)' },
            { key: 'NH', value: 'New Hampshire (NH)' },
            { key: 'NJ', value: 'New Jersey (NJ)' },
            { key: 'NM', value: 'New Mexico (NM)' },
            { key: 'NY', value: 'New York (NY)' },
            { key: 'NC', value: 'North Carolina (NC)' },
            { key: 'ND', value: 'North Dakota (ND)' },
            { key: 'MP', value: 'Northern Mariana Islands (MP)' },
            
            
            { key: 'OH', value: 'Ohio (OH)' },
            { key: 'OK', value: 'Oklahoma (OK)' },
            { key: 'OR', value: 'Oregon (OR)' },
            { key: 'PA', value: 'Pennsylvania (PA)' },
            { key: 'PW', value: 'Palau (PW)' },
            
            { key: 'PR', value: 'Puerto Rico (PR)' },
            { key: 'RI', value: 'Rhode Island(RI)' },
            { key: 'SC', value: 'South Carolina (SC)' },
            { key: 'SD', value: 'South Dakota (SD)' },
            { key: 'TN', value: 'Tennessee (TN)' },
            { key: 'TX', value: 'Texas (TX)' },
            { key: 'UT', value: 'Utah (UT)' },
            { key: 'VT', value: 'Vermont (VT)' },
            { key: 'VI', value: 'U.S. Virgin Islands (VI)' },
            { key: 'VA', value: 'Virginia (VA)' },
            { key: 'WA', value: 'Washington (WA)' },
            { key: 'WV', value: 'West Virginia (WV)' },
            { key: 'WI', value: 'Wisconsin (WI)' },
            { key: 'WY', value: 'Wyoming (WY)' }
        ],


        DD_ProductPaymentType: [
            { key: '', value: 'Select' },
            { key: '0', value: 'Interest only' },
            { key: '1', value: '1% Repayment' },
            { key: '2', value: '2% Repayment' }
        ],

        DD_ProductPaymentFrequency: [
            { key: '', value: 'Select' },
            { key: 'M', value: 'Monthly' }
        ],

        DD_ProductBalloonOption: [
            { key: '', value: 'Select' },
            { key: '0', value: 'None' },
            { key: '15', value: '15 Yrs.' },
            { key: '30', value: '30 Yrs.' }
        ],

        DD_ProductMarginIndex: [
            { key: '', value: 'Select' },
            { key: 'P', value: 'Prime' },
            { key: 'L', value: 'Libor' }
        ],

        DD_ProductPreferredPaymentDueDate: [
            { key: '', value: 'Select' },
            { key: '1', value: '1' },
            { key: '2', value: '2' },
            { key: '3', value: '3' },
            { key: '4', value: '4' },
            { key: '5', value: '5' },
            { key: '6', value: '6' },
            { key: '7', value: '7' },
            { key: '8', value: '8' },
            { key: '9', value: '9' },
            { key: '10', value: '10' },
            { key: '11', value: '11' },
            { key: '12', value: '12' },
            { key: '13', value: '13' },
            { key: '14', value: '14' },
            { key: '15', value: '15' },
            { key: '16', value: '16' },
            { key: '17', value: '17' },
            { key: '18', value: '18' },
            { key: '19', value: '19' },
            { key: '20', value: '20' },
            { key: '21', value: '21' },
            { key: '22', value: '22' },
            { key: '23', value: '23' },
            { key: '24', value: '24' },
            { key: '25', value: '25' },
            { key: '26', value: '26' },
            { key: '27', value: '27' },
            { key: '28', value: '28' }
        ],
        Languages:
        [
            { key: '', value: 'Select' },
            { key: 'ENG', value: 'English Only' },
            { key: 'SPN', value: 'Spanish and English' },
            { key: 'CHI', value: 'Chinese and English' },
            { key: 'KOR', value: 'Korean and English' },
            { key: 'TAG', value: 'Tagalog and English' },
            { key: 'VIE', value: 'Vietnamese and English' },
            { key: 'SPN', value: 'Spanish Only' },
            { key: 'CHI', value: 'Chinese Only' },
            { key: 'KOR', value: 'Korean Only' },
            { key: 'TAG', value: 'Tagalog Only' },
            { key: 'VIE', value: 'Vietnamese Only' },
            { key: 'OTH', value: 'Other' }
        ],
        Race:
        [
            { key: '1', value: 'American Indian/Alaskan Native' },
            { key: '2', value: 'Asian' },
            { key: '3', value: 'Black/African American' },
            { key: '4', value: 'Native Hawaiian or Other Pacific Islander' },
            { key: '5', value: 'White' },
            { key: '6', value: 'Does not wish to provide' }
        ],
        ddCheckDeliveryAddress: [
            { key: '', value: 'Select' },
            { key: 'P', value: 'Checks for Primary and Secondary Borrower-Primary address' },
            { key: 'A', value: 'Checks for Primary and Secondary Borrower-Alternate address' },
            { key: 'C', value: 'Checks for Primary and Secondary Borrower-Specified address' }
        ],
        ddOccupancyType: [
            { key: '', value: 'Select' },
            { key: 'P', value: 'Primary' },
            { key: 'S', value: 'Secondary' },
            { key: 'V', value: 'Vacation' },
            { key: 'R', value: 'Rental' }
        ],
        ddPropertyType: [
            { key: '', value: 'Select' },
            { key: 'SF', value: 'Single Family' },
            { key: 'TH', value: 'Town House' },
            { key: 'C', value: 'Condo' },
            { key: 'M2', value: 'Multi Family – 2 Unit' },
            { key: 'M3', value: 'Multi Family – 3 Unit' },
            { key: 'M4', value: 'Multi Family – 4 Unit' }
        ],

        ddCDSavingCollateralType: [
            { key: '', value: 'Select' },
			//{ key: 'xxxxProjectxxx Market Rate Savings', value: 'xxxxProjectxxx Market Rate Savings' },
			//{ key: 'xxxxProjectxxx Performance Savings', value: 'xxxxProjectxxx Performance Savings' },
			//{ key: 'xxxxProjectxxx Plus Savings', value: 'xxxxProjectxxx Plus Savings' },
			//{ key: 'xxxxProjectxxx Almost CD', value: 'xxxxProjectxxx Almost CD' },
			{ key: 'SAVINGS', value: 'Savings' },
			{ key: 'CERTIFICATE OF DEPOSIT', value: 'Certificate Of Deposit' }

        ],
        ddPaymentFrequency: [
           { key: '', value: 'Select' },
           { key: 'M', value: 'Monthly' },
           { key: 'S', value: 'Semi-annual' },
           { key: 'A', value: 'Annual' }
        ],

        ddPreferredTimeToCall: [
           { key: '', value: 'Select' },
           { key: 'M', value: 'Morning' },
           { key: 'A', value: 'Afternoon' },
           { key: 'E', value: 'Evening' }
        ],

        ddGender: [
          { key: '', value: 'Select' },
          { key: '1', value: 'Male' },
          { key: '2', value: 'Female' }
        ],

        ddEthnicity: [
          { key: '', value: 'Select' },
          { key: '1', value: 'Hispanic or Latino' },
          { key: '2', value: 'Not Hispanic or Latino' }
        ],

        ddHmdaPropertyType: [
          { key: '', value: 'Select' },
          { key: '1', value: '1-4 Family' },
          { key: '2', value: 'Manufactured Home' },
          { key: '3', value: 'Multifamily (5 or more units)' }
        ],

        ddCollateralPropertyOccupancy: [
            { key: '', value: 'Select' },
            { key: '1', value: 'Owner Occupied (Primary Dwelling)' },
            { key: '2', value: 'Non-Owner Occupied' }
        ],

        ddUnderWritingType: [
            { key: '', value: 'Select' },
            { key: 'FUW', value: 'Premier-Full' },
            { key: 'LUW', value: 'Express-Limited' }
        ],

        ddBusinessType: [
           { key: '', value: 'Select' },
           { key: 'GENERAL PARTNERSHIP', value: 'General Partnership' },
           { key: 'LIMITED PARTNERSHIP', value: 'Limited Partnership' },
           { key: 'LIMITED LIABILITY PARTNERSHIP', value: 'Limited Liability Partnership' },
           //{ key: 'Limited Liability Company', value: 'Limited Liability Company' },
           { key: 'CORP', value: 'Corporation' },
           //{ key: 'Family Limited Partnership', value: 'Family Limited Partnership' }
            { key: 'ASSOCIATION', value: 'Association' },
            { key: 'MANAGER-MANAGED LLC', value: 'Manager-Managed LLC' },
            { key: 'MEMBER-MANAGED LLC', value: 'Member-Managed LLC' }
        ],

        ddMembersOfficers: [
          { key: '', value: 'Select' },
          { key: 'One', value: 'One' },
          { key: 'Two', value: 'Two' },
          { key: 'Three', value: 'Three' },
          { key: 'Four', value: 'Four' },
          { key: 'Five', value: 'Five' },
          { key: 'Six', value: 'Six' }
        ],

        ddSchedule3Types: [
         { key: '', value: 'Select' },
         { key: 'R', value: 'Rental' },
         { key: 'S', value: 'Sold' },
         { key: 'PS', value: 'Pending Sale' },
         { key: 'PR', value: 'Residence' }
        ],

        ddMemberOfficerRole: [
            { key: '', value: 'Select' },
            { key: 'AUTHORIZER', value: 'Authorizer' },
            { key: 'PARTNER/MEMBER', value: 'Partner/Member (for partnerships and LLCs)' },
            { key: 'OFFICER1', value: 'Officer 1 (corporation signer)' },
            { key: 'OFFICER2', value: 'Officer 2 (corporation signer #2)' }
           // { key: 'Both', value: 'Both' }
        ],

        ddTitleOrPosition: [
            { key: '', value: 'Select' },
            { key: 'CHIEF EXECUTIVE OFFICER', value: 'Chief Executive Officer' },
            { key: 'CHIEF OPERATION OFFICER', value: 'Chief Operation Officer' },
            { key: 'CHIEF FINANCIAL OFFICER', value: 'Chief Financial Officer' },
            { key: 'CHAIRMAN OF THE BOARD', value: 'Chairman of the Board' },
            { key: 'SECRETARY', value: 'Secretary' },
            { key: 'TREASURER', value: 'Treasurer' },
            { key: 'PRESIDENT', value: 'President' },
            { key: 'VICE PRESIDENT', value: 'Vice President' },
            { key: 'ASSISTANT VICE PRESIDENT', value: 'Assistant Vice President' },
            { key: 'DIRECTOR', value: 'Director' },
            { key: 'MEMBER', value: 'Member' },
            { key: 'MANAGER', value: 'Manager' },
            { key: 'GENERAL PARTNER', value: 'General Partner' },
            { key: 'LIMITED PARTNER', value: 'Limited Partner' }
        ],

        ddMaritalStatus: [
            { key: '', value: 'Select' },
            { key: 'M', value: 'Married' },
            { key: 'U', value: 'Unmarried' },
            { key: 'S', value: 'Separated' }
        ],

        ddRevocable: [
           { key: '', value: 'Select' },
           { key: 'I', value: 'Irrevocable' },
           { key: 'R', value: 'Revocable' }
        ],
        ddMethodFace: [
           { key: '', value: 'Select' },
           { key: 'F', value: 'Face to Face' },
           { key: 'P', value: 'Phone' },
            
           { key: 'M', value: 'Mail' }
          

        ]
    }
