package com.shifa.kent.chatsdk;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.database.Cursor;
import android.os.Bundle;
import android.os.Handler;
import android.util.Log;
import android.widget.ArrayAdapter;
import android.widget.ListView;

import com.shifa.kent.R;

public class chat_menu extends Activity implements DownloadResultReceiver.Receiver {

    final String CONST_PRIVATE_CHAT_OVERVIEW_LIST = "PrivateChatterNames";
    final String CONST_PRIVATE_MESGGING_CHAT = "PrivateMessageChatting";
    final String CONST_PUBLIC_CHAT = "PublicChat";
    final String CONST_DISCUSSION_CHAT = "DiscussionMessageChatting";
    final String CONST_DISCUSSION_CHAT_OVERVIEW_LIST = "DiscussionChat";
    final String CONST_DISCUSSION_PERSONAL_POSTED_OVERVIEW_LIST = "DiscussionPostedByUser";
    final String CONST_CONTACT_CHAT = "ShifaMembers";
    final String CONST_EXPERT_CHAT = "ShifaExpertMembers";
    final String TAG1 = "Chat_menu";
    final String url = "http://kent..XXXXXXX/app_php/Shifa4o/WebService/WSChat.php";
    Super_Library_AppClass SLAc;
    boolean LoadUI = false;
    Context ctx;
    ShifaDepartment ShifaDepart;
    private ListView listView = null;
    private ArrayAdapter arrayAdapter = null;
    private DownloadResultReceiver mReceiver;

    @Override
    public void onReceiveResult(int resultCode, Bundle resultData) {
        switch (resultCode) {
            case DownloadService.STATUS_RUNNING:

                setProgressBarIndeterminateVisibility(true);
                break;
            case DownloadService.STATUS_FINISHED:
                       /* Hide progress & extract result from bundle */
                setProgressBarIndeterminateVisibility(false);
                String results = resultData.getString("result");
                String resultscode = resultData.getString("resultcode");
                //    Log.e(TAG1,"resultCode: "+resultscode);


                /* Update ListView with result */
                //    arrayAdapter = new ArrayAdapter(chat_menu.this, android.R.layout.simple_list_item_2, results);
                //   listView.setAdapter(arrayAdapter);
                Log.e(TAG1, "OnReceiveResult:STATUS_FINISHED: " + results + ", resultscode: " + resultscode);
                DownloadChatIntoDatabase(results, resultscode);
                break;
            case DownloadService.STATUS_ERROR:
               /* Handle the error */
                String error = resultData.getString(Intent.EXTRA_TEXT);
                String resultscode1 = resultData.getString("resultcode");
                Log.e(TAG1, "OnReceiveResult:STATUS_ERROR: " + error);
                DownloadChatIntoDatabase(null, resultscode1);
                //  Toast.makeText(this, error, Toast.LENGTH_LONG).show();
                break;
        }
    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        setContentView(R.layout.chat_menu);
        SLAc = new Super_Library_AppClass(this);
        ShifaDepart = new ShifaDepartment(this);
        ctx = this;
         /* Allow activity to show indeterminate progressbar */
        //requestWindowFeature(Window.FEATURE_INDETERMINATE_PROGRESS);
        if (!SLAc.GetPreferenceValue("FirstTime").equals("true")) {
            SLAc.SavePreference("FirstTime", "true");


            // DBHelper db3 = new DBHelper(ctx);  db3.initializeDataBase(); db3.close(); SLAc.SavePreference(".XXXXXXX","426779554151673");SLAc.SavePreference("session_name","Niaz");

            DBHelper db2 = new DBHelper(ctx);
            db2.initializeDataBase();
            db2.close(); //SLAc.SavePreference(".XXXXXXX","818904868132997");SLAc.SavePreference("session_name","Nazim Dadan");
            //       DBHelper db1 = new DBHelper(ctx);  db1.initializeDataBase(); db1.close(); SLAc.SavePreference(".XXXXXXX","10205304767877899");SLAc.SavePreference("session_name","Nasz Sayed");
        }
           /* Starting Download Service */
        mReceiver = new DownloadResultReceiver(new Handler());
        mReceiver.setReceiver(this);


        ThreadChatLookUp(ShifaDepart.CheckNewOrExistingPvtChatAvaiable(SLAc.UserSessionId(), FinderLastConfig("msg")), CONST_PRIVATE_CHAT_OVERVIEW_LIST);
        // ThreadChatLookUp(ShifaDepart.CheckContacts("", "0"), CONST_EXPERT_CHAT);


        Handler handler = new Handler();

        handler.postDelayed(new Runnable() {

            @Override
            public void run()

            {
                if (LoadUI == true) return;
                LoadUI = true;
//Display Data here
                Intent intent1 = new Intent(chat_menu.this, chat.class);
                //    intent1.putExtra("ScreenMode", "DiscussionChat");
                startActivity(intent1);

                //   Intent intent = new Intent(chat_menu.this, chatter.class);
                //   intent.putExtra("Mode", "disc");
                //   intent.putExtra("discid", "4642");
                finish();

            }
        }, 10000);

    }

