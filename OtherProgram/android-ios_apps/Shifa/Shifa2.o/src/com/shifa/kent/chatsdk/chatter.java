package com.shifa.kent.chatsdk;


import android.annotation.SuppressLint;
import android.app.ActionBar;
import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.database.Cursor;
import android.graphics.Color;
import android.graphics.drawable.ColorDrawable;
import android.os.Bundle;
import android.os.Handler;
import android.util.Log;
import android.view.KeyEvent;
import android.view.View;
import android.view.WindowManager;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.LinearLayout;
import android.widget.ListView;
import android.widget.RelativeLayout;
import android.widget.SimpleCursorAdapter;

import com.shifa.kent.R;

import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.UUID;

public class chatter extends Activity implements DownloadResultReceiver.Receiver {
    final String CONST_PRIVATE_CHAT_OVERVIEW_LIST = "PrivateChatterNames";
    final String CONST_PRIVATE_MESGGING_CHAT = "PrivateMessageChatting";
    final String CONST_PUBLIC_CHAT = "PublicChat";
    final String CONST_DISCUSSION_CHAT = "DiscussionMessageChatting";
    final String CONST_TABLE_APP_CHAT_MENU = "DiscussionChat";
    final String CONST_DISCUSSION_PERSONAL_POSTED_OVERVIEW_LIST = "DiscussionPostedByUser";
    final String CONST_CONTACT_CHAT = "ShifaMembers";
    final String CONST_TABLE_APP_CONTACT_MENU = "ShifaExpertMembers";
    final String TAG1 = "ChatterService";
    final String url = "http://kent..XXXXXXX/app_php/Shifa4o/WebService/WSChat.php";
    public String ChatTextSend = "";
    public String SessionID = "";
    public String SessionName = "";
    SimpleCursorAdapter CursorAdapter;
    Handler mHandler;
    ListView myList;
    boolean ScrollTouchActivated = false;
    String PublicChatIdCode = "-999";
    boolean chatactive = true;
    int Interval = 60000;
    EditText txtChatMsg;
    int ScrollStatus = 0; //ScrollStatus if -1 means top , 0 means in the middle, 1 means at bottom of the scroll
    boolean FirstTimeView = true;
    String TAG2 = "DB";
    String TAG = "ChatterUI";
    String ScreenMode;
    String BaseChat;
    Context ctx;
    Super_Library_AppClass SLAc;
    boolean ChatSendEnterKeyHit = false;
    boolean ListRefresh = false;
    ShifaDepartment ShifaDepart;
    String DiscId = "";
    String .XXXXXXX_to = "";
    String .XXXXXXX_to_name = "";
    ChatAdapter ChatAdapter;
    ArrayList<ChatMain> userArrContact = new ArrayList<ChatMain>();
    ArrayList<ChatMain> userArray = new ArrayList<ChatMain>();
    String LastQuery = "";
    private String preFirstId_web;
    private ArrayAdapter arrayAdapter = null;
    private DownloadResultReceiver mReceiver;
    private String RetryTabOpenChats = "";
    private String IncomingTabOpenChats = "";

    @Override
    public void onReceiveResult(int resultCode, Bundle resultData) {
        switch (resultCode) {
            case DownloadService.STATUS_RUNNING:
                Log.e(TAG1, "running...");
                setProgressBarIndeterminateVisibility(true);
                break;
            case DownloadService.STATUS_FINISHED:
                /* Hide progress & extract result from bundle */
                setProgressBarIndeterminateVisibility(false);
                String results = resultData.getString("result");
                String resultscode = resultData.getString("resultcode");
                Log.e(TAG1, "resultCode: " + resultscode);
                DownloadChatIntoDatabase(results, resultscode);


                Log.e(TAG1, "resultCode: " + resultscode + ";");// OnReceiveResult:STATUS_FINISHED: "+results);
                break;
            case DownloadService.STATUS_ERROR:
                /* Handle the error */
                String error = resultData.getString(Intent.EXTRA_TEXT);
                Log.e(TAG1, "OnReceiveResult:STATUS_ERROR: " + error);
                HideProgressBar();
                //  Toast.makeText(this, error, Toast.LENGTH_LONG).show();
                break;
        }
    }

    private void InComingChat() {
        new Thread(new Runnable() {
            @Override
            public void run() {
                while (true) {
                    if (ScreenMode.equals("chat")) {
                        try {
                            Thread.sleep(3000);
                            IncomingTabOpenChats = ChatLastIdWebNumber_Chat();
                            ThreadChatLookUp(ShifaDepart.CheckExistingPublicChatAvailable("", IncomingTabOpenChats), "incoming_chat");

                        } catch (Exception ex) {

                        }
                    }
                }
            }
        }).start();
        ;
    }

