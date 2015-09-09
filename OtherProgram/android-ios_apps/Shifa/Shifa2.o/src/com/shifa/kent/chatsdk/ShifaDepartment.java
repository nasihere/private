package com.shifa.kent.chatsdk;

import android.app.Activity;
import android.content.Context;
import android.util.Log;

import org.apache.http.NameValuePair;
import org.apache.http.message.BasicNameValuePair;

import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.List;
import java.util.TimeZone;

/**
 * Created by Nasz on 3/20/15.
 */
public class ShifaDepartment {


    protected static String WebService_GetPaidStatus = "http://kent..XXXXXXX/app_php/Shifa4o/WebService/WSPayment.php?action=GetPaidAccountStatus&.XXXXXXX=";//8419562625169491
    protected static String WebService_SetPaidStatus = "http://kent..XXXXXXX/app_php/Shifa4o/WebService/WSPayment.php?action=SetPaidAccount&.XXXXXXX=";//8419562625169491
    protected static String WebService_SetCounterBuyNow = "http://kent..XXXXXXX/app_php/Shifa4o/WebService/WSPayment.php?action=SetCounterBuyNow&.XXXXXXX=";//8419562625169491
    protected static String WebService_WSLoginPHP_GetLoginDetails = "http://kent..XXXXXXX/app_php/Shifa4o/WebService/WSLogin.php?action=GetLoginDetails";//8419562625169491
    protected static String WebService_WSChatPHP_GetShifaMember = "http://kent..XXXXXXX/app_php/Shifa4o/WebService/WSChat.php?action=GetShifaMember";//8419562625169491
    protected static String WebService_WSChatPHP_GetShifaHomeopathyExpertMemberList = "http://kent..XXXXXXX/app_php/Shifa4o/WebService/WSChat.php?action=GetShifaHomeopathyExpertMemberList";//8419562625169491
    protected static String WebService_WSChatPHP_GetShifaPublicChat = "http://kent..XXXXXXX/app_php/Shifa4o/WebService/WSChat.php?action=GetShifaPublicChat";//8419562625169491
    protected static String WebService_WSChatPHP_CheckNewChat = "http://kent..XXXXXXX/app_php/Shifa4o/WebService/WSChat.php?action=New";//8419562625169491
    protected static String WebService_WSChatPHP_GetShifaDiscussionChat = "http://kent..XXXXXXX/app_php/Shifa4o/WebService/WSChat.php?action=GetShifaDiscussionChat";//8419562625169491
    protected static String WebService_WSChatPHP_GetShifaDiscussionBetweenUsers = "http://kent..XXXXXXX/app_php/Shifa4o/WebService/WSChat.php?action=GetShifaDiscussionBetweenUsers";//8419562625169491
    protected static String WebService_WSChatPHP_GetShifaDiscussionOthers = "http://kent..XXXXXXX/app_php/Shifa4o/WebService/WSChat.php?action=GetShifaDiscussionOthers";//8419562625169491
    protected static String WebService_WSChatPHP_PostShifaPvtPublicChat = "http://kent..XXXXXXX/app_php/Shifa4o/WebService/WSChat.php?action=PostShifaPvtPublicChat";//8419562625169491
    protected static String WebService_WSChatPHP_GetPvtMsgBetweenUsers = "http://kent..XXXXXXX/app_php/Shifa4o/WebService/WSChat.php?action=GetPvtMsgBetweenUsers";//8419562625169491
    protected static String WebService_WSChatPHP_GetShifaPvtChat = "http://kent..XXXXXXX/app_php/Shifa4o/WebService/WSChat.php?action=GetShifaPvtChat";//8419562625169491
    protected static String WebService_WSChatPHP_SetSaveIReadChatId = "http://kent..XXXXXXX/app_php/Shifa4o/WebService/WSChat.php?action=SaveIReadChatId";//8419562625169491
    protected static String WebService_WSChatPHP_SetSaveMsgIReadChatId = "http://kent..XXXXXXX/app_php/Shifa4o/WebService/WSChat.php?action=SaveIReadMsgChatId";//8419562625169491

    protected static String WebService_WSChatPHP_SetSettings = "http://kent..XXXXXXX/app_php/Shifa4o/WebService/WSChat.php?action=Setttings";

    protected static String WebService_WSChatPHP_GetSettingsContactInfo = "http://kent..XXXXXXX/app_php/Shifa4o/WebService/WSChat.php?action=Setttings_other";
    Context ctx;
    Super_Library_AppClass SLAc;
    String SessionID = "";

