package com.shifa.kent;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.os.Bundle;

import com.android.vending.billing.util.IabResult;
import com.android.vending.billing.util.Purchase;
import com.shifa.kent.inappbilling.Product;
import com.shifa.kent.inappbilling.PurchaseActivity;

import org.apache.http.NameValuePair;
import org.apache.http.message.BasicNameValuePair;

import java.util.ArrayList;
import java.util.List;

/**
 * This activity will purchase a Passport from Google Play.
 * <p/>
 * If you wanted to change to purchase something else all you have to change is the SKU (item id) that is used
 * you could even pass this in as an Intent EXTRA to avoid duplication for multiple items to purchase
 * <p/>
 * N.B that we extend PurchaseActivity if you don't understand something look up to this class
 *
 * @author Blundell
 */
public class PurchaseItemActivity extends PurchaseActivity {

    Context ctx;
    Super_Library_AppClass SLAc;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        // Set the result as cancelled in case anything fails before we purchase the item
        //  popBurntToast("Puchase item activity oncreate");

        SLAc = new Super_Library_AppClass(this);
        ctx = this;
        setResult(RESULT_CANCELED);

        // Then wait for the callback if we have successfully setup in app billing or not (because we extend PurchaseActivity)
    }

    @Override
    protected void dealWithIabSetupFailure() {
        popBurntToast("Sorry buying a passport is not available at this current time");
        finish();
    }

    @Override
    protected void dealWithIabSetupSuccess() {
        // popBurntToast("Puchase item activity dealWithIabSetupSuccess");
        purchaseItem(Product.SKU);
    }

    @Override
    protected void dealWithPurchaseSuccess(IabResult result, Purchase info) {
        super.dealWithPurchaseSuccess(result, info);
        MarkUserAsPaid();
        popBurntToast("Purchase Item Successfully! Order Id: " + info.getOrderId());
        try {

            int responseCode = result.getResponse();
            String purchaseData = info.toString();
            String dataSignature = info.getSignature();
            String WebService_GetPaidStatus = "http://kent..XXXXXXX/app_php/Shifa4o/WebService/WSPayment.php?action=Result_InAPP";//8419562625169491
            List<NameValuePair> nameValuePairs = new ArrayList<NameValuePair>(4);
            nameValuePairs.add(new BasicNameValuePair(".XXXXXXX", SLAc.UserSessionId()));
            nameValuePairs.add(new BasicNameValuePair("inapp_result_code", String.valueOf(responseCode)));
            nameValuePairs.add(new BasicNameValuePair("inapp_result_purchase_Data", purchaseData));
            nameValuePairs.add(new BasicNameValuePair("inapp_result_data_signature", dataSignature));

            Super_Library_URLV2 SLU2 = new Super_Library_URLV2(WebService_GetPaidStatus, nameValuePairs, ((Activity) ctx), "");
        } catch (Exception ex) {
            String WebService_GetPaidStatus = "http://kent..XXXXXXX/app_php/Shifa4o/WebService/WSPayment.php?action=ERROR_InAPP";//8419562625169491
            List<NameValuePair> nameValuePairs = new ArrayList<NameValuePair>(2);
            nameValuePairs.add(new BasicNameValuePair(".XXXXXXX", SLAc.UserSessionId()));
            nameValuePairs.add(new BasicNameValuePair("purchase_item_error", ex.toString()));

            Super_Library_URLV2 SLU2 = new Super_Library_URLV2(WebService_GetPaidStatus, nameValuePairs, ((Activity) ctx), "");
        }

        setResult(RESULT_OK);
        openHomeMenu();
    }

    @Override
    protected void dealWithPurchaseFailed(IabResult result) {
        super.dealWithPurchaseFailed(result);
        try {

            String WebService_GetPaidStatus = "http://kent..XXXXXXX/app_php/Shifa4o/WebService/WSPayment.php?action=ERROR_InAPP";//8419562625169491
            List<NameValuePair> nameValuePairs = new ArrayList<NameValuePair>(2);
            nameValuePairs.add(new BasicNameValuePair(".XXXXXXX", SLAc.UserSessionId()));
            nameValuePairs.add(new BasicNameValuePair("purchase_item_error", result.toString()));

            Super_Library_URLV2 SLU2 = new Super_Library_URLV2(WebService_GetPaidStatus, nameValuePairs, ((Activity) ctx), "");
        } catch (Exception ex) {

        }
        //popBurntToast("Puchase item activity dealWithIabSetupfaildd");

        setResult(RESULT_CANCELED);
        openHomeMenu();
    }

    private void openHomeMenu() {
        Intent intent = new Intent(this, home_menu.class);
        intent.putExtra("SessionID", SLAc.UserSessionId());
        startActivity(intent);
        finish();

    }

    public void MarkUserAsPaid() {
        SLAc.SavePreference("PaidUser", "true");
        SLAc.SavePreference("PaidUserServer", "false");
        ShifaDepartment ShifaDepart = new ShifaDepartment(ctx);
        ShifaDepart.SetUserAsPaidAccount();


    }
}