    private void DownloadChatIntoDatabase(String results, String resultCode) {
        if (ScreenMode.equals("chat")) {
            LinearLayout ln_progressbar = (LinearLayout) findViewById(R.id.ln_progressbar);
            ln_progressbar.setVisibility(View.GONE);
        }
        if (results == null) {
            return;

        }
        SLAc.SaveWSChatPHPGetUserPvtPublicChat(results, ".XXXXXXXapp_chat", resultCode);

        if (myList.getCount() == 0)
            TabOpenChats("", "");

        else {
            if (resultCode.equals("incoming_chat")) {
                TabOpenChats("", IncomingTabOpenChats);

            } else {
                TabOpenChats(RetryTabOpenChats, "");
            }
        }

    }

    private void FetchingDataFromInternetForChats(String ScreenMode_, String first_id_web_) {

        final String ScreenMode = ScreenMode_;
        final String first_id_web = first_id_web_;
        if (ScreenMode.equals("chat")) {
            LinearLayout ln_progressbar = (LinearLayout) findViewById(R.id.ln_progressbar);
            ln_progressbar.setVisibility(View.VISIBLE);
        }
        Log.e(TAG1, ScreenMode + ", FetchingDataFromInternetForChats: " + first_id_web);
        mHandler = new Handler();
        new Thread(new Runnable() {
            @Override
            public void run() {

                // TODO Auto-generated method stub
                try {


                    if (ScreenMode.equals("chat")) {
                        ThreadChatLookUp(ShifaDepart.CheckExistingPublicChatAvailable(first_id_web, ""), ScreenMode);
                    } else if (ScreenMode.equals("disc")) {
                        Thread.sleep(3000);
                        ThreadChatLookUp(ShifaDepart.CheckNewOrExistingDiscussionBetweenUsers(DiscId), ScreenMode);
                    } else if (ScreenMode.equals("msg")) {
                        Thread.sleep(3000);
                        ThreadChatLookUp(ShifaDepart.CheckPvtMsgBetweenTwoUser(SLAc.UserSessionId(), .XXXXXXX_to, first_id_web), ScreenMode);
                    }

                    Log.e(TAG1, ScreenMode + ", FetchingDataFromInternetForChats: " + first_id_web);


                } catch (Exception e) {


                    //Toast.makeText(ctx.getApplicationContext(), e.toString(), 1000).show();
                    // TODO: handle exception
                }


            }
        }).start();
        Log.e(TAG1, ScreenMode + ", first_id_web: " + first_id_web);

    }

    private void OldFetchingDataFromInternetForChats(String ScreenMode, String id_web) {
        if (ScreenMode.equals(CONST_TABLE_APP_CHAT_MENU)) {
            ThreadChatLookUp(ShifaDepart.CheckNewOrExistingDiscussionOthers_V2(id_web, "0"), ScreenMode);
        }

    }

    private void DownloadNewOldChatLookup(String results) {
        String newId = null;
        String oldId = null;
        if (results != null) {
            String[] DataId = results.split("#=#");
            if (DataId[0].indexOf("new:") != -1) {
                newId = DataId[0].substring(4);
                if (DataId.length > 2 && DataId[1].indexOf("old:") != -1) {
                    oldId = DataId[1].substring(4);
                }
            } else if (DataId[0].indexOf("old:") != -1) {
                oldId = DataId[0].substring(4);

            }

        }
        Log.e(TAG1, "NewId:" + newId + " oldId: " + oldId + " ScrollStatus: " + ScrollStatus);

    }

    private void ShowNeccessaryInputs() {
        final RelativeLayout llout = (RelativeLayout) findViewById(R.id.llout1);
        llout.setVisibility(View.VISIBLE);
        Log.e("ScreenModeRefer", SLAc.GetPreferenceValue("ScreenMode"));


        SetTitleBarHeader();
        getWindow().setSoftInputMode(
                WindowManager.LayoutParams.SOFT_INPUT_STATE_ALWAYS_HIDDEN
        );

    }

    private void SetTitleBarHeader() {
        try {
            if (ScreenMode.equals("msg")) {
                SetTitleBar(BaseChat, "#006699");
                getActionBar().setIcon(R.drawable.ic_content_markunread);


            } else if (ScreenMode.equals("chat")) {
                SetTitleBar("Group Chat", "#880000");
                getActionBar().setIcon(R.drawable.ic_action_social_group);
            } else if (ScreenMode.equals("disc")) {

                SetTitleBar(BaseChat, "#867791");
                getActionBar().setIcon(R.drawable.ic_action_account_child);
            }

        } catch (Exception ex) {

        }
    }