    public ShifaDepartment(Context ctx) {
        this.ctx = ctx;
        SLAc = new Super_Library_AppClass(ctx);
        SessionID = SLAc.UserSessionId();
        if (SessionID.equalsIgnoreCase("")) return;
        if (SLAc.NewDay() == true) {
            //      AskAppToCheckUserPaidAccount();
            //     CheckRealPaidUserStatusUpdatedInShifaServer();
            //  SLAc.SavePreference("WSChatPHPGetShifaPublicChat", ""); // Remove this line.... after development
            //  CheckNeworExistingMemberInformationAndSaveInDatabaseForGroupChat();
            //  CheckNewOrExistingPublicChatAvaiable();
        }
    }


    protected void AskAppToCheckUserPaidAccount() {
        // Ask App to check current user is paid or not paid if paid then set PaidUser flag as true so we will remove buynow feature from homemenu and give them full version of the app. otherwise restrict features.. response expection are 1 as paid 0 as non paid
        List<NameValuePair> nameValuePairs = new ArrayList<NameValuePair>(2);
        nameValuePairs.add(new BasicNameValuePair(".XXXXXXX", SLAc.UserSessionId()));
        Super_Library_URLV2 SLU2 = new Super_Library_URLV2(WebService_GetPaidStatus, nameValuePairs, ((Activity) ctx), "SetUserAccountAsPaid");

    }


    protected void GetLoginToHomeScreen(String email, String password) {
        // Get Email id and password from UI screen and check the sessionid is availbale in databasew if yes then set it to app property
        // Get Login Information and do neccessary action based on user information given in login screen
        List<NameValuePair> nameValuePairs = new ArrayList<NameValuePair>(2);
        nameValuePairs.add(new BasicNameValuePair("email", email));
        nameValuePairs.add(new BasicNameValuePair("password", password));

        SLAc.SavePreference("UserEmail", email);
        Super_Library_URLV2 SLU2 = new Super_Library_URLV2(WebService_WSLoginPHP_GetLoginDetails, nameValuePairs, ((Activity) ctx), "GetLoginDetails");

    }


    protected void SetSettings(String Name, String Email, String City, String Country, String Info, String Occupation, boolean Push_notification, boolean Email_Notification) {
        //This function will update user information profile
        List<NameValuePair> nameValuePairs = new ArrayList<NameValuePair>(2);
        nameValuePairs.add(new BasicNameValuePair(".XXXXXXX", SLAc.UserSessionId()));
        nameValuePairs.add(new BasicNameValuePair("name", Name));
        nameValuePairs.add(new BasicNameValuePair("email", Email));
        nameValuePairs.add(new BasicNameValuePair("city", City));
        nameValuePairs.add(new BasicNameValuePair("country", Country));
        nameValuePairs.add(new BasicNameValuePair("info", Info));
        nameValuePairs.add(new BasicNameValuePair("occupation", Occupation));
        if (Push_notification == true) {
            nameValuePairs.add(new BasicNameValuePair("push_notification_flag", "1"));
            SLAc.SavePreference(SLAc.CONST_USER_PUSH_NOTIFICATION, "1");
        } else {
            nameValuePairs.add(new BasicNameValuePair("push_notification_flag", "0"));
            SLAc.SavePreference(SLAc.CONST_USER_PUSH_NOTIFICATION, "0");
        }
        if (Email_Notification == true) {
            nameValuePairs.add(new BasicNameValuePair("email_notification_flag", "1"));
            SLAc.SavePreference(SLAc.CONST_USER_EMAIL_NOTIFICATION, "1");
        } else {
            nameValuePairs.add(new BasicNameValuePair("email_notification_flag", "0"));
            SLAc.SavePreference(SLAc.CONST_USER_EMAIL_NOTIFICATION, "0");
        }

        SLAc.SavePreference(SLAc.CONST_USER_NAME, Name);
        SLAc.SavePreference(SLAc.CONST_USER_CITY, City);
        SLAc.SavePreference(SLAc.CONST_USER_COUNTRY, Country);
        SLAc.SavePreference(SLAc.CONST_USER_EMAIL, Email);
        SLAc.SavePreference(SLAc.CONST_USER_INFO, Info);
        SLAc.SavePreference(SLAc.CONST_USER_OCCUPATION, Occupation);
        Super_Library_URLV2 SLU2 = new Super_Library_URLV2(WebService_WSChatPHP_SetSettings, nameValuePairs, ((Activity) ctx), "WebService_WSChatPHP_SetSettings");

    }

