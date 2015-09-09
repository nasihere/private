package com.shifa.kent;

import android.app.Activity;
import android.content.Context;

import org.apache.http.NameValuePair;
import org.apache.http.message.BasicNameValuePair;

import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.List;

/**
 * Created by Nasz on 3/20/15.
 */
public class ShifaDepartment {

    protected static String WebService_GetPaidStatus = "http://kent..XXXXXXX/app_php/Shifa4o/WebService/WSPayment.php?action=GetPaidAccountStatus&.XXXXXXX=";//8419562625169491
    protected static String WebService_SetPaidStatus = "http://kent..XXXXXXX/app_php/Shifa4o/WebService/WSPayment.php?action=SetPaidAccount&.XXXXXXX=";//8419562625169491
    protected static String WebService_SetCounterBuyNow = "http://kent..XXXXXXX/app_php/Shifa4o/WebService/WSPayment.php?action=SetCounterBuyNow&.XXXXXXX=";//8419562625169491
    protected static String WebService_WSLoginPHP_GetLoginDetails = "http://kent..XXXXXXX/app_php/Shifa4o/WebService/WSLogin.php?action=GetLoginDetails";//8419562625169491
    protected static String WebService_WSChatPHP_GetShifaMember = "http://kent..XXXXXXX/app_php/Shifa4o/WebService/WSChat.php?action=GetShifaMember";//8419562625169491
    protected static String WebService_WSChatPHP_GetShifaPublicChat = "http://kent..XXXXXXX/app_php/Shifa4o/WebService/WSChat.php?action=GetShifaPublicChat";//8419562625169491
    protected static String WebService_WSChatPHP_PostShifaPvtPublicChat = "http://kent..XXXXXXX/app_php/Shifa4o/WebService/WSChat.php?action=PostShifaPvtPublicChat";//8419562625169491

    Context ctx;
    Super_Library_AppClass SLAc;
    String SessionID = "";

    public ShifaDepartment(Context ctx) {
        this.ctx = ctx;
        SLAc = new Super_Library_AppClass(ctx);
        SessionID = SLAc.UserSessionId();
        if (SessionID.equalsIgnoreCase("")) return;
        if (SLAc.NewDay() == true) {
            AskAppToCheckUserPaidAccount();
            CheckRealPaidUserStatusUpdatedInShifaServer();
            // SLAc.SavePreference("WSChatPHPGetShifaPublicChat", ""); // Remove this line.... after development
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

    protected void SetUserAsPaidAccount() {
        List<NameValuePair> nameValuePairs = new ArrayList<NameValuePair>(2);
        nameValuePairs.add(new BasicNameValuePair(".XXXXXXX", SLAc.UserSessionId()));
        Super_Library_URLV2 SLU2 = new Super_Library_URLV2(WebService_SetPaidStatus, nameValuePairs, ((Activity) ctx), "SetPaidAccount");

    }


    protected void CheckRealPaidUserStatusUpdatedInShifaServer() {
        //Will check here real user paid by google but for some reason shifa server not updated real user account mark as paid so for that reason we set again request to server to mark as paid
        if (!SLAc.GetPreferenceValue("PaidUserServer").equalsIgnoreCase("true")) {
            List<NameValuePair> nameValuePairs = new ArrayList<NameValuePair>(2);
            nameValuePairs.add(new BasicNameValuePair(".XXXXXXX", SLAc.UserSessionId()));

            Super_Library_URLV2 SLU2 = new Super_Library_URLV2(WebService_GetPaidStatus, nameValuePairs, ((Activity) ctx), "SetUserAccountAsPaid");
        }
    }


    protected void CheckNeworExistingMemberInformationAndSaveInDatabaseForGroupChat() {
        //Everyday it will update Member Details and status and add new member information in public chat
        List<NameValuePair> nameValuePairs = new ArrayList<NameValuePair>(2);
        nameValuePairs.add(new BasicNameValuePair(".XXXXXXX", SLAc.UserSessionId()));
        nameValuePairs.add(new BasicNameValuePair("pvt_id_web", SLAc.GetPreferenceValue("pvt_id_web")));
        nameValuePairs.add(new BasicNameValuePair("contact_id_web", SLAc.GetPreferenceValue("contact_id_web")));
        Super_Library_URLV2 SLU2 = new Super_Library_URLV2(WebService_WSChatPHP_GetShifaMember, nameValuePairs, ((Activity) ctx), "WSChatPHPGetShifaMember");

    }


    protected void CheckNewOrExistingPublicChatAvaiable() {
        //This function will pull all the private and public chat and set top most id_web in app property for new records
        List<NameValuePair> nameValuePairs = new ArrayList<NameValuePair>(2);
        nameValuePairs.add(new BasicNameValuePair(".XXXXXXX", SLAc.UserSessionId()));
        nameValuePairs.add(new BasicNameValuePair("public_id_web", SLAc.GetPreferenceValue("public_id_web")));
        Super_Library_URLV2 SLU2 = new Super_Library_URLV2(WebService_WSChatPHP_GetShifaPublicChat, nameValuePairs, ((Activity) ctx), "WSChatPHPGetShifaPublicChat");

    }


    protected void PostPvtPublicChatSend(String Chat, String PrivatePublicCode, String picture, String Video, String id_app) {
        //This function will take minimual value of chat and  put on server also update in database for offline purpose.. user dont have to wait to see his message went or not

        List<NameValuePair> nameValuePairs = new ArrayList<NameValuePair>(2);
        nameValuePairs.add(new BasicNameValuePair("_frm", SLAc.UserSessionId()));
        nameValuePairs.add(new BasicNameValuePair("chat", Chat));
        nameValuePairs.add(new BasicNameValuePair("chatter", SLAc.UserName()));
        nameValuePairs.add(new BasicNameValuePair("to", PrivatePublicCode));
        nameValuePairs.add(new BasicNameValuePair("picture", picture));
        nameValuePairs.add(new BasicNameValuePair("video", Video));
        nameValuePairs.add(new BasicNameValuePair("id_app", id_app));

        Calendar c = Calendar.getInstance();
        SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
        String strDate = sdf.format(c.getTime());

        String TempResponseData = "id_app#=#" + id_app + "#-#";
        TempResponseData += "chatter#=#" + SLAc.UserName() + "#-#";
        TempResponseData += "frm#=#" + SLAc.UserSessionId() + "#-#";
        TempResponseData += "chat#=#" + Chat + "#-#";
        TempResponseData += "_to#=#" + PrivatePublicCode + "#-#";
        TempResponseData += "picture#=#" + picture + "#-#";
        TempResponseData += "video#=#" + PrivatePublicCode + "#-#";
        TempResponseData += "datetime#=#" + strDate + "#-#";

        TempResponseData += "##-##";
        SLAc.SaveWSChatPHPGetUserPvtPublicChat(TempResponseData, ".XXXXXXXapp_chat");


        Super_Library_URLV2 SLU2 = new Super_Library_URLV2(WebService_WSChatPHP_PostShifaPvtPublicChat, nameValuePairs, ((Activity) ctx), "DoResponseActionForPvtPublicChatSend");

    }


}