    private void SetTitleBar(String Caption, String ColorCode) {
        try {
            setTitle(Caption);
            ActionBar bar = getActionBar();
            bar.setBackgroundDrawable(new ColorDrawable((Color.parseColor(ColorCode))));

        } catch (Exception e) {
            Log.e(TAG, "Error in SetTtitleBar " + e.toString());
        }
    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        // ShowProgressBar();
        this.ctx = this;
        SLAc = new Super_Library_AppClass(this);
        setContentView(R.layout.chat);
        ShifaDepart = new ShifaDepartment(this);
        SessionID = SLAc.UserSessionId();
        SessionName = SLAc.UserName();


        myList = (ListView) findViewById(R.id.listviewchatter);


        Bundle extras = getIntent().getExtras();
        if (extras != null) {
            ScreenMode = GetCursorVal(extras.getString("Mode"));
            DiscId = GetCursorVal(extras.getString("discid"));
            BaseChat = GetCursorVal(extras.getString("basechat"));
            .XXXXXXX_to = GetCursorVal(extras.getString(".XXXXXXX_to"));
            SLAc.SavePreference("PvtMsgFriendSessionId", .XXXXXXX_to);
            .XXXXXXX_to_name = GetCursorVal(extras.getString(".XXXXXXX_to_name"));
            SLAc.SavePreference("DiscId", DiscId);

            SLAc.SavePreference("PvtMsgFriendSessionName", .XXXXXXX_to_name);
        } else {
            ScreenMode = "chat";
        }
        //////////////Download Service

           /* Starting Download Service */
        mReceiver = new DownloadResultReceiver(new Handler());
        mReceiver.setReceiver(this);


        final Button button1 = (Button) findViewById(R.id.imgbtnmsgsend);
        button1.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                ChatSend();
            }
        });

        final Button button2 = (Button) findViewById(R.id.btnAttachment);
        button2.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {

                String uniqueID = UUID.randomUUID().toString();
                SLAc.SavePreference("ScreenMode", ScreenMode);
                SLAc.SavePreference("DiscId", DiscId);
                Intent intent = new Intent(chatter.this, ChatImageUpload.class);

                intent.putExtra("uniqueID", uniqueID);
                startActivity(intent);

            }
        });
        ShowNeccessaryInputs();


        myList.setTranscriptMode(ListView.TRANSCRIPT_MODE_ALWAYS_SCROLL);
        Log.e(TAG1, "ScreenMode:" + ScreenMode);
        if (ScreenMode.equals("chat")) {
            TabOpenChats("", "");
            InComingChat();
        } else if (ScreenMode.equals("disc")) {
            TabOpenChats(DiscId, "");
            SetTitleBarHeader();

        } else if (ScreenMode.equals("msg")) {
            TabOpenChats("", "");
            SetTitleBarHeader();


        }
        myList.setOnItemClickListener(new OnItemClickListener() {

            @SuppressLint("DefaultLocale")
            @Override
            public void onItemClick(AdapterView<?> listView, View view, int position,
                                    long id) {
                ChatMain listItem = (ChatMain) view.findViewById(R.id.id_chat_shifamember_logo).getTag();
                Log.e(TAG, "ScreenMode: " + ScreenMode + " listItem.chatter: " + listItem.id_web);

                if (position == 0) {
                    RetryTabOpenChats = listItem.id_web;
                    TabOpenChats(listItem.id_web, "");
                    return;
                }

            }
        });

        txtChatMsg = (EditText) findViewById(R.id.txtChatMsg);

        txtChatMsg.setOnKeyListener(new View.OnKeyListener() {
            public boolean onKey(View v, int keyCode, KeyEvent event) {
                // If the event is a key-down event on the "enter" button
                if ((event.getAction() == KeyEvent.ACTION_DOWN) &&
                        (keyCode == KeyEvent.KEYCODE_ENTER)) {
                    ChatSend();

                    return true;
                }
                return false;
            }
        });
        PostChatSendFromDatabase();
        Log.e(TAG1, "Chat Ready...");
    }

    private void GetChats() {

    }

    private String GetCursorVal(String Val) {

        if (Val == null) return "";
        return Val;
    }

    private boolean listIsAtTop() {
        if (myList.getChildCount() == 0) return true;
        return myList.getChildAt(0).getTop() == 0;
    }

    private void UpdateDBIRead(String id_web) {
        String q = "update .XXXXXXXapp_chat  set iRead = 1 where id_web = '" + id_web + "'";
        Log.e(TAG, "Query: " + q);
        DBHelper db1 = new DBHelper(ctx);
        db1.getWritableDatabase().execSQL(q);

        if (db1 != null) {
            db1.close();
        }
        Log.e(TAG, "Saved IRead: " + id_web);

    }

    private boolean ScrollIsTop() {
        try {
            //     int position = myList.getFirstVisiblePosition();
            View v = myList.getChildAt(0);
            int offset = (v == null) ? 0 : v.getTop();
            ScrollTouchActivated = true;
            Log.e(TAG, "offset:" + offset);
            if (offset == 0) return true;
            return false;
        } catch (Exception ex) {
            return false;
        }
    }

    private void ShowPage(String ScreenName) {

        SLAc.SavePreference("ScreenMode", ScreenName);
        Intent intent = getIntent();
        finish();
        startActivity(intent);
    }

    private void ProcessDatabase(boolean Bottom) {

        Log.e(TAG, "fetching public chat: " + Bottom + " " + ScreenMode);
        if (ScreenMode.equals(CONST_CONTACT_CHAT)) {
            if (SLAc.GetPreferenceValue("Server").equals("true")) {
                SLAc.SavePreference("Server", "");
                // ShowProgressBar();
                ShifaDepart.CheckNeworExistingMemberInformationAndSaveInDatabaseForGroupChat();
            }
        } else if (ScreenMode.equals(CONST_DISCUSSION_PERSONAL_POSTED_OVERVIEW_LIST)) {

            if (SLAc.GetPreferenceValue("Server").equals("true")) {
                Log.e(TAG, "REFRESH SERVER");
                SLAc.SavePreference("Server", "");
                //  ShowProgressBar();
                ShifaDepart.CheckNewOrExistingDisucssionChat();

            }
        } else if (ScreenMode.equals(CONST_TABLE_APP_CHAT_MENU)) {


        } else if (ScreenMode.equals(CONST_DISCUSSION_CHAT)) {
            ShifaDepart.CheckNewOrExistingDiscussionBetweenUsers(SLAc.GetPreferenceValue("DiscId"));

        } else if (ScreenMode.equals(CONST_PRIVATE_MESGGING_CHAT)) {

            ShifaDepart.CheckPvtMsgBetweenTwoUser(SLAc.UserSessionId(), SLAc.GetPreferenceValue("PvtMsgFriendSessionId")
                    , ChatLastIdWebNumberForPVtChat(SLAc.UserSessionId(), SLAc.GetPreferenceValue("PvtMsgFriendSessionId")));

        } else if (ScreenMode.equals(CONST_PRIVATE_CHAT_OVERVIEW_LIST)) {
            //  if (SLAc.GetPreferenceValue("Server").equals("true")) {
            //    SLAc.SavePreference("Server", "");
            //   ShifaDepart.CheckNewOrExistingPvtChatAvaiable(SLAc.UserSessionId());
            //   }
        } else {
            if (Bottom == false) {
                Log.e(TAG, "list is at top ");

// if first time scroll is on top but idweb not set or matched so get in the statement block and do action

                //  if (CheckInAppDatabaseDataAvailable(preFirstId_web) == false) {
                Log.e(TAG, "Fetching old data: " + preFirstId_web);
                //    ShowProgressBar();
                //  ShifaDepart.CheckExistingPublicChatAvailable(ChatFirstIdWebNumber());
                // Your code here
                //  }
              /*  else{
                    Log.e(TAG,"Asking db to pull it from db : " +preFirstId_web );

                    SLAc.SavePreference("BroadCast_LoadMsg_Hide","DOAction");
                    SLAc.SavePreference("BroadCast_PvtPublic_Chat","RefreshYourSelf");
                }
*/

            } else {
                //    Log.e(TAG,"Public Chat");
                //ShifaDepart.AnyNewChat(ChatLastIdWebNumber(),PublicChatIdCode);
                //if  (SLAc.GetPreferenceValue("NewChat").equals("true")) {
                //  SLAc.SavePreference("NewChat", "");

                //      Log.e(TAG,"Public Chat in");
                //    ShifaDepart.CheckNewOrExistingPublicChatAvaiable(ChatLastIdWebNumber());
                //}

            }

        }


    }

    private void ThreadChatLookUp(String URL, String requestid) {

        Intent intent = new Intent(Intent.ACTION_SYNC, null, ctx, DownloadService.class);

    /* Send optional extras to Download IntentService */
        Log.e(TAG1, URL);
        intent.putExtra("url", URL);
        intent.putExtra("receiver", mReceiver);
        intent.putExtra("requestId", requestid);

        startService(intent);
    }

    private String GetFirstIdWeb() {
        try {
            View view1 = myList.getChildAt(myList.getChildCount() - 1);

            if (view1 != null) {
                ScrollStatus = 1;
                ChatMain listItem = (ChatMain) view1.findViewById(R.id.id_chat_shifamember_logo).getTag();
                preFirstId_web = listItem.id_web;
            }
            if (preFirstId_web == null) {
                return "";
            }
            // bottom of the scrollview
            return preFirstId_web;

        } catch (Exception ex) {
            Log.e(TAG, "Error in SetLastIdWeb:" + ex.toString());
        }
        return "";
        // Log.e(TAG,"SetLastIdWeb: preFirstId_web:"+preFirstId_web);
    }

    private boolean CheckInAppDatabaseDataAvailable(String id_web) {
        if (id_web == null) return false;
        String q = "select id_web from .XXXXXXXapp_chat  where _to = '" + PublicChatIdCode + "' and id_web <= '" + (Integer.valueOf(id_web) - 1) + "' limit 0,1";
        Log.e(TAG, "Query: " + q);
        DBHelper db1 = new DBHelper(ctx);
        Cursor cursor = db1.getReadableDatabase().rawQuery(q, null);
        boolean RecordExists = false;
        cursor.moveToFirst();
        Log.e(TAG, "move first: " + q);
        while (!cursor.isAfterLast()) {
            RecordExists = true;
            cursor.moveToNext();
        }
        if (cursor != null) {
            cursor.close();

        }
        if (db1 != null) {
            db1.close();
        }
        Log.e(TAG, "RecordExists: " + RecordExists);
        return RecordExists;
    }

    protected void PostChatSendFromDatabase() {
        if (!SLAc.GetPreferenceValue("CheckPendingMsgstoSend").equals("true")) return;
        //Look in the database for any unpending chat not transfered yet to server so do query and get all the details and push to ther server and update offline too
        String q = "select id,chat,frm,chatter,dt,datetime,id_web,picture,video,id_app,_to,.XXXXXXX_to,chatter_to from .XXXXXXXapp_chat where id_web is null";
        Log.e("QueryQuery", q);

        DBHelper db1 = new DBHelper(ctx);
        Cursor cursor = db1.getReadableDatabase().rawQuery(q, null);
        // Log.e(TAG, "PostPvtPublicChatSendFromDatabase COunt " + cursor.getCount());
        if (cursor.getCount() == 0) {

            SLAc.SavePreference("CheckPendingMsgstoSend", "");
        }
        try {
            cursor.moveToFirst();
            while (!cursor.isAfterLast()) {
                Log.e(TAG, "Cursor Reading ... for offline chat");


                Log.e(TAG, "Chat " + cursor.getString(cursor.getColumnIndex("chat")));

                Log.e(TAG, "_to " + cursor.getString(cursor.getColumnIndex("_to")));
                Log.e(TAG, "frm " + cursor.getString(cursor.getColumnIndex("frm")));
                Log.e(TAG, ".XXXXXXX_to " + cursor.getString(cursor.getColumnIndex(".XXXXXXX_to")));

                Log.e(TAG, "chatter_to " + cursor.getString(cursor.getColumnIndex("chatter_to")));
                Log.e(TAG, "id_app " + cursor.getString(cursor.getColumnIndex("id_app")));
                Log.e(TAG, "picture " + cursor.getString(cursor.getColumnIndex("picture")));
                Log.e(TAG, "--------------------------------------------------------");


                String ChatTextSend = cursor.getString(cursor.getColumnIndex("chat"));
                String id_app = cursor.getString(cursor.getColumnIndex("id_app"));
                String _to = cursor.getString(cursor.getColumnIndex("_to"));
                String picture = cursor.getString(cursor.getColumnIndex("picture"));
                String video = cursor.getString(cursor.getColumnIndex("video"));
                String SessionIDTo = cursor.getString(cursor.getColumnIndex(".XXXXXXX_to"));
                String ChatterToName = cursor.getString(cursor.getColumnIndex("chatter_to"));
                String base_chat = cursor.getString(cursor.getColumnIndex("base_chat"));
                ShifaDepart.PostChatSend(ChatTextSend, _to, picture, video, id_app, SessionIDTo, ChatterToName, base_chat);


                cursor.moveToNext();
            }
        } catch (Exception ex) {
            Log.e(TAG, "Error while posing offline chat to the server might be some cols error " + ex.toString());

            //	Toast.makeText(getApplicationContext(), "Please wait.." + ex.toString() , 1000).show();
        }
        if (cursor != null) {
            cursor.close();
        }
        if (db1 != null) {
            db1.close();
        }

    }

    /*--uncomment when we deployed in shifa page
    @Override
    public boolean onKeyDown(int keyCode, KeyEvent event) {

        if (keyCode == KeyEvent.KEYCODE_BACK) {
            chatactive = false;
            Intent intent = new Intent(chat.this, home_menu.class);
            intent.putExtra("SessionID", SessionID);
              startActivity(intent);
              finish();
        }
        return super.onKeyDown(keyCode, event);
    }*/
    private String ChatLastIdWebNumber_Chat() {
        String q = "select id_web from .XXXXXXXapp_chat where _to = '-999' order by id_web desc limit 0,1";
        //Log.e(TAG,"Query: " + q);
        String id_web = "0";
        DBHelper db1 = new DBHelper(ctx);
        Cursor cursor = db1.getReadableDatabase().rawQuery(q, null);
        cursor.moveToFirst();
        while (!cursor.isAfterLast()) {

            id_web = cursor.getString(cursor.getColumnIndex("id_web"));
            cursor.moveToNext();
        }
        if (cursor != null) {
            cursor.close();

        }
        if (db1 != null) {
            db1.close();
        }
        return id_web;
    }

    private String ChatFirstIdWebNumber() {
        String q = "select id_web from .XXXXXXXapp_chat where _to = '-888' or '-777' order by id_web asc limit 0,1";
        //Log.e(TAG,"Query: " + q);
        String id_web = "0";
        DBHelper db1 = new DBHelper(ctx);
        Cursor cursor = db1.getReadableDatabase().rawQuery(q, null);
        cursor.moveToFirst();
        while (!cursor.isAfterLast()) {

            id_web = cursor.getString(cursor.getColumnIndex("id_web"));
            cursor.moveToNext();
        }
        if (cursor != null) {
            cursor.close();

        }
        if (db1 != null) {
            db1.close();
        }
        return id_web;
    }

    private String ChatLastIdWebNumberForPVtChat(String Session_id, String Session_id_to) {
        String q = "select id_web from .XXXXXXXapp_chat where frm = '" + Session_id + "' and .XXXXXXX_to = '" + Session_id_to + "' order by id_web desc limit 0,1";
        //Log.e(TAG,"Query: " + q);
        String id_web = "0";
        DBHelper db1 = new DBHelper(ctx);
        Cursor cursor = db1.getReadableDatabase().rawQuery(q, null);
        cursor.moveToFirst();
        while (!cursor.isAfterLast()) {

            id_web = cursor.getString(cursor.getColumnIndex("id_web"));
            cursor.moveToNext();
        }
        if (cursor != null) {
            cursor.close();

        }
        if (db1 != null) {
            db1.close();
        }
        return id_web;
    }

    //qprocess
    private void TabOpenChats(String clicked_user_first_id_web, String after_id_web) {
        Log.e(TAG1, "clicked_user_first_id_web: " + clicked_user_first_id_web);
        String q1 = "";
        boolean Bottom = false;
        int PublicID_WEB = 0;
        if (clicked_user_first_id_web == null) clicked_user_first_id_web = "";
        if (ScreenMode.equals("chat")) {

            if (!after_id_web.equals("")) {
                PublicID_WEB = Integer.valueOf(after_id_web);
                q1 = ".XXXXXXX .XXXXXXXapp_chat where id_web > " + PublicID_WEB + " and chat_type = '" + ScreenMode + "' order by id_web desc limit 100 ";
                Bottom = true;
            } else if (clicked_user_first_id_web.equals("")) {
                q1 = ".XXXXXXX .XXXXXXXapp_chat where chat_type = '" + ScreenMode + "'  order by id_web desc limit 100 ";
                Bottom = false;
            } else if (!clicked_user_first_id_web.equals("")) {
                PublicID_WEB = Integer.valueOf(clicked_user_first_id_web);
                q1 = ".XXXXXXX .XXXXXXXapp_chat where id_web <= " + (PublicID_WEB - 1) + " and chat_type = '" + ScreenMode + "' order by id_web desc limit 100 ";
                Bottom = false;
            }


        } else if (ScreenMode.equals("disc")) {
            if (!DiscId.equals("")) {
                userArray.clear();
                q1 = ".XXXXXXX .XXXXXXXapp_chat where (_to = '" + DiscId + "' or id_web = '" + DiscId + "') and chat_type = '" + ScreenMode + "' order by id_web desc limit 100 ";
            }
        } else if (ScreenMode.equals("msg")) {

            userArray.clear();
            q1 = ".XXXXXXX .XXXXXXXapp_chat WHERE ((frm = '" + .XXXXXXX_to + "' and .XXXXXXX_to = '" + SLAc.UserSessionId() + "') or (frm = '" + SLAc.UserSessionId() + "' and  .XXXXXXX_to  = '" + .XXXXXXX_to + "')) order by id_web desc";

        }
        TabListUpdate(Bottom, q1, clicked_user_first_id_web);
        //TabListUpdate(q2);
        HideProgressBar();
    }

    private void TabListUpdate(boolean Bottom, String q, String clicked_user_first_id_web) {
        DBHelper db1 = new DBHelper(ctx);
        Cursor cursor = null;
        int RCount = 0;
        if (!q.equals("")) {
            Log.e(TAG2, "Query: " + q);
            cursor = db1.getReadableDatabase().rawQuery(q, null);
            RCount += cursor.getCount();
            if (cursor.getCount() == 0) {
                if (cursor != null) {
                    cursor.close();
                }
                if (db1 != null) {
                    db1.close();
                }
                Log.e(TAG, "Searching for old chat ");

                FetchingDataFromInternetForChats(ScreenMode, clicked_user_first_id_web);
                //return;
            } else {
                Log.e(TAG, "RCount: " + RCount);
                TabPassData(cursor, CONST_TABLE_APP_CHAT_MENU, "Menu", Bottom);
                if (ScreenMode.equals("disc")) {
                    FetchingDataFromInternetForChats(ScreenMode, clicked_user_first_id_web);
                } else if (ScreenMode.equals("msg")) {
                    FetchingDataFromInternetForChats(ScreenMode, "");
                }
            }
        }

        TabListBind(RCount, Bottom);
        if (cursor != null) {
            cursor.close();

        }
        if (db1 != null) {
            db1.close();
        }

    }

    private void TabPassData(Cursor cursor, String DataType, String ArrayId, boolean bottom) {
        cursor.moveToFirst();
        while (!cursor.isAfterLast()) {
            // Logger(cursor);
            if (bottom == false) {
                userArray.add(0, new ChatMain(cursor.getString(cursor.getColumnIndex("chat")),
                        cursor.getString(cursor.getColumnIndex("frm")),
                        cursor.getString(cursor.getColumnIndex("chatter")),
                        cursor.getString(cursor.getColumnIndex("id")),
                        cursor.getString(cursor.getColumnIndex("datetime")),
                        cursor.getString(cursor.getColumnIndex("id_web")),
                        cursor.getString(cursor.getColumnIndex("id_app")),
                        cursor.getString(cursor.getColumnIndex("picture")),
                        cursor.getString(cursor.getColumnIndex("chat_type"))));
            } else {
                String frm = GetCursorVal(cursor.getString(cursor.getColumnIndex("frm")));
                if (frm.equals(SLAc.UserSessionId())) {
                    String id_app = GetCursorVal(cursor.getString(cursor.getColumnIndex("id_app")));
                    String id_web = GetCursorVal(cursor.getString(cursor.getColumnIndex("id_web")));
                    Update_IdWeb(id_app, id_web);
                } else {
                    userArray.add(new ChatMain(cursor.getString(cursor.getColumnIndex("chat")),
                            cursor.getString(cursor.getColumnIndex("frm")),
                            cursor.getString(cursor.getColumnIndex("chatter")),
                            cursor.getString(cursor.getColumnIndex("id")),
                            cursor.getString(cursor.getColumnIndex("datetime")),
                            cursor.getString(cursor.getColumnIndex("id_web")),
                            cursor.getString(cursor.getColumnIndex("id_app")),
                            cursor.getString(cursor.getColumnIndex("picture")),
                            cursor.getString(cursor.getColumnIndex("chat_type"))));
                }
            }
            cursor.moveToNext();
        }
    }

    private void Update_IdWeb(String id_app, String id_web) {
        if (id_app.equals("")) return;
        String q = "update .XXXXXXXapp_chat  set id_web = " + id_web + " where id_app = '" + id_app + "'";
        Log.e(TAG, "Query: " + q);
        DBHelper db1 = new DBHelper(ctx);
        db1.getWritableDatabase().execSQL(q);

        if (db1 != null) {
            db1.close();
        }
        Log.e(TAG, "Saved IRead: " + id_app);
    }

    private void TabListBind(int RCount, boolean Bottom) {
        if (ChatAdapter != null) {
            //     myList.setSelection(0);
            Log.e(TAG1, "Scroll bottom " + Bottom);
            if (Bottom == false) {
                if ((RCount - 4) > 0) {

                    Log.e(TAG1, "RcOunt" + (RCount - 4));
                    myList.setSelection(RCount - 4);
                }
            } else {
                scrollMyListViewToBottom();
            }
        }

        if (myList.getCount() == 0) {
            ChatAdapter = new ChatAdapter(this, R.layout.listview_chat, userArray, SessionID);
            myList.setAdapter(ChatAdapter);
            myList.setSelection(ChatAdapter.getCount());
        }


    }

    private void Logger(Cursor cursor) {
        Log.e(TAG, "progress " + ScreenMode + " " + cursor.getString(cursor.getColumnIndex("id_web")));

        Log.e(TAG, "Chat " + cursor.getString(cursor.getColumnIndex("chat")));

        Log.e(TAG, "frm " + cursor.getString(cursor.getColumnIndex("frm")));
        Log.e(TAG, "chatter " + cursor.getString(cursor.getColumnIndex("chatter")));
        Log.e(TAG, "id " + cursor.getString(cursor.getColumnIndex("id")));
        Log.e(TAG, "id_app " + cursor.getString(cursor.getColumnIndex("id_app")));
        Log.e(TAG, "datetime " + cursor.getString(cursor.getColumnIndex("datetime")));
        Log.e(TAG, "--------------------------------------------------------");

    }

    @Override
    public void onRestoreInstanceState(Bundle savedInstanceState) {
        final int listIndex = savedInstanceState.getInt("ListIndex");

    }

    private void HideProgressBar() {
        // Log.e(TAG,"HideProgressBar.... " );

    }

    private void scrollMyListViewToBottom() {

        myList.post(new Runnable() {
            @Override
            public void run() {
                // Select the last row so it will scroll into view...
                if (ChatAdapter != null) {
                    //      Log.e(TAG, "Scroll My ListView discussion");
                    myList.setSelection(ChatAdapter.getCount() - 1);
                    myList.smoothScrollToPosition(ChatAdapter.getCount() - 1);
                }
            }
        });

    }

    @Override
    protected void onStop() {
        super.onStop();
        chatactive = false;
        try {
            if (mReceiver != null) {
                mReceiver = null;
            }
        } catch (Exception ex) {

        }
        Log.e("Chat Activity", "user not longer in the application");

    }


    @Override
    protected void onStart() {
        super.onStart();
        chatactive = true;
        Log.e("Chat Activity", "user is back");
        if (SLAc.GetPreferenceValue("ImageUploadSuccess").equals("true")) {
            SLAc.SavePreference("ImageUploadSuccess", "");
            Calendar c = Calendar.getInstance();
            SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
            String strDate = sdf.format(c.getTime());


            if (ScreenMode.equals("chat")) {
                ShifaDepart.PostChatSend(SLAc.GetPreferenceValue("ImageChatSend"), PublicChatIdCode, SLAc.GetPreferenceValue("ImagePath"), "", SLAc.GetPreferenceValue("Imageid_app"), "", "", BaseChat);
                userArray.add(new ChatMain(SLAc.GetPreferenceValue("ImageChatSend"), SLAc.UserSessionId(), SLAc.UserName(), null, strDate, null, SLAc.GetPreferenceValue("Imageid_app"), SLAc.GetPreferenceValue("ImagePath"), ScreenMode));
                //       userArray.add(new ChatMain(SLAc.GetPreferenceValue("ImageChatSend"), SLAc.UserSessionId(), SLAc.UserName(), null, strDate,null,SLAc.GetPreferenceValue("Imageid_app"),SLAc.GetPreferenceValue("ImagePath")));
                scrollMyListViewToBottom();
            } else if (ScreenMode.equals("msg")) {
                ShifaDepart.PostChatSend(SLAc.GetPreferenceValue("ImageChatSend"), PublicChatIdCode, SLAc.GetPreferenceValue("ImagePath"), "", SLAc.GetPreferenceValue("Imageid_app"), SLAc.GetPreferenceValue("PvtMsgFriendSessionId"), SLAc.GetPreferenceValue("PvtMsgFriendSessionName"), BaseChat);
                userArray.add(new ChatMain(SLAc.GetPreferenceValue("ImageChatSend"), SLAc.UserSessionId(), SLAc.UserName(), null, strDate, null, SLAc.GetPreferenceValue("Imageid_app"), SLAc.GetPreferenceValue("ImagePath"), ScreenMode));
                SLAc.SavePreference("BroadCast_LoadMsg_Hide", "DOAction");
                SLAc.SavePreference("BroadCast_PvtPublic_Chat", "RefreshYourSelf");
                scrollMyListViewToBottom();
                // userArray.add(new ChatMain(SLAc.GetPreferenceValue("ImageChatSend"), SLAc.UserSessionId(), SLAc.UserName(), null, strDate,null,SLAc.GetPreferenceValue("Imageid_app"),SLAc.GetPreferenceValue("ImagePath")));

            } else if (ScreenMode.equals("disc")) {
                ShifaDepart.PostChatSend(SLAc.GetPreferenceValue("ImageChatSend"), SLAc.GetPreferenceValue("DiscId"), SLAc.GetPreferenceValue("ImagePath"), "", SLAc.GetPreferenceValue("Imageid_app"), SLAc.GetPreferenceValue("PvtMsgFriendSessionId"), SLAc.GetPreferenceValue("PvtMsgFriendSessionName"), BaseChat);
                userArray.add(new ChatMain(SLAc.GetPreferenceValue("ImageChatSend"), SLAc.UserSessionId(), SLAc.UserName(), null, strDate, null, SLAc.GetPreferenceValue("Imageid_app"), SLAc.GetPreferenceValue("ImagePath"), ScreenMode));
                SLAc.SavePreference("BroadCast_LoadMsg_Hide", "DOAction");
                SLAc.SavePreference("BroadCast_PvtPublic_Chat", "RefreshYourSelf");
                // userArray.add(new ChatMain(SLAc.GetPreferenceValue("ImageChatSend"), SLAc.UserSessionId(), SLAc.UserName(), null, strDate,null,SLAc.GetPreferenceValue("Imageid_app"),SLAc.GetPreferenceValue("ImagePath")));
                scrollMyListViewToBottom();
            }

        }
        // ChatReveiverAsync("",false);

    }

    public void ChatSend() {
        ChatSendEnterKeyHit = true;
        EditText txtChatMsg = (EditText) findViewById(R.id.txtChatMsg);

        String chat = txtChatMsg.getText().toString();
        txtChatMsg.setText("");
        if (chat.trim().equals("")) return;

        //	 Log.e("ChatSend","enter");
        //	//DownloadWebPageTask task = new DownloadWebPageTask();
        //	Log.e("DownloadWebPageTask","enter");

        chat = chat.replaceAll("'", "");
        chat = chat.replaceAll(",", " ");
        chat = chat.replaceAll(":", " ");
        ChatTextSend = chat;

        //task.execute(new String[] { "http://kent..XXXXXXX/app_php/pp_chat2.php"});
        //Log.e("task.execu","enter");

        String id_app = UUID.randomUUID().toString();
        Calendar c = Calendar.getInstance();
        SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
        String strDate = sdf.format(c.getTime());
        userArray.add(new ChatMain(ChatTextSend, SLAc.UserSessionId(), SLAc.UserName(), null, strDate, null, id_app, "", ScreenMode));


        if (ScreenMode.equals("chat")) {
            ShifaDepart.PostChatSend(ChatTextSend, "-999", "", "", id_app, "", "", BaseChat);
        } else if (ScreenMode.equals("msg")) {
            ShifaDepart.PostChatSend(ChatTextSend, "-777", "", "", id_app, .XXXXXXX_to, .XXXXXXX_to_name, BaseChat);

        } else if (ScreenMode.equals("disc")) {
            ShifaDepart.PostChatSend(ChatTextSend, DiscId, "", "", id_app, .XXXXXXX_to, .XXXXXXX_to_name, BaseChat);

        }
        scrollMyListViewToBottom();
        PostChatSendFromDatabase();
        //   myList.setSelection(userAdapter.getCount() - 1);
        SLAc.SavePreference("CheckPendingMsgstoSend", "true");
        ScrollTouchActivated = false;


    }


}