    protected void CheckRealPaidUserStatusUpdatedInShifaServer() {
        //Will check here real user paid by google but for some reason shifa server not updated real user account mark as paid so for that reason we set again request to server to mark as paid
        if (SLAc.GetPreferenceValue("PaidUserServer").equalsIgnoreCase("false")) {
            List<NameValuePair> nameValuePairs = new ArrayList<NameValuePair>(2);
            nameValuePairs.add(new BasicNameValuePair(".XXXXXXX", SLAc.UserSessionId()));

            Super_Library_URLV2 SLU2 = new Super_Library_URLV2(WebService_SetPaidStatus, nameValuePairs, ((Activity) ctx), "SetPaidAccount");
        }
    }


    protected String CheckNewOrExistingDiscussionOthers_V2(String before_id_web, String after_id_web) {
        //List<NameValuePair> nameValuePairs = new ArrayList<NameValuePair>(2);
        //nameValuePairs.add(new BasicNameValuePair(".XXXXXXX", SLAc.UserSessionId()));
        //nameValuePairs.add(new BasicNameValuePair("before_id_web", before_id_web));

        //  nameValuePairs.add(new BasicNameValuePair("after_id_web", after_id_web));
        return WebService_WSChatPHP_GetShifaDiscussionOthers + "&before_id_web=" + before_id_web + "&after_id_web=" + after_id_web + "&.XXXXXXX=" + SLAc.UserSessionId();
        // Super_Library_URLV2 SLU2 = new Super_Library_URLV2(WebService_WSChatPHP_GetShifaDiscussionOthers,nameValuePairs, ((Activity) ctx), "WSChatPHPGetShifaDiscussionOthers");
    }

    protected String CheckNewOrExistingDiscussionBetweenUsers(String _to) {
        //List<NameValuePair> nameValuePairs = new ArrayList<NameValuePair>(2);
        //nameValuePairs.add(new BasicNameValuePair("id_web", id_web));
        //Super_Library_URLV2 SLU2 = new Super_Library_URLV2(WebService_WSChatPHP_GetShifaDiscussionBetweenUsers,nameValuePairs, ((Activity) ctx), "WSChatPHPGetShifaDiscussionBetweenUsers");


        return WebService_WSChatPHP_GetShifaDiscussionBetweenUsers + "&_to=" + _to;
    }

    protected void CheckNewOrExistingDisucssionChat() {

        List<NameValuePair> nameValuePairs = new ArrayList<NameValuePair>(2);
        nameValuePairs.add(new BasicNameValuePair(".XXXXXXX", SLAc.UserSessionId()));
        Super_Library_URLV2 SLU2 = new Super_Library_URLV2(WebService_WSChatPHP_GetShifaDiscussionChat, nameValuePairs, ((Activity) ctx), "WSChatPHPGetShifaDiscussionChat");

    }


    protected void CheckNeworExistingMemberInformationAndSaveInDatabaseForGroupChat() {
        //Everyday it will update Member Details and status and add new member information in public chat
        List<NameValuePair> nameValuePairs = new ArrayList<NameValuePair>(2);
        nameValuePairs.add(new BasicNameValuePair(".XXXXXXX", SLAc.UserSessionId()));
        Super_Library_URLV2 SLU2 = new Super_Library_URLV2(WebService_WSChatPHP_GetShifaMember, nameValuePairs, ((Activity) ctx), "WSChatPHPGetShifaMember");

    }

    protected void PostSaveMsgIReadChatId(String frm) {
        //Everyday it will update Member Details and status and add new member information in public chat
        List<NameValuePair> nameValuePairs = new ArrayList<NameValuePair>(2);
        nameValuePairs.add(new BasicNameValuePair("_frm", frm));
        Super_Library_URLV2 SLU2 = new Super_Library_URLV2(WebService_WSChatPHP_SetSaveMsgIReadChatId, nameValuePairs, ((Activity) ctx), "");

    }

    protected void PostSaveIReadChatId(String id_web) {
        //Everyday it will update Member Details and status and add new member information in public chat
        List<NameValuePair> nameValuePairs = new ArrayList<NameValuePair>(2);
        nameValuePairs.add(new BasicNameValuePair("id_web", id_web));
        Super_Library_URLV2 SLU2 = new Super_Library_URLV2(WebService_WSChatPHP_SetSaveIReadChatId, nameValuePairs, ((Activity) ctx), "");

    }