    private String FinderLastConfig(String find) {
        if (find.equals("contacts")) {
            return DBTblLookupLastItem("select datetime from .XXXXXXXapp_contact  order by datetime desc limit 0,1");
        } else {
            return DBTblLookupLastItem("select id_web from .XXXXXXXapp_chat where chat_type = '" + find + "' order by id_web desc limit 0,1");

        }
    }

    private String DBTblLookupLastItem(String q) {
        //Log.e(TAG,"Query: " + q);
        String id_web = "";
        DBHelper db1 = new DBHelper(ctx);
        Cursor cursor = db1.getReadableDatabase().rawQuery(q, null);
        if (cursor.getCount() != 0) {

            cursor.moveToFirst();
            while (!cursor.isAfterLast()) {

                id_web = cursor.getString(0);
                cursor.moveToNext();
            }
        }
        if (cursor != null) {
            cursor.close();

        }
        if (db1 != null) {
            db1.close();
        }
        Log.e(TAG1, id_web + " = " + q);
        return id_web;
    }

    private void ThreadChatLookUp(String URL, String requestid) {
        if (URL == null || URL.equals("")) {
            Log.e(TAG1, "REQUESId is null url " + requestid);
            return;
        }
        ;
        Intent intent = new Intent(Intent.ACTION_SYNC, null, ctx, DownloadService.class);

    /* Send optional extras to Download IntentService */
        Log.e(TAG1, URL);
        intent.putExtra("url", URL);
        intent.putExtra("receiver", mReceiver);
        intent.putExtra("requestId", requestid);

        startService(intent);
    }

    private void ShowPage(String ScreenName) {


    }

    private void DownloadChatIntoDatabase(String results, String resultCode) {
        if (results != null) {
            if (resultCode.equals(CONST_CONTACT_CHAT)) {
                SLAc.SaveWSChatPHPGetUserPvtPublicChat(results, ".XXXXXXXapp_contact", resultCode);
            } else {
                SLAc.SaveWSChatPHPGetUserPvtPublicChat(results, ".XXXXXXXapp_chat", resultCode);

            }
        }
        if (resultCode.equals(CONST_PRIVATE_CHAT_OVERVIEW_LIST)) {

            ThreadChatLookUp(ShifaDepart.CheckExistingPublicChatAvailable("", FinderLastConfig("chat")), CONST_PUBLIC_CHAT);

        } else if (resultCode.equals(CONST_PUBLIC_CHAT)) {
            ThreadChatLookUp(ShifaDepart.CheckNewOrExistingDiscussionOthers_V2("", FinderLastConfig("disc")), CONST_DISCUSSION_CHAT_OVERVIEW_LIST);
        } else if (resultCode.equals(CONST_DISCUSSION_CHAT_OVERVIEW_LIST)) {
            ThreadChatLookUp(ShifaDepart.CheckContacts(FinderLastConfig("contacts")), CONST_CONTACT_CHAT);
        } else if (resultCode.equals(CONST_CONTACT_CHAT)) {
            if (LoadUI == true) return;
            LoadUI = true;
            Intent intent1 = new Intent(chat_menu.this, chat.class);
            //    intent1.putExtra("ScreenMode", "DiscussionChat");
            startActivity(intent1);

            //   Intent intent = new Intent(chat_menu.this, chatter.class);
            //   intent.putExtra("Mode", "disc");
            //   intent.putExtra("discid", "4642");
            finish();
            // startActivity(intent);
        }
    }
}

