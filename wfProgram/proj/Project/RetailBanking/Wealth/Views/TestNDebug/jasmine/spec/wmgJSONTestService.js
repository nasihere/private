describe("Wealth Management Indicator ", function() {
  var wmgTest;
    var json;
//  var wmgJson;

  beforeEach(function() {
      wmgTest = new WmgJson();
      json = Root;
    
      //  wmgJson = new wmgJson();
  });

  
    
  it("should be applicationInfo.isSavedApp indicator flag is present in JSON ", function () {
      expect(json.applicationInfo.isSavedApp).toBeDefined(true);
      // expect(wmgTest.isCoAppAvailable(json)).toThrow();

  });
    
  it("should be  applicationInfo.savedScreen indicator flag is present in JSON ", function () {
      expect(json.applicationInfo.savedScreen).toBeDefined(true);
      // expect(wmgTest.isCoAppAvailable(json)).toThrow();

  });
    
  it("should be  applicationInfo.isCoApplicable indicator flag is present in JSON ", function () {
      expect(wmgTest.tobeYesNo(json.applicationInfo.isCoAppAvailable)).toBe(true);
      // expect(wmgTest.isCoAppAvailable(json)).toThrow();

  });
  it("should be  applicationInfo.isTBPInd indicator flag is present in JSON", function () {
      expect(wmgTest.tobeYesNo(json.applicationInfo.isTBPInd)).toBe(true);
      // expect(wmgTest.isCoAppAvailable(json)).toThrow();

  });
  it("should be applicationInfo.isNonSpouseAvailable flag is present in JSON", function () {
     // expect(wmgTest.tobeYesNo(json.applicationInfo.isNonSpouseAvailable)).toBe(true);
      // expect(wmgTest.isCoAppAvailable(json)).toThrow();

  });
  it("should be applicationInfo.isWIDisclosureApplicable flag is present in JSON ", function () {
      expect(wmgTest.tobeYesNo(json.applicationInfo.isWIDisclosureApplicable)).toBe(true); 
      // expect(wmgTest.isCoAppAvailable(json)).toThrow();

  });
 

  
  it("should be applicationInfo.jointApplicationApprovalInd flag is present in JSON", function () {
      expect(wmgTest.tobeYesNo(json.applicationInfo.jointApplicationApprovalInd)).toBe(true);
      
  });

  it("should be applicationInfo.isAMLKYCApplicable flag is present in JSON", function () {
      expect(wmgTest.tobeYesNo(json.applicationInfo.isAMLKYCApplicable)).toBe(true);
      // expect(wmgTest.isCoAppAvailable(json)).toThrow();

  });

  it("should be applicationInfo BankerInfo membersOfficersList an array is present in JSON", function () {
      expect(wmgTest.tobeYesNo(json.applicationInfo.bankerInfo.membersOfficersList)).toBeDefined();
      // expect(wmgTest.isCoAppAvailable(json)).toThrow();

  });

});





describe("Compliance Screen", function () {
    var wmgTest;
    var json;
    //  var wmgJson;

    beforeEach(function () {
        wmgTest = new WmgJson();
        json = Root;

        //  wmgJson = new wmgJson();
    });



    it("should be Wisconsin Discloser content has some value if  isWIDisclosureApplicable flag value is Y", function () {
        expect(wmgTest.wisconsinDiscloserContent(json.applicationInfo.isWIDisclosureApplicable, json.applicationInfo.WIDisclosureContent)).toBe(true);

    });


    it("should be applicant information is present in JSON", function () {
        
        expect(wmgTest.checkApplicantInfomations(json.applicationInfo.applicantInfoList.applicantInfo)).toBe(true);
       
    });
   
    
});






describe("Product Screen", function () {
    var wmgTest;
    var json;
    //  var wmgJson;

    beforeEach(function () {
        wmgTest = new WmgJson();
        json = Root;

        //  wmgJson = new wmgJson();
    });



    it("should be product information is present in JSON", function () {
        expect(json.applicationInfo.WealthData.productList.product).toBeDefined(true);

        expect(wmgTest.checkProducts(json.applicationInfo.WealthData.productList.product)).toBe(true);

    });

});