    protected String CheckContacts(String After_id_web) {
        //Everyday it will update Member Details and status and add new member information in public chat
     /*   List<NameValuePair> nameValuePairs = new ArrayList<NameValuePair>(2);
        nameValuePairs.add(new BasicNameValuePair(".XXXXXXX", SLAc.UserSessionId()));
        Super_Library_URLV2 SLU2 = new Super_Library_URLV2(WebService_WSChatPHP_GetShifaHomeopathyExpertMemberList,nameValuePairs, ((Activity) ctx), "WSChatPHPGetShifaMember");
        */
        return WebService_WSChatPHP_GetShifaHomeopathyExpertMemberList + "&public_id_web=" + After_id_web;
    }

    protected void AnyNewChat(String id_web, String _to) {
        //This function used to check the before chat is available for this public chat
        List<NameValuePair> nameValuePairs = new ArrayList<NameValuePair>(2);
        if (id_web == null) return;
        nameValuePairs.add(new BasicNameValuePair("id_web", id_web));
        Log.e("ChatUI", "id_web for checking new record " + id_web);
        nameValuePairs.add(new BasicNameValuePair("_to", _to));
        Super_Library_URLV2 SLU2 = new Super_Library_URLV2(WebService_WSChatPHP_CheckNewChat, nameValuePairs, ((Activity) ctx), "WSChatPHPAnyNewChat");

    }


    protected String CheckExistingPublicChatAvailable(String before_id_web, String After_id_web) {
        //This function used to check the before chat is available for this public chat
      /*  List<NameValuePair> nameValuePairs = new ArrayList<NameValuePair>(2);
        if (before_id_web == null) before_id_web = "";
        nameValuePairs.add(new BasicNameValuePair("before_id_web", before_id_web));
        Super_Library_URLV2 SLU2 = new Super_Library_URLV2(WebService_WSChatPHP_GetShifaPublicChat,nameValuePairs, ((Activity) ctx), "WSChatPHPGetShifaPublicChat");
        */
        return WebService_WSChatPHP_GetShifaPublicChat + "&before_id_web=" + before_id_web + "&public_id_web=" + After_id_web;


    }

    protected void GetFriendContactInfo(String Session_id_to) {
        //This function will fetched all the friends information and show to the user
        List<NameValuePair> nameValuePairs = new ArrayList<NameValuePair>(2);
        nameValuePairs.add(new BasicNameValuePair(".XXXXXXX", SLAc.UserSessionId()));
        nameValuePairs.add(new BasicNameValuePair(".XXXXXXX_to", Session_id_to));
        Super_Library_URLV2 SLU2 = new Super_Library_URLV2(WebService_WSChatPHP_GetSettingsContactInfo, nameValuePairs, ((Activity) ctx), "WSChatPHPGetSettingsContactInfo");

    }

    protected String CheckNewOrExistingPublicChatAvaiable(String id_web) {
        //This function will pull all the private and public chat and set top most id_web in app property for new records
     /*   List<NameValuePair> nameValuePairs = new ArrayList<NameValuePair>(2);
        nameValuePairs.add(new BasicNameValuePair(".XXXXXXX", SLAc.UserSessionId()));
        nameValuePairs.add(new BasicNameValuePair("public_id_web", id_web));
        Super_Library_URLV2 SLU2 = new Super_Library_URLV2(WebService_WSChatPHP_GetShifaPublicChat,nameValuePairs, ((Activity) ctx), "WSChatPHPGetShifaPublicChat");
*/
        return "";
    }

    protected String CheckNewOrExistingPvtChatAvaiable(String Session_id, String after_id_web) {
        //This function will pull all the private and public chat and set top most id_web in app property for new records
   /*     List<NameValuePair> nameValuePairs = new ArrayList<NameValuePair>(2);
        nameValuePairs.add(new BasicNameValuePair(".XXXXXXX", Session_id));
    //    nameValuePairs.add(new BasicNameValuePair("public_id_web", SLAc.GetPreferenceValue("public_id_web")));
        Log.e("ShifaDepart","Sessionid:"+Session_id);
        Super_Library_URLV2 SLU2 = new Super_Library_URLV2(WebService_WSChatPHP_GetShifaPvtChat,nameValuePairs, ((Activity) ctx), "WSChatPHPGetShifaPvtChat");

    */
        return WebService_WSChatPHP_GetShifaPvtChat + "&.XXXXXXX=" + Session_id + "&after_id_web=" + after_id_web;

    }

