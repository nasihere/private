package com.shifa.kent.wecure;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.util.Log;

import com.android.vending.billing.util.IabResult;
import com.android.vending.billing.util.Purchase;
import com.shifa.kent.Super_Library_AppClass;
import com.shifa.kent.Super_Library_URLV2;
import com.shifa.kent.chatsdk.chatter;
import com.shifa.kent.inappbilling.Product;
import com.shifa.kent.inappbilling.PurchaseActivity;

import org.apache.http.NameValuePair;
import org.apache.http.message.BasicNameValuePair;

import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
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
public class PurchaseWeCureActivity extends PurchaseActivity {

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
    private String readServiceItemCode() {
        String sItemCode = "";
        try {
            Bundle extras = getIntent().getExtras();
            if (extras != null) {
                sItemCode = extras.getString("itemcode");

            }
        } catch (Exception ex) {
        }
        return sItemCode;
    }
    private String readServicedrname() {
        String sItemCode = "";
        try {
            Bundle extras = getIntent().getExtras();
            if (extras != null) {
                sItemCode = extras.getString("drname");

            }
        } catch (Exception ex) {
        }
        return sItemCode;
    }
    @Override
    protected void dealWithIabSetupFailure() {
        popBurntToast("Sorry buying a passport is not available at this current time");
        finish();
    }

    @Override
    protected void dealWithIabSetupSuccess() {
        // popBurntToast("Puchase item activity dealWithIabSetupSuccess");
        String toItemCode = NormalMenuToCode();
        purchaseItem(toItemCode);
    }
    private String NormalMenuToCode(){
        Product prod = new Product();
        String NormalString = readServiceItemCode();
        if (NormalString.equals("Dietitians and Nutritionists")){
            return prod.SKU_dietnutri;
        }
        else  if (NormalString.equals("Sexual Health")){
            return prod.SKU_sexhealth;
        }
        else  if (NormalString.equals("Homeopathy")){
            return prod.SKU_homeopathy;
        }
        else  if (NormalString.equals("Paediatric Homeopathy")){
            return prod.SKU_paedhomeo;
        }
        return prod.SKU;
    }
    @Override
    protected void dealWithPurchaseSuccess(IabResult result, Purchase info) {
        super.dealWithPurchaseSuccess(result, info);
        openCureWindow();
        popBurntToast("Purchase Item Successfully! Order Id: " + info.getOrderId());
        try {

            int responseCode = result.getResponse();
            String purchaseData = info.toString();
            String dataSignature = info.getSignature();
            String WebService_GetPaidStatus = "http://kent..XXXXXXX/app_php/Shifa4o/WebService/WSPayment.php?action=SetWeCureAccount";//8419562625169491
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
        //openCureWindow();
    }

    @Override
    protected void dealWithPurchaseFailed(IabResult result) {
        super.dealWithPurchaseFailed(result);
        try {

            String WebService_GetPaidStatus = "http://kent..XXXXXXX/app_php/Shifa4o/WebService/WSPayment.php?action=SetWeCureAccount";//8419562625169491
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
        Intent intent = new Intent(this, cure_menu.class);
        intent.putExtra("SessionID", SLAc.UserSessionId());
        startActivity(intent);
        finish();

    }

    private String CurrentDate(){
        Date d = new Date();
        SimpleDateFormat format = new SimpleDateFormat("yyyy-MM-dd");
        Date newDate = new Date(d.getTime() - 604800000L); // 7 * 24 * 60 * 60 * 1000
        Log.e("newDate",format.format(newDate));
        return format.format(newDate);

    }
    public void openCureWindow() {

        SLAc.SavePreference("PaidItemCode", readServiceItemCode());
        SLAc.SavePreference("PaidServiceExpiry"+readServiceItemCode(), CurrentDate());
        SLAc.SavePreference("PaidServiceDrName"+readServiceItemCode(), readServicedrname());
        SLAc.SavePreference("PaidServiceDrId"+readServiceItemCode(), "818904868132997");
        Intent intent = new Intent(ctx, chatter.class);
        intent.putExtra("Mode", "msg");
        intent.putExtra(".XXXXXXX_to_name", readServicedrname());
        intent.putExtra(".XXXXXXX_to", "818904868132997");
        intent.putExtra("basechat", readServicedrname() + " - " + SLAc.UserName());
        startActivity(intent);
        finish();
    }
}
