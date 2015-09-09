package com.shifa.kent.inappbilling;

import android.app.Activity;
import android.content.Intent;

import com.shifa.kent.PurchaseItemActivity;
import com.shifa.kent.login;
import com.shifa.kent.wecure.PurchaseWeCureActivity;

import java.util.Random;

/**
 * A wrapper around the Android Intent mechanism
 *
 * @author Blundell
 */
public class Navigator {

    public static final int REQUEST_PASSPORT_PURCHASE = 2012;

    private final Activity activity;
    private String[] RandomName = {"Dr Nazim Dadan","Dr Afiya Shaikh","Dr Yogesh Jaiswal","Dr Prassana Joshi","Dr Sabah Shaikh","Dr Aparna Joshi","Dr Meeta Sharma","Dr Neeharika","Dr Premchand","Dr Sudarshan","Dr Muzammil","Dr Vipul Shah","Dr Vikram","Dr Reddy"};
    public Navigator(Activity activity) {
        this.activity = activity;
    }

    public void toMainActivity() {
        Intent intent = new Intent(activity, login.class);
        activity.startActivity(intent);
    }

    public void toPurchasePassportActivityForResult() {
        Intent intent = new Intent(activity, PurchaseItemActivity.class);
        activity.startActivityForResult(intent, REQUEST_PASSPORT_PURCHASE);
    }
    public void toPurchaseWeCureActivityForResult(String itemcode) {
        String RandomNameStr = RandomName[new Random().nextInt(RandomName.length)];
        Intent intent = new Intent(activity, PurchaseWeCureActivity.class);
        intent.putExtra("itemcode",itemcode);
        intent.putExtra("drname",RandomNameStr);
        activity.startActivityForResult(intent, REQUEST_PASSPORT_PURCHASE);// uncomenet it when debug is over
    }
}