    protected String CheckPvtMsgBetweenTwoUser(String Session_id, String Session_id_to, String Public_id_web) {
        //This function will take user Session id and afriends session id to check internet and fetched all the records...
      /*  List<NameValuePair> nameValuePairs = new ArrayList<NameValuePair>(2);
        nameValuePairs.add(new BasicNameValuePair(".XXXXXXX", Session_id)); //SLAc.UserSessionId()));
        nameValuePairs.add(new BasicNameValuePair(".XXXXXXX_to", Session_id_to)); //SLAc.GetPreferenceValue("PvtMsgFriendSessionId")));
        nameValuePairs.add(new BasicNameValuePair("public_id_web", Public_id_web));
        Log.e("ShifaParam","public_id_web " + Public_id_web);
        Log.e("ShifaParam",".XXXXXXX " + Session_id);
        Log.e("ShifaParam",".XXXXXXX_to " + Session_id_to);
        Super_Library_URLV2 SLU2 = new Super_Library_URLV2(WebService_WSChatPHP_GetPvtMsgBetweenUsers,nameValuePairs, ((Activity) ctx), "WSChatPHPGetPvtMsgBetweenUsers");
        */
        return WebService_WSChatPHP_GetPvtMsgBetweenUsers + "&.XXXXXXX=" + Session_id + "&.XXXXXXX_to=" + Session_id_to + "&public_id_web" + Public_id_web;
    }


    protected void PostChatSend(String Chat, String PrivatePublicCode, String picture, String Video, String id_app, String SessionIdTo, String ChatterToName, String BaseChat) {
        //This function will take minimual value of chat and  put on server also update in database for offline purpose.. user dont have to wait to see his message went or not
        //this function used to upload image in shifa websetting ....
        // this will upload in three folder one is chat_img, normal , and thumbnail...
        Calendar c = Calendar.getInstance();
        SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
        String strDate = sdf.format(c.getTime());
        TimeZone tz = TimeZone.getDefault();


        List<NameValuePair> nameValuePairs = new ArrayList<NameValuePair>(2);
        nameValuePairs.add(new BasicNameValuePair("_frm", SLAc.UserSessionId()));
        nameValuePairs.add(new BasicNameValuePair("chat", Chat));
        nameValuePairs.add(new BasicNameValuePair("chatter", SLAc.UserName()));
        nameValuePairs.add(new BasicNameValuePair("to", PrivatePublicCode));
        nameValuePairs.add(new BasicNameValuePair("picture", picture));
        nameValuePairs.add(new BasicNameValuePair("video", Video));
        nameValuePairs.add(new BasicNameValuePair("id_app", id_app));
        nameValuePairs.add(new BasicNameValuePair(".XXXXXXX_to", SessionIdTo));
        nameValuePairs.add(new BasicNameValuePair("chatter_to", ChatterToName));
        nameValuePairs.add(new BasicNameValuePair("timezone", tz.getID()));
        nameValuePairs.add(new BasicNameValuePair("datetime", strDate));
        nameValuePairs.add(new BasicNameValuePair("base_chat", BaseChat));


        String TempResponseData = "id_app#=#" + id_app + "#-#";
        TempResponseData += "chatter#=#" + SLAc.UserName() + "#-#";
        TempResponseData += "frm#=#" + SLAc.UserSessionId() + "#-#";
        TempResponseData += "chat#=#" + Chat + "#-#";
        TempResponseData += "_to#=#" + PrivatePublicCode + "#-#";
        TempResponseData += ".XXXXXXX_to#=#" + SessionIdTo + "#-#";
        TempResponseData += "chatter_to#=#" + ChatterToName + "#-#";
        TempResponseData += "picture#=#" + picture + "#-#";
        TempResponseData += "video#=#" + PrivatePublicCode + "#-#";
        TempResponseData += "datetime#=#" + strDate + "#-#";
        TempResponseData += "timezone#=#" + tz.getID() + "#-#";
        TempResponseData += "base_chat#=#" + BaseChat + "#-#";

        TempResponseData += "##-##";
        Log.e("TempResponseData", TempResponseData);
        SLAc.SaveWSChatPHPGetUserPvtPublicChat(TempResponseData, ".XXXXXXXapp_chat", "PostChatSend");


        Super_Library_URLV2 SLU2 = new Super_Library_URLV2(WebService_WSChatPHP_PostShifaPvtPublicChat, nameValuePairs, ((Activity) ctx), "DoResponseActionForPvtPublicChatSend");

    }


}
