package com.shifa.kent.chatsdk;


import android.annotation.SuppressLint;
import android.app.ActionBar;
import android.app.ActionBar.Tab;
import android.app.Activity;
import android.app.FragmentTransaction;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.Intent;
import android.database.Cursor;
import android.graphics.Color;
import android.graphics.drawable.ColorDrawable;
import android.os.Bundle;
import android.os.Handler;
import android.util.Log;
import android.view.KeyEvent;
import android.view.Menu;
import android.view.MenuItem;
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
import java.util.Iterator;
import java.util.UUID;

public class chat extends Activity implements DownloadResultReceiver.Receiver,
        ActionBar.TabListener {
    final String CONST_PRIVATE_CHAT_OVERVIEW_LIST = "PrivateChatterNames";
    final String CONST_PRIVATE_MESGGING_CHAT = "PrivateMessageChatting";
    final String CONST_PUBLIC_CHAT = "PublicChat";
    final String CONST_DISCUSSION_CHAT = "DiscussionMessageChatting";
    final String CONST_TABLE_APP_CHAT_MENU = "DiscussionChat";
    final String CONST_DISCUSSION_PERSONAL_POSTED_OVERVIEW_LIST = "DiscussionPostedByUser";
    final String CONST_CONTACT_CHAT = "ShifaMembers";
    final String CONST_TABLE_APP_CONTACT_MENU = "ShifaExpertMembers";
    final String TAG1 = "ChatService";
    final String url = "http://kent..XXXXXXX/app_php/Shifa4o/WebService/WSChat.php";
    public String ChatTextSend = "";
    public String SessionID = "";
    public String SessionName = "";
    SimpleCursorAdapter CursorAdapter;
    Handler mHandler;
    ListView myList;
    public int state;
            ListView myContact;
    boolean ScrollTouchActivated = false;
    boolean autoscroll = true;
    String RecentChat = "";
    String PublicChatIdCode = "-999";
    String ChatIndex = "0";
    boolean chatactive = true;
    int ListCount = 0;
    ProgressDialog barProgressDialog;
    int Interval = 60000;
    boolean TitleBarSetter = false;
    String PrevChat = "";
    EditText txtChatMsg;
    int ScrollStatus = 0; //ScrollStatus if -1 means top , 0 means in the middle, 1 means at bottom of the scroll
    boolean FirstTimeView = true;
    String TAG2 = "DB";
    String TAG = "ChatUI";
    String ScreenMode;
    int iLoadMsg = 2000;
    Context ctx;
    Super_Library_AppClass SLAc;
    boolean ChatSendEnterKeyHit = false;
    boolean ListRefresh = false;
    ShifaDepartment ShifaDepart;
    ChatAdapter userAdapter;
    ChatAdapterView ChatAdapter;
    ChatAdapterView ContactAdapter;
    ArrayList<ChatMain> userArrContact = new ArrayList<ChatMain>();
    ArrayList<ChatMain> userArray = new ArrayList<ChatMain>();
    String LastQuery = "";
    String url_old = "http://kent..XXXXXXX/app_php/Shifa4o/WebService/WSChat.php";
    private String preFirstId_web;
    private ArrayAdapter arrayAdapter = null;
    private DownloadResultReceiver mReceiver;
    private String RetryTabOpenChats;

    @Override
    public void onTabReselected(Tab tab, FragmentTransaction ft) {

    }

    @Override
    public void onTabSelected(Tab tab, FragmentTransaction ft) {
        Log.e(TAG1, "Tab: " + tab.getText());
        if (tab.getText().toString().equals("New Post")) {

            Intent intent = new Intent(this, chat_case_open.class);

            startActivity(intent);
            // getActionBar().setSelectedNavigationItem(1); //where 1 equals the 2nd tab
            //    finish();
        } else if (tab.getText().toString().equals("Group Chat")) {
            Intent intent = new Intent(this, chatter.class);
            intent.putExtra("Mode", "chat");
            startActivity(intent);
            //    ShowPage(CONST_PUBLIC_CHAT);
            //startActivity(intent);
            // getActionBar().setSelectedNavigationItem(1); //where 1 equals the 2nd tab
            //  finish();
        } else if (tab.getText().toString().equals("Friends")) {
            myList.setVisibility(View.GONE);
            myContact.setVisibility(View.VISIBLE);
        } else if (tab.getText().toString().equals("Notification")) {
            myList.setVisibility(View.VISIBLE);
            myContact.setVisibility(View.GONE);
        }

    }

    @Override
    public void onTabUnselected(Tab tab, FragmentTransaction ft) {
           /* if (fragList.size() > tab.getPosition()) {
                ft.remove(fragList.get(tab.getPosition()));
            }
            */

    }

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
                if (resultscode.equals("NEWCHAT")) {
                    DownloadNewOldChatLookup(results);
                } else {
                    DownloadChatIntoDatabase(results, resultscode);
                }


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

    private void DownloadChatIntoDatabase(String results, String resultCode) {
        if (results == null) {

        } else {
            SLAc.SaveWSChatPHPGetUserPvtPublicChat(results, ".XXXXXXXapp_chat", resultCode);
        }
        if (myList.getCount() == 0)
            TabOpenChats("");
        else {
            TabOpenChats(RetryTabOpenChats);
        }
    }

    private void FetchingDataFromInternetForChats(String ScreenMode, boolean Startup) {
        if (ScreenMode.equals(CONST_PRIVATE_CHAT_OVERVIEW_LIST)) {
            ThreadChatLookUp(ShifaDepart.CheckNewOrExistingPvtChatAvaiable(SLAc.UserSessionId(), ""), ScreenMode);
        }
        if (ScreenMode.equals(CONST_PUBLIC_CHAT)) {
            ThreadChatLookUp(ShifaDepart.CheckExistingPublicChatAvailable("", ""), ScreenMode);
        }
        if (ScreenMode.equals(CONST_TABLE_APP_CHAT_MENU)) {
            ThreadChatLookUp(ShifaDepart.CheckNewOrExistingDiscussionOthers_V2("", "0"), ScreenMode);
        }
      /* else if (ScreenMode.equals(CONST_PUBLIC_CHAT)){
            if (myList.getCount() == 0) {

                ThreadChatLookUp(ShifaDepart.CheckExistingPublicChatAvailable("",ChatLastIdWebNumber()), CONST_PUBLIC_CHAT);
            }
            else{
                ThreadChatLookUp(ShifaDepart.CheckExistingPublicChatAvailable(ChatFirstIdWebNumber(),""), CONST_PUBLIC_CHAT);


        }
        */
    }

    private void OldFetchingDataFromInternetForChats(String ScreenMode, String id_web) {
        if (ScreenMode.equals(CONST_TABLE_APP_CHAT_MENU)) {
            ThreadChatLookUp(ShifaDepart.CheckNewOrExistingDiscussionOthers_V2(id_web, "0"), ScreenMode);
        }
      /* else if (ScreenMode.equals(CONST_PUBLIC_CHAT)){
            if (myList.getCount() == 0) {

                ThreadChatLookUp(ShifaDepart.CheckExistingPublicChatAvailable("",ChatLastIdWebNumber()), CONST_PUBLIC_CHAT);
            }
            else{
                ThreadChatLookUp(ShifaDepart.CheckExistingPublicChatAvailable(ChatFirstIdWebNumber(),""), CONST_PUBLIC_CHAT);


        }
        */
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
            if (null != newId) {
                if (ScrollStatus == 1 || ScrollStatus == 0) {
                    //Bottom Page
                    Log.e(TAG1, "ScrollBottom Data Fetched");
                    ProcessDatabase(true);
                }
            } else if (null != oldId && ScrollIsTop() == true) {
                //Top Page
                Log.e(TAG1, "ScrollTop Data Fetched");
                ProcessDatabase(false);
                ScrollStatus = 1;

            }

        }
        Log.e(TAG1, "NewId:" + newId + " oldId: " + oldId + " ScrollStatus: " + ScrollStatus);

    }

    private void ShowNeccessaryInputs() {
        final RelativeLayout llout = (RelativeLayout) findViewById(R.id.llout);
        llout.setVisibility(View.GONE);

        Log.e("ScreenModeRefer", SLAc.GetPreferenceValue("ScreenMode"));

        if (ScreenMode.equals(CONST_PRIVATE_MESGGING_CHAT)) {
            PublicChatIdCode = "";
            //   SetTitleBar("Inbox");

        } else if (ScreenMode.equals(CONST_PUBLIC_CHAT)) {
            PublicChatIdCode = "-999";

        } else if (ScreenMode.equals(CONST_DISCUSSION_PERSONAL_POSTED_OVERVIEW_LIST)) {
            PublicChatIdCode = "-888";
            // Interval =  100;
        } else if (ScreenMode.equals(CONST_TABLE_APP_CHAT_MENU)) {
            PublicChatIdCode = "-888";

        }  //SetTitleBarHeader();
        SLAc.SavePreference("Server", "true");
        SLAc.SavePreference("PublicChatIdCode", PublicChatIdCode);
        getWindow().setSoftInputMode(
                WindowManager.LayoutParams.SOFT_INPUT_STATE_ALWAYS_HIDDEN
        );
        //  SetTitleBar("Please wait...","#880000");

    }

    private void SetTitleBarHeader() {
        try {
            if (ScreenMode.equals(CONST_PRIVATE_MESGGING_CHAT)) {
                SetTitleBar(SLAc.GetPreferenceValue("PvtMsgFriendSessionName") + " - You", "#006699");
                getActionBar().setIcon(R.drawable.ic_action_action_question_answer);


            } else if (ScreenMode.equals(CONST_PUBLIC_CHAT)) {
                SetTitleBar("Group Chat", "#880000");
                getActionBar().setIcon(R.drawable.ic_action_social_group);
            } else if (ScreenMode.equals(CONST_DISCUSSION_PERSONAL_POSTED_OVERVIEW_LIST)) {
                SetTitleBar("My Discussion", "#6b297f");

                getActionBar().setIcon(R.drawable.ic_my_posted_discussion);
            } else if (ScreenMode.equals(CONST_TABLE_APP_CHAT_MENU)) {

                SetTitleBar("Discussion", "#5e3603");
                getActionBar().setIcon(R.drawable.ic_action_communication_message);
            } else if (ScreenMode.equals(CONST_TABLE_APP_CONTACT_MENU)) {

                SetTitleBar("Expert", "#3E80B4");
                getActionBar().setIcon(R.drawable.ic_action_action_face_unlock);
            } else if (ScreenMode.equals(CONST_CONTACT_CHAT)) {

                SetTitleBar("Friends", "#880000");
                getActionBar().setIcon(R.drawable.ic_action_action_supervisor_account);
            } else if (ScreenMode.equals(CONST_PRIVATE_CHAT_OVERVIEW_LIST)) {

                SetTitleBar("Inbox", "#006633");
                getActionBar().setIcon(R.drawable.ic_action_action_question_answer);
            } else if (ScreenMode.equals(CONST_DISCUSSION_CHAT)) {

                SetTitleBar("You - All Users", "#867791");
                getActionBar().setIcon(R.drawable.ic_my_posted_discussion);
            }

        } catch (Exception ex) {

        }
    }

    private void ShowProgressBar() {


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
        if (!SLAc.GetPreferenceValue("ScreenMode").equals("0")) {
            ScreenMode = SLAc.GetPreferenceValue("ScreenMode");
        } else {
            ScreenMode = CONST_PUBLIC_CHAT;
        }
        Log.e("SessionName ", SessionName);
        Log.e("SessionID", SessionID);
        myList = (ListView) findViewById(R.id.lstViewChat);
        myContact = (ListView) findViewById(R.id.listviewContact);

        String[] TabStr = {"Notification", "Group Chat", "New Post"};
        ActionBar bar = getActionBar();
        bar.setBackgroundDrawable(new ColorDrawable((Color.parseColor("#003200"))));
        bar.setStackedBackgroundDrawable(new ColorDrawable((Color.parseColor("#004600"))));
        bar.setNavigationMode(ActionBar.NAVIGATION_MODE_TABS);
        for (int i = 0; i <= TabStr.length - 1; i++) {
            ActionBar.Tab tab = bar.newTab();
            tab.setText(TabStr[i]);
            tab.setTabListener(this);
            if (TabStr[i].equals("New Post")) {
                tab.setIcon(R.drawable.ic_action_account_child);
            } else if (TabStr[i].equals("Group Chat")) {
                tab.setIcon(R.drawable.ic_communication_forum);

            } else if (TabStr[i].equals("Notification")) {
                tab.setIcon(R.drawable.ic_action_action_view_list);

            } else if (TabStr[i].equals("Friends")) {
                tab.setIcon(R.drawable.ic_action_image_timer_auto);

            }
            bar.addTab(tab);


        }

        //////////////Download Service

           /* Starting Download Service */
        mReceiver = new DownloadResultReceiver(new Handler());
        mReceiver.setReceiver(this);


        /////////////////Download Service
        //
        //
        if (!SLAc.GetPreferenceValue("FirstTime").equals("true")) {
            SLAc.SavePreference("FirstTime", "true");


            // DBHelper db3 = new DBHelper(ctx);  db3.initializeDataBase(); db3.close(); SLAc.SavePreference(".XXXXXXX","426779554151673");SLAc.SavePreference("session_name","Niaz");

            //   DBHelper db2 = new DBHelper(ctx);  db2.initializeDataBase(); db2.close(); SLAc.SavePreference(".XXXXXXX","818904868132997");SLAc.SavePreference("session_name","Nazim Dadan");
            DBHelper db1 = new DBHelper(ctx);
            db1.initializeDataBase();
            db1.close();
            SLAc.SavePreference(".XXXXXXX", "10205304767877899");
            SLAc.SavePreference("session_name", "Nasz Sayed");
        }

        //  DBHelper db1 = new DBHelper(ctx);  db1.initializeDataBase(); db1.close(); SLAc.SavePreference(".XXXXXXX","818904868132997");SLAc.SavePreference("session_name","Nazim Dadan");SLAc.SavePreference("WSChatPHPGetShifaPublicChat","");SLAc.SavePreference("WSChatPHPGetShifaPvtChat","");SLAc.SavePreference("WSChatPHPGetShifaDiscussionOthers","");SLAc.SavePreference("WSChatPHPGetShifaDiscussionChat","");SLAc.SavePreference("WSChatPHPGetShifaMember","");
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
                Intent intent = new Intent(chat.this, ChatImageUpload.class);
                intent.putExtra("uniqueID", uniqueID);
                startActivity(intent);

                //DBHelper db1 = new DBHelper(ctx);  db1.initializeDataBase(); db1.close(); SLAc.SavePreference(".XXXXXXX","818904868132997");SLAc.SavePreference("session_name","Nazim Dadan");SLAc.SavePreference("WSChatPHPGetShifaPublicChat","");
            }
        });
        ShowNeccessaryInputs();


        myList.setTranscriptMode(ListView.TRANSCRIPT_MODE_ALWAYS_SCROLL);
        myContact.setTranscriptMode(ListView.TRANSCRIPT_MODE_ALWAYS_SCROLL);
        TabOpenChats("");


        // myList.setSelection(Integer.valueOf(ChatIndex));


        //  ChatReveiverAsync(ChatIndex, false);
        myContact.setOnItemClickListener(new OnItemClickListener() {

            @SuppressLint("DefaultLocale")
            @Override
            public void onItemClick(AdapterView<?> listView, View view, int position,
                                    long id) {
                ChatMain listItem = (ChatMain) view.findViewById(R.id.id_chat_shifamember_logo).getTag();
                Log.e(TAG, "ScreenMode: " + ScreenMode + " listItem.chatter: " + listItem.id_web);
                if (listItem.chat_type.equals("Friends") || listItem.chat_type.equals("msg")) {
                    Intent intent = new Intent(ctx, chatter.class);
                    intent.putExtra("Mode", "msg");
                    intent.putExtra(".XXXXXXX_to_name", listItem.chatter);
                    intent.putExtra(".XXXXXXX_to", listItem.frm);
                    intent.putExtra("basechat", listItem.chatter + " - " + SLAc.UserName());
                    UpdateMsgDBIRead(listItem.frm);
                    listItem.iRead = 2;
                    startActivity(intent);
                    return;
                }


            }
        });
        myList.setOnItemClickListener(new OnItemClickListener() {
            //wProcess
            @SuppressLint("DefaultLocale")
            @Override
            public void onItemClick(AdapterView<?> listView, View view, int position,
                                    long id) {
                int scrolly =  myList.getFirstVisiblePosition();
                state = scrolly;
                SLAc.SavePreference("myListScroll", state+"");
                Log.e("scrolly ",scrolly+"");
                ChatMain listItem = (ChatMain) view.findViewById(R.id.id_chat_shifamember_logo).getTag();
                Log.e(TAG, "ScreenMode: " + ScreenMode + " listItem.chatter: " + listItem.name_member);
                if (position == myList.getCount() - 1) {
                    RetryTabOpenChats = listItem.id_web;
                    TabOpenChats(listItem.id_web);
                    return;
                } else if (listItem.chat_type.equals("chat")) {
                    Intent intent = new Intent(ctx, chatter.class);
                    intent.putExtra("Mode", listItem.chat_type);
                    startActivity(intent);
                    UpdateDBIRead(listItem.id_web);
                    listItem.iRead = 2;
                    return;
                } else if (listItem.chat_type.equals("disc")) {
                    Intent intent = new Intent(ctx, chatter.class);
                    intent.putExtra("Mode", listItem.chat_type);
                    if (listItem._to.equals("-888")) {
                        intent.putExtra("discid", listItem.id_web);
                        intent.putExtra("basechat", listItem.chat);
                    } else {
                        intent.putExtra("discid", listItem._to);
                        intent.putExtra("basechat", listItem.base_chat);
                    }
                    UpdateDBIRead(listItem.id_web);
                    listItem.iRead = 2;
                    startActivity(intent);
                    return;
                } else if (listItem.chat_type.equals("msg") || listItem.chat_type.equals("Friends")) {
                    Intent intent = new Intent(ctx, chatter.class);
                    intent.putExtra("Mode", "msg");
                    intent.putExtra(".XXXXXXX_to_name", listItem.chatter);
                    intent.putExtra(".XXXXXXX_to", listItem.frm);
                    intent.putExtra("basechat", listItem.chatter + " - " + SLAc.UserName());
                    UpdateMsgDBIRead(listItem.frm);
                    listItem.iRead = 2;
                    startActivity(intent);
                    return;
                } else {
                    // SLAc.SavePreference("DiscId", listItem.id_web);
                    //ScreenMode = CONST_DISCUSSION_CHAT;
                    UpdateDBIRead(listItem.id_web);
                }

               // ShowPage(ScreenMode);

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

        Log.e(TAG1, "Chat Ready...");
    }

    private void GetChats() {

    }

    private boolean listIsAtTop() {
        if (myList.getChildCount() == 0) return true;
        return myList.getChildAt(0).getTop() == 0;
    }

    private void UpdateMsgDBIRead(String frm) {
        String q = "update .XXXXXXXapp_chat  set iRead = 2 where frm = '" + frm + "'";
        Log.e(TAG, "Query: " + q);
        DBHelper db1 = new DBHelper(ctx);
        db1.getWritableDatabase().execSQL(q);

        if (db1 != null) {
            db1.close();
        }
        Log.e(TAG, "Saved IRead: " + frm);
        ShifaDepart.PostSaveMsgIReadChatId(frm);
    }

    private void UpdateDBIRead(String id_web) {
        String q = "update .XXXXXXXapp_chat  set iRead = 2 where id_web = '" + id_web + "'";
        Log.e(TAG, "Query: " + q);
        DBHelper db1 = new DBHelper(ctx);
        db1.getWritableDatabase().execSQL(q);

        if (db1 != null) {
            db1.close();
        }
        Log.e(TAG, "Saved IRead: " + id_web);
        ShifaDepart.PostSaveIReadChatId(id_web);
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


            if (Bottom == false) {
                ThreadChatLookUp(ShifaDepart.CheckNewOrExistingDiscussionOthers_V2(ChatFirstIdWebNumber(), ""), CONST_TABLE_APP_CHAT_MENU);
                //ShifaDepart.CheckNewOrExistingDiscussionOthers(ChatFirstIdWebNumber(),"");
            } else {
                ThreadChatLookUp(ShifaDepart.CheckNewOrExistingDiscussionOthers_V2("", ChatLastIdWebNumber()), CONST_TABLE_APP_CHAT_MENU);
                //ShifaDepart.CheckNewOrExistingDiscussionOthers("", ChatLastIdWebNumber());
            }


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
                PostChatSendFromDatabase();
            }

        }


    }

    @SuppressLint("NewApi")
    @Override
    public boolean onCreateOptionsMenu(Menu menu) {

        getMenuInflater().inflate(R.menu.chat, menu);
        return super.onCreateOptionsMenu(menu);
    }
    int SwitchList = 0;
    public boolean onOptionsItemSelected(MenuItem item) {
        switch (item.getItemId()) {

            case R.id.menu_user_friends:
                if (SwitchList == 0) {
                    SwitchList = 1;
                    myList.setVisibility(View.GONE);
                    myContact.setVisibility(View.VISIBLE);
                }
                else{

                    myList.setVisibility(View.VISIBLE);
                    myContact.setVisibility(View.GONE);
                    SwitchList = 0;
                }
                return true;
            case R.id.menu_user_NewPost:

                Intent intent1 = new Intent(ctx, chat_case_open.class);

                startActivity(intent1);
                return true;
            default:
                return super.onOptionsItemSelected(item);

        }
    }

    private String DataCheckUp() {

        return "?action=New&id_web_new=" + ChatLastIdWebNumber() + "&id_web_old=" + ChatFirstIdWebNumber() + "&_to=" + PublicChatIdCode;
    }

    private void ChatReveiverAsync(String cIndex, boolean bRange) {

        final String ChatIndex = cIndex;
        final boolean bFRange = bRange;
        mHandler = new Handler();
        new Thread(new Runnable() {
            @Override
            public void run() {

                // TODO Auto-generated method stub
                while (chatactive == true) {
                    try {

                        mHandler.post(new Runnable() {
   /* Starting Download Service */


                            @Override
                            public void run() {
                                if (SLAc.GetPreferenceValue("BroadCast_PvtPublic_Chat").equals("RefreshYourSelf") || FirstTimeView == true) {
                                    SLAc.SavePreference("BroadCast_PvtPublic_Chat", "");
                                    //  Log.e(TAG,"BroadCast_PvtPublic_Chat: exeutre");
                                    //ChatOpen("");
                                    // HideProgressBar();
                                    // return;
                                }

                                //ThreadChatLookUp();
                            }
                        });
                        Thread.sleep(Interval);
                    } catch (Exception e) {


                        //Toast.makeText(ctx.getApplicationContext(), e.toString(), 1000).show();
                        // TODO: handle exception
                    }

                }
            }
        }).start();


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
    private String DBDiscMsgLastIDWeb() {
        String q = "select id_web from .XXXXXXXapp_chat where (_to = '-777' or _to = '-888') order by id_web asc limit 0,1";
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

    private String ChatLastIdWebNumber() {
        String q = "select id_web from .XXXXXXXapp_chat where _to = '" + PublicChatIdCode + "' order by id_web desc limit 0,1";
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
    private void TabOpenChats(String clicked_user_first_id_web) {
        LinearLayout ln_progressbar = (LinearLayout) findViewById(R.id.ln_progressbar);
        ln_progressbar.setVisibility(View.VISIBLE);
        String q2 = "";
        String q1 = "";
        String q3 = "";
        String q4 = "";
        String q5 = "";
        String q6 = "";
        boolean Startup = false;
        int PublicID_WEB = 0;
        ShowProgressBar();
        if (clicked_user_first_id_web.equals("")) {
            userArray.clear();
            userArrContact.clear();
            q3 = ".XXXXXXX .XXXXXXXapp_chat where  chat_type = 'msg' and iRead = '1' group by frm order by id_web desc";
            q1 = ".XXXXXXX .XXXXXXXapp_chat where  chat_type = 'chat'   order by id_web desc limit 3";
            q4 = "";
            q2 = ".XXXXXXX .XXXXXXXapp_chat where  chat_type = 'disc'  order by id_web desc limit 100";
            q5 = ".XXXXXXX .XXXXXXXapp_contact   order by  datetime desc limit 500";
            q6 = ".XXXXXXX .XXXXXXXapp_chat where chat_type = 'msg' group by frm   order by  id_web desc limit 500";
            Startup = true;
        } else if (!clicked_user_first_id_web.equals("")) {
            PublicID_WEB = Integer.valueOf(clicked_user_first_id_web);
            q2 = ".XXXXXXX .XXXXXXXapp_chat where id_web <= " + (PublicID_WEB - 1) + " and chat_type = 'disc'  order by id_web desc limit 100 ";

        }

        TabListUpdate(Startup, q1, q2, q3, q4, q5, q6, clicked_user_first_id_web);
        //TabListUpdate(q2);
        HideProgressBar();
    }

    private void TabListUpdate(boolean Startup, String q, String q2, String q3, String q4, String q5, String q6, String clicked_user_first_id_web) {
        DBHelper db1 = new DBHelper(ctx);
        Cursor cursor = null;
        int RCount = 0;
        if (!q3.equals("")) {
            Log.e(TAG2, "Query: " + q3);
            cursor = db1.getReadableDatabase().rawQuery(q3, null);
            RCount += cursor.getCount();
            if (cursor.getCount() == 0) {
                Log.e(TAG, "No Data found so fetching from internet");
                if (cursor != null) {
                    cursor.close();
                }
                if (db1 != null) {
                    db1.close();
                }
                //fetching
            } else {
                TabPassData(cursor, CONST_TABLE_APP_CHAT_MENU, "Notification");
            }
        }
        if (!q.equals("")) {
            Log.e(TAG2, "Query: " + q);
            cursor = db1.getReadableDatabase().rawQuery(q, null);
            RCount += cursor.getCount();
            if (cursor.getCount() == 0) {
                Log.e(TAG, "No Data found so fetching from internet");
                if (cursor != null) {
                    cursor.close();
                }
                if (db1 != null) {
                    db1.close();
                }
                FetchingDataFromInternetForChats(CONST_PUBLIC_CHAT, Startup);
                return;
            }
            TabPassData(cursor, CONST_TABLE_APP_CHAT_MENU, "Notification");

        }
        if (!q4.equals("")) { // Expert list
            Log.e(TAG2, "Query: " + q4);
            cursor = db1.getReadableDatabase().rawQuery(q4, null);
            RCount += cursor.getCount();

            TabPassData(cursor, CONST_TABLE_APP_CONTACT_MENU, "Notification");
            RetryTabOpenChats = "";
        }
        if (!q2.equals("")) {
            Log.e(TAG2, "Query: " + q2);
            cursor = db1.getReadableDatabase().rawQuery(q2, null);
            RCount += cursor.getCount();
            if (cursor.getCount() == 0) {
                Log.e(TAG, "No Data found so fetching from internet");
                if (cursor != null) {
                    cursor.close();
                }
                if (db1 != null) {
                    db1.close();
                }
                if (Startup == false) {
                    OldFetchingDataFromInternetForChats(CONST_TABLE_APP_CHAT_MENU, DBDiscMsgLastIDWeb());
                } else {
                    FetchingDataFromInternetForChats(CONST_TABLE_APP_CHAT_MENU, Startup);
                }
                return;
            }
            TabPassData(cursor, CONST_TABLE_APP_CHAT_MENU, "Notification");
            RetryTabOpenChats = "";
        }
        if (!q6.equals("")) {
            Log.e(TAG2, "Query: " + q6);
            cursor = db1.getReadableDatabase().rawQuery(q6, null);
            RCount += cursor.getCount();
            if (cursor.getCount() == 0) {
                Log.e(TAG, "No Data found so fetching from internet");
                if (cursor != null) {
                    cursor.close();
                }
                if (db1 != null) {
                    db1.close();
                }

                //return;
            } else {
                TabPassData(cursor, CONST_TABLE_APP_CHAT_MENU, "Friends");
            }
        }
        if (!q5.equals("")) {
            Log.e(TAG2, "Query: " + q5);
            cursor = db1.getReadableDatabase().rawQuery(q5, null);
            RCount += cursor.getCount();
            if (cursor.getCount() == 0) {
                Log.e(TAG, "No Data found so fetching from internet");
                if (cursor != null) {
                    cursor.close();
                }
                if (db1 != null) {
                    db1.close();
                }

                // return;
            } else {
                TabPassData(cursor, CONST_TABLE_APP_CONTACT_MENU, "Friends");
            }
        }


        TabListBind(RCount);
        if (cursor != null) {
            cursor.close();

        }
        if (db1 != null) {
            db1.close();
        }

    }

    private void TabPassData(Cursor cursor, String DataType, String ArrayId) {
        cursor.moveToFirst();
        while (!cursor.isAfterLast()) {
            // Logger(cursor);
            if (ArrayId.equals("Notification")) {
                userArray.add(new ChatMain(cursor, DataType));
            } else if (ArrayId.equals("Friends")) {
                userArrContact.add(new ChatMain(cursor, DataType));
            }
            cursor.moveToNext();
        }
    }

    private void TabListBind(int RCount) {
        LinearLayout ln_progressbar = (LinearLayout) findViewById(R.id.ln_progressbar);
        ln_progressbar.setVisibility(View.GONE);
        if (myList.getCount() == 0) {
            ChatAdapter = new ChatAdapterView(this, R.layout.listview_chat_pvtmsg_recent_list, userArray, SessionID);
            myList.setAdapter(ChatAdapter);
        }
        if (myContact.getCount() == 0) {
            ContactAdapter = new ChatAdapterView(this, R.layout.listview_chat_pvtmsg_recent_list, userArrContact, SessionID);
            myContact.setAdapter(ContactAdapter);
        }


        if (ChatAdapter != null) {
            myList.setSelection(ChatAdapter.getCount() - RCount);
        }

        if (userArrContact != null) {
            myContact.setSelection(ContactAdapter.getCount() - RCount);
        }


    }

    @SuppressWarnings("deprecation")
    public void ChatOpen(String clicked_user_first_id_web) {
        DBHelper db1 = new DBHelper(ctx);
        Cursor cursor = null;
        int RCount = 0;
        String q = "";

        int PublicID_WEB = 0;
        if (ScreenMode.equals(CONST_PUBLIC_CHAT)) {
               /* if (ScrollStatus == 1 && preFirstId_web != null) { // scroll is at bottom

                    PublicID_WEB = Integer.valueOf(preFirstId_web);
                    q = "select id,chat,frm,chatter,dt,datetime,id_web,picture,video,id_app,iRead from .XXXXXXXapp_chat where id_web > " + Integer.valueOf(PublicID_WEB)  + " and _to = '" + PublicChatIdCode + "'  order by id_web asc limit 100 ";

                }
                else if (ScrollStatus == -1 && preFirstId_web != null) {
                    PublicID_WEB = Integer.valueOf(preFirstId_web);

                    q = "select id,chat,frm,chatter,dt,datetime,id_web,picture,video,id_app,iRead from .XXXXXXXapp_chat where id_web <= " + (Integer.valueOf(PublicID_WEB) - 1) + " and _to = '" + PublicChatIdCode + "'  order by id_web desc limit 100 ";

                }
                else if (ScrollStatus == 0){
                    PublicID_WEB = Integer.valueOf(ChatLastIdWebNumber());
                    if(myList.getCount() == 0) {
                        q = "select id,chat,frm,chatter,dt,datetime,id_web,picture,video,id_app,iRead from .XXXXXXXapp_chat where  _to = '" + PublicChatIdCode + "'  order by id_web desc limit  40";
                    }

                }
                */
            if (myList.getCount() == 0) {
                q = "select id,chat,frm,chatter,dt,datetime,id_web,picture,video,id_app,iRead,_to,chatter_to,_to,id_app,iRead,.XXXXXXX_to from .XXXXXXXapp_chat where  _to = '-999'  order by id_web asc limit 100";
            } else if (!clicked_user_first_id_web.equals("")) {
                PublicID_WEB = Integer.valueOf(clicked_user_first_id_web);
                q = "select id,chat,frm,chatter,dt,datetime,id_web,picture,video,id_app,iRead,_to,chatter_to,_to,id_app,iRead,.XXXXXXX_to from .XXXXXXXapp_chat where id_web <= " + (PublicID_WEB - 1) + " and _to = '-999'  order by id_web desc limit 100 ";

            }

        } else if (ScreenMode.equals(CONST_DISCUSSION_PERSONAL_POSTED_OVERVIEW_LIST)) {
            q = "select id,chat,frm,chatter,dt,datetime,id_web,picture,video,id_app,iRead,_to,chatter_to,_to,id_app,iRead,.XXXXXXX_to from .XXXXXXXapp_chat where _to = '" + PublicChatIdCode + "' and frm = '" + SLAc.UserSessionId() + "' order by id_web asc ";
        } else if (ScreenMode.equals(CONST_TABLE_APP_CHAT_MENU)) {
            if (myList.getCount() == 0) {
                q = "select id,chat,frm,chatter,dt,datetime,id_web,picture,video,id_app,iRead,_to,chatter_to,_to,id_app,iRead,.XXXXXXX_to from .XXXXXXXapp_chat where   _to = '" + PublicChatIdCode + "'  order by id_web desc limit 100 ";
            } else if (!clicked_user_first_id_web.equals("")) {
                PublicID_WEB = Integer.valueOf(clicked_user_first_id_web);
                q = "select id,chat,frm,chatter,dt,datetime,id_web,picture,video,id_app,iRead,_to,chatter_to,_to,id_app,iRead,.XXXXXXX_to from .XXXXXXXapp_chat where id_web <= " + (PublicID_WEB - 1) + " and _to = '" + PublicChatIdCode + "'  order by id_web desc  limit 100";

            }

        } else if (ScreenMode.equals(CONST_DISCUSSION_CHAT)) {
            q = "select id,chat,frm,chatter,dt,datetime,id_web,picture,video,id_web,.XXXXXXX_to,chatter_to,_to,id_app,iRead from .XXXXXXXapp_chat WHERE _to = '" + SLAc.GetPreferenceValue("DiscId") + "' or id_web = '" + SLAc.GetPreferenceValue("DiscId") + "' order by id_web asc";
        } else if (ScreenMode.equals(CONST_PRIVATE_MESGGING_CHAT)) {
            q = "select id,chat,frm,chatter,dt,datetime,id_web,picture,video,id_web,.XXXXXXX_to,chatter_to,_to,id_app,iRead from .XXXXXXXapp_chat WHERE ((frm = '" + SLAc.GetPreferenceValue("PvtMsgFriendSessionId") + "' and .XXXXXXX_to = '" + SLAc.UserSessionId() + "') or (frm = '" + SLAc.UserSessionId() + "' and  .XXXXXXX_to  = '" + SLAc.GetPreferenceValue("PvtMsgFriendSessionId") + "')) order by id_web asc";
        } else if (ScreenMode.equals(CONST_PRIVATE_CHAT_OVERVIEW_LIST)) {
            q = "select distinct chatter,.XXXXXXX_to,chatter_to,frm,chat,datetime,_to,id_web,id_app,iRead from .XXXXXXXapp_chat where   (.XXXXXXX_to = '" + SLAc.UserSessionId() + "') group by frm order by id_web desc ";

        } else if (ScreenMode.equals(CONST_CONTACT_CHAT)) {
            q = "select  name_member,.XXXXXXX,datetime,location,info,id_web,id_app from .XXXXXXXapp_contact order by datetime desc limit 300";

        } else if (ScreenMode.equals(CONST_TABLE_APP_CONTACT_MENU)) {
            q = "select  name_member,.XXXXXXX,datetime,location,info,id_web,id_app from .XXXXXXXapp_contact where expert = 1 order by datetime desc limit 300";

        }
        if (q.equals("")) {

            return;
        }
        Log.e(TAG, "Query: " + q);
        cursor = db1.getReadableDatabase().rawQuery(q, null);
        Log.e(TAG, "Result Found Count: " + cursor.getCount());
        RCount = cursor.getCount();

        if (RCount == 0) {

            Log.e(TAG, "No Data found so fetching from internet");
            if (cursor != null) {
                cursor.close();
            }
            if (db1 != null) {
                db1.close();
            }
            FetchingDataFromInternetForChats(ScreenMode, false);
            return;
        }
        if (LastQuery.equals(q)) {

            return;
        }
        LastQuery = q;
        // userArray.clear();
        chatactive = true;
        cursor.moveToFirst();
        while (!cursor.isAfterLast()) {

            //Logger(cursor);
            /////////////////////////////Data Show Code //////////////////
            if (ScreenMode.equals(CONST_PUBLIC_CHAT)) {

                if (ScrollStatus == 1) {
                    boolean AppendOrNew = UserArrayAppendOrAddItem(cursor.getString(cursor.getColumnIndex("id_web")), cursor.getString(cursor.getColumnIndex("id_app")));
                    preFirstId_web = cursor.getString(cursor.getColumnIndex("id_web"));

                    if (AppendOrNew == true) {
                        //   userArray.add(new ChatMain(cursor.getString(cursor.getColumnIndex("chat")), cursor.getString(cursor.getColumnIndex("frm")), cursor.getString(cursor.getColumnIndex("chatter")), cursor.getString(cursor.getColumnIndex("id")), cursor.getString(cursor.getColumnIndex("datetime")), cursor.getString(cursor.getColumnIndex("id_web")), cursor.getString(cursor.getColumnIndex("id_app")), cursor.getString(cursor.getColumnIndex("picture"))));
                        ScrollTouchActivated = false;
                        scrollMyListViewToBottom();
                    }

                } else {
                    if (ScrollStatus == 0) {
                        preFirstId_web = cursor.getString(cursor.getColumnIndex("id_web"));
                        //     userArray.add(0,new ChatMain(cursor.getString(cursor.getColumnIndex("chat")), cursor.getString(cursor.getColumnIndex("frm")), cursor.getString(cursor.getColumnIndex("chatter")), cursor.getString(cursor.getColumnIndex("id")), cursor.getString(cursor.getColumnIndex("datetime")), cursor.getString(cursor.getColumnIndex("id_web")), cursor.getString(cursor.getColumnIndex("id_app")), cursor.getString(cursor.getColumnIndex("picture"))));
                    } else {

                        //                               userArray.add(0,new ChatMain(cursor.getString(cursor.getColumnIndex("chat")), cursor.getString(cursor.getColumnIndex("frm")), cursor.getString(cursor.getColumnIndex("chatter")), cursor.getString(cursor.getColumnIndex("id")), cursor.getString(cursor.getColumnIndex("datetime")), cursor.getString(cursor.getColumnIndex("id_web")), cursor.getString(cursor.getColumnIndex("id_app")), cursor.getString(cursor.getColumnIndex("picture"))));
                    }

                }


            } else if (ScreenMode.equals(CONST_PRIVATE_MESGGING_CHAT)) {
                boolean AppendOrNew = UserArrayAppendOrAddItem(cursor.getString(cursor.getColumnIndex("id_web")), cursor.getString(cursor.getColumnIndex("id_app")));
                if (AppendOrNew == true || FirstTimeView == true) {
                    userArray.add(new ChatMain(cursor, CONST_PRIVATE_MESGGING_CHAT));
                    ScrollTouchActivated = false;
                }
                if (AppendOrNew == true && FirstTimeView == false) // if newRecordadded and firsttimeview is false only then execute scroll down
                {
                    myList.setSelection(userAdapter.getCount() - 1);
                }


            } else if (ScreenMode.equals(CONST_DISCUSSION_CHAT)) {
                boolean AppendOrNew = UserArrayAppendOrAddItem(cursor.getString(cursor.getColumnIndex("id_web")), cursor.getString(cursor.getColumnIndex("id_app")));
                if (AppendOrNew == true || FirstTimeView == true) {
                    userArray.add(new ChatMain(cursor, CONST_PRIVATE_MESGGING_CHAT));
                }


            } else if (ScreenMode.equals(CONST_TABLE_APP_CHAT_MENU) || ScreenMode.equals(CONST_DISCUSSION_PERSONAL_POSTED_OVERVIEW_LIST)) {

                userArray.add(new ChatMain(cursor, CONST_TABLE_APP_CHAT_MENU));

            } else if (ScreenMode.equals(CONST_PRIVATE_CHAT_OVERVIEW_LIST)) {
                if (!SLAc.UserSessionId().equals(cursor.getString(cursor.getColumnIndex("frm"))) && FirstTimeView == true) {
                    // not equal user own session id to friend id
                    userArray.add(new ChatMain(cursor, CONST_PRIVATE_CHAT_OVERVIEW_LIST));
                    ListRefresh = true;
                }

            } else if ((ScreenMode.equals(CONST_CONTACT_CHAT) || ScreenMode.equals(CONST_TABLE_APP_CONTACT_MENU))) {
                boolean AppendOrNew = UserArrayAddNewAppendForContacts(cursor.getString(cursor.getColumnIndex(".XXXXXXX")));

                if (AppendOrNew == true) {
                    userArray.add(new ChatMain(cursor, CONST_CONTACT_CHAT));
                    ListRefresh = true;
                }
            }
            cursor.moveToNext();
        }

        if (FirstTimeView == true || ListRefresh == true) {
            FirstTimeView = false;
            ListRefresh = false;
            if (ScreenMode.equals(CONST_PUBLIC_CHAT) || ScreenMode.equals(CONST_PRIVATE_MESGGING_CHAT) || ScreenMode.equals(CONST_DISCUSSION_CHAT)) {

                userAdapter = new ChatAdapter(this, R.layout.listview_chat, userArray, SessionID);
                myList.setAdapter(userAdapter);
                scrollMyListViewToBottom();

            } else if (ScreenMode.equals(CONST_TABLE_APP_CHAT_MENU)) {
                if (myList.getCount() == 0) {
                    ChatAdapter = new ChatAdapterView(this, R.layout.listview_chat_pvtmsg_recent_list, userArray, SessionID);
                    myList.setAdapter(ChatAdapter);
                }

            } else if (ScreenMode.equals(CONST_PRIVATE_CHAT_OVERVIEW_LIST) || ScreenMode.equals(CONST_DISCUSSION_PERSONAL_POSTED_OVERVIEW_LIST)) {
                ChatAdapter = new ChatAdapterView(this, R.layout.listview_chat_pvtmsg_recent_list, userArray, SessionID);
                myList.setAdapter(ChatAdapter);

            } else if (ScreenMode.equals(CONST_CONTACT_CHAT) || ScreenMode.equals(CONST_TABLE_APP_CONTACT_MENU)) {


            }


        }

        if (userAdapter != null) {
            myList.setSelection(userAdapter.getCount() - RCount);
        } else if (ChatAdapter != null) {
            myList.setSelection(ChatAdapter.getCount() - RCount);
        }


        if (cursor != null) {
            cursor.close();

        }
        if (db1 != null) {
            db1.close();
        }


    }

    private void Logger(Cursor cursor) {
        if (ScreenMode.equals(CONST_PRIVATE_MESGGING_CHAT)) {
            Log.e(TAG, CONST_PRIVATE_MESGGING_CHAT + " progress " + cursor.getString(cursor.getColumnIndex("id_web")) + " ChatIndex: " + ChatIndex);

            Log.e(TAG, "Chat " + cursor.getString(cursor.getColumnIndex("chat")));

            Log.e(TAG, "frm " + cursor.getString(cursor.getColumnIndex("frm")));
            Log.e(TAG, "chatter " + cursor.getString(cursor.getColumnIndex("chatter")));
            Log.e(TAG, "id " + cursor.getString(cursor.getColumnIndex("id")));
            Log.e(TAG, "datetime " + cursor.getString(cursor.getColumnIndex("datetime")));

            Log.e(TAG, ".XXXXXXX_to " + cursor.getString(cursor.getColumnIndex(".XXXXXXX_to")));
            Log.e(TAG, "chatter_to " + cursor.getString(cursor.getColumnIndex("chatter_to")));
            Log.e(TAG, "id_app " + cursor.getString(cursor.getColumnIndex("id_app")));

            Log.e(TAG, "--------------------------------------------------------");

        } else if (ScreenMode.equals(CONST_DISCUSSION_CHAT)) {
            Log.e(TAG, CONST_DISCUSSION_CHAT + " progress " + cursor.getString(cursor.getColumnIndex("id_web")));

            Log.e(TAG, "Chat " + cursor.getString(cursor.getColumnIndex("chat")));

            Log.e(TAG, "_to " + cursor.getString(cursor.getColumnIndex("_to")));
            Log.e(TAG, "frm " + cursor.getString(cursor.getColumnIndex("frm")));
            Log.e(TAG, "chatter " + cursor.getString(cursor.getColumnIndex("chatter")));
            Log.e(TAG, "id " + cursor.getString(cursor.getColumnIndex("id")));
            Log.e(TAG, "datetime " + cursor.getString(cursor.getColumnIndex("datetime")));

            Log.e(TAG, ".XXXXXXX_to " + cursor.getString(cursor.getColumnIndex(".XXXXXXX_to")));
            Log.e(TAG, "chatter_to " + cursor.getString(cursor.getColumnIndex("chatter_to")));
            Log.e(TAG, "id_app " + cursor.getString(cursor.getColumnIndex("id_app")));

            Log.e(TAG, "--------------------------------------------------------");

        } else if (ScreenMode.equals(CONST_PUBLIC_CHAT)) {
            Log.e(TAG, "progress " + ScreenMode + " " + cursor.getString(cursor.getColumnIndex("id_web")) + " ChatIndex: " + ChatIndex);

            Log.e(TAG, "Chat " + cursor.getString(cursor.getColumnIndex("chat")));

            Log.e(TAG, "frm " + cursor.getString(cursor.getColumnIndex("frm")));
            Log.e(TAG, "chatter " + cursor.getString(cursor.getColumnIndex("chatter")));
            Log.e(TAG, "id " + cursor.getString(cursor.getColumnIndex("id")));
            Log.e(TAG, "id_app " + cursor.getString(cursor.getColumnIndex("id_app")));
            Log.e(TAG, "datetime " + cursor.getString(cursor.getColumnIndex("datetime")));
            Log.e(TAG, "--------------------------------------------------------");
        } else if (ScreenMode.equals(CONST_TABLE_APP_CHAT_MENU) || ScreenMode.equals(CONST_DISCUSSION_PERSONAL_POSTED_OVERVIEW_LIST)) {

            Log.e(TAG, "_to " + cursor.getString(cursor.getColumnIndex("_to")));
            Log.e(TAG, "chatter(Sender Name) " + cursor.getString(cursor.getColumnIndex("chatter")));
            Log.e(TAG, ".XXXXXXX_to(Sender ID) " + cursor.getString(cursor.getColumnIndex("frm")));
            Log.e(TAG, "chatter_to(Rec Name) " + cursor.getString(cursor.getColumnIndex("chatter_to")));
            Log.e(TAG, "frm(Rec Id) " + cursor.getString(cursor.getColumnIndex(".XXXXXXX_to")));
            Log.e(TAG, "Message: " + cursor.getString(cursor.getColumnIndex("chat")));
            Log.e(TAG, "id_app " + cursor.getString(cursor.getColumnIndex("id_app")));
            Log.e(TAG, "datetime: " + cursor.getString(cursor.getColumnIndex("datetime")));
            Log.e(TAG, "--------------------------------------------------------");


        } else if (ScreenMode.equals(CONST_PRIVATE_CHAT_OVERVIEW_LIST)) {

            Log.e(TAG, "chatter(Sender Name) " + cursor.getString(cursor.getColumnIndex("chatter")));
            Log.e(TAG, ".XXXXXXX_to(Sender ID) " + cursor.getString(cursor.getColumnIndex("frm")));
            Log.e(TAG, "chatter_to(Rec Name) " + cursor.getString(cursor.getColumnIndex("chatter_to")));
            Log.e(TAG, "frm(Rec Id) " + cursor.getString(cursor.getColumnIndex(".XXXXXXX_to")));
            Log.e(TAG, "Message: " + cursor.getString(cursor.getColumnIndex("chat")));
            Log.e(TAG, "id_app " + cursor.getString(cursor.getColumnIndex("id_app")));
            Log.e(TAG, "datetime: " + cursor.getString(cursor.getColumnIndex("datetime")));
            Log.e(TAG, "--------------------------------------------------------");


        } else if (ScreenMode.equals(CONST_CONTACT_CHAT) || ScreenMode.equals(CONST_TABLE_APP_CONTACT_MENU)) {

            Log.e(TAG, "name_member: " + cursor.getString(cursor.getColumnIndex("name_member")));
            Log.e(TAG, ".XXXXXXX: " + cursor.getString(cursor.getColumnIndex(".XXXXXXX")));

            Log.e(TAG, "location: " + cursor.getString(cursor.getColumnIndex("location")));
            Log.e(TAG, "datetime: " + cursor.getString(cursor.getColumnIndex("datetime")));
            Log.e(TAG, "info: " + cursor.getString(cursor.getColumnIndex("info")));
            Log.e(TAG, "--------------------------------------------------------");

        }
    }

    @Override
    public void onRestoreInstanceState(Bundle savedInstanceState) {
        final int listIndex = savedInstanceState.getInt("ListIndex");

    }

    private void HideProgressBar() {
        // Log.e(TAG,"HideProgressBar.... " );

        //  HideLoadingtext();
        if (barProgressDialog != null) {
            Log.e(TAG, "HideProgressBar is not null.... ");
            barProgressDialog.dismiss();
            barProgressDialog = null;
        }
    }

    private void HideLoadingtext() {

    }

    private void ShowLoadingtext() {

    }

    private void scrollMyListViewToBottom() {

       /* myList.post(new Runnable() {
            @Override
            public void run() {
                // Select the last row so it will scroll into view...
                if (userAdapter != null) {
              //      Log.e(TAG, "Scroll My ListView discussion");
                    myList.setSelection(userAdapter.getCount() - 1);
                    myList.smoothScrollToPosition(userAdapter.getCount() - 1);
                }
            }
        });*/

    }

    private void scrollMyListViewToBottom_Discussion() {
        /*myList.post(new Runnable() {
            @Override
            public void run() {
                // Select the last row so it will scroll into view...
               // Log.e(TAG,"Scroll My ListView discussion");
                if (ChatAdapter != null) {
                    myList.setSelection(ChatAdapter.getCount() - 1);
                    myList.smoothScrollToPosition(ChatAdapter.getCount() - 1);
                }
            }
        });*/
    }

    private boolean UserArrayAppendOrAddItem(String db_id_web, String db_id_app) {


        if (db_id_web == null || FirstTimeView == true) return false;
        Iterator<ChatMain> itr = userArray.iterator();

        while (itr.hasNext()) {
            ChatMain item = itr.next();
            //
            if (item.id_app != null && item.id_app.equals(db_id_app)) {
                Log.e(TAG, "itr: " + item.id_web + " == " + db_id_web);
                Log.e(TAG, "itr: " + item.id_app + " == " + db_id_app);
                item.id_web = db_id_web;
                Log.e(TAG, "itr: " + item.id_web);
                userAdapter.notifyDataSetChanged();
                return false;
            }
            if (item.id_web != null && item.id_web.equals(db_id_web)) {
                return false;
            }
        }
        Log.e(TAG, "itr: New Item found: " + db_id_web);
        return true;
    }

    private boolean UserArrayAddNewAppendForContacts(String db_.XXXXXXX) {


        //    if (db_id_web == null) return true;
        Iterator<ChatMain> itr = userArray.iterator();

        while (itr.hasNext()) {
            ChatMain item = itr.next();
            //
            Log.e(TAG, "item.frm:  " + item.frm);
            Log.e(TAG, "db_.XXXXXXX:  " + db_.XXXXXXX);
            if (item.frm.equals(db_.XXXXXXX)) {
                return false;
            }
        }
        Log.e(TAG, "itr: New Item found in addnew append: " + db_.XXXXXXX);
        return true;
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


            if (ScreenMode.equals(CONST_PUBLIC_CHAT)) {
                ShifaDepart.PostChatSend(SLAc.GetPreferenceValue("ImageChatSend"), PublicChatIdCode, SLAc.GetPreferenceValue("ImagePath"), "", SLAc.GetPreferenceValue("Imageid_app"), "", "", SLAc.GetPreferenceValue("BaseChat"));
                userArray.add(new ChatMain(SLAc.GetPreferenceValue("ImageChatSend"), SLAc.UserSessionId(), SLAc.UserName(), null, strDate, null, SLAc.GetPreferenceValue("Imageid_app"), SLAc.GetPreferenceValue("ImagePath"), "chat"));
            } else if (ScreenMode.equals(CONST_PRIVATE_MESGGING_CHAT)) {
                ShifaDepart.PostChatSend(SLAc.GetPreferenceValue("ImageChatSend"), PublicChatIdCode, SLAc.GetPreferenceValue("ImagePath"), "", SLAc.GetPreferenceValue("Imageid_app"), SLAc.GetPreferenceValue("PvtMsgFriendSessionId"), SLAc.GetPreferenceValue("PvtMsgFriendSessionName"), SLAc.GetPreferenceValue("BaseChat"));
                SLAc.SavePreference("BroadCast_LoadMsg_Hide", "DOAction");
                SLAc.SavePreference("BroadCast_PvtPublic_Chat", "RefreshYourSelf");

                userArray.add(new ChatMain(SLAc.GetPreferenceValue("ImageChatSend"), SLAc.UserSessionId(), SLAc.UserName(), null, strDate, null, SLAc.GetPreferenceValue("Imageid_app"), SLAc.GetPreferenceValue("ImagePath"), "msg"));

            } else if (ScreenMode.equals(CONST_DISCUSSION_CHAT)) {
                ShifaDepart.PostChatSend(SLAc.GetPreferenceValue("ImageChatSend"), SLAc.GetPreferenceValue("DiscId"), SLAc.GetPreferenceValue("ImagePath"), "", SLAc.GetPreferenceValue("Imageid_app"), SLAc.GetPreferenceValue("PvtMsgFriendSessionId"), SLAc.GetPreferenceValue("PvtMsgFriendSessionName"), SLAc.GetPreferenceValue("BaseChat"));
                SLAc.SavePreference("BroadCast_LoadMsg_Hide", "DOAction");
                SLAc.SavePreference("BroadCast_PvtPublic_Chat", "RefreshYourSelf");
                userArray.add(new ChatMain(SLAc.GetPreferenceValue("ImageChatSend"), SLAc.UserSessionId(), SLAc.UserName(), null, strDate, null, SLAc.GetPreferenceValue("Imageid_app"), SLAc.GetPreferenceValue("ImagePath"), "disc"));

            }

        }
        try {
            ChatReveiverAsync(ChatIndex, false);
            myList.setSelection(Integer.valueOf(SLAc.GetPreferenceValue("myListScroll")));
          }
        catch(Exception ex){

        }
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


        if (ScreenMode.equals(CONST_PUBLIC_CHAT)) {
            //ShifaDepart.PostChatSend(ChatTextSend, PublicChatIdCode, "", "", id_app, "", "");
            scrollMyListViewToBottom();
        } else if (ScreenMode.equals(CONST_PRIVATE_MESGGING_CHAT)) {
            //ShifaDepart.PostChatSend(ChatTextSend, PublicChatIdCode, "", "", id_app, SLAc.GetPreferenceValue("PvtMsgFriendSessionId"), SLAc.GetPreferenceValue("PvtMsgFriendSessionName"));
            scrollMyListViewToBottom();

        } else if (ScreenMode.equals(CONST_DISCUSSION_CHAT)) {
            //   ShifaDepart.PostChatSend(ChatTextSend, SLAc.GetPreferenceValue("DiscId"), "", "", id_app, SLAc.GetPreferenceValue("PvtMsgFriendSessionId"), SLAc.GetPreferenceValue("PvtMsgFriendSessionName"));
            scrollMyListViewToBottom_Discussion();

        }
        //   myList.setSelection(userAdapter.getCount() - 1);
        SLAc.SavePreference("CheckPendingMsgstoSend", "true");
        ScrollTouchActivated = false;


    }


}
