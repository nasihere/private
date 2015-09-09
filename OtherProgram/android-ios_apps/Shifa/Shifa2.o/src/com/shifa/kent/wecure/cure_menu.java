package com.shifa.kent.wecure;


import android.annotation.SuppressLint;
import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ListView;
import android.widget.TextView;

import com.shifa.kent.R;
import com.shifa.kent.Super_Library_AppClass;
import com.shifa.kent.chatsdk.chatter;
import com.shifa.kent.home_adapter;
import com.shifa.kent.home_classAdapter;
import com.shifa.kent.home_modal;
import com.shifa.kent.inappbilling.BlundellActivity;

import java.util.ArrayList;

public class cure_menu extends BlundellActivity   {
    Context ctx;
    ListView listView;



    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        ctx = this;
        setContentView(R.layout.home_menu);
        listView = (ListView) findViewById(R.id.lvhomemenu);


        refreshmenu();

        Super_Library_AppClass SLAc = new Super_Library_AppClass(ctx);


    }






    public void refreshmenu() {
        //	Log.e("Home screen progress ", "1");
        ArrayList<home_modal> Items = home_adapter.LoadCureModel(ctx);
        // Log.e("Home screen progress ", "2");

        String[] ids = new String[Items.size()];
        for (int i = 0; i < ids.length; i++) {

            ids[i] = Integer.toString(i + 1);
        }

        //    Log.e("Home screen progress ", "3");
        home_classAdapter adapter = new home_classAdapter(this, R.layout.listview_homemenu, ids, Items);
        //     Log.e("Home screen progress ", "3.1");
        //Parcelable state = listView.onSaveInstanceState();
        listView.setAdapter(adapter);
        listView.setSelection(0);

        listView.setOnItemClickListener(new AdapterView.OnItemClickListener() {

            @SuppressLint("DefaultLocale")
            @Override
            public void onItemClick(AdapterView<?> listView, View view, int position,
                                    long id) {
                String Selection = ((TextView) view.findViewById(R.id.tvListItemHome)).getText().toString();
                String Status = ((TextView) view.findViewById(R.id.tvViewStatusHome)).getText().toString();

               // Log.e("Selection", Selection + " = " + Status);
                if (Status.indexOf("Expiry") != -1){
                    openCureWindow(Selection);
                }
                else  {
                   navigate().toPurchaseWeCureActivityForResult(Selection);
                }
                finish();

            }
        });
        //  Log.e("Home screen progress ", "4");
    }
    public void openCureWindow(String SelectionText) {
        Super_Library_AppClass SLAc = new Super_Library_AppClass(ctx);
        Intent intent = new Intent(ctx, chatter.class);
        intent.putExtra("Mode", "msg");
        intent.putExtra(".XXXXXXX_to_name", SLAc.GetPreferenceValue("PaidServiceDrName"+SelectionText));
        intent.putExtra(".XXXXXXX_to", SLAc.GetPreferenceValue("PaidServiceDrId"+SelectionText));
        intent.putExtra("basechat", SLAc.GetPreferenceValue("PaidServiceDrName"+SelectionText) + " - " + SLAc.UserName());
        startActivity(intent);

    }

}
